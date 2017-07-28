using System;
using System.Linq;
using ActionLibrary.DTOs;
using Conditions;
using SerializationUtilities;
using WellFormedNames;

namespace ActionLibrary
{
	[Serializable]
	public abstract class BaseActionDefinition : ICustomSerialization
	{
		private ConditionSet m_activationConditions = null;

		public Guid Id { get; private set; }
		public Name Target { get; private set; }
		internal IActionSelector Manager { get; private set; }

		private Name m_actionTemplate;
		public Name ActionName {
			get { return m_actionTemplate.GetFirstTerm(); }
		}

		public ConditionSet ActivationConditions {
			get { return m_activationConditions; }
			set
			{
				if(m_activationConditions == value)
					return;

				var old = m_activationConditions;
				m_activationConditions = value;
				Manager?.OnConditionsUpdated(this,old);
			}
		}

		private void AssertAndInitialize(Name actionTemplate, Name target, ConditionSet activationConditions)
		{
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new ArgumentException("Invalid Action Template format", nameof(actionTemplate));

			if (target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

			Id = Guid.NewGuid();
			m_actionTemplate = actionTemplate;
			Target = target;
			ActivationConditions = activationConditions;
		}

		protected BaseActionDefinition(Name actionTemplate, Name target, ConditionSet activationConditions)
		{
			AssertAndInitialize(actionTemplate,target, activationConditions);
		}

		protected BaseActionDefinition(BaseActionDefinition other)
		{
			Id = other.Id;
			m_actionTemplate = other.m_actionTemplate;
			Target = other.Target;
			ActivationConditions = new ConditionSet(other.ActivationConditions);
		}

		protected BaseActionDefinition(ActionDefinitionDTO dto)
		{
			AssertAndInitialize(Name.BuildName(dto.Action),Name.BuildName(dto.Target),new ConditionSet(dto.Conditions));
		}

		internal IAction GenerateAction(SubstitutionSet constraints)
		{
			var actionName = m_actionTemplate.MakeGround(constraints);
			if (!actionName.IsGrounded)
				return null;

			var targetName = Target.MakeGround(constraints);
			if (!targetName.IsGrounded)
				return null;
			
			var a = new Action(actionName.GetTerms(), targetName);

            float p = CalculateActionUtility(a);
            
            //change the utility of the action by looking at the confidence in each substitution certainty
            var minCertainty = constraints.Select(s => s.SubValue.Certainty).Min();

            a.Utility = p + minCertainty;

			return a;
		}

		internal void OnManagerSet(IActionSelector manager)
		{
			Manager = manager;
		}

		protected abstract float CalculateActionUtility(IAction a);

		public Name GetActionTemplate()
		{
			return m_actionTemplate;
		}

		protected T FillDTO<T>(T dto) where T : ActionDefinitionDTO
		{
			dto.Id = Id;
			dto.Action = m_actionTemplate.ToString();
			dto.Target = Target.ToString();
			dto.Conditions = m_activationConditions.ToDTO();
			return dto;
		}

		protected void SetFromDTO<T>(T dto) where T : ActionDefinitionDTO
		{
			m_actionTemplate = (Name) dto.Action;
			Target = (Name) dto.Target;
			m_activationConditions = new ConditionSet(dto.Conditions);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var def = obj as BaseActionDefinition;
			if (def == null)
				return false;
			return def.Id == Id;
		}
		
		public virtual void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Action",GetActionTemplate());
			if (Target != null && Target != Name.NIL_SYMBOL)
				dataHolder.SetValue("Target",Target);
			dataHolder.SetValue("Conditions",ActivationConditions);
		}

		public virtual void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			var actionTemplate = dataHolder.GetValue<Name>("Action");
			var target = dataHolder.ContainsField("Target") ? dataHolder.GetValue<Name>("Target") : Name.NIL_SYMBOL;
			var conditions = dataHolder.GetValue<ConditionSet>("Conditions");
			AssertAndInitialize(actionTemplate, target, conditions);
		}
	}
}
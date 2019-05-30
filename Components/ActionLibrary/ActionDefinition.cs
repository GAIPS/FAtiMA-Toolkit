using System;
using System.Linq;
using ActionLibrary.DTOs;
using Conditions;
using SerializationUtilities;
using WellFormedNames;

namespace ActionLibrary
{
	[Serializable]
	public class ActionRule : ICustomSerialization
	{
		private ConditionSet m_activationConditions = null;

		public Guid Id { get; private set; }
		public Name Target { get; private set; }
        public Name Layer { get; set; }
        public Name Priority { get; set; }

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
			}
		}

		private void AssertAndInitialize(Name actionTemplate, Name target, Name priority, Name layer, ConditionSet activationConditions)
		{
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			
			if (target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

            Priority = priority;


            Id = Guid.NewGuid();
			m_actionTemplate = actionTemplate;
			Target = target;
            Layer = layer;
			ActivationConditions = activationConditions;
		}

		protected ActionRule(Name actionTemplate, Name target, Name priority, Name type, ConditionSet activationConditions)
		{
			AssertAndInitialize(actionTemplate,target, priority, type, activationConditions);
		}

		protected ActionRule(ActionRule other)
		{
			Id = other.Id;
			m_actionTemplate = other.m_actionTemplate;
			Target = other.Target;
            Priority = other.Priority;
            Layer = other.Layer;
			ActivationConditions = new ConditionSet(other.ActivationConditions);
		}

		public ActionRule(ActionRuleDTO dto)
		{
			AssertAndInitialize(dto.Action,dto.Target,dto.Priority,dto.Layer, new ConditionSet(dto.Conditions));
		}

		public IAction GenerateAction(SubstitutionSet constraints)
		{
			var actionName = m_actionTemplate.MakeGround(constraints);
			if (!actionName.IsGrounded)
				return null;

			var targetName = Target.MakeGround(constraints);
			if (!targetName.IsGrounded)
				return null;

            var priority = Priority.MakeGround(constraints);
            if (!priority.IsGrounded)
                return null;

            var a = new Action(actionName.GetTerms(), targetName);

            //Determine Priority (Utility)
            float p = float.Parse(priority.ToString());
            if (constraints.Any())
            {
                var minCertainty = constraints.FindMinimumCertainty();
                a.Utility = p + minCertainty;
            }
            else
            {
                a.Utility = p + 1; // minCertainty is 1 if there are no conditions to be uncertain about
            }


            return a;
		}

		public Name GetActionTemplate()
		{
			return m_actionTemplate;
		}

		protected T FillDTO<T>(T dto) where T : ActionRuleDTO
		{
			dto.Id = Id;
			dto.Action = m_actionTemplate;
			dto.Target = Target;
			dto.Conditions = m_activationConditions.ToDTO();
            dto.Layer = Layer;
			return dto;
		}

		protected void SetFromDTO<T>(T dto) where T : ActionRuleDTO
		{
			m_actionTemplate = (Name) dto.Action;
			Target = (Name) dto.Target;
            Priority = (Name)dto.Priority;
            Layer = (Name)dto.Layer;
			m_activationConditions = new ConditionSet(dto.Conditions);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var def = obj as ActionRule;
			if (def == null)
				return false;
			return def.Id == Id;
		}

        public ActionRuleDTO ToDTO()
        {
            return FillDTO(new ActionRuleDTO()
            {
                Action = this.ActionName,
                Conditions = m_activationConditions.ToDTO(),
                Id = this.Id,
                Layer = this.Layer,
                Target = this.Target,
                Priority = this.Priority
            });
        }

        public virtual void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Action",GetActionTemplate());
			dataHolder.SetValue("Target",Target);
            dataHolder.SetValue("Layer", Layer);
            dataHolder.SetValue("Conditions",ActivationConditions);
            if (!(context.Context is Name) || (Priority != (Name)context.Context))
                dataHolder.SetValue("Priority", Priority);
            
        }

		public virtual void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			var actionTemplate = dataHolder.GetValue<Name>("Action");
			var target = dataHolder.ContainsField("Target") ? dataHolder.GetValue<Name>("Target") : Name.NIL_SYMBOL;
            var type = dataHolder.ContainsField("Layer") ? dataHolder.GetValue<Name>("Layer") : Name.NIL_SYMBOL;
            var conditions = dataHolder.GetValue<ConditionSet>("Conditions");
            Name priority;
            if (dataHolder.ContainsField("Priority"))
                priority = dataHolder.GetValue<Name>("Priority");
            else
                priority = context.Context as Name ?? (Name)"1";
            AssertAndInitialize(actionTemplate, target, priority, type, conditions);
		}
	}
}
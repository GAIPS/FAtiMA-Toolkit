using System;
using System.Linq;
using ActionLibrary.DTOs;
using Conditions;
using SerializationUtilities;
using WellFormedNames;

namespace ActionLibrary
{
	[Serializable]
	public class ActionDefinition : ICustomSerialization
	{
		private ConditionSet m_activationConditions = null;

		public Guid Id { get; private set; }
		public Name Target { get; private set; }
        public Name Type { get; set; }
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

		private void AssertAndInitialize(Name actionTemplate, Name target, Name priority, Name type, ConditionSet activationConditions)
		{
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			
			if (target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

            Priority = priority;


            Id = Guid.NewGuid();
			m_actionTemplate = actionTemplate;
			Target = target;
            Type = type;
			ActivationConditions = activationConditions;
		}

		protected ActionDefinition(Name actionTemplate, Name target, Name priority, Name type, ConditionSet activationConditions)
		{
			AssertAndInitialize(actionTemplate,target, priority, type, activationConditions);
		}

		protected ActionDefinition(ActionDefinition other)
		{
			Id = other.Id;
			m_actionTemplate = other.m_actionTemplate;
			Target = other.Target;
			ActivationConditions = new ConditionSet(other.ActivationConditions);
		}

		public ActionDefinition(ActionDefinitionDTO dto)
		{
			AssertAndInitialize(Name.BuildName(dto.Action),Name.BuildName(dto.Target), Name.BuildName(dto.Priority), Name.BuildName(dto.Type), new ConditionSet(dto.Conditions));
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
                a.Utility = p;
            }


            return a;
		}

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
            dto.Type = Type.ToString();
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
			var def = obj as ActionDefinition;
			if (def == null)
				return false;
			return def.Id == Id;
		}


        public ActionDefinitionDTO ToDTO()
        {
            return FillDTO(new ActionDefinitionDTO()
            {
                Priority = this.Priority.ToString()
            });
        }

        public virtual void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Action",GetActionTemplate());
			dataHolder.SetValue("Target",Target);
            dataHolder.SetValue("Type", Target);
            dataHolder.SetValue("Conditions",ActivationConditions);
            if (!(context.Context is Name) || (Priority != (Name)context.Context))
                dataHolder.SetValue("Priority", Priority);
            
        }

		public virtual void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			var actionTemplate = dataHolder.GetValue<Name>("Action");
			var target = dataHolder.ContainsField("Target") ? dataHolder.GetValue<Name>("Target") : Name.NIL_SYMBOL;
            var type = dataHolder.ContainsField("Type") ? dataHolder.GetValue<Name>("Type") : Name.NIL_SYMBOL;
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
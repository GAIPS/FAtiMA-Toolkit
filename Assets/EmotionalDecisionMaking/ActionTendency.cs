using ActionLibrary;
using Conditions;
using EmotionalDecisionMaking.DTOs;
using SerializationUtilities;
using WellFormedNames;

namespace EmotionalDecisionMaking
{
	public class ActionTendency : BaseActionDefinition
	{
		public ActionTendency(Name actionName) : this(actionName, Name.NIL_SYMBOL, Name.NIL_SYMBOL, Name.SELF_SYMBOL) {}

		public ActionTendency(Name actionName, Name target, Name type, Name identity) : this(actionName, target, type, identity, new ConditionSet()) {}

		public ActionTendency(Name actionName, Name target, Name type, Name identity, ConditionSet activationConditions) : base(actionName, target, type, identity, activationConditions)
		{
            Priority = (Name)"1"; //Default
		}

		public ActionTendency(ReactionDTO dto) : base(dto)
		{
			Priority = (Name)dto.Priority;
		}

		public ReactionDTO ToDTO()
		{
			return FillDTO(new ReactionDTO()
			{
				Priority = this.Priority.ToString()
			});
		}

		
		public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.GetObjectData(dataHolder, context);
			if(!(context.Context is Name) || (Priority != (Name)context.Context))
				dataHolder.SetValue("Priority", Priority);
		}

		public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.SetObjectData(dataHolder, context);
			if (dataHolder.ContainsField("Priority"))
				Priority = dataHolder.GetValue<Name>("Priority");
			else
				Priority = context.Context as Name ?? (Name)"1";
		}
	}
}
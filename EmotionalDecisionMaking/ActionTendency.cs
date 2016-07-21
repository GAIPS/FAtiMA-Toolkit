using System;
using ActionLibrary;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using WellFormedNames;

namespace EmotionalDecisionMaking
{
	public class ActionTendency : BaseActionDefinition
	{
		private const float DEFAULT_ACTION_PRIORITY = 1f;

		private float m_priority;
		public float Priority {
			get { return m_priority; }
			set { m_priority = value < 0 ? 0 : value; }
		}

		public ActionTendency(Name actionName) : this(actionName,Name.NIL_SYMBOL) {}

		public ActionTendency(Name actionName, Name target) : this(actionName, target, new ConditionSet()) {}

		public ActionTendency(Name actionName, Name target, ConditionSet activationConditions) : base(actionName, target, activationConditions)
		{
			m_priority = DEFAULT_ACTION_PRIORITY;
		}

		public ActionTendency(ReactionDTO dto)
			: base(dto)
		{
			Priority = dto.Priority;
		}

		public ReactionDTO ToDTO()
		{
			return FillDTO(new ReactionDTO()
			{
				Priority = this.Priority
			});
		}

		protected override float CalculateActionUtility(IAction a)
		{
			return m_priority;
		}

		public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.GetObjectData(dataHolder, context);
			if(!(context.Context is float) || (Priority != (float)context.Context))
				dataHolder.SetValue("Priority", Priority);
		}

		public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.SetObjectData(dataHolder, context);
			if (dataHolder.ContainsField("Priority"))
				Priority = dataHolder.GetValue<float>("Priority");
			else
				Priority = context.Context as float? ?? DEFAULT_ACTION_PRIORITY;
		}
	}
}
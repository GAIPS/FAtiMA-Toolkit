using System;
using ActionLibrary;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public class ActionTendency : BaseActionDefinition
	{
		private const float DEFAULT_ACTIVATION_COOLDOWN = 1f;
		private DateTime m_lastActivationTimestamp;

		private float m_activationCooldown;
		public float ActivationCooldown {
			get { return m_activationCooldown; }
			set { m_activationCooldown = value < 0 ? 0 : value; }
		}

		public bool IsCoolingdown
		{
			get { return (DateTime.UtcNow - m_lastActivationTimestamp).TotalSeconds <= ActivationCooldown; }
		}

		public ActionTendency(Name actionName) : this(actionName,Name.NIL_SYMBOL) {}

		public ActionTendency(Name actionName, Name target) : this(actionName, target, new ConditionSet()) {}

		public ActionTendency(Name actionName, Name target, ConditionSet activationConditions) : base(actionName, target, activationConditions)
		{
			m_activationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
		}

		//public ActionTendency(Name actionName,Name target, IEnumerable<Condition> activationConditions) : base(actionName,target,new ConditionSet(activationConditions))
		//{
		//	ActivationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
		//}

		public ActionTendency(ReactionDTO dto)
			: this(Name.BuildName(dto.Action), Name.BuildName(dto.Target), new ConditionSet(dto.Conditions))
		{
			ActivationCooldown = dto.Cooldown;
		}

		//private ActionTendency(ActionTendency other) : base(other)
		//{
		//	ActivationCooldown = other.ActivationCooldown;
		//}

		protected override void OnActionGenerated(IAction action)
		{
			m_lastActivationTimestamp = DateTime.UtcNow;
		}

		public ReactionDTO ToDTO()
		{
			return new ReactionDTO()
			{
				Id = Id,
				Action = GetActionTemplate().ToString(),
				Target = Target.ToString(),
				Conditions = ActivationConditions.ToDTO(),
				Cooldown = ActivationCooldown
			};
		}

		public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.GetObjectData(dataHolder, context);
			if(!(context.Context is float) || (ActivationCooldown != (float)context.Context))
				dataHolder.SetValue("Cooldown", ActivationCooldown);
		}

		public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			base.SetObjectData(dataHolder, context);
			if (dataHolder.ContainsField("Cooldown"))
				ActivationCooldown = dataHolder.GetValue<float>("Cooldown");
			else
				ActivationCooldown = context.Context as float? ?? DEFAULT_ACTIVATION_COOLDOWN;
		}
	}
}
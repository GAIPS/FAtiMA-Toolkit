using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public class ActionTendency : BaseActionDefinition
	{
		private const float DEFAULT_ACTIVATION_COOLDOWN = 1f;
		private DateTime m_lastActivationTimestamp;

		public float ActivationCooldown { get; set; }

		public bool IsCoolingdown
		{
			get { return (DateTime.UtcNow - m_lastActivationTimestamp).TotalSeconds <= ActivationCooldown; }
		}

		public ActionTendency(Name actionName) : this(actionName,Name.NIL_SYMBOL) {}

		public ActionTendency(Name actionName, Name target) : this(actionName, target, Enumerable.Empty<Condition>()) { }

		public ActionTendency(Name actionName,Name target, IEnumerable<Condition> activationConditions) : base(actionName,target,activationConditions)
		{
			ActivationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
		}

		private ActionTendency(ActionTendency other) : base(other)
		{
			ActivationCooldown = other.ActivationCooldown;
		}

		protected override void OnActionGenerated(IAction action)
		{
			m_lastActivationTimestamp = DateTime.UtcNow;
		}

		public override object Clone()
		{
			return new ActionTendency(this);
		}

		public ReactionDTO ToDTO()
		{
			return new ReactionDTO()
			{
				Id = Id,
				Action = GetActionTemplate().ToString(),
				Target = Target.ToString(),
				Conditions = ActivationConditions.ToDTO(),
				Cooldown = ActivationCooldown.ToString()
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
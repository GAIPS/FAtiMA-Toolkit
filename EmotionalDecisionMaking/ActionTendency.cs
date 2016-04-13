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

		public ActionTendenciesDTO ToDTO()
		{
			return new ActionTendenciesDTO()
			{
				Id = Id,
				Action = GetActionTemplate().ToString(),
				Target = Target.ToString(),
				Conditions = ActivationConditions.ToDTO(),
				Cooldown = ActivationCooldown
			};
		}

		public override void GetSerializationData(Graph serializationParent, IObjectGraphNode node, object contextData)
		{
			base.GetSerializationData(serializationParent, node, contextData);
			if (ActivationCooldown != (float)contextData)
				node["Cooldown"] = serializationParent.BuildNode(ActivationCooldown);
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public ActionTendency(IObjectGraphNode node, object contextData) : base(node,contextData)
		{
			ActivationCooldown = SerializationServices.GetFieldOrDefault(node, "Cooldown", (float)contextData);
		}
	}
}
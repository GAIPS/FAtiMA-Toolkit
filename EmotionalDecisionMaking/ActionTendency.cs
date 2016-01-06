using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalDecisionMaking
{
	[Serializable]
	public class ActionTendency : ICloneable, ICustomSerialization
	{
		private const float DEFAULT_ACTIVATION_COOLDOWN = 5f;
		private HashSet<Name> m_parameters = new HashSet<Name>();
		private DateTime m_lastActivationTimestamp;

		public string ActionName { get; private set; }
		public float ActivationCooldown { get; set; }
		public IEnumerable<Name> Parameters
		{
			get { return m_parameters; }
		}
		public ConditionEvaluatorSet ActivationConditions { get; private set; }
		public bool IsCoolingdown
		{
			get { return (DateTime.UtcNow - m_lastActivationTimestamp).TotalSeconds <= ActivationCooldown; }
		}

		public ActionTendency(string actionName)
		{
			ActionName = actionName;
			ActivationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
			ActivationConditions = new ConditionEvaluatorSet();
		}

		public void AddParameter(string parameterName)
		{
			var variable = (Name) ("[" + parameterName + "]");
			m_parameters.Add(variable);
		}

		private ActionTendency(ActionTendency other)
		{
			ActionName = other.ActionName;
			ActivationCooldown = other.ActivationCooldown;
			m_parameters.UnionWith(other.m_parameters);
			ActivationConditions = new ConditionEvaluatorSet(other.ActivationConditions);
		}

		public void Activate()
		{
			m_lastActivationTimestamp=DateTime.UtcNow;
		}

		public object Clone()
		{
			return new ActionTendency(this);
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			var actionName = Name.BuildName(m_parameters.Prepend((Name) ActionName));
			dataHolder.SetValue("Action",actionName);
			dataHolder.SetValue("Conditions",ActivationConditions);
			dataHolder.SetValue("Cooldown", ActivationCooldown);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			var actionName = dataHolder.GetValue<Name>("Action");
			var terms = actionName.GetTerms().ToArray();
			if(!terms.Skip(1).All(t=>t.IsVariable))
				throw new Exception("Action parameteres can only be variables");

			if(terms[0].IsVariable)
				throw new Exception("Action name cannot be a variable");

			ActivationCooldown = dataHolder.GetValue<float>("Cooldown");
			ActivationConditions = dataHolder.GetValue<ConditionEvaluatorSet>("Conditions");
			ActionName = terms[0].ToString();
			m_parameters = new HashSet<Name>();
			m_parameters.UnionWith(terms.Skip(1));
		}
	}
}
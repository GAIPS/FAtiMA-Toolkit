using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalDecisionMaking
{
	public partial class ActionTendency : ICloneable
	{
		private const float DEFAULT_ACTIVATION_COOLDOWN = 1f;
		private Dictionary<Name, Name> m_parameters;
		private DateTime m_lastActivationTimestamp;

		public Name ActionName { get; private set; }
		public float ActivationCooldown { get; set; }

		public ConditionEvaluatorSet ActivationConditions { get; private set; }
		public bool IsCoolingdown
		{
			get { return (DateTime.UtcNow - m_lastActivationTimestamp).TotalSeconds <= ActivationCooldown; }
		}

		public ActionTendency(Name actionName) : this(actionName,Enumerable.Empty<Condition>()) {}

		public ActionTendency(Name actionName, IEnumerable<Condition> activationConditions)
		{
			var terms = actionName.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new Exception("ActionName name needs to be a primitive value Name");

			ActivationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
			ActivationConditions = new ConditionEvaluatorSet(activationConditions);
			ActionName = name;

			if (terms.Length > 1)
			{
				m_parameters = new Dictionary<Name, Name>();
				for (int i = 1; i < terms.Length; i++)
				{
					var p = terms[i];
					m_parameters.Add(p.GetFirstTerm(), p.GetNTerm(1));
				}
			}
		}

		public void AddParameter(string parameterName, Name value)
		{
			var name = (Name) parameterName;
			if(!name.IsPrimitive)
				throw new Exception("Invalid parameter name");

			if(m_parameters==null)
				m_parameters=new Dictionary<Name, Name>();

			m_parameters.Add(name,value);
		}

		private ActionTendency(ActionTendency other)
		{
			ActionName = other.ActionName;
			ActivationCooldown = other.ActivationCooldown;
			ActivationConditions = new ConditionEvaluatorSet(other.ActivationConditions);

			if (other.m_parameters != null)
				m_parameters = new Dictionary<Name, Name>(other.m_parameters);
		}

		public IAction GenerateAction(SubstitutionSet constraints)
		{
			List<IActionParameter> validParameters = new List<IActionParameter>();
			if (m_parameters != null)
			{
				foreach (var p in m_parameters)
				{
					var value = p.Value.MakeGround(constraints);
					if (!value.IsGrounded)
						return null;

					var parameter = new ActionParameter(p.Key.ToString(), value);
					validParameters.Add(parameter);
				}
			}
			m_lastActivationTimestamp = DateTime.UtcNow;
			return new Action(ActionName, validParameters);
		}

		public object Clone()
		{
			return new ActionTendency(this);
		}

		public Name ToName()
		{
			var actionName = (Name)ActionName;
			if (m_parameters != null)
				actionName = Name.BuildName(m_parameters.Select(pair => Name.BuildName(pair.Key, pair.Value)).Prepend(actionName));
			return actionName;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	[Serializable]
	public sealed partial class ReactiveActions : ICustomSerialization
	{
		private ConditionMapper<ActionTendency> m_actions = null;

		public ReactiveActions()
		{
			m_actions = new ConditionMapper<ActionTendency>();
		}

		public void AddReactiveAction(ActionTendency action)
		{
			m_actions.Add(action.ActivationConditions, (ActionTendency)action.Clone());
		}

		public IAction MakeAction(string perspective, KB knowledgeBase)
		{
			var validActions = m_actions.MatchConditions(knowledgeBase, new SubstitutionSet());
			foreach (var action in validActions.Where(a => !a.Item1.IsCoolingdown))
			{
				foreach (var subs in action.Item2)
				{
					var a = GenerateAction(perspective, action.Item1, subs);
					if (a == null)
						continue;

					action.Item1.Activate();
					return a;
				}
			}
			return null;
		}

		private IAction GenerateAction(string perspective, ActionTendency actionDesc, SubstitutionSet constraints)
		{
			string target = null;
			List<IActionParameter> validParameters = new List<IActionParameter>();
			foreach (var p in actionDesc.Parameters)
			{
				var value = constraints[p];
				if (value == null)
					return null;

				var name = p.ToString();
				name = name.Substring(1, name.Length - 2);
				if (string.Equals(name, "target", StringComparison.InvariantCultureIgnoreCase))
				{
					target = value.ToString();
				}
				else
				{
					var parameter = new ActionParameter(name, value);
					validParameters.Add(parameter);
				}
			}

			return new Action_impl(actionDesc.ActionName, perspective, target, validParameters.ToArray());
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			var actions = m_actions.Select(s => s.Value).ToArray();
			dataHolder.SetValue("ReactiveActions", actions);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			var loadedActions = dataHolder.GetValue<ActionTendency[]>("ReactiveActions");

			m_actions=new ConditionMapper<ActionTendency>();
			foreach (var a in loadedActions)
				m_actions.Add(a.ActivationConditions, a);
		}
	}
}
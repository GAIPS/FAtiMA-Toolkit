using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	[Serializable]
	public sealed partial class ReactiveActions : ICustomSerialization
	{
		private float m_defaultActionCooldown = 2;
		private ConditionMapper<ActionTendency> m_actions = null;

		public float DefaultActionCooldown
		{
			get { return m_defaultActionCooldown; }
			set { m_defaultActionCooldown = value < 0 ? 0 : value; }
		}

		public ReactiveActions()
		{
			m_actions = new ConditionMapper<ActionTendency>();
		}

		public void AddReactiveAction(ActionTendency action)
		{
			m_actions.Add(action.ActivationConditions, (ActionTendency)action.Clone());
		}

		public IEnumerable<IAction> MakeAction(KB knowledgeBase, Name perspective)
		{
			var validActions = m_actions.MatchConditions(knowledgeBase,perspective, new SubstitutionSet());
			foreach (var action in validActions.Where(a => !a.Item1.IsCoolingdown))
			{
				var a = action.Item1.GenerateAction(action.Item2);
				if (a == null)
					continue;

				yield return a;
			}
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("DefaultActionCooldown",m_defaultActionCooldown);
			var actions = dataHolder.ParentGraph.BuildSequenceNode();
			foreach (var action in m_actions.Select(s => s.Item2))
			{
				var a = dataHolder.ParentGraph.CreateObjectData();
				action.GetSerializationData(dataHolder.ParentGraph,a, m_defaultActionCooldown);
				actions.Add(a);
			}
			dataHolder.SetValueGraphNode("ActionTendencies",actions);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			DefaultActionCooldown = dataHolder.GetValue<float>("DefaultActionCooldown");
			List<ActionTendency> loadedActions = new List<ActionTendency>();
			var actions = (ISequenceGraphNode) dataHolder.GetValueGraphNode("ActionTendencies");
			foreach (var actionDef in actions.Cast<IObjectGraphNode>())
				loadedActions.Add(new ActionTendency(actionDef,m_defaultActionCooldown));

			m_actions=new ConditionMapper<ActionTendency>();
			foreach (var a in loadedActions)
				m_actions.Add(a.ActivationConditions, a);
		}
	}
}
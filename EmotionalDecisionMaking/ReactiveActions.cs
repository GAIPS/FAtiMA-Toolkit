using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	[Serializable]
	public sealed class ReactiveActions : ICustomSerialization
	{
		private float m_defaultActionCooldown = 2;
		public float DefaultActionCooldown
		{
			get { return m_defaultActionCooldown; }
			set { m_defaultActionCooldown = value < 0 ? 0 : value; }
		}

		private ActionSelector<ActionTendency> m_actionTendencies;

		public ReactiveActions()
		{
			m_actionTendencies = new ActionSelector<ActionTendency>((tendency, set) => !tendency.IsCoolingdown);
		}

		public void AddActionTendency(ActionTendency at)
		{
			m_actionTendencies.AddActionDefinition(at);
		}

        public void RemoveAction(Guid id)
        {
            var action = m_actionTendencies.GetActionDefinition(id);
            if (action != null)
            {
                m_actionTendencies.RemoveActionDefinition(action);
            }
        }

        public IEnumerable<IAction> SelectAction(KB kb, Name perspective)
		{
			return m_actionTendencies.SelectAction(kb, perspective);
		}

		public IEnumerable<ReactionDTO> GetAllActionTendencies()
		{
			return m_actionTendencies.GetAllActionDefinitions().Select(at => at.ToDTO());
		}

		public ReactionDTO GetDTOFromGUID(Guid id)
		{
			return m_actionTendencies.GetActionDefinition(id).ToDTO();
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("DefaultActionCooldown", m_defaultActionCooldown);
			
			var actions = dataHolder.ParentGraph.BuildSequenceNode();
			foreach (var action in m_actionTendencies.GetAllActionDefinitions())
			{
				var a = dataHolder.ParentGraph.CreateObjectData();
				action.GetSerializationData(dataHolder.ParentGraph, a, m_defaultActionCooldown);
				actions.Add(a);
			}
			dataHolder.SetValueGraphNode("ActionTendencies", actions);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			DefaultActionCooldown = dataHolder.GetValue<float>("DefaultActionCooldown");
			var actions = (ISequenceGraphNode)dataHolder.GetValueGraphNode("ActionTendencies");
			m_actionTendencies.Clear();
			foreach (var actionDef in actions.Cast<IObjectGraphNode>())
				m_actionTendencies.AddActionDefinition(new ActionTendency(actionDef, m_defaultActionCooldown));
		}

	    
	}
}
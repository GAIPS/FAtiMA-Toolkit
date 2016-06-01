using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public sealed class ReactiveActions
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
			m_actionTendencies = new ActionSelector<ActionTendency>((tendency,p, set) => !tendency.IsCoolingdown);
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
			return m_actionTendencies.SelectAction(kb, perspective).Select(p => p.Item1);
		}

		public IEnumerable<ActionTendency> GetAllActionTendencies()
		{
			return m_actionTendencies.GetAllActionDefinitions();
		}

	    public ActionTendency GetActionTendency(Guid id)
	    {
	        return m_actionTendencies.GetActionDefinition(id);
	    }
	}
}
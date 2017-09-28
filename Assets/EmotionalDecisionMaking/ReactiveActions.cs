using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;

namespace EmotionalDecisionMaking
{
	public sealed class ReactiveActions
	{
        public Name DefaultActionPriority;

		private ActionSelector<ActionTendency> m_actionTendencies;

		public ReactiveActions()
		{
			m_actionTendencies = new ActionSelector<ActionTendency>();
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

        public IEnumerable<IAction> SelectActions(IQueryable kb, Name perspective)
		{
			return m_actionTendencies.SelectActions(kb, perspective).Select(p => p.Item1);
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
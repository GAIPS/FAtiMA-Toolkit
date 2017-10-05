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

		private List<ActionTendency> m_actionTendencies;

		public ReactiveActions()
		{
            m_actionTendencies = new List<ActionTendency>(); 
		}

		public void AddActionTendency(ActionTendency at)
		{
            m_actionTendencies.Add(at);
		}

        public void RemoveAction(Guid id)
        {
            m_actionTendencies.Remove(GetActionTendency(id));
        }

        public List<IAction> SelectActions(IQueryable kb, Name perspective, Name actionType)
		{
            List<IAction> result = new List<IAction>();

            foreach (var at in m_actionTendencies)
            {
                if (actionType != Name.UNIVERSAL_SYMBOL && actionType != at.Type)
                    continue;

                if (at.ActivationConditions == null) // empty condition set
                {
                    result.Add(at.GenerateAction(new SubstitutionSet()));
                }
                else
                {
                    foreach (var set in at.ActivationConditions.Unify(kb, perspective, null))
                    {
                        result.Add(at.GenerateAction(set));
                    }
                }
            }
            return result;
		}

		public IEnumerable<ActionTendency> GetAllActionTendencies()
		{
            return m_actionTendencies;
		}

	    public ActionTendency GetActionTendency(Guid id)
	    {
            return m_actionTendencies.Where(a => a.Id == id).FirstOrDefault();
        }
	}
}
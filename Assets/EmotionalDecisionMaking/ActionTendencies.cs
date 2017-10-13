using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;

namespace EmotionalDecisionMaking
{
	public sealed class ActionTendencies
	{
        public Name DefaultActionPriority;

		private List<ActionDefinition> m_actionDefinitions;

		public ActionTendencies()
		{
            m_actionDefinitions = new List<ActionDefinition>(); 
		}

		public void AddActionDefinition(ActionDefinition at)
		{
            m_actionDefinitions.Add(at);
		}

        public void RemoveAction(Guid id)
        {
            m_actionDefinitions.Remove(GetActionDefinition(id));
        }

        public List<IAction> SelectActions(IQueryable kb, Name perspective, Name actionType)
		{
            List<IAction> result = new List<IAction>();

            foreach (var at in m_actionDefinitions)
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

		public IEnumerable<ActionDefinition> GetAllActionDefinitions()
		{
            return m_actionDefinitions;
		}

	    public ActionDefinition GetActionDefinition(Guid id)
	    {
            return m_actionDefinitions.Where(a => a.Id == id).FirstOrDefault();
        }
	}
}
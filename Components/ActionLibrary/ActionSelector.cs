using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public class ActionSelector<T>
		where T:IActionDefinition
	{
		private ConditionMapper<T> m_actions = new ConditionMapper<T>();
		private Func<T, SubstitutionSet, bool> m_validator;

		public ActionSelector(Func<T, SubstitutionSet, bool> validationFunction)
		{
			m_validator = validationFunction;
		}

		public void AddActionDefinition(T actionDef)
		{
			m_actions.Add(actionDef.ActivationConditions, (T)actionDef.Clone());
		}

		public void RemoveActionDefinition(T actionDef)
		{
			m_actions.Remove(actionDef.ActivationConditions, actionDef);
		}

		public void Clear()
		{
			m_actions.Clear();
		}

		public IEnumerable<T> GetAllActionDefinitions()
		{
			return m_actions.Select(p => p.Item2);
		}

		public T GetActionDefinition(Guid id)
		{
			return m_actions.Select(p => p.Item2).FirstOrDefault(t => t.Id == id);
		}

		public IEnumerable<IAction> SelectAction(KB knowledgeBase, Name perspective)
		{
			var validActions = m_actions.MatchConditions(knowledgeBase, perspective, new SubstitutionSet());
			validActions = validActions.Where(p => m_validator(p.Item1, p.Item2));
			return validActions.Select(p => p.Item1.GenerateAction(p.Item2)).Where(a => a != null);
		}
	}
}
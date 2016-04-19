using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public class ActionSelector<T> : IActionSelector
		where T: BaseActionDefinition
	{
		private ConditionMapper<T> m_actions = new ConditionMapper<T>();
		private readonly Func<T, SubstitutionSet, bool> m_validator;

		public ActionSelector(Func<T, SubstitutionSet, bool> validationFunction)
		{
			m_validator = validationFunction;
		}

		public void AddActionDefinition(T actionDef)
		{
			if(actionDef.Manager!=null)
				throw new ArgumentException($"The given {nameof(T)} is already associated to a {nameof(ActionSelector<T>)} instance",nameof(actionDef));

			if (m_actions.Add(actionDef.ActivationConditions, actionDef))
				actionDef.OnManagerSet(this);
		}

		public void RemoveActionDefinition(T actionDef)
		{
			if (actionDef.Manager != this)
				throw new ArgumentException($"The given {nameof(T)} is not associated to this {nameof(ActionSelector<T>)} instance", nameof(actionDef));

			if(m_actions.Remove(actionDef.ActivationConditions, actionDef))
				actionDef.OnManagerSet(null);
		}

		public void Clear()
		{
			foreach (var pair in m_actions)
				pair.Item2.OnManagerSet(null);
			m_actions.Clear();
		}

		public IEnumerable<T> GetAllActionDefinitions()
		{
			return m_actions.Select(p => p.Item2);
		}

		public T GetActionDefinition(Guid id)
		{
			return GetAllActionDefinitions().FirstOrDefault(t => t.Id == id);
		}
        
        public IEnumerable<IAction> SelectAction(KB knowledgeBase, Name perspective)
		{
			var validActions = m_actions.MatchConditions(knowledgeBase, perspective, new SubstitutionSet());
			validActions = validActions.Where(p => m_validator((T)p.Item1, p.Item2));
			return validActions.Select(p => p.Item1.GenerateAction(p.Item2)).Where(a => a != null);
		}

		public void OnConditionsUpdated(BaseActionDefinition def, ConditionSet oldConditions)
		{
			if (m_actions.Remove(oldConditions, (T) def))
			{
				m_actions.Add(def.ActivationConditions, (T)def);
			}
		}
	}
}
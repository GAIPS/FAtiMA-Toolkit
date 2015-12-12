using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public class ConditionMapper<T> : IEnumerable<KeyValuePair<ConditionEvaluatorSet,T>>
	{
		private Dictionary<ConditionEvaluatorSet, T> m_dict = new Dictionary<ConditionEvaluatorSet, T>();

		public int Count
		{
			get { return m_dict.Count; }
		}

		public void Add(ConditionEvaluatorSet conditionEvaluator, T value)
		{
			if(conditionEvaluator==null)
				conditionEvaluator=new ConditionEvaluatorSet();
			m_dict.Add(conditionEvaluator,value);
		}

		public bool Remove(ConditionEvaluatorSet conditionEvaluator)
		{
			return m_dict.Remove(conditionEvaluator);
		}

		public void Clear()
		{
			m_dict.Clear();
		}

		public IEnumerable<Pair<T,IEnumerable<SubstitutionSet>>> MatchConditions(KB kb, SubstitutionSet constraints)
		{
			return from e in m_dict let set = e.Key.UnifyEvaluate(kb, constraints) where set.Any() select Tuples.Create(e.Value, set);
		}

		public IEnumerator<KeyValuePair<ConditionEvaluatorSet, T>> GetEnumerator()
		{
			return m_dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override int GetHashCode()
		{
			return m_dict.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ConditionMapper<T> map = obj as ConditionMapper<T>;
			if (map == null)
				return false;

			if (m_dict.Count != map.m_dict.Count)
				return false;

			foreach (var p in map.m_dict)
			{
				T v;
				if (!m_dict.TryGetValue(p.Key, out v))
					return false;

				if (!p.Value.Equals(v))
					return false;
			}
			return true;
		}
	}
}
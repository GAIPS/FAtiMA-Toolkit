using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public class ConditionMapper<T> : IEnumerable<KeyValuePair<ConditionSet,T>>
	{
		private Dictionary<ConditionSet, T> m_dict = new Dictionary<ConditionSet, T>();

		public int Count
		{
			get { return m_dict.Count; }
		}

		public void Add(ConditionSet condition, T value)
		{
			if(condition==null)
				condition=new ConditionSet();
			m_dict.Add(condition,value);
		}

		public bool Remove(ConditionSet condition)
		{
			return m_dict.Remove(condition);
		}

		public void Clear()
		{
			m_dict.Clear();
		}

		public IEnumerable<Pair<T,IEnumerable<SubstitutionSet>>> MatchConditions(KB kb, SubstitutionSet constraints)
		{
			return from e in m_dict let set = e.Key.UnifyEvaluate(kb, constraints) where set.Any() select Tuple.Create(e.Value, set);
		}

		public IEnumerator<KeyValuePair<ConditionSet, T>> GetEnumerator()
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
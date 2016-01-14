using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public class ConditionMapper<T> : IEnumerable<Pair<ConditionEvaluatorSet,T>>
	{
		private static readonly IEqualityComparer<Pair<ConditionEvaluatorSet, T>> EQUALITY_COMPARER = new ConditionMapperEquality();
		private HashSet<Pair<ConditionEvaluatorSet, T>> m_conditions = new HashSet<Pair<ConditionEvaluatorSet, T>>(EQUALITY_COMPARER);

		public int Count
		{
			get { return m_conditions.Count; }
		}

		public bool Add(ConditionEvaluatorSet conditionEvaluator, T value)
		{
			if (conditionEvaluator!=null && conditionEvaluator.Count == 0)
				conditionEvaluator = null;

			return m_conditions.Add(Tuples.Create(conditionEvaluator, value));
		}

		public bool Remove(ConditionEvaluatorSet conditionEvaluator, T value)
		{
			if (conditionEvaluator.Count == 0)
				conditionEvaluator = null;

			return m_conditions.Remove(Tuples.Create(conditionEvaluator, value));
		}

		public void Clear()
		{
			m_conditions.Clear();
		}

		public IEnumerable<Pair<T,SubstitutionSet>> MatchConditions(KB kb, SubstitutionSet constraints)
		{
			foreach (var e in m_conditions)
			{
				if (e.Item1 == null)
				{
					yield return Tuples.Create(e.Item2, new SubstitutionSet(constraints));
					continue;
				}

				foreach (var set in e.Item1.UnifyEvaluate(kb, constraints))
					yield return Tuples.Create(e.Item2, set);
			}
		}

		public IEnumerator<Pair<ConditionEvaluatorSet, T>> GetEnumerator()
		{
			return m_conditions.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override int GetHashCode()
		{
			const int BASE_HASHCODE = 0x3a74ed8a;
			return m_conditions.Aggregate(BASE_HASHCODE, (hash, pair) => hash ^ EQUALITY_COMPARER.GetHashCode(pair));
		}

		public override bool Equals(object obj)
		{
			ConditionMapper<T> map = obj as ConditionMapper<T>;
			if (map == null)
				return false;

			return m_conditions.SetEquals(map.m_conditions);
		}

		private class ConditionMapperEquality : IEqualityComparer<Pair<ConditionEvaluatorSet,T>>
		{
			public bool Equals(Pair<ConditionEvaluatorSet, T> x, Pair<ConditionEvaluatorSet, T> y)
			{
				return object.Equals(x.Item1, y.Item1) && object.Equals(x.Item2, y.Item2);
			}

			public int GetHashCode(Pair<ConditionEvaluatorSet, T> obj)
			{
				return obj.Item1 == null ? 0 : obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode();
			}
		}
	}
}
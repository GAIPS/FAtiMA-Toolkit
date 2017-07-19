using System;
using System.Collections.Generic;
using WellFormedNames;

namespace Conditions
{
	public partial class Condition
	{
		private class PredicateCondition : Condition
		{
			private readonly IValueRetriever m_predicate;
			private readonly bool m_invert;

			public PredicateCondition(IValueRetriever p, bool expectedResult)
			{
				m_predicate = p;
				m_invert = !expectedResult;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				foreach (var pair in m_predicate.Retrieve(db, perspective, constraints))
				{
					bool b;
					if(!pair.Item1.TryConvertToValue(out b))
						continue;

					if(b!=m_invert)
						yield return pair.Item2;
				}
			}

			public override string ToString()
			{
				return $"{m_predicate} = {!m_invert}";
			}

			public override bool Equals(object obj)
			{
				PredicateCondition c = obj as PredicateCondition;
				if (c == null)
					return false;

				return m_predicate.Equals(c.m_predicate) && m_invert == c.m_invert;
			}

			public override int GetHashCode()
			{
				int h = m_predicate.GetHashCode();
				return m_invert ? ~h : h;
			}
		}
	}
}
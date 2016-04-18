using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public partial class Condition
	{
		private class PredicateCondition : Condition
		{
			private readonly IValueRetriver m_predicate;
			private readonly bool m_invert;

			public PredicateCondition(IValueRetriver p, bool expectedResult)
			{
				m_predicate = p;
				m_invert = !expectedResult;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				foreach (var pair in m_predicate.Retrive(kb, perspective, constraints))
				{
					if (pair.Item1.TypeCode != TypeCode.Boolean)
						continue;

					if (((bool) pair.Item1) != m_invert)
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
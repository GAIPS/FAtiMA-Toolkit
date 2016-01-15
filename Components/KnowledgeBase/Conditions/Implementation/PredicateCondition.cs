using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public partial class Condition
	{
		public class PredicateCondition : Condition
		{
			private readonly Name m_predicate;
			private readonly bool m_invert;

			public PredicateCondition(Name p, bool expectedResult)
			{
				m_predicate = p;
				m_invert = !expectedResult;
			}

			public override IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
			{
				if(constraints==null)
					constraints = new SubstitutionSet();

				List<SubstitutionSet> results = new List<SubstitutionSet>();
				var sets = kb.AskPossibleProperties(m_predicate, constraints).ToList();
				if (sets.Count == 0 && m_invert)
				{
					results.Add(constraints);
				}
				else
				{
					foreach (var pair in sets)
					{
						if (pair.Item1.TypeCode != TypeCode.Boolean)
							continue;

						if (((bool)pair.Item1) != m_invert)
							results.Add(pair.Item2);
					}	
				}

				return results;
			}

			public override string ToString()
			{
				return string.Format("{0} = {1}", m_predicate, !m_invert);
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
using System;
using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public partial class Condition
	{
		private sealed class EqualityDefinitionCondition : Condition
		{
			private Name m_variable;
			private Name m_other;

			public EqualityDefinitionCondition(Name variable, Name other)
			{
				m_variable = variable;
				m_other = other;
			}

			public override IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
			{
				if (m_other.IsVariable || m_other.IsPrimitive)
				{
					var sub = new Substitution(m_variable,m_other);
					if (constraints.AddSubstitution(sub))
						yield return constraints;
					yield break;
				}

				foreach (var result in kb.AskPossibleProperties(m_other, constraints))
				{
					var sub = new Substitution(m_variable, Name.BuildName(result.Item1));
					if (result.Item2.AddSubstitution(sub))
						yield return result.Item2;
				}
			}

			public override string ToString()
			{
				return string.Format("{0} = {1}", m_other, m_variable);
			}

			public override bool Equals(object obj)
			{
				var d = obj as EqualityDefinitionCondition;
				if (d == null)
					return false;

				if (m_variable.Equals(d.m_variable))
					return m_other.Equals(d.m_other);

				if (m_other.IsVariable && d.m_other.IsVariable)
				{
					return m_variable.Equals(d.m_other) && m_other.Equals(d.m_variable);
				}

				return false;
			}

			public override int GetHashCode()
			{
				int baseHash = 0x1b0668c7;
				return m_variable.GetHashCode() ^ m_other.GetHashCode() ^ baseHash;
			}
		}
	}
}
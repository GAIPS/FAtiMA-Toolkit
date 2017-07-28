using System.Collections.Generic;
using WellFormedNames;

namespace Conditions
{
	public partial class Condition
	{
		private sealed class EqualityDefinitionCondition : Condition
		{
			private Name m_variable;
			private IValueRetriever m_other;

			public EqualityDefinitionCondition(Name variable, IValueRetriever other)
			{
				m_variable = variable;
				m_other = other;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				foreach (var result in m_other.Retrieve(db, perspective, constraints))
				{
					var sub = new Substitution(m_variable, new ComplexValue(result.Item1.Value, result.Item1.Certainty));
					if (result.Item2.AddSubstitution(sub))
						yield return result.Item2;
				}
			}

			public override string ToString()
			{
				return $"{m_other} = {m_variable}";
			}

			public override bool Equals(object obj)
			{
				var d = obj as EqualityDefinitionCondition;
				if (d == null)
					return false;

				if (!m_variable.Equals(d.m_variable))
					return false;

				return m_other.Equals(d.m_other);
			}

			public override int GetHashCode()
			{
				int baseHash = 0x1b0668c7;
				return m_variable.GetHashCode() ^ m_other.GetHashCode() ^ baseHash;
			}
		}
	}
}
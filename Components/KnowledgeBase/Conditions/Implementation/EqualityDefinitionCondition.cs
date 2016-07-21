using System.Collections.Generic;
using WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public partial class Condition
	{
		private sealed class EqualityDefinitionCondition : Condition
		{
			private Name m_variable;
			private IValueRetriver m_other;

			public EqualityDefinitionCondition(Name variable, IValueRetriver other)
			{
				m_variable = variable;
				m_other = other;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				//if (!m_other.HasModifier && (m_other.InnerName.IsVariable || m_other.InnerName.IsPrimitive))
				//{
				//	foreach (var constraint in constraints)
				//	{
				//		var sub = new Substitution(m_variable, m_other.InnerName);
				//		if (!constraint.Conflicts(sub))
				//		{
				//			var c = new SubstitutionSet(constraint);
				//			c.AddSubstitution(sub);
				//			yield return c;
				//		}	
				//	}
				//}
				//else
				//{

				//}

				foreach (var result in m_other.Retrive(kb, perspective, constraints))
				{
					var sub = new Substitution(m_variable, Name.BuildName(result.Item1));
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
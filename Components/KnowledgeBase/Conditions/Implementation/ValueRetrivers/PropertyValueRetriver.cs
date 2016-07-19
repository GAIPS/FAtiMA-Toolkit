using System.Collections.Generic;
using WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	public abstract partial class Condition
	{
		private class PropertyValueRetriver : IValueRetriver
		{
			private readonly Name m_name;

			public PropertyValueRetriver(Name name)
			{
				m_name = name;
			}

			public IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> Retrive(KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				foreach (var pair in kb.AskPossibleProperties(m_name, perspective, constraints))
				{
					foreach (var s in pair.Item2)
						yield return Tuples.Create(pair.Item1, s);
				}
			}

			public Name InnerName
			{
				get { return m_name; }
			}

			public bool HasModifier
			{
				get { return false; }
			}

			public override string ToString()
			{
				return m_name.ToString();
			}

			public override int GetHashCode()
			{
				const int BASE_HASH = 0x0f1c7d73;
				return BASE_HASH ^ m_name.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				PropertyValueRetriver r = obj as PropertyValueRetriver;
				if (r == null)
					return false;
				return m_name.Equals(r.m_name);
			}
		}
	}
}
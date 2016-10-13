using System.Collections.Generic;
using System.Linq;

namespace WellFormedNames
{
	public partial class SubstitutionSet
	{
		private sealed class SimpleImplementation : ISetImplementation
		{
			private Dictionary<Name, Name> m_substitutions;

			public SimpleImplementation()
			{
				m_substitutions = new Dictionary<Name, Name>();
			}

			private SimpleImplementation(Dictionary<Name,Name> other)
			{
				m_substitutions = new Dictionary<Name, Name>(other);
			}

			public Name GetValue(Name variable)
			{
				Name r;
				return m_substitutions.TryGetValue(variable, out r) ? r : null;
			}

			public bool ContainsVariable(Name variable)
			{
				return m_substitutions.ContainsKey(variable);
			}

			public int Count()
			{
				return m_substitutions.Count;
			}

			public void AddSubstitution(Substitution s)
			{
				m_substitutions.Add(s.Variable, s.Value);
			}

			public ISetImplementation Clone()
			{
				return new SimpleImplementation(m_substitutions);
			}

			public bool TestConflict(Substitution subs, SubstitutionSet set, out bool canAdd)
			{
				canAdd = true;
				Name value;
				if (!m_substitutions.TryGetValue(subs.Variable, out value))
					return false;

				canAdd = false;
				var G1 = value.MakeGround(set);
				var G2 = subs.Value.MakeGround(set);
				return !G1.Equals(G2);  //Conflict!!!
			}

			public IEnumerator<Substitution> GetEnumerator()
			{
				return m_substitutions.Select(e => new Substitution(e.Key, e.Value)).GetEnumerator();
			}

			public IEnumerable<Substitution> GetGroundedSubstitutions(SubstitutionSet other)
			{
				if (m_substitutions.Count > 0)
					return m_substitutions.Select(e => new Substitution(e.Key, e.Value.MakeGround(other))).Distinct();
				return Enumerable.Empty<Substitution>();
			}
		}
	}
}
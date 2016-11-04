using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace WellFormedNames
{
	public partial class SubstitutionSet
	{
		private sealed class SimpleImplementation : ISetImplementation
		{
			private Dictionary<Name, Name> m_substitutions;

			public SimpleImplementation()
			{
				m_substitutions = ObjectPool<Dictionary<Name, Name>>.GetObject();
			}

			~SimpleImplementation()
			{
				m_substitutions.Clear();
				ObjectPool<Dictionary<Name, Name>>.Recycle(m_substitutions);
				GC.ReRegisterForFinalize(m_substitutions);
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
				AddSubs(s.Variable,s.Value);
			}

			private void AddSubs(Name variable, Name value)
			{
				m_substitutions.Add(variable,value);
			}

			public ISetImplementation Clone()
			{
				var s = new SimpleImplementation();
				foreach (var pair in m_substitutions)
					s.AddSubs(pair.Key, pair.Value);
				return s;
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

			public int CalculateHashCode(int emptyHashCode, SubstitutionSet subs)
			{
				var set = GetGroundedSubstitutions(subs);
				if (!set.Any())
					return emptyHashCode;

				var hashs = set.Select(s => s.GetHashCode());
				var h = hashs.Aggregate((v1, v2) => v1 ^ v2);
				return emptyHashCode ^ h;
			}
		}
	}
}
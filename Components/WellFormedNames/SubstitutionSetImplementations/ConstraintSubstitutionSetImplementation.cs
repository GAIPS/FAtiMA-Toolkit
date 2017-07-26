using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace WellFormedNames
{
	public partial class SubstitutionSet
	{
		//todo: this class will be instantiated a lot of times. need to find a way to reduce instantiation (pooling)
		private sealed class Constraints
		{
			private sealed class Constraint
			{
				public HashSet<Name> EquivalentVariables = new HashSet<Name>();
				public Name Value;

				public Constraint Clone()
				{
					var c = new Constraint();
					c.EquivalentVariables.UnionWith(EquivalentVariables);
					c.Value = Value;
					return c;
				}

				public void UnionWith(Constraint other)
				{
					EquivalentVariables.UnionWith(other.EquivalentVariables);
					if (Value == null)
						Value = other.Value;
				}
			}

			private HashSet<Substitution> m_substitutions;
			private Dictionary<Name, Constraint> m_constraints;
			private int m_hash;
			private bool m_hashIsDirty = true;

			public Constraints()
			{
				m_substitutions = new HashSet<Substitution>();
				m_constraints = new Dictionary<Name, Constraint>();
			}
			/*
			~Constraints()
			{
				m_constraints.Clear();
				m_substitutions.Clear();

				GC.ReRegisterForFinalize(this);
			}
			*/
			public Name GetValue(Name variable)
			{
				Constraint c;
				if (m_constraints.TryGetValue(variable, out c))
				{
					if (c.Value == null)
						return c.EquivalentVariables.FirstOrDefault(v => v != variable);
					return c.Value;
				}
				return null;
			}

			public bool ContainsVariable(Name variable)
			{
				return m_constraints.ContainsKey(variable);
			}

			public int Count()
			{
				return m_substitutions.Count;
			}

			public void AddSubstitution(Substitution s)
			{
				Constraint c;
				bool needsBuild = !m_constraints.TryGetValue(s.Variable, out c);

				if (s.SubValue.Value.IsVariable)
				{
					Constraint c2;
					if (m_constraints.TryGetValue(s.SubValue.Value, out c2))
					{
						if (needsBuild)
						{
							m_constraints[s.Variable] = c2;
							c2.EquivalentVariables.Add(s.Variable);
						}
						else if (!Object.ReferenceEquals(c, c2))
						{
							c2.UnionWith(c);
							foreach (var v in c2.EquivalentVariables)
								m_constraints[v] = c2;
						}
					}
					else
					{
						if (needsBuild)
						{
							c = new Constraint();
							m_constraints[s.Variable] = c;
							c.EquivalentVariables.Add(s.Variable);
						}

						m_constraints[s.SubValue.Value] = c;
						c.EquivalentVariables.Add(s.SubValue.Value);
					}
				}
				else
				{
					if (needsBuild)
					{
						c = new Constraint();
						m_constraints[s.Variable] = c;
						c.EquivalentVariables.Add(s.Variable);
					}

					c.Value = s.SubValue.Value;
				}

				m_substitutions.Add(s);
				m_hashIsDirty = true;
			}

			public Constraints Clone()
			{
				var c = new Constraints();
				if (!m_hashIsDirty)
				{
					c.m_hashIsDirty = false;
					c.m_hash = m_hash;
				}

				if (m_substitutions.Count == 0)
					return c;

				c.m_substitutions.UnionWith(m_substitutions);
				var cloned = ObjectPool<HashSet<Name>>.GetObject();
				foreach (var key in m_constraints.Keys)
				{
					if (cloned.Contains(key))
						continue;

					var constraint = m_constraints[key].Clone();
					foreach (var v in constraint.EquivalentVariables)
					{
						c.m_constraints[v] = constraint;
						cloned.Add(v);
					}
				}
				cloned.Clear();
				ObjectPool<HashSet<Name>>.Recycle(cloned);

				return c;
			}

			public bool TestConflict(Substitution subs, SubstitutionSet set, out bool canAdd)
			{
				canAdd = true;
				Constraint c;
				if (!m_constraints.TryGetValue(subs.Variable, out c))
					return false;

				Name G1 = c.Value;
				Name G2;
				if (subs.SubValue.Value.IsVariable)
				{
					if (c.EquivalentVariables.Contains(subs.SubValue.Value))
					{
						canAdd = false;
						return false;
					}

					Constraint c2;
					if (!m_constraints.TryGetValue(subs.SubValue.Value, out c2))
						return false;

					if (c.Value == null || c2.Value == null)
						return false;

					G2 = c2.Value;
				}
				else
				{
					if (G1 == null)
						return false;

					canAdd = false;
					G2 = subs.SubValue.Value;
					return !G1.Equals(G2);  //Conflict!!!
				}

				G1 = G1.MakeGround(set);
				G2 = G2.MakeGround(set);
				return !G1.Equals(G2);  //Conflict!!!
			}

			public IEnumerator<Substitution> GetEnumerator()
			{
				return m_substitutions.GetEnumerator();
			}

			public IEnumerable<Substitution> GetGroundedSubstitutions(SubstitutionSet other)
			{
				if (m_substitutions.Count == 0)
					yield break;

				var cloned = ObjectPool<HashSet<Name>>.GetObject();
				foreach (var key in m_constraints.Keys)
				{
					if (cloned.Contains(key))
						continue;

					var constraint = m_constraints[key];
					foreach (var v in constraint.EquivalentVariables)
					{
						if (constraint.Value != null)
							yield return new Substitution(v, new ComplexValue(constraint.Value.MakeGround(other)));
						cloned.Add(v);
					}
				}
				cloned.Clear();
				ObjectPool<HashSet<Name>>.Recycle(cloned);
			}

			public int CalculateHashCode(int emptyHash, SubstitutionSet set)
			{
				if (m_hashIsDirty)
				{
					m_hash = emptyHash;
					if (m_substitutions.Count > 0)
					{
						var cloned = ObjectPool<HashSet<Name>>.GetObject();
						foreach (var key in m_constraints.Keys)
						{
							if (cloned.Contains(key))
								continue;

							var constraint = m_constraints[key];
							m_hash ^= constraint.EquivalentVariables.Select(v => v.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
							if (constraint.Value != null)
								m_hash ^= constraint.Value.GetHashCode();
						}
						cloned.Clear();
						ObjectPool<HashSet<Name>>.Recycle(cloned);
					}
					m_hashIsDirty = false;
				}
				return m_hash;
			}
		}
	}
}
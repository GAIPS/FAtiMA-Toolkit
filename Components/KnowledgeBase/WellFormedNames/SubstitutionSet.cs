using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	[Serializable]
	public sealed class SubstitutionSet : IEnumerable<Substitution>
	{
		private Dictionary<Name,Name> m_substitutions = new Dictionary<Name, Name>();

		public SubstitutionSet() {
		}

		public SubstitutionSet(params Substitution[] substitutions)
			: this((IEnumerable<Substitution>)substitutions)
		{}

		public SubstitutionSet(IEnumerable<Substitution> substitutions)
		{
			if(!AddSubstitutions(substitutions))
				throw new ArgumentException("The given substitutions will generate a conflict.", "substitutions");
		}

		public Name this[Name variable]
		{
			get
			{
				if (!variable.IsVariable)
					return null;

				Name r;
				return m_substitutions.TryGetValue(variable, out r) ? r : null;
			}
		}

		public bool Contains(Name variable)
		{
			if (!variable.IsVariable)
				return false;

			return m_substitutions.ContainsKey(variable);
		}

		public int Count()
		{
			return m_substitutions.Count;
		}

		public bool AddSubstitution(Substitution substitution)
		{
			bool canAdd;
			if (TestConflict(substitution, this, out canAdd))
				return false;

			if (canAdd)
				m_substitutions.Add(substitution.Variable,substitution.Value);

			return true;
		}

		public bool AddSubstitutions(SubstitutionSet substitutions)
		{
			if (Conflicts(substitutions))
				return false;

			foreach (var s in substitutions)
			{
				m_substitutions.Add(s.Variable,s.Value);
			}
			return true;
		}

		public bool AddSubstitutions(IEnumerable<Substitution> substitutions)
		{
			bool rollback = false;
			List<Name> added = new List<Name>(); //TODO Pool?

			foreach (var s in substitutions)
			{
				bool canAdd;
				if (TestConflict(s, this, out canAdd))
				{
					rollback = true;
					break;
				}

				if (canAdd)
				{
					m_substitutions.Add(s.Variable, s.Value);
					added.Add(s.Variable);
				}
			}

			if (rollback)
			{
				foreach (var s in added)
					m_substitutions.Remove(s);
			}

			return !rollback;
		}

		private static bool TestConflict(Substitution subs, SubstitutionSet substitutions, out bool canAdd)
		{
			canAdd = true;
			Name value;
			if (!substitutions.m_substitutions.TryGetValue(subs.Variable, out value))
				return false;

			canAdd = false;
			var G1 = value.MakeGround(substitutions);
			var G2 = subs.Value.MakeGround(substitutions);
			return !G1.Equals(G2);	//Conflict!!!
		}
		
		public bool Conflicts(Substitution substitution)
		{
			bool aux;
			return TestConflict(substitution,this, out aux);
		}

		public bool Conflicts(SubstitutionSet substitutions)
		{
			foreach (var pair in substitutions.m_substitutions)
			{
				Name value;
				if (m_substitutions.TryGetValue(pair.Key, out value))
				{
					var g1 = pair.Value.MakeGround(substitutions).MakeGround(this);
					var g2 = value.MakeGround(this).MakeGround(substitutions);
					if (!g1.Equals(g2))
						return true;
				}
			}
			return false;
		}
		
		public IEnumerator<Substitution> GetEnumerator()
		{
			return m_substitutions.Select(e => new Substitution(e.Key, e.Value)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IEnumerable<Substitution> GetGroundedSubstitutions()
		{
			if (m_substitutions.Count > 0)
				return m_substitutions.Select(e => new Substitution(e.Key, e.Value.MakeGround(this)));
			return Enumerable.Empty<Substitution>();
		}

		public override int GetHashCode()
		{
			//This is a random value to represent an empty set,
			//since it does not have elements to calculate an hash,
			//and two empty sets are equal.
			const int emptyHashCode = 0x0fc43f9;

			var set = GetGroundedSubstitutions();
			if (!set.Any())
				return emptyHashCode;

			var hashs = set.Select(s => s.GetHashCode());
			var h = hashs.Aggregate((v1, v2) => v1 ^ v2);
			return emptyHashCode ^ h;
		}

		public override bool Equals(object obj)
		{
			SubstitutionSet other = obj as SubstitutionSet;
			if (other == null)
				return false;

			HashSet<Substitution> aux1 = ObjectPool<HashSet<Substitution>>.GetObject();
			HashSet<Substitution> aux2 = ObjectPool<HashSet<Substitution>>.GetObject();
			try 
			{	        
				aux1.UnionWith(this.GetGroundedSubstitutions());
				aux2.UnionWith(other.GetGroundedSubstitutions());
				var b = aux1.SetEquals(aux2);
				return b;
			}
			finally
			{
				aux1.Clear();
				ObjectPool<HashSet<Substitution>>.Recycle(aux1);
				aux2.Clear();
				ObjectPool<HashSet<Substitution>>.Recycle(aux2);
			}
		}

		public override string ToString()
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				builder.Append("(");
				bool addComma = false;
				foreach (var e in m_substitutions)
				{
					if (addComma)
						builder.Append(", ");

					builder.AppendFormat("{0}/{1}", e.Key, e.Value);
					addComma = true;
				}
				builder.Append(")");
				return builder.ToString();
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}
	}
}
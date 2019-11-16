using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;


namespace WellFormedNames
{
	/// <summary>
	/// Class representing a set of Substitution objects.
	/// 
	/// SubstitutionSet objects cannot contain conflicting substitutions, like:
	///  - [x] -> John
	///  - [x] -> Mary
	/// </summary>
	[Serializable]
	public sealed partial class SubstitutionSet : IEnumerable<Substitution>
	{
		private Constraints m_impl = new Constraints();

		/// <summary>
		/// Creates an empty SubstitutionSet
		/// </summary>
		public SubstitutionSet() {
		}

		/// <summary>
		/// Creates a new SubstitutionSet, given a set of Substitution objects.
		/// </summary>
		/// <param name="substitutions">The set of Substitution objects.</param>
		/// <exception cref="ArgumentException">Thrown if the given set will create substitution conflicts.</exception>
		/// @{
		public SubstitutionSet(params Substitution[] substitutions)
			: this((IEnumerable<Substitution>)substitutions)
		{}

		public SubstitutionSet(IEnumerable<Substitution> substitutions)
		{
			if(!internal_add(substitutions))
				throw new ArgumentException("The given substitutions will generate a conflict.", nameof(substitutions));
		}

		/// @}

		/// <summary>
		/// Gets/Sets the Name to substitute for a give variable.
		/// </summary>
		/// <param name="variable">The Name of the variable of the substitution value to get</param>
		/// <returns>The Name that will substitute the given variable Name,
		/// or null if the variable is not contained within this SubstitutionSet.</returns>
		/// <exception cref="ArgumentException">Thrown if the given Name does not represent a variable.</exception>
		public Name this[Name variable]
		{
			get
			{
				if (!variable.IsVariable)
					throw new ArgumentException("The given Name is not a variable.");

				return m_impl.GetValue(variable);
			}
		}

		/// <summary>
		/// Tells if a given variable is contained within this SubstitutionSet.
		/// </summary>
		/// <param name="variable">The Name of variable to test.</param>
		/// <exception cref="ArgumentException">Thrown if the given Name is not a variable definition.</exception>
		public bool Contains(Name variable)
		{
			if (!variable.IsVariable)
				throw new ArgumentException("The given Name is not a variable.",nameof(variable));

			return m_impl.ContainsVariable(variable);
		}

		/// <summary>
		/// Returns how many substitutions are in this set.
		/// </summary>
		public int Count()
		{
			return m_impl.Count();
		}

		/// <summary>
		/// Adds a new Substitution to this set.
		/// The adding process might fail, if the addition of the new Substitution would create a conflict.
		/// </summary>
		/// <param name="substitution">The Substitution to add to this set.</param>
		/// <returns>True if the substitution was sucessfully added to the set. False otherwise.</returns>
		/// <remarks>
		/// If the given Substitution already exists in this set, this method will return true as if it
		/// was successfully added, but it would not produce any changes to the underlying set.
		/// </remarks>
		public bool AddSubstitution(Substitution substitution)
		{
			bool canAdd;
            if (substitution == null) return false;

			if (m_impl.TestConflict(substitution, this, out canAdd))
				return false;

			if (canAdd)
				m_impl.AddSubstitution(substitution);

			return true;
		}

		/// <summary>
		/// Merge a Substitution set with this one.
		/// The merging will only ocurr if no conflicts between the two sets are found.
		/// </summary>
		/// <param name="substitution">The SubstitutionSet to merge with this one.</param>
		/// <returns>True if the substitutions was sucessfully merged. False otherwise.</returns>
		public bool AddSubstitutions(SubstitutionSet substitutions)
		{
			if (Conflicts(substitutions))
				return false;

			foreach (var s in substitutions)
				m_impl.AddSubstitution(s);

			return true;
		}

		/// <summary>
		/// Adds a set of Substitution objects to this set.
		/// If conflics are detected, the original SubstitutionSet object in not changed.
		/// </summary>
		/// <param name="substitutions">The Substitution objects set to add.</param>
		/// <returns>True if all the substitutions were added to this object. False if conflics were detected.</returns>
		public bool AddSubstitutions(IEnumerable<Substitution> substitutions)
		{
			var clone = m_impl.Clone();
			var pass = internal_add(substitutions);
			if (!pass)
				m_impl = clone;
			return pass;
		}

		private bool internal_add(IEnumerable<Substitution> substitutions)
		{
			foreach (var s in substitutions)
			{
				bool canAdd;
				if (m_impl.TestConflict(s, this, out canAdd))
				{
					return false;
				}

				if (canAdd)
					m_impl.AddSubstitution(s);
			}
			return true;
		}

		/// <summary>
		/// Determines if a given Substitution object will conflict with this set.
		/// </summary>
		/// <param name="substitution">The Substitution object to test.</param>
		public bool Conflicts(Substitution substitution)
		{
			bool aux;
			return m_impl.TestConflict(substitution,this, out aux);
		}

		/// <summary>
		/// Determines if a given SubstitutionSet object will conflict with this set.
		/// </summary>
		/// <param name="substitution">The SubstitutionSet object to test.</param>
		public bool Conflicts(SubstitutionSet substitutions)
		{
			foreach (var s in substitutions)
			{
				Name value = m_impl.GetValue(s.Variable);
				if(value==null)
					continue;

				var g1 = s.SubValue.Value.MakeGround(substitutions).MakeGround(this);
				var g2 = value.MakeGround(this).MakeGround(substitutions);
				if (!g1.Equals(g2))
					return true;
			}
			return false;
		}
		
		/// <summary>
		/// Returns the enumerator of this set.
		/// </summary>
		public IEnumerator<Substitution> GetEnumerator()
		{
			return m_impl.GetEnumerator();
		}

		/// <summary>
		/// Returns the enumerator of this set.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// @cond DOXYGEN_SHOULD_SKIP_THIS

		public override int GetHashCode()
		{
			//This is a random value to represent an empty set,
			//since it does not have elements to calculate an hash,
			//and two empty sets are equal.
			const int emptyHashCode = 0x0fc43f9;

			return m_impl.CalculateHashCode(emptyHashCode,this);
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
				//aux1.UnionWith(this.GetGroundedSubstitutions());
				//aux2.UnionWith(other.GetGroundedSubstitutions());
				aux1.UnionWith(m_impl.GetGroundedSubstitutions(this));
				aux2.UnionWith(other.m_impl.GetGroundedSubstitutions(other));
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
				var it = m_impl.GetEnumerator();
				while (it.MoveNext())
				{
					var e = it.Current;
					if (addComma)
						builder.Append(", ");

					builder.Append(e);
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


        public float FindMinimumCertainty()
        {
            if (!this.Any())
                return 1;
            var result = float.MaxValue;

            
            foreach (var sub in this)
            {
                if (sub.SubValue.Certainty < result)
                    result = sub.SubValue.Certainty;
            }
            return result;
        }
        /// @endcond
    }
}
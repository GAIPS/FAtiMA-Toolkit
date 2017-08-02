using System;
using System.Collections.Generic;

namespace WellFormedNames
{
	public partial class Name
	{
		private class VariableSymbol : Symbol
		{
			private string m_variableName;

			public VariableSymbol(string name)
				: base(false, false, false, true, false)
			{
				m_variableName = name;
			}

			/// <summary>
			/// Indicates whether the current object is equal to another object of the same type.
			/// </summary>
			/// <returns>
			/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
			/// </returns>
			/// <param name="other">An object to compare with this object.</param>
			public override bool Equals(Name name)
			{
				if (!name.IsVariable)
					return false;

				StringComparer c;

				c = StringComparer.InvariantCultureIgnoreCase;

				return c.Equals(m_variableName, ((VariableSymbol) name).m_variableName);
			}

			private static readonly int BASE_HASH = '['.GetHashCode() ^ ']'.GetHashCode();
			public override int GetHashCode()
			{
				return m_variableName.ToUpperInvariant().GetHashCode() ^ BASE_HASH;
			}

			public override string ToString()
			{
				return $"[{m_variableName}]";
			}

			public override IEnumerable<Name> GetVariables()
			{
				yield return this;
			}

			public override bool HasGhostVariable()
			{
				return m_variableName[0] == '_';
			}

			public override bool HasSelf()
			{
				return false;
			}

			public override Name SwapTerms(Name original, Name newName)
			{
				return this;
			}

			public override Name ReplaceUnboundVariables(string id)
			{
				return new VariableSymbol(m_variableName + id);
			}

			public override Name RemoveBoundedVariables(string id)
			{
				StringComparison c;
#if PORTABLE
				c = StringComparison.OrdinalIgnoreCase;
#else
				c = StringComparison.InvariantCultureIgnoreCase;
#endif
				if (m_variableName.EndsWith(id,c))
					return new VariableSymbol(m_variableName.Substring(0,m_variableName.Length-id.Length));
				return this;
			}

			public override Name MakeGround(SubstitutionSet bindings)
			{
				Name v = bindings[this];
				if (v == null)
					return this;
				return v.MakeGround(bindings);
			}

			public override bool Match(Name name)
			{
				if (name.IsUniversal)
					return true;

				return Equals(name as VariableSymbol);
			}

			/// @endcond
			public override bool TryConvertToValue<T>(out T value)
			{
				value = default(T);
				return false;
			}
		}
	}
}
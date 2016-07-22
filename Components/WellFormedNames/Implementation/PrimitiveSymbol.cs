using System;
using System.Collections.Generic;
using WellFormedNames.Exceptions;

namespace WellFormedNames
{
	public partial class Name
	{
		private class PrimitiveSymbol : Symbol
		{
			private PrimitiveValue m_value;

			public PrimitiveSymbol(string str) : base(true, false, true, false, true)
			{
				if (str == null)
				{
					m_value = null;
					return;
				}

				if(!PrimitiveValue.TryParse(str,out m_value))
					throw new ParsingException($"\"{str}\" is not a primitive value string.");
			}

			public PrimitiveSymbol(PrimitiveValue value) : base(true, false, true, false, true)
			{
				m_value = value;
			}

			public override bool Equals(Name name)
			{
				if (!name.IsPrimitive)
					return false;

				return m_value.Equals(((PrimitiveSymbol) name).m_value);
			}

			public override int GetHashCode()
			{
				if (m_value == null)
					return 0;
				return m_value.GetHashCode();
			}

			public override IEnumerable<Name> GetVariables()
			{
				yield break;
			}

			public override bool HasGhostVariable()
			{
				return false;
			}

			public override bool HasSelf()
			{
				return SELF_SYMBOL.Equals(this);
			}

			public override Name SwapTerms(Name original, Name newName)
			{
				if (original.Equals(this))
					return newName;
				return this;
			}

			public override Name ReplaceUnboundVariables(string id)
			{
				return this;
			}

			public override Name RemoveBoundedVariables(string id)
			{
				return this;
			}

			public override Name MakeGround(SubstitutionSet bindings)
			{
				return this;
			}

			public override bool Match(Name name)
			{
				PrimitiveSymbol s = name as PrimitiveSymbol;
				if (s == null)
					return false;

				return m_value.Equals(s.m_value);
			}

			public override string ToString()
			{
				return m_value?.ToString() ?? "-";
			}

			/// @endcond
			public override bool TryConvertToValue<T>(out T value)
			{
				if (m_value == null)
				{
					value = default(T);
					return false;
				}

				return PrimitiveValue.TryOpen<T>(m_value, out value);
			}

			public static bool TryCompare(PrimitiveSymbol s1, PrimitiveSymbol s2, out int delta)
			{
				return PrimitiveValue.TryCompare(s1.m_value, s2.m_value, out delta);
			}
		}
	}
}
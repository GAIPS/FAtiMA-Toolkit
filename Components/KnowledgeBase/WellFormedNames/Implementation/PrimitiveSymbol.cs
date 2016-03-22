using System;
using System.Collections.Generic;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		private class PrimitiveSymbol : Symbol
		{
			private PrimitiveValue m_value;

			public PrimitiveSymbol(PrimitiveValue value) : base(true,false,true,false,true)
			{
				m_value = value;
			}

			private bool CompareNameWithString(string str)
			{
				return string.Equals(m_value.ToString(), str, StringComparison.InvariantCultureIgnoreCase);
			}

			public override bool Equals(object obj)
			{
				PrimitiveSymbol s = obj as PrimitiveSymbol;
				if (s == null)
					return false;

				if (m_value == s.m_value)
					return true;

				if (m_value == null || s.m_value == null)
					return false;

				return CompareNameWithString(s.m_value.ToString());
			}

			public override int GetHashCode()
			{
				if (m_value == null)
					return 0;
				return m_value.ToString().ToUpperInvariant().GetHashCode();
			}

			public override IEnumerable<Name> GetVariableList()
			{
				yield break;
			}

			public override bool HasGhostVariable()
			{
				return false;
			}

			public override bool HasSelf()
			{
				return CompareNameWithString(Name.SELF_STRING);
			}

			protected override Name SwapPerspective(Name original, Name newName)
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

			public override object Clone()
			{
				return new PrimitiveSymbol(m_value);
			}

			public override bool Match(Name name)
			{
				PrimitiveSymbol s = name as PrimitiveSymbol;
				if (s == null)
					return false;

				if (m_value == s.m_value)
					return true;

				if (m_value == null || s.m_value == null)
					return false;

				return CompareNameWithString(s.m_value);
			}

			public override PrimitiveValue GetPrimitiveValue()
			{
				return m_value;
			}

			public override string ToString()
			{
				return (m_value == null) ? "-" : m_value.ToString();
			}
		}
	}
}
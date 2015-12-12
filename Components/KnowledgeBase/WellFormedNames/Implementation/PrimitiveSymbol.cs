using System;
using System.Collections.Generic;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		private class PrimitiveSymbol : Symbol
		{
			private string m_value;

			public PrimitiveSymbol(string value) : base(true,false,true,false,true)
			{
				m_value = value??"-";
			}
			
			public override bool Equals(object obj)
			{
				PrimitiveSymbol s = obj as PrimitiveSymbol;
				if (s == null)
					return false;

				return string.Equals(m_value, s.m_value,StringComparison.InvariantCultureIgnoreCase);
			}

			public override int GetHashCode()
			{
				return m_value.ToUpperInvariant().GetHashCode();
			}

			public override IEnumerable<Name> GetVariableList()
			{
				yield break;
			}

			public override bool HasGhostVariable()
			{
				return false;
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

				return string.Equals(m_value, s.m_value, StringComparison.InvariantCultureIgnoreCase);
			}

			public override PrimitiveValue GetPrimitiveValue()
			{
				return PrimitiveValue.Parse(m_value);
			}

			public override string ToString()
			{
				return m_value??"-";
			}
		}
	}
}
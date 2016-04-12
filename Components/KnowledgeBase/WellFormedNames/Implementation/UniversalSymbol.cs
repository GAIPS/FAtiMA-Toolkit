using System.Collections.Generic;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		private class UniversalSymbol : Symbol
		{
			public UniversalSymbol()
				: base(true, true, false, false, false)
			{
			}

			public override string ToString()
			{
				return "*";
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
				return false;
			}

			public override Name SwapPerspective(Name original, Name newName)
			{
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
				return new UniversalSymbol();
			}

			public override bool Match(Name name)
			{
				return true;
			}

			public override PrimitiveValue GetPrimitiveValue()
			{
				return null;
			}

			public override bool Equals(object obj)
			{
				return obj is UniversalSymbol;
			}

			public override int GetHashCode()
			{
				return '*'.GetHashCode();
			}
		}
	}
}
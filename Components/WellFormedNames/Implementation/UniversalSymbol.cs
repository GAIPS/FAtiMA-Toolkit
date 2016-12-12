using System.Collections.Generic;

namespace WellFormedNames
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

			/*public override IEnumerable<Name> GetVariables()
			{
				yield break;
			}*/

			/*public override bool HasGhostVariable()
			{
				return false;
			}*/

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
				return true;
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
				return name.IsUniversal;
			}

			public override int GetHashCode()
			{
				return '*'.GetHashCode();
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
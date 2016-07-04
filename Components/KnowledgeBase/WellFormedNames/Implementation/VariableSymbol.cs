using System;
using System.Collections.Generic;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		private class VariableSymbol : Symbol, IEquatable<VariableSymbol>
		{
			private string m_variableName;

			public VariableSymbol(string name)
				: base(false, false, false, true, false)
			{
				m_variableName = name;
			}

			public override bool Equals(object obj)
			{
				return Equals(obj as VariableSymbol);
			}

			public bool Equals(VariableSymbol other)
			{
				if (other == null)
					return false;
				return string.Equals(m_variableName, other.m_variableName, StringComparison.InvariantCultureIgnoreCase);
			}

			private static readonly int BASE_HASH = '['.GetHashCode() ^ '['.GetHashCode();
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
				if(m_variableName.EndsWith(id,StringComparison.InvariantCultureIgnoreCase))
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

			public override object Clone()
			{
				return new VariableSymbol(m_variableName);
			}

			public override bool Match(Name name)
			{
				return Equals(name as VariableSymbol);
			}

			public override PrimitiveValue GetPrimitiveValue()
			{
				return null;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		/// <summary>
		/// Well Formed Name composed by several symbols. 
		/// If S and s1,s2,...sn are symbols, then S(s1,s2,...,sn) is a Composed Name
		/// the first symbol "S" is called the major symbol and it is followed by a list of
		/// comma separated parameter symbols (s1,s2,..,sn), which are enclosed in parenthesis.
		/// 
		/// @author: João Dias
		/// @author: Pedro Gonçalves (C# version)
		/// </summary>
		private class ComposedName : Name
		{
			private Symbol RootSymbol;
			private Name[] Terms;
			/*
			private bool m_isGrounded;
			public override bool IsGrounded
			{
				get { return m_isGrounded; }
			}

			public override bool IsUniversal
			{
				get { return false; }
			}

			public override bool IsVariable
			{
				get { return false; }
			}

			public override bool IsPrimitive
			{
				get { return false; }
			}

			private bool m_isConstant;
			public override bool IsConstant
			{
				get { return m_isConstant; }
			}
			*/
			public override int NumberOfTerms
			{
				get { return Terms.Length + 1; }
			}

			/// <summary>
			/// Creates a new ComposedName, receiving a major symbol, followed by several parameter symbols
			/// parameter symbols
			/// </summary>
			/// <param name="head">The head symbol</param>
			/// <param name="terms">A set of parameter symbols</param>
			public ComposedName(Symbol head, Name[] terms)
				: base(
					head.IsGrounded && terms.All(n=>n.IsGrounded),false,
					head.IsConstant && terms.All(n=>n.IsConstant),false,false
				)
			{
				RootSymbol = head;
				Terms = terms;
			}

			/// <summary>
			/// Clone Constructor
			/// </summary>
			/// <param name="composedName">The composedName to clone</param>
			private ComposedName(ComposedName composedName)
				: base(composedName.IsGrounded,false,composedName.IsConstant,false,false)
			{
				RootSymbol = composedName.RootSymbol;
				Terms = composedName.Terms.Clone<Name>().ToArray();
			}
			
			public override Name GetFirstTerm()
			{
				return RootSymbol;
			}

			public override IEnumerable<Name> GetTerms()
			{
				return Terms.Prepend(RootSymbol);
			}

			public override Name GetNTerm(int index)
			{
				if(index<0 || index>Terms.Length)
					throw new IndexOutOfRangeException();

				if (index == 0)
					return RootSymbol;
				index--;
				return Terms[index];
			}

			public override IEnumerable<Name> GetLiterals()
			{
				return Terms.SelectMany(t => t.GetLiterals()).Prepend(RootSymbol);
			}

			public override IEnumerable<Name> GetVariableList()
			{
				return GetTerms().SelectMany(l => l.GetVariableList());
			}

			public override bool HasGhostVariable()
			{
				return GetTerms().Any(s => s.HasGhostVariable());
			}

			protected override Name SwapPerspective(Name original, Name newName)
			{
				return new ComposedName((Symbol)RootSymbol.SwapPerspective(original,newName), Terms.Select(t => t.SwapPerspective(original,newName)).ToArray());
			}

			public override Name ReplaceUnboundVariables(string id)
			{
				if (IsGrounded)
					return this;

				return new ComposedName((Symbol)RootSymbol.ReplaceUnboundVariables(id),Terms.Select(t => t.ReplaceUnboundVariables(id)).ToArray());
			}

			public override Name RemoveBoundedVariables(string id)
			{
				if (IsGrounded)
					return this;

				return new ComposedName((Symbol)RootSymbol.RemoveBoundedVariables(id), Terms.Select(t => t.RemoveBoundedVariables(id)).ToArray());
			}

			public override Name MakeGround(SubstitutionSet bindings)
			{
				if (IsGrounded)
					return (Name)Clone();

				return new ComposedName((Symbol)RootSymbol.MakeGround(bindings), Terms.Select(t => t.MakeGround(bindings)).ToArray());
			}

			public override object Clone()
			{
				return new ComposedName(this);
			}

			public override string ToString()
			{
				StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
				builder.Append(RootSymbol);
				builder.Append('(');
				for (int i = 0; i < Terms.Length; i++)
				{
					builder.Append(Terms[i]);
					if (i + 1 < Terms.Length)
						builder.Append(", ");
				}

				builder.Append(')');

				string result = builder.ToString();
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
				return result;
			}

			public override bool Equals(object obj)
			{
				var other = obj as ComposedName;
				if (other == null)
					return false;

				if (other.Terms.Length != Terms.Length)
					return false;

				if (!other.RootSymbol.Equals(RootSymbol))
				{
					return false;
				}

				return !Terms.Where((t, i) => !other.Terms[i].Equals(t)).Any();
			}

			public override bool Match(Name name)
			{
				var other = name as ComposedName;
				if (other == null)
					return false;

				if (other.Terms.Length != Terms.Length)
					return false;

				if (!other.RootSymbol.Match(RootSymbol))
				{
					return false;
				}

				for (int i = 0; i < Terms.Length; i++)
					if (!Terms[i].Match(other.Terms[i]))
						return false;

				return true;
			}

			public override PrimitiveValue GetPrimitiveValue()
			{
				return null;
			}

			public override Name Unfold(out SubstitutionSet set)
			{
				Name[] terms = new Name[Terms.Length];
				set = null;
				for (int i = 0; i < terms.Length; i++)
				{
					Symbol s = Terms[i] as Symbol;
					if (s == null)
					{
						s = (Symbol)GenerateUniqueGhostVariable();
						SubstitutionSet aux;
						var sub = new Substitution(s, Terms[i].Unfold(out aux));

						if (set == null)
							set = aux ?? new SubstitutionSet();
						else if (aux != null)
							set.AddSubstitutions(aux);

						set.AddSubstitution(sub);
					}

					terms[i] = s;
				}

				return new ComposedName(RootSymbol, terms);
			}

			public override int GetHashCode()
			{
				return GetTerms().Select(t => t.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
			}
		}	 
	}
}

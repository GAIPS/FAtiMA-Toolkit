using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WellFormedNames
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
					head.IsConstant && terms.All(n=>n.IsConstant),false,false,true
				)
			{
				RootSymbol = head;
				Terms = terms;
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

            public override bool HasSelf()
            {
                return GetTerms().Any(s => s.HasSelf());
            }

            /*public override IEnumerable<Name> GetLiterals()
			{
				return Terms.SelectMany(t => t.GetLiterals()).Prepend(RootSymbol);
			}*/

            /*public override IEnumerable<Name> GetVariables()
			{
				return GetTerms().SelectMany(l => l.GetVariables());
			}*/

            /*public override bool HasGhostVariable()
			{
				return GetTerms().Any(s => s.HasGhostVariable());
			}*/


            public override Name SwapTerms(Name original, Name newName)
			{
				return new ComposedName((Symbol)RootSymbol.SwapTerms(original,newName), Terms.Select(t => t.SwapTerms(original,newName)).ToArray());
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
					return this;

				return new ComposedName((Symbol)RootSymbol.MakeGround(bindings), Terms.Select(t => t.MakeGround(bindings)).ToArray());
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

			/// <summary>
			/// Indicates whether the current object is equal to another object of the same type.
			/// </summary>
			/// <returns>
			/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
			/// </returns>
			/// <param name="other">An object to compare with this object.</param>
			public override bool Equals(Name name)
			{
				if (!name.IsComposed)
					return false;

				if (name.NumberOfTerms != NumberOfTerms)
					return false;

				return GetTerms().Zip(name.GetTerms(), (n1, n2) => n1.Equals(n2)).All(b => b);
			}

			public override bool Match(Name name)
			{
				if (name.IsUniversal)
					return true;

				var other = name as ComposedName;
				if (other?.Terms.Length != Terms.Length)
					return false;

				if (!other.RootSymbol.Match(RootSymbol))
				{
					return false;
				}

				return !Terms.Where((t, i) => !t.Match(other.Terms[i])).Any();
			}

			public override Name ApplyToTerms(Func<Name, Name> transformFunction)
			{
				return new ComposedName(RootSymbol, Terms.Select(transformFunction).ToArray());
			}

			public override int GetHashCode()
			{
				return GetTerms().Select(t => t.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
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

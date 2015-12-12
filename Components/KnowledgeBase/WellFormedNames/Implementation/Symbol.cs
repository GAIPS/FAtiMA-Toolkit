using System;
using System.Collections.Generic;
using KnowledgeBase.Exceptions;

namespace KnowledgeBase.WellFormedNames
{
	public partial class Name
	{
		/// <summary>
		/// Instantiation of a Simple Well Formed Name composed by just one literal 
		/// 
		/// Well Formed Name with just one literal The alphabet that makes up the symbols
		/// expressions consists of: 
		/// • The set of letters, upper and lowercase.
		/// • The set of digits, 0,1,..,9 
		/// • The symbols “_”, “-”, "*"
		/// 
		/// Symbols expressions begin with a letter and are followed by any sequence of 
		/// these legal characters. Well formed names are composed by four types 
		/// of symbols:
		///		1. The Truth symbols "True" and "False".
		///		2. Constant symbols, which are simple symbol expressions.
		///		3. Variables symbols, which are symbol expressions enclosed in square
		///		parentheses. Ex: [x] represents the variable x. 
		///		4. The Self symbol [SELF], a reserved special variable which
		///		refers to the agent.
		///		
		/// @author: João Dias
		/// @author: Pedro Gonçalves (C# version)
		/// </summary>
		/// <see cref="Name"/>
		[Serializable]
		private abstract class Symbol : Name
		{
			protected Symbol(bool isGrounded, bool isUniversal, bool isConstant, bool isVariable, bool isPrimitive)
				: base(isGrounded, isUniversal, isConstant, isVariable, isPrimitive)
			{
			}

			public sealed override int NumberOfTerms
			{
				get { return 1; }
			}

			public sealed override Name GetFirstTerm()
			{
				return this;
			}

			public sealed override IEnumerable<Name> GetTerms()
			{
				yield return this;
			}

			public sealed override IEnumerable<Name> GetLiterals()
			{
				yield return this;
			}

			public sealed override Name Unfold(out SubstitutionSet set)
			{
				set = null;
				return this;
			}
		}
	}
}

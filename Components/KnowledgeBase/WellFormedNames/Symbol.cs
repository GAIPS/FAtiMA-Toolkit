using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using KnowledgeBase.WellFormedNames.Exceptions;

namespace KnowledgeBase.WellFormedNames
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
	/// <see cref="FAtiMA.Core.WellFormedNames.Name"/>
	public class Symbol : Name
	{
		public const string VARIABLE_SYMBOL_VALIDATION_PATTERN = @"\[[A-Za-z_][\w-]*\]";
		public const string VALUE_SYMBOL_VALIDATION_PATTERN = @"\*|[\w-]+";
		public const string SYMBOL_VALIDATION_PATTERN = @"(("+VARIABLE_SYMBOL_VALIDATION_PATTERN+")|("+VALUE_SYMBOL_VALIDATION_PATTERN+"))";
		private readonly static Regex VALIDATION_REGEX = new Regex("^"+SYMBOL_VALIDATION_PATTERN+"$");

		public const string SELF_STRING = "SELF";
		public const string UNIVERSAL_STRING = "*";
		public const string AGENT_STRING = "[AGENT]";

		public static readonly Symbol SELF_SYMBOL = new Symbol(SELF_STRING);
		public static readonly Symbol UNIVERSAL_SYMBOL = new Symbol(UNIVERSAL_STRING);
		public static readonly Symbol AGENT_SYMBOL = new Symbol(AGENT_STRING);

		/// <summary>
		/// Gets the String that represents the Symbol's name
		/// </summary>
		public string Name
		{
			get;
			protected set;
		}

		public override bool IsUniversal
		{
			get { return this.Name == UNIVERSAL_STRING; }
		}

		public override bool IsVariable
		{
			get { return this.Name.StartsWith("["); }
		}

		public override int NumberOfTerms
		{
			get { return 1; }
		}

		/// <summary>
		/// Creates a new Symbol
		/// </summary>
		/// <param name="symbolString">A String that corresponds to a Well Formed Symbol</param>
		/// <exception cref="FAtiMA.Core.Exceptions.InvalidSymbolDefinitionException">
		/// Thrown if the given name string is not a proper Well Formed Symbol definition
		/// </exception>
		public Symbol(string symbolString)
		{
			symbolString = symbolString.Trim();
			if(!VALIDATION_REGEX.IsMatch(symbolString))
				throw new InvalidSymbolDefinitionException(symbolString);

			this.IsGrounded = (symbolString[0] != '[');
			this.Name = this.IsGrounded?symbolString:symbolString.ToLowerInvariant();
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="symbol">The symbol to clone.</param>
		protected Symbol(Symbol symbol)
		{
			this.IsGrounded = symbol.IsGrounded;
			this.Name = symbol.Name;
		}

		public override Name GetFirstTerm()
		{
			return this;
		}

		public override IEnumerable<Name> GetTerms()
		{
			yield return this;
		}

		public override IEnumerable<Symbol> GetLiterals()
		{
			yield return this;
		}

		public override IEnumerable<Symbol> GetVariableList()
		{
			if (IsGrounded)
				yield break;
			yield return this;
		}

		public override bool HasGhostVariable()
		{
			return Name.StartsWith("[_");
		}

		public override Name SwapPerspective(string original, string newName)
		{
			if (this.Name != original)
				return (Symbol)Clone();
			return new Symbol(newName);
		}

		/// <summary>
		/// Replaces all unbound variables in the object by applying a numeric 
		/// identifier to each one. For example, the variable [x] becomes [x4]
		/// if the received ID is 4.
		/// </summary>
		/// <remarks>Attention, this method modifies the original object.</remarks>
		/// <param name="variableID">the identifier to be applied</param>
		public override Name ReplaceUnboundVariables(long variableID)
		{
			if (IsGrounded)
				return (Symbol)Clone();

			return new Symbol(this.Name.Substring(0, this.Name.Length - 1) + variableID + ']');
		}

		/// <summary>
		/// Applies a set of substitutions to the object, grounding it.
		/// Example: Applying the substitution "[X]/John" in the name "Weak([X])" returns
		/// "Weak(John)". 
		/// </summary>
		/// <remarks>Attention, this method modifies the original object.</remarks>
		/// <param name="bindings">The substitutions of the type "[Variable]/value"</param>
		/// <see cref="FAtiMA.Core.WellFormedNames.Substitution"/>
		public override Name MakeGround(IEnumerable<Substitution> bindings)
		{
			if (this.IsGrounded)
				return this;

			using (IEnumerator<Substitution> it = bindings.GetEnumerator())
			{
				while (it.MoveNext())
				{
					if (string.Equals(this.Name, it.Current.Variable.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						if (it.Current.Value.IsGrounded)
							return (Name)it.Current.Value.Clone();
						else
							return it.Current.Value.MakeGround(bindings);
					}
				}
			}

			return this;
		}

		public override string ToString()
		{
			return Name;
		}

		public override object Clone()
		{
			return new Symbol(this);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			Symbol s = obj as Symbol;
			if (s == null)
				return false;

			return s.Name == this.Name;
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public override bool Match(Name name)
		{
			if (this.IsUniversal || name.IsUniversal)
				return true;

			Symbol other = name as Symbol;
			if (other == null)
				return false;

			return other.Name == this.Name;
		}

		public override bool SimilarStructure(Name other)
		{
			if (other == null)
				return false;

			return other is Symbol;
		}

		/*
		/// <summary>
		/// Evaluates this Name according to the data stored in the KnowledgeBase
		/// If this clone is changed afterwards, the original object remains the same.
		/// </summary>
		/// <param name="m">a reference to the KnowledgeBase</param>
		/// <returns>if the name is a symbol, it returns its name, otherwise it returns the value associated to the name in the KB</returns>
		public override object evaluate(Memory m)
		{
			return (IsGrounded ? Name : null);
		}
		*/
	}
}

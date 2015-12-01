/** 
 * Copyright (C) 2006 GAIPS/INESC-ID 
 *  
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 * 
 * Company: GAIPS/INESC-ID
 * Project: FAtiMA
 * Created: 12/07/2006
 * Ported to C#: 17/06/2015
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.Exceptions;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Abstract Well Formed Name
	/// A well formed name is used to specify goal/action names, objects, properties,
	/// constants, and relations.
	/// It's syntax is based on first order logic symbols, variables and predicates.
	/// a Name can be either a Symbol or a ComposedName (composed by several symbols)
	/// 
	/// <see cref="Symbol"/>
	/// <see cref="ComposedName"/>
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	[Serializable]
	public abstract class Name : IGroundable<Name>, ICloneable
	{
		private static long _variableIdCounter = 0;

		public bool IsGrounded
		{
			get;
			protected set;
		}

		public abstract bool IsUniversal
		{
			get;
		}

		/// <summary>
		/// Returns true if this Name doesn't contain universal or variable Symbols
		/// </summary>
		public abstract bool IsConstant { get; }	//TODO: find a better name for this property

		public abstract bool IsVariable
		{
			get;
		}

		public abstract bool IsPrimitive { get; }

		public abstract int NumberOfTerms { get; }

		public abstract override bool Equals(object obj);
		public abstract override int GetHashCode();

		public abstract Name GetFirstTerm();

		/// <summary>
		/// Generates a sequence with all Names contained inside this Name
		/// </summary>
		public abstract IEnumerable<Name> GetTerms();

		/// <summary>
		/// Generates a sequence with all Symbols contained inside this Name
		/// </summary>
		public abstract IEnumerable<Symbol> GetLiterals();

		public abstract IEnumerable<Symbol> GetVariableList();

		public abstract bool HasGhostVariable();

		public bool ContainsVariable(Symbol variable)
		{
			if (variable.IsGrounded)
				throw new ArgumentException("The given Symbol is not a variable","variable");

			return this.GetVariableList().Any(s => string.Equals(variable.Name,s.Name,StringComparison.InvariantCultureIgnoreCase));
		}

		public Name ApplyPerspective(string name)
		{
			return SwapPerspective(name, Symbol.SELF_STRING);
		}

		public Name RemovePerspective(string name)
		{
			return SwapPerspective(Symbol.SELF_STRING, name);
		}

		public abstract Name SwapPerspective(string original, string newName);
        public abstract Name ReplaceUnboundVariables(long variableId);
		public abstract Name MakeGround(SubstitutionSet bindings);

		/// <summary>
		/// Clones this Name, returning an equal copy.
		/// If this clone is changed afterwards, the original object remains the same.
		/// </summary>
		/// <returns>The Name's copy.</returns>
		public abstract object Clone();

		/// <summary>
		/// Determines if a Wellformed Name matches the given name template.
		/// Both Names are matched to each other if all their Symbols are equal to one another or if a Symbol matches a universal Symbol
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public abstract bool Match(Name name);

		/// <summary>
		/// Unfolds a name into depth 0 Name with a set of binding.
		/// Ex:
		///    A(B,C(D,E(F))) unfolds into A(B,[_0]) with the bindings
		///    [_0]/C(D,[_1])
		///    [_1]/E(F)
		/// 
		/// </summary>
		/// <param name="set">The output set of substitutions that fold the name again into it's original form.</param>
		/// <returns>The unfolded Name.</returns>
		public abstract Name Unfold(out SubstitutionSet set);

		#region Parsing

		/// <summary>
		/// <see cref="FAtiMA.Core.WellFormedName.Symbol"/>
		/// <see cref="FAtiMA.Core.WellFormedName.ComposedName"/>
		/// 
		/// Root			->	(#|\?)?Name
		/// Name			->	Symbol
		///						ComposedName
		///	ComposedName	->	Symbol(NameList)
		///	NameList		->	Name,NameList
		///					->	Name
		///	Symbol			->	*
		///						[\w-]+
		///						\[\w[\w-]*\]
		/// </summary>
		/// <param name="str">the String to be parsed</param>
		/// <returns>the parsed Name (can be either a Symbol or a ComposedName)</returns>
		public static Name Parse(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				if(str == null)
					throw new ArgumentNullException("str");
				throw new ArgumentException("Cannot parse an empty string","str");
			}
			
			str = str.Trim();
			Name result = ParseName(str);
			return result;
		}

		// Internal Name Parser
		private static readonly char[] NAME_PARSER_LOOK_FORWARD = { ',', '(' };
		private static Name ParseName(string str)
		{
			int index = str.IndexOfAny(NAME_PARSER_LOOK_FORWARD);
			if (index < 0)
				return ParseSymbol(str);
			return ParseComposedName(str);
		}

		private static Symbol ParseSymbol(string str)
		{
			return new Symbol(str);
		}

		private static ComposedName ParseComposedName(string str)
		{
			if (str[str.Length - 1] != ')')
				throw new ParsingException("Failed to parse name. Expected ')', got '{0}'", str[str.Length - 1]);

			int index = str.IndexOf('(');

			List<Name> names = ObjectPool<List<Name>>.GetObject();
			try
			{
				string listString = str.Substring(index + 1, str.Length - index - 2);
				ParseNameList(listString, names);
				Symbol headSymbol = new Symbol(str.Substring(0, index));
				names.Insert(0, headSymbol);
				return new ComposedName(names);
			}
			finally
			{
				names.Clear();
				ObjectPool<List<Name>>.Recycle(names);
			}
		}

		private static void ParseNameList(string str, List<Name> result)
		{
			int openedBrackets = 0;
			int index = 0;
			for (; index < str.Length; index++)
			{
				char c = str[index];
				if ((c == ',') && (openedBrackets == 0))
					break;
				else if (c == '(')
					openedBrackets++;
				else if (c == ')')
					openedBrackets--;
			}

			if (index == str.Length)
			{
				result.Add(ParseName(str));
				return;
			}

			string nameStr = str.Substring(0, index);
			result.Add(ParseName(nameStr));
			ParseNameList(str.Substring(index + 1), result);
		}

		#endregion

		public static Symbol GenerateUniqueGhostVariable()
		{
			Symbol ghost = new Symbol("[_]");
            ghost = (Symbol) ghost.ReplaceUnboundVariables(_variableIdCounter++);
            return ghost;
		}

		#region Operators

		public static explicit operator Name(string definition)
		{
			return Parse(definition);
		}

		public static bool operator ==(Name n1, Name n2)
		{
            if (ReferenceEquals(n1, n2))
				return true;

		    if ((object) n1 == null || (object) n2 == null)
		        return false;
            
            return n1.Equals(n2);
		}

		public static bool operator !=(Name n1, Name n2)
		{
			return !(n1 == n2);
		}

		#endregion

		/*
		/// <summary>
		/// Evaluates this Name according to the data stored in the KB
		/// If this clone is changed afterwards, the original object remains the same.
		/// </summary>
		/// <param name="m">a reference to the KB</param>
		/// <returns>if the name is a symbol, it returns its name, otherwise it returns the value associated to the name in the KB</returns>
		public abstract Object evaluate(KB m);
		*/
	}
}

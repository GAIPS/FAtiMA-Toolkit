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
using System.Text.RegularExpressions;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames.Interfaces;
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
	public abstract partial class Name : IGroundable<Name>, IComparable<Name>, IPerspective<Name>, ICloneable
	{
		public const string NUMBER_VALIDATION_PATTERN = @"(?:-|\+)?\d+(?:\.\d+)?(?:e(?:-|\+)?[1-9]\d*)?";
		public const string VARIABLE_SYMBOL_VALIDATION_PATTERN = @"^\[([A-Za-z_][\w-]*)\]$";
		public const string VALUE_SYMBOL_VALIDATION_PATTERN = @"^(?:(?:[A-Za-z_][\w-]*)|(?:" + NUMBER_VALIDATION_PATTERN + @"))$";
		public static readonly Regex VARIABLE_VALIDATION_PATTERN = new Regex(VARIABLE_SYMBOL_VALIDATION_PATTERN,RegexOptions.IgnoreCase);
		public static readonly Regex PRIMITIVE_VALIDATION_PATTERN = new Regex(VALUE_SYMBOL_VALIDATION_PATTERN,RegexOptions.IgnoreCase);

		public const string NIL_STRING = "-";
		public const string SELF_STRING = "SELF";
		public const string UNIVERSAL_STRING = "*";
		public const string AGENT_STRING = "[AGENT]";

		public static readonly Name NIL_SYMBOL = new PrimitiveSymbol(null);
		public static readonly Name SELF_SYMBOL = new PrimitiveSymbol(SELF_STRING);
		public static readonly Name UNIVERSAL_SYMBOL = new UniversalSymbol();
		public static readonly Name AGENT_SYMBOL = new VariableSymbol("AGENT");

		public bool IsGrounded { get; }

		public readonly bool IsUniversal;
		/// <summary>
		/// Does this Name not contain universal or variable Symbols
		/// </summary>
		public readonly bool IsConstant;

		/// <summary>
		/// Is this name a variable
		/// </summary>
		public readonly bool IsVariable;

		/// <summary>
		/// Is this name a primitive value
		/// </summary>
		public readonly bool IsPrimitive;

		/// <summary>
		/// Is this name composed
		/// </summary>
		public readonly bool IsComposed;

		public abstract int NumberOfTerms { get; }

		private Name(bool isGrounded, bool isUniversal, bool isConstant, bool isVariable, bool isPrimitive, bool isComposed)
		{
			this.IsGrounded = isGrounded;
			this.IsUniversal = isUniversal;
			this.IsConstant = isConstant;
			this.IsVariable = isVariable;
			this.IsPrimitive = isPrimitive;
			this.IsComposed = isComposed;
		}
		
		public abstract override bool Equals(object obj);
		public abstract override int GetHashCode();
		public abstract override string ToString();

		public abstract Name GetFirstTerm();

		/// <summary>
		/// Generates a sequence with all Names contained inside this Name
		/// </summary>
		public abstract IEnumerable<Name> GetTerms();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		public abstract Name GetNTerm(int index);

		/// <summary>
		/// Generates a sequence with all Symbols contained inside this Name
		/// </summary>
		public abstract IEnumerable<Name> GetLiterals();

		public abstract IEnumerable<Name> GetVariableList();

		public abstract bool HasGhostVariable();

		public abstract bool HasSelf();

		public bool ContainsVariable(Name variable)
		{
			if (!variable.IsVariable)
				throw new ArgumentException("The given Name is not a variable","variable");

			var v = (VariableSymbol) variable;

			return this.GetVariableList().Cast<VariableSymbol>().Any(s => s.Equals(v));
		}

		public Name ApplyPerspective(string name)
		{
			return ApplyPerspective(BuildName(name));
		}

		public Name RemovePerspective(string name)
		{
			return RemovePerspective(BuildName(name));
		}

		public Name ApplyPerspective(Name name)
		{
			return SwapPerspective(name, SELF_SYMBOL);
		}

		public Name RemovePerspective(Name name)
		{
			return SwapPerspective(SELF_SYMBOL, name);
		}

		public abstract Name SwapPerspective(Name original, Name newName);

		public abstract Name MakeGround(SubstitutionSet bindings);
		public abstract Name ReplaceUnboundVariables(string id);
		public abstract Name RemoveBoundedVariables(string id);

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

		public abstract Name ApplyToTerms(Func<Name, Name> transformFunction);

		public abstract PrimitiveValue GetPrimitiveValue();

		private static ulong _variableIdCounter = 0;
		public static Name GenerateUniqueGhostVariable()
		{
			Name ghost = new VariableSymbol("_");
            ghost = ghost.ReplaceUnboundVariables(_variableIdCounter.ToString());
			_variableIdCounter++;
            return ghost;
		}

		#region Operators

		public static explicit operator Name(string definition)
		{
			return BuildName(definition);
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

#region Builders

		public static Name BuildName(Name firstTerm, Name secondTerm, params Name[] otherTerms)
		{
			return BuildName(otherTerms.Prepend(secondTerm).Prepend(firstTerm));
		}

		public static Name BuildName(IEnumerable<Name> terms)
		{
			var set = ObjectPool<List<Name>>.GetObject();
			try
			{
				set.AddRange(terms);
				if (set.Count < 2)
					throw new ArgumentException("Need at least 2 term to create a composed symbol", "terms");

				Symbol head = set[0] as Symbol;
				if (head == null)
					throw new ArgumentException("The first term needs to be a Symbol object", "terms");

				set.RemoveAt(0);
				return new ComposedName(head,set.Select(n => n??NIL_SYMBOL).ToArray());
			}
			finally
			{
				set.Clear();
				ObjectPool<List<Name>>.Recycle(set);
			}
		}

		public static Name BuildName(PrimitiveValue value)
		{
			if (value.TypeCode == TypeCode.String)
				return BuildName((string) value);

			return new PrimitiveSymbol(value);
		}

		public static Name BuildName(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				if (str == null)
					return NIL_SYMBOL;

				throw new ArgumentException("Cannot parse an empty string", "str");
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
			str = str.Trim();
			if (str == "*")
				return (Symbol)UNIVERSAL_SYMBOL;
			if (str == "-")
				return (Symbol) NIL_SYMBOL;

			var varMatch = VARIABLE_VALIDATION_PATTERN.Match(str);
			if (varMatch.Success)
			{
				string varName = varMatch.Groups[1].Value;
				return new VariableSymbol(varName);
			}

			var primitiveMatch = PRIMITIVE_VALIDATION_PATTERN.Match(str);
			if (primitiveMatch.Success)
				return new PrimitiveSymbol(PrimitiveValue.Parse(str));

			throw new ParsingException(str + " is not a well formated name definition");
		}

		private static ComposedName ParseComposedName(string str)
		{
			if (str[str.Length - 1] != ')')
				throw new ParsingException($"Failed to parse name. Expected ')', got '{str[str.Length - 1]}'");

			int index = str.IndexOf('(');

			List<Name> names = ObjectPool<List<Name>>.GetObject();
			try
			{
				string listString = str.Substring(index + 1, str.Length - index - 2);
				ParseNameList(listString, names);
				Symbol headSymbol = ParseSymbol(str.Substring(0, index));
				return new ComposedName(headSymbol, names.ToArray());
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

		/*
		/// <summary>
		/// Evaluates this Name according to the data stored in the KB
		/// If this clone is changed afterwards, the original object remains the same.
		/// </summary>
		/// <param name="m">a reference to the KB</param>
		/// <returns>if the name is a symbol, it returns its name, otherwise it returns the value associated to the name in the KB</returns>
		public abstract Object evaluate(KB m);
		*/

		public static string ApplyPerspective(string name, string me)
		{
			if (string.Compare(name, me, StringComparison.InvariantCultureIgnoreCase) == 0)
				return SELF_STRING;
			return name;
		}

		public static string RemovePerspective(string name, string me)
		{
			if (string.Compare(name, SELF_STRING, StringComparison.InvariantCultureIgnoreCase) == 0)
				return me;
			return name;
		}

		public int CompareTo(Name other)
		{
			if (other == null)
				return 1;

			return StringComparer.InvariantCultureIgnoreCase.Compare(ToString(), other.ToString());
		}
	}
}

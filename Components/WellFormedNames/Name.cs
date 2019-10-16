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
using Utilities;
using WellFormedNames.Exceptions;


namespace WellFormedNames
{
	/// <summary>
	/// Well Formed Name Class.
	/// </summary>
	/// <remarks>
	/// <para>
	///  A well formed name is used to specify goal/action names, objects, properties,
	/// constants, and relations.
	/// 
	/// Its syntax is based on first order logic symbols, variables and predicates.
	/// </para>
	/// <para>
	/// Names can be generated from strings, or from composition with other names.
	/// All names are case-insensitive.
	/// </para>
	/// Even though the Name is class type, its underlying behaviour is similar to a value type structure.
	/// This means that every modification to its values, returns a new instance of a Name object,
	/// preserving the state of the original one.
	/// </remarks>
	/// <example>
	/// By default, Names separated in the following categories:
	///		- Primitives
	///			- John
	///			- Dog
	///			- Blue
	///			- 34.5
	///		- Variables
	///			- [x]
	///			- [strength]
	///		- Composed Names
	///			- Color(Sky)
	///			- Likes(John)
	///			- Size(Ball)
	///			- Kick(Hard, Low)
	/// </example>
	[Serializable]
	public abstract partial class Name : IComparable<Name>, IEquatable<Name>
	{
		private const string NUMBER_VALIDATION_PATTERN = @"(?:-|\+)?\d+(?:\.\d+)?(?:e(?:-|\+)?[1-9]\d*)?";
		private const string VARIABLE_SYMBOL_VALIDATION_PATTERN = @"^\[([A-Za-z_][\w-]*)\]$";
		private const string VALUE_SYMBOL_VALIDATION_PATTERN = @"^(?:(?:[A-Za-z1-9_][\w-]*)|(?:" + NUMBER_VALIDATION_PATTERN + @"))$";
		private static readonly Regex VARIABLE_VALIDATION_PATTERN = new Regex(VARIABLE_SYMBOL_VALIDATION_PATTERN,RegexOptions.IgnoreCase);
		private static readonly Regex PRIMITIVE_VALIDATION_PATTERN = new Regex(VALUE_SYMBOL_VALIDATION_PATTERN,RegexOptions.IgnoreCase);

#region Constants

		/// <summary>
		/// The string representation of a <b>NIL</b> value Name.
		/// </summary>
		public const string NIL_STRING = "-";
		/// <summary>
		/// The string representation of the <b>"SELF"</b> primitive Name.
		/// </summary>
		public const string SELF_STRING = "SELF";
		/// <summary>
		/// The string representation of the Universal matching Name.
		/// </summary>
		public const string UNIVERSAL_STRING = "*";
		
		/// <summary>
		/// A constant containing an instance of a NIL Name
		/// </summary>
		/// @hideinitializer
		public static readonly Name NIL_SYMBOL = new PrimitiveSymbol((PrimitiveValue)null);
		/// <summary>
		/// A constant containing an instance of a SELF Name
		/// </summary>
		/// @hideinitializer
		public static readonly Name SELF_SYMBOL = new PrimitiveSymbol(SELF_STRING);
		/// <summary>
		/// A constant containing an instance of a Universal matching Name
		/// </summary>
		/// @hideinitializer
		public static readonly Name UNIVERSAL_SYMBOL = new UniversalSymbol();

#endregion

		/// <summary>
		/// Tells if this name is grounded.
		/// A grounded Name is one that do not contain variables.
		/// </summary>
		public readonly bool IsGrounded;

		/// <summary>
		/// Tells if this is name the Universal Matching Symbol
		/// </summary>
		public readonly bool IsUniversal;

		/// <summary>
		/// Tells if this name does not contain universal or variable Symbols
		/// </summary>
		public readonly bool IsConstant;

		/// <summary>
		/// Tells if this name is a variable definition
		/// </summary>
		public readonly bool IsVariable;

		/// <summary>
		/// Tells if this name is a primitive value
		/// </summary>
		public readonly bool IsPrimitive;

		/// <summary>
		/// Tells if this name is a composition of other names
		/// </summary>
		public readonly bool IsComposed;

		/// <summary>
		/// The number of terms that compose this name.
		/// Primitive and Variable Names will always return 1.
		/// </summary>
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

		/// @cond DOXYGEN_SHOULD_SKIP_THIS
		public sealed override bool Equals(object obj)
		{
			Name n = obj as Name;
			if (n == null)
				return false;

			return Equals(n);
		}

		public abstract override int GetHashCode();
		public abstract override string ToString();

		public abstract bool Equals(Name name);
		/// @endcond

		/// <summary>
		/// Returns the first term of this Name.
		/// Primitive and Variable Names will always return them selfs.
		/// </summary>
		public abstract Name GetFirstTerm();

		/// <summary>
		/// Return all terms contained inside this Name.
		/// </summary>
		public abstract IEnumerable<Name> GetTerms();

        /// <summary>
        /// Return the term at the specified index.
        /// - For Primitive or Variable Names, any index different from 0, will throw an IndexOutOfRangeException.
        /// - Using this method with a 0 index is the same as using GetFirstTerm()
        /// </summary>
        /// <param name="index">The zero-based index of the term to get.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the given index is out of bounds.</exception>

        public abstract Name GetNTerm(int index);

		/// <summary>
		/// Generates a sequence of all Names contained inside this Name.
		/// </summary>
		public abstract IEnumerable<Name> GetLiterals();

		/// <summary>
		/// Generates a sequence of all variables contained inside this Name.
		/// </summary>
		public abstract IEnumerable<Name> GetVariables();

		/// <summary>
		/// Tells if this name contains a Ghost variable
		/// </summary>
		/// <see cref="GenerateUniqueGhostVariable()"/>
		public abstract bool HasGhostVariable();

		/// <summary>
		/// Tells if this name contains a SELF primitive.
		/// </summary>
		public abstract bool HasSelf();

		/// <summary>
		/// Verifies if a specific variable is contained inside this Name.
		/// </summary>
		/// <param name="variable">The variable Name we want to verify</param>
		/// <exception cref="ArgumentException">Thrown if the given argument is not a variable definition.</exception>
		public bool ContainsVariable(Name variable)
		{
			if (!variable.IsVariable)
				throw new ArgumentException("The given Name is not a variable",nameof(variable));

			var v = (VariableSymbol) variable;

			return GetVariables().Cast<VariableSymbol>().Any(s => s.Equals(v));
		}

		/// <summary>
		/// Swaps every instance of the requested Name with another.
		/// </summary>
		/// <param name="original">The Name instance to swap from.</param>
		/// <param name="newName">The Name instance to swap to.</param>
		/// <returns>A new instance, which is a clone of this Name, but with every instance of the original Name swaped with the new one.</returns>
		public abstract Name SwapTerms(Name original, Name newName);

		/// <summary>
		/// Given a SubstitutionSet, tries to ground this Name by substituting every variable with the corresponding value.
		/// </summary>
		/// <param name="bindings">The SubstitutionSet to be used to ground this Name.</param>
		/// <returns>A new instance, which is a clone of this Name, but grounded as much as possible.</returns>
		/// <remarks>
		/// - If this instance is already grounded before calling this method, it will just return the same Name.
		/// - This method does not warrant that this Name will be fully grounded, as the given SubstitutionSet
		/// may not contain the substitution variables needed to perform the task.
		/// </remarks>
		public abstract Name MakeGround(SubstitutionSet bindings);

		/// <summary>
		/// Adds a tag to the end of every variable inside this Name,
		/// effectively modifying their identifier.
		/// </summary>
		/// <param name="id">The tag to add to every variable.</param>
		/// <returns>A new instance, which is a clone of this Name, but with every variable identifier changed in order to include the new tag.</returns>
		/// /// <remarks>
		/// - If this instance is already grounded before calling this method, it will just return the same Name.
		/// </remarks>
		public abstract Name ReplaceUnboundVariables(string id);

		/// <summary>
		/// Removes a tag from the end of every variable inside this Name,
		/// effectively modifying their identifier.
		/// </summary>
		/// <param name="id">The tag to remove from every variable.</param>
		/// <returns>A new instance, which is a clone of this Name, but with every variable identifier changed in order to exclude the requested tag.</returns>
		/// /// <remarks>
		/// - If this instance is already grounded before calling this method, it will just return the same Name.
		/// - The tag is only removed if, and only if, the variable identifier ends with the requested tag.
		/// </remarks>
		public abstract Name RemoveBoundedVariables(string id);

		/// <summary>
		/// Determines if this matches the given name template.
		/// Both Names are matched to each other if all their Symbols are equal to one another or if a Symbol matches a universal Symbol.
		/// </summary>
		/// <param name="name">The Name to match with this instance.</param>
		/// <returns>True if both Names match with each other, false otherwise.</returns>
		public abstract bool Match(Name name);

		/// <summary>
		/// Apply a transformation function to this Name.
		///  The function will receive every term of this name,
		/// and should return a name to be swapped with the old one.
		/// </summary>
		/// <param name="transformFunction">The function we want to apply to this Name.</param>
		/// <returns>A new Name instance, which is the original one with the transformed function applied.</returns>
		public abstract Name ApplyToTerms(Func<Name, Name> transformFunction);
		
		
		#region Operators

		/// <summary>
		/// Explicit cast from a string to a Name.
		/// Similar from calling Name.Build(string)
		/// </summary>
		public static explicit operator Name(string definition)
		{
			return BuildName(definition);
		}

		/// <summary>
		/// Name comparison operator.
		/// Tells if two names are equal to one another.
		/// </summary>
		public static bool operator ==(Name n1, Name n2)
		{
            if (ReferenceEquals(n1, n2))
				return true;

		    if ((object) n1 == null || (object) n2 == null)
		        return false;
            
            return n1.Equals(n2);
		}

		/// <summary>
		/// Name comparison operator.
		/// Tells if two names are diferent from one another.
		/// </summary>
		public static bool operator !=(Name n1, Name n2)
		{
			return !(n1 == n2);
		}

		public static bool operator <(Name a, Name b)
		{
			PrimitiveSymbol pa = a as PrimitiveSymbol;
			PrimitiveSymbol pb = b as PrimitiveSymbol;

			if (pa == null || pb == null)
				return false;

			int delta;
			if (PrimitiveSymbol.TryCompare(pa, pb, out delta))
				return delta < 0;
			return false;
		}

		public static bool operator <=(Name a, Name b)
		{
			PrimitiveSymbol pa = a as PrimitiveSymbol;
			PrimitiveSymbol pb = b as PrimitiveSymbol;

			if (pa == null || pb == null)
				return false;

			int delta;
			if (PrimitiveSymbol.TryCompare(pa, pb, out delta))
				return delta <= 0;
			return false;
		}

		public static bool operator >(Name a, Name b)
		{
			return !(a <= b);
		}

		public static bool operator >=(Name a, Name b)
		{
			return !(a < b);
		}

		#endregion

		#region Builders

		/// <summary>
		/// Creates a composed Name, using two or more Names
		/// </summary>
		/// <param name="rootTerm">The Name that will be root of the composed Name.</param>
		/// <param name="firstTerm">The first term of the composed Name.</param>
		/// <param name="otherTerms">The remaining terms of the composed Name.</param>
		/// <exception cref="ArgumentException">Thrown if the rootTerm is not a primitive Name.</exception>
		public static Name BuildName(Name rootTerm, Name firstTerm, params Name[] otherTerms)
		{
			return BuildName(otherTerms.Prepend(firstTerm).Prepend(rootTerm));
		}

		/// <summary>
		/// Creates a Name, using a sequence of Names.
		/// </summary>
		/// <param name="terms">The Name set used to generate the new one.</param>
		/// <exception cref="ArgumentException">Thrown if the first element of the set is not a primitive Name.</exception>
		public static Name BuildName(IEnumerable<Name> terms)
		{
			var set = ObjectPool<List<Name>>.GetObject();
			try
			{
				set.AddRange(terms);
				if (set.Count < 2)
					return set[0];

				Symbol head = set[0] as Symbol;
				if (head == null)
					throw new ArgumentException("The first term needs to be a Symbol object");

				set.RemoveAt(0);
				return new ComposedName(head,set.Select(n => n??NIL_SYMBOL).ToArray());
			}
			finally
			{
				set.Clear();
				ObjectPool<List<Name>>.Recycle(set);
			}
		}

		public static Name BuildName(object value)
		{
			if (value == null)
				return NIL_SYMBOL;

			var str = value as string;
			if (str != null)
			{
                if (string.IsNullOrEmpty(str))
                    throw new ArgumentException("Invalid symbol \"\"");

				str = str.Trim();
				return ParseName(str);
			}
			return new PrimitiveSymbol(PrimitiveValue.Cast(value));
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
			{
				PrimitiveValue p;
				if(PrimitiveValue.TryParse(str,out p))
					return new PrimitiveSymbol(p);
			}

			throw new ParsingException(str + " is not a well formed name definition");
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

		/// @cond DOXYGEN_SHOULD_SKIP_THIS
		
		public int CompareTo(Name other)
		{
			if (other == null)
				return 1;

			StringComparer c;

			c = StringComparer.InvariantCultureIgnoreCase;

			return c.Compare(ToString(), other.ToString());
		}

		/// @endcond

		public abstract bool TryConvertToValue<T>(out T value);

		public virtual object GetValue()
		{
			return null;
		}
	}
}

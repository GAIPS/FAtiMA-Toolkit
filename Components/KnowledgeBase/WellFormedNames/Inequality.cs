using System;
using System.Text.RegularExpressions;
using KnowledgeBase.WellFormedNames.Exceptions;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Represents a specific substitution condition in which a variable cannot have a specified value.
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	//public class Inequality : Substitution
	//{
	//	public const string INEQUALITY_VALIDATION_PATTERN = "^(" + Symbol.VARIABLE_SYMBOL_VALIDATION_PATTERN + @")(?:!=)(" + Symbol.SYMBOL_VALIDATION_PATTERN + ")$";
	//	private static readonly Regex VALIDATION_REGEX = new Regex(INEQUALITY_VALIDATION_PATTERN);

	//	/// <summary>
	//	/// Substitution Constructor
	//	/// </summary>
	//	/// <param name="variable">the variable to be replaced</param>
	//	/// <param name="value">the new value to apply in the place of the old variable</param>
	//	/// <exception cref="FAtiMA.Core.Exceptions.BadSubstitutionException">Thrown if the variable symbol is grounded (ie. is not a valid variable)</exception>
	//	public Inequality(Symbol variable, Name value) : base(variable,value){}

	//	/// <summary>
	//	/// Constructs a Substitution using a string definition
	//	/// </summary>
	//	/// <param name="substitutionDefinition"></param>
	//	/// <exception cref="FAtiMA.Core.Exceptions.BadSubstitutionException">Thrown if the given definition is not a valid substitution</exception>
	//	public Inequality(string substitutionDefinition)
	//	{
	//		Match m = VALIDATION_REGEX.Match(substitutionDefinition);
	//		if (!m.Success)
	//			throw new BadSubstitutionException("\""+substitutionDefinition+"\" is not a valid inequality definition");

	//		this.Variable = new Symbol(m.Groups[1].Value);
	//		this.Value = new Symbol(m.Groups[2].Value);
	//	}

	//	/// <summary>
	//	/// Clone Constructor
	//	/// 
	//	/// It can also be used to creates a new inequality condition from an existing Substitution
	//	/// (by negating the Substitution)
	//	/// </summary>
	//	/// <see cref="FAtiMA.Core.WellFormedNames.Substitution"/>
	//	/// <param name="substitution">The inequality to clone or a substitution to negate</param>
	//	public Inequality(Substitution substitution) : base()
	//	{
	//		this.Variable = (Symbol)substitution.Variable.Clone();
	//		this.Value = (Name)substitution.Value.Clone();
	//	}

	//	public override object Clone()
	//	{
	//		return new Inequality(this);
	//	}

	//	public override string ToString()
	//	{
	//		return string.Format("{0}!={1}", Variable.ToString(), Value.ToString());
	//	}
	//}
}

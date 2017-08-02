using System;
using WellFormedNames.Exceptions;


namespace WellFormedNames
{
	/// <summary>
	/// Represents a substitution of a variable Name for another Name object.
	/// </summary>
	/// <remarks>
	/// The variable can be substituted by any type of Name object, meaning that
	/// grounding a Name with this substitution will not guarantee a grounded Name.
	/// </remarks>
	[Serializable]
	public sealed class Substitution
	{
		private static readonly char[] SUBSTITUTION_SEPARATORS= {'/'};

		/// <summary>
		/// The Name variable to substitute.
		/// </summary>
		public Name Variable
		{
			get;
		}

		/// <summary>
		/// The Name value to substitute the variable with.
		/// </summary>
		public ComplexValue SubValue
		{
			get;
		}

		private void Validation(Name variable, ComplexValue value)
		{
			if (!variable.IsVariable)
				throw new BadSubstitutionException($"{variable} is not a valid variable definition.");

			if (value.Value.ContainsVariable(variable))
				throw new BadSubstitutionException($"The substitution {variable}->{value} will create a cyclical reference.");
		}

		/// <summary>
		/// Substitution Constructor
		/// </summary>
		/// <param name="variable">the variable to be replaced</param>
		/// <param name="value">the new value to apply in the place of the old variable</param>
		/// <exception cref="BadSubstitutionException">Thrown if the Name given for the variable, is not an actual variable.</exception>
		public Substitution(Name variable, ComplexValue value)
		{
			Validation(variable, value);

			this.Variable = variable;
			this.SubValue = value;
		}

		/// <summary>
		/// Constructs a Substitution using a string definition
		/// </summary>
		/// <param name="substitutionDefinition">The string to be parsed as a Substitution</param>
		/// <exception cref="BadSubstitutionException">Thrown if the given definition is not a valid substitution</exception>
		public Substitution(string substitutionDefinition)
		{
			string[] elem = substitutionDefinition.Split(SUBSTITUTION_SEPARATORS, 2);
			if(elem.Length != 2)
				throw new BadSubstitutionException("\"" + substitutionDefinition + "\" is not a valid substitution definition");

			try
			{
				var v = Name.BuildName(elem[0]);
				var n = Name.BuildName(elem[1]);
				Validation(v, new ComplexValue(n));
				this.Variable = v;
				this.SubValue = new ComplexValue(n);
			}
			catch (BadSubstitutionException)
			{
				throw;
			}
			catch (System.Exception e)
			{
				throw new BadSubstitutionException($"\"{substitutionDefinition}\" is not a valid substitution definition", e);
			}
		}

		/// <summary>
		/// Substitution Constructor
		/// </summary>
		/// <param name="variable">the variable to be replaced</param>
		/// <param name="value">the new value to apply in the place of the old variable</param>
		/// <exception cref="BadSubstitutionException">Thrown if the variable symbol is grounded (ie. is not a valid variable)</exception>
		public Substitution(string variable, string value)
		{
			var v = Name.BuildName(variable);
			var n = Name.BuildName(value);
			Validation(v, new ComplexValue(n));

			this.Variable = v;
			this.SubValue = new ComplexValue(n);
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="substitution">The substitution to clone</param>
		private Substitution(Substitution substitution)
		{
			this.Variable = substitution.Variable;
			this.SubValue = substitution.SubValue;
		}

		/// <summary>
		/// Adds a tag to the end of every variable inside this Substitution,
		/// effectively modifying their identifier.
		/// </summary>
		/// <param name="id">The tag to add to every variable.</param>
		/// <returns>A new instance, which is a clone of this Substitution, but with every variable identifier changed in order to include the new tag.</returns>
		public Substitution ReplaceUnboundVariables(string id)
		{
			return new Substitution(Variable.ReplaceUnboundVariables(id),new ComplexValue(SubValue.Value.ReplaceUnboundVariables(id)));
		}

		/// <summary>
		/// Removes a tag from the end of every variable inside this Substitution,
		/// effectively modifying their identifier.
		/// </summary>
		/// <param name="id">The tag to remove from every variable.</param>
		/// <returns>A new instance, which is a clone of this Substitution, but with every variable identifier changed in order to exclude the requested tag.</returns>
		/// /// <remarks>
		/// - The tag is only removed if, and only if, the variable identifier ends with the requested tag.
		/// </remarks>
		public Substitution RemoveBoundedVariables(string id)
		{
			return new Substitution(Variable.RemoveBoundedVariables(id), new ComplexValue(SubValue.Value.RemoveBoundedVariables(id)));
		}

		/// @cond DOXYGEN_SHOULD_SKIP_THIS
		
		public override bool Equals(object obj)
		{
			Substitution s = obj as Substitution;
			if (s == null)
				return false;

			return Variable.Equals(s.Variable) && SubValue.Equals(s.SubValue);
		}

		public override int GetHashCode()
		{
			return Variable.GetHashCode() ^ SubValue.Value.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Variable}/{SubValue}";
		}

		/// @endcond
	}
}

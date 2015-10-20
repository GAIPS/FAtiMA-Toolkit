using System;
using System.Text.RegularExpressions;

namespace WellFormedNames
{
	/// <summary>
	/// Represents a substitution of a variable for another variable or constant symbol
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	public class Substitution : ICloneable
	{
		private static readonly char[] SUBSTITUTION_SEPARATORS= {'/','\\'};

		public Symbol Variable
		{
			get;
			protected set;
		}

		public Name Value
		{
			get;
			protected set;
		}

		private void Validation(Symbol variable, Name value)
		{
			if (variable.IsGrounded)
				throw new BadSubstitutionException(string.Format("{0} is not a valid variable definition.", variable));

			if (value.ContainsVariable(variable))
				throw new BadSubstitutionException(string.Format("The substitution {0}->{1} while create a cyclical reference.", variable, value));
		}

		/// <summary>
		/// Base constructor for extended classes
		/// </summary>
		protected Substitution(){}

		/// <summary>
		/// Substitution Constructor
		/// </summary>
		/// <param name="variable">the variable to be replaced</param>
		/// <param name="value">the new value to apply in the place of the old variable</param>
		/// <exception cref="FAtiMA.Core.Exceptions.BadSubstitutionException">Thrown if the variable symbol is grounded (ie. is not a valid variable)</exception>
		public Substitution(Symbol variable, Name value)
		{
			Validation(variable, value);

			this.Variable = new Symbol(variable);
			this.Value = (Name)value.Clone();
		}

		/// <summary>
		/// Constructs a Substitution using a string definition
		/// </summary>
		/// <param name="substitutionDefinition"></param>
		/// <exception cref="FAtiMA.Core.Exceptions.BadSubstitutionException">Thrown if the given definition is not a valid substitution</exception>
		public Substitution(string substitutionDefinition)
		{
			string[] elem = substitutionDefinition.Split(SUBSTITUTION_SEPARATORS, 2);
			if(elem.Length != 2)
				throw new BadSubstitutionException("\"" + substitutionDefinition + "\" is not a valid substitution definition");

			try
			{
				var v = new Symbol(elem[0]);
				var n = Name.Parse(elem[1]);
				Validation(v, n);
				this.Variable = v;
				this.Value = n;
			}
			catch (BadSubstitutionException)
			{
				throw;
			}
			catch (System.Exception e)
			{
				throw new BadSubstitutionException("\"" + substitutionDefinition + "\" is not a valid substitution definition", e);
			}
			
		}

		/// <summary>
		/// Substitution Constructor
		/// </summary>
		/// <param name="variable">the variable to be replaced</param>
		/// <param name="value">the new value to apply in the place of the old variable</param>
		/// <exception cref="FAtiMA.Core.Exceptions.BadSubstitutionException">Thrown if the variable symbol is grounded (ie. is not a valid variable)</exception>
		public Substitution(string variable, string value)
		{
			var v = new Symbol(variable);
			var n = Name.Parse(value);
			Validation(v, n);

			this.Variable = v;
			this.Value = n;
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="substitution">The substitution to clone</param>
		public Substitution(Substitution substitution)
		{
			this.Variable = new Symbol(substitution.Variable);
			this.Value = (Name)substitution.Value.Clone();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			Substitution s = obj as Substitution;
			if (s == null)
				return false;

			bool b1 = this.Variable.Equals(s.Variable);
			bool b2 = this.Value.Equals(s.Value);
			return b1 && b2;
		}

		public override int GetHashCode()
		{
			int hash = this.Variable.GetHashCode();
			hash ^= this.Value.GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return string.Format("{0}/{1}", Variable.ToString(), Value.ToString());
		}

		public virtual object Clone()
		{
			return new Substitution(this);
		}
	}
}

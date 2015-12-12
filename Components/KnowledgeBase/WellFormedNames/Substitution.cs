using System;
using KnowledgeBase.Exceptions;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Represents a substitution of a variable for another variable or constant symbol
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	[Serializable]
	public class Substitution : IVariableRenamer<Substitution>, ICloneable
	{
		private static readonly char[] SUBSTITUTION_SEPARATORS= {'/'};

		public Name Variable
		{
			get;
			protected set;
		}

		public Name Value
		{
			get;
			protected set;
		}

		private void Validation(Name variable, Name value)
		{
			if (!variable.IsVariable)
				throw new BadSubstitutionException(string.Format("{0} is not a valid variable definition.", variable));

			if (value.ContainsVariable(variable))
				throw new BadSubstitutionException(string.Format("The substitution {0}->{1} will create a cyclical reference.", variable, value));
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
		public Substitution(Name variable, Name value)
		{
			Validation(variable, value);

			this.Variable = (Name)variable.Clone();
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
				var v = Name.BuildName(elem[0]);
				var n = Name.BuildName(elem[1]);
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
			var v = Name.BuildName(variable);
			var n = Name.BuildName(value);
			Validation(v, n);

			this.Variable = v;
			this.Value = n;
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="substitution">The substitution to clone</param>
		protected Substitution(Substitution substitution)
		{
			this.Variable = (Name)substitution.Variable.Clone();
			this.Value = (Name)substitution.Value.Clone();
		}

		public override bool Equals(object obj)
		{
			Substitution s = obj as Substitution;
			if (s == null)
				return false;

			return Variable.Equals(s.Variable) && Value.Equals(s.Value);
		}

		public override int GetHashCode()
		{
			return Variable.GetHashCode() ^ Value.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}/{1}", Variable.ToString(), Value.ToString());
		}

		public virtual object Clone()
		{
			return new Substitution(this);
		}

		public Substitution ReplaceUnboundVariables(string id)
		{
			return new Substitution(Variable.ReplaceUnboundVariables(id),Value.ReplaceUnboundVariables(id));
		}


		public Substitution RemoveBoundedVariables(string id)
		{
			return new Substitution(Variable.RemoveBoundedVariables(id), Value.RemoveBoundedVariables(id));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WellFormedNames;
using WellFormedNames.Exceptions;
using IQueryable = WellFormedNames.IQueryable;

namespace Conditions
{
	[Serializable]
	public abstract partial class Condition : IConditionEvaluator
	{
		//public Guid Id { get; set; }

        private const string WFN_CHARACTERS = @"\w\s-\+\(\)\.\,\[\]\*";
		private const string VALID_OPERATORS = @"=|!=|<|<=|>|>=";
		private const string REGEX_PATTERN = @"^\s*(#)?(["+WFN_CHARACTERS+@"]+)\s*("+VALID_OPERATORS+@")\s*(#)?(["+WFN_CHARACTERS+@"]+)\s*$";
		private static readonly Regex REGEX_PARSER = new Regex(REGEX_PATTERN,RegexOptions.Singleline);

		//private Condition()
		//{
		//    this.Id = Guid.NewGuid();
		//}

        public IEnumerable<SubstitutionSet> UnifyEvaluate(IQueryable kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null || !constraints.Any())
				constraints = new[] { new SubstitutionSet() };

			return CheckActivation(kb, perspective, constraints).Distinct();
		}

		public bool Evaluate(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			return UnifyEvaluate(db,perspective, constraints).Any();
		}

		protected abstract IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints);

		public abstract override string ToString();

		public abstract override bool Equals(object obj);

		public abstract override int GetHashCode();

		private static bool CompareValues(PrimitiveValue a, PrimitiveValue b, ComparisonOperator op)
		{
			switch (op)
			{
				case ComparisonOperator.Equal:
					return a == b;
				case ComparisonOperator.NotEqual:
					return a != b;
				case ComparisonOperator.LessThan:
					return a < b;
				case ComparisonOperator.LessOrEqualThan:
					return a <= b;
				case ComparisonOperator.GreatherThan:
					return a > b;
				case ComparisonOperator.GreatherOrEqualThan:
					return a >= b;
				default:
					throw new ArgumentOutOfRangeException(nameof(op), op, null);
			}
		}

		private static string OperatorRepresentation(ComparisonOperator op)
		{
			switch (op)
			{
				case ComparisonOperator.Equal:
					return "=";
				case ComparisonOperator.NotEqual:
					return "!=";
				case ComparisonOperator.LessThan:
					return "<";
				case ComparisonOperator.LessOrEqualThan:
					return "<=";
				case ComparisonOperator.GreatherThan:
					return ">";
				case ComparisonOperator.GreatherOrEqualThan:
					return ">=";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static Condition Parse(string str)
		{
			var m = REGEX_PARSER.Match(str);
			if (!m.Success)
				throw new ParsingException($"Unable to parse \"{str}\" as a condition");

			string mod1 = m.Groups[1].Value;
			string str1 = m.Groups[2].Value;
			string op = m.Groups[3].Value;
			string mod2 = m.Groups[4].Value;
			string str2 = m.Groups[5].Value;

			Name v1 = Name.BuildName(str1);
			Name v2 = Name.BuildName(str2);
			ComparisonOperator ope = ComparisonOperator.NotEqual;
			switch (op)
			{
				case "=":
					ope = ComparisonOperator.Equal;
					break;
				case "!=":
					ope = ComparisonOperator.NotEqual;
					break;
				case "<":
					ope = ComparisonOperator.LessThan;
					break;
				case "<=":
					ope = ComparisonOperator.LessOrEqualThan;
					break;
				case ">":
					ope = ComparisonOperator.GreatherThan;
					break;
				case ">=":
					ope = ComparisonOperator.GreatherOrEqualThan;
					break;
			}

			return internal_buildCondition(mod1, v1, mod2, v2, ope);
		}

		public static Condition BuildCondition(Name v1, Name v2, ComparisonOperator op)
		{
			return internal_buildCondition(null, v1, null, v2, ComparisonOperator.Equal);
		}

		private static Condition internal_buildCondition(string modifier1, Name v1, string modifier2, Name v2, ComparisonOperator op)
		{
			IValueRetriver value1 = ConvertToValueRetriever(modifier1, v1);
			IValueRetriver value2 = ConvertToValueRetriever(modifier2, v2);

			if (value1.Equals(value2))
				throw new InvalidOperationException("Both given property names are intrinsically equal. Condition would always return a constant result.");

			if (value1.InnerName.IsPrimitive && value2.InnerName.IsPrimitive && !value1.HasModifier && !value2.HasModifier)
				throw new InvalidOperationException("Both given property names are primitive values. Expected at least one non-primitive value.");

			if (op == ComparisonOperator.Equal)
			{
				//May be a definition
				if (value1.InnerName.IsVariable && !value1.HasModifier)
					return new EqualityDefinitionCondition(v1, value2);

				if (value2.InnerName.IsVariable && !value2.HasModifier)
					return new EqualityDefinitionCondition(v2, value1);
			}

			if (value1.InnerName.IsPrimitive != value2.InnerName.IsPrimitive)
			{
				IValueRetriver prop = value1.InnerName.IsPrimitive ? value2 : value1;
				PrimitiveValue value = (value1.InnerName.IsPrimitive ? value1.InnerName : value2.InnerName).GetPrimitiveValue();
				op = v1.IsPrimitive ? op.Mirror() : op;

				if (value.TypeCode == TypeCode.Boolean)
				{
					switch (op)
					{
						case ComparisonOperator.Equal:
							return new PredicateCondition(prop, value);
						case ComparisonOperator.NotEqual:
							return new PredicateCondition(prop, !value);
					}
				}

				return new PrimitiveComparisonCondition(prop, value, op);
			}

			return new PropertyComparisonCondition(value1,value2, op);
		}

		private static IValueRetriver ConvertToValueRetriever(string modifier, Name name)
		{
			if(string.IsNullOrEmpty(modifier))
				return new PropertyValueRetriver(name);

			if (modifier == "#")
			{
				if (name.IsVariable)
					return new CountValueRetriver(name);

				throw new ParsingException("Count modifier (#) only accepts variables");
			}

			throw new ParsingException("Unrecognized modifier " + modifier);
		}

		#region Operators

		public static bool operator ==(Condition c1, Condition c2)
		{
			if (object.ReferenceEquals(c1, c2))
				return true;
			return c1?.Equals(c2) ?? c2.Equals(null);
		}

		public static bool operator !=(Condition c1, Condition c2)
		{
			return !(c1 == c2);
		}

		#endregion
	}
}
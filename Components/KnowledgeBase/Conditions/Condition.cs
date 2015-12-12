using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public abstract partial class Condition : IConditionEvaluator
	{
		private const string REGEX_PATTERN = @"^\s*([\w-\(\)\.\,\[\]]+)\s*(=|!=|<|<=|>|>=)\s*([\w-\(\)\.\,\[\]]+)\s*$";
		private static readonly Regex REGEX_PARSER = new Regex(REGEX_PATTERN,RegexOptions.Singleline);

		private Condition()
		{
		}

		public abstract IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints);

		public bool Evaluate(KB kb, SubstitutionSet constraints)
		{
			return UnifyEvaluate(kb, constraints).Any();
		}

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
					throw new ArgumentOutOfRangeException("op", op, null);
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
				throw new ParsingException(@"Unable to parse ""{0}"" as a condition", str);

			string str1 = m.Groups[1].Value;
			string op = m.Groups[2].Value;
			string str2 = m.Groups[3].Value;

			Name v1 = Name.BuildName(str1);
			Name v2 = Name.BuildName(str2);
			ComparisonOperator ope;
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
				default:
					throw new ParsingException(@"Invalid comparison operator ""{0}"".", op);
			}

			return BuildCondition(v1,v2,ope);
		}

		public static Condition BuildCondition(Name v1, Name v2, ComparisonOperator op)
		{
			if (v1.IsPrimitive && v2.IsPrimitive)
				throw new InvalidOperationException("Both given property names are primitive values. Expected at least one non-primitive value.");

			if (v1 == v2)
				throw new InvalidOperationException("Both given property names are intrinsically equal. Condition would always return a constant result.");

			if (v1.IsPrimitive != v2.IsPrimitive)
			{
				Name prop = v1.IsPrimitive ? v2 : v1;
				PrimitiveValue value = (v1.IsPrimitive ? v1 : v2).GetPrimitiveValue();
				op = v1.IsPrimitive ? op.Mirror() : op;

				if (value.GetTypeCode() == TypeCode.Boolean)
				{
					switch (op)
					{
						case ComparisonOperator.Equal:
							return new PredicateCondition(prop, value);	
						case ComparisonOperator.NotEqual:
							return new PredicateCondition(prop, !value);	
					}
				}

				return new PrimitiveComparisonCondition(prop,value,op);
			}

			return new PropertyComparisonCondition(v1, v2, op);
		}
	}
}
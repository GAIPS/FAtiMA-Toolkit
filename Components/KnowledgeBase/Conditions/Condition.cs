using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase.Conditions
{
	[Serializable]
	public sealed class Condition : ICondition
	{
		private const string REGEX_PATTERN = @"^\s*([\w-\(\)\.\,\[\]]+)\s*(=|!=|<|<=|>|>=)\s*([\w-\(\)\.\,\[\]]+)\s*$";
		private static readonly Regex REGEX_PARSER = new Regex(REGEX_PATTERN,RegexOptions.Singleline);

		public readonly ComparisonOperator Operator;
		public readonly Name Property1;
		public readonly Name Property2;

		public Condition(Name property1, Name property2, ComparisonOperator op)
		{
			if (property1.IsPrimitive && property2.IsPrimitive)
				throw new InvalidOperationException("Both given property names are primitive values. Expected at least one non-primitive value.");

			if(property1==property2)
				throw new InvalidOperationException("Both given property names are intrinsically equal. Condition would always return a constant result.");

			Property1 = property1;
			Property2 = property2;
			Operator = op;
		}

		public IEnumerable<SubstitutionSet> UnifyEvaluate(KB kb, SubstitutionSet constraints)
		{
			if(constraints==null)
				constraints = new SubstitutionSet();

			Name p1 = Property1;
			if (!p1.IsGrounded)
				p1 = p1.MakeGround(constraints);

			Name p2 = Property2;
			if (!p2.IsGrounded)
				p2 = p2.MakeGround(constraints);

			if (p1.IsGrounded == p2.IsGrounded)
			{
				if (p1.IsGrounded)
				{
					var v1 = kb.AskProperty(p1)??p1.ToString();
					var v2 = kb.AskProperty(p2)??p2.ToString();
					if (CompareValues(v1, v2,Operator))
						yield return constraints;
				}
				else
				{
					foreach (var pair in kb.AskPossibleValues(p1, constraints))
					{
						foreach (var crossPair in kb.AskPossibleValues(p2, pair.Item2))
						{
							if (CompareValues(pair.Item1, crossPair.Item1,Operator))
								yield return crossPair.Item2;
						}
					}	
				}
			}
			else
			{
				Name ungrounded = p1.IsGrounded ? p2 : p1;
				Name grounded = p1.IsGrounded ? p1 : p2;
				ComparisonOperator op = p1.IsGrounded ? Operator : Operator.Mirror();

				var value = kb.AskProperty(grounded)??grounded.ToString();
				foreach (var pair in kb.AskPossibleValues(ungrounded, constraints))
				{
					if (CompareValues(value, pair.Item1,op))
						yield return pair.Item2;
				}
			}
		}

		public bool Evaluate(KB kb, SubstitutionSet constraints)
		{
			return UnifyEvaluate(kb, constraints).Any();
		}

		public override string ToString()
		{
			string op;
			switch (Operator)
			{
				case ComparisonOperator.Equal:
					op = "=";
					break;
				case ComparisonOperator.NotEqual:
					op = "!=";
					break;
				case ComparisonOperator.LessThan:
					op = "<";
					break;
				case ComparisonOperator.LessOrEqualThan:
					op = "<=";
					break;
				case ComparisonOperator.GreatherThan:
					op = ">";
					break;
				case ComparisonOperator.GreatherOrEqualThan:
					op = ">=";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return string.Format("{0} {1} {2}", Property1, op, Property2);
		}

		public static Condition Parse(string str)
		{
			var m = REGEX_PARSER.Match(str);
			if (!m.Success)
				throw new ParsingException(@"Unable to parse ""{0}"" as a condition", str);

			string str1 = m.Groups[1].Value;
			string op = m.Groups[2].Value;
			string str2 = m.Groups[3].Value;

			Name v1 = Name.Parse(str1);
			Name v2 = Name.Parse(str2);
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

			return new Condition(v1, v2, ope);
		}

		public override bool Equals(object obj)
		{
			Condition c = obj as Condition;
			if(c==null)
				return false;

			Name p1 = c.Property1;
			Name p2 = c.Property2;
			var op = c.Operator;

			if (Operator != op)
			{
				op = op.Mirror();
				if (Operator != op)
					return false;
				var s = p1;
				p1 = p2;
				p2 = s;
			}

			switch (op)
			{
				case ComparisonOperator.Equal:
				case ComparisonOperator.NotEqual:
					return (Property1 == p1 && Property2 == p2) || (Property1 == p2 && Property2 == p1);
			}
			return Property1 == p1 && Property2 == p2;
		}

		public override int GetHashCode()
		{
			Name p1 = Property1;
			Name p2 = Property2;
			var op = Operator;
			switch (op)
			{
				case ComparisonOperator.GreatherThan:
				case ComparisonOperator.GreatherOrEqualThan:
					op = op.Mirror();
					var s = p1;
					p1 = p2;
					p2 = s;
					break;
			}

			var c = op.GetHashCode();
			return p1.GetHashCode() ^ ~p2.GetHashCode() ^ c;
		}

		#region Helpers

		private static bool CompareValues(object a, object b, ComparisonOperator op)
		{
			var ta = Type.GetTypeCode(a.GetType());
			var tb = Type.GetTypeCode(b.GetType());

			if (!ta.IsPrimitiveData() && !tb.IsPrimitiveData())
				return false;

			var na = ta.IsNumeric();
			var nb = tb.IsNumeric();
			if (na && nb)
			{
				var comparisonType = GetBestNumberTypeComparison(ta, tb);
				if (comparisonType != TypeCode.Empty)
				{
					a = Convert.ChangeType(a, comparisonType);
					b = Convert.ChangeType(b, comparisonType);
				}

				int c = ((IComparable) a).CompareTo(b);
				switch (op)
				{
					case ComparisonOperator.Equal:
						return c == 0;
					case ComparisonOperator.NotEqual:
						return c != 0;
					case ComparisonOperator.LessThan:
						return c < 0;
					case ComparisonOperator.LessOrEqualThan:
						return c <= 0;
					case ComparisonOperator.GreatherThan:
						return c > 0;
					case ComparisonOperator.GreatherOrEqualThan:
						return c >= 0;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			if (na || nb)
				return false;

			if (ta != tb)
				return false;

			bool equalCompare;
			switch (ta)
			{
				case TypeCode.Boolean:
					equalCompare = ((bool)a) == ((bool)b);
					break;
				case TypeCode.String:
					equalCompare = string.Equals((string)a, (string)b, StringComparison.InvariantCultureIgnoreCase);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			switch (op)
			{
				case ComparisonOperator.Equal:
					return equalCompare;
				case ComparisonOperator.NotEqual:
					return !equalCompare;
			}

			return false;
		}

		private static TypeCode GetBestNumberTypeComparison(TypeCode a, TypeCode b)
		{
			if (a == b)
				return TypeCode.Empty;

			var ua = a.IsUnsignedNumeric();
			var ub = b.IsUnsignedNumeric();

			if (ua == ub)
				return (TypeCode) Math.Max((int) a, (int) b);

			var unsignedCode = ua ? a : b;
			var signedCode = ua ? b : a;

			if (signedCode > unsignedCode)
				return signedCode;

			switch (unsignedCode)
			{
				case TypeCode.Byte:
					return TypeCode.Int16;
				case TypeCode.UInt16:
					return TypeCode.Int32;
				case TypeCode.UInt32:
					return TypeCode.Int64;
			}
			return TypeCode.Double;
		}

		#endregion
	}
}
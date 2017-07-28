using System.Collections.Generic;
using System.Linq;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;

namespace Conditions
{
	public partial class Condition
	{
		private sealed class PropertyComparisonCondition : Condition
		{
			private readonly ComparisonOperator Operator;
			private readonly IValueRetriever Property1;
			private readonly IValueRetriever Property2;

			public PropertyComparisonCondition(IValueRetriever property1, IValueRetriever property2, ComparisonOperator op)
			{
				Property1 = property1;
				Property2 = property2;
				Operator = op;
			}

			protected override IEnumerable<SubstitutionSet> CheckActivation(IQueryable db, Name perspective, IEnumerable<SubstitutionSet> constraints)
			{
				var r1 = Property1.Retrieve(db,perspective, constraints).GroupBy(p => p.Item1, p => p.Item2);
				foreach (var g in r1)
				{
					foreach (var crossPair in Property2.Retrieve(db,perspective, g))
					{
						if (CompareValues(g.Key.Value, crossPair.Item1.Value, Operator))
							yield return crossPair.Item2;
					}
				}
			}

			public override bool Equals(object obj)
			{
				PropertyComparisonCondition c = obj as PropertyComparisonCondition;
				if (c == null)
					return false;

				var p1 = c.Property1;
				var p2 = c.Property2;
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

				var result = Property1.Equals(p1) && Property2.Equals(p2);
				switch (op)
				{
					case ComparisonOperator.Equal:
					case ComparisonOperator.NotEqual:
						return result || (Property1.Equals(p2) && Property2.Equals(p1));
				}
				return result;
			}

			public override int GetHashCode()
			{
				var p1 = Property1;
				var p2 = Property2;
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

			public override string ToString()
			{
				return $"{Property1} {OperatorRepresentation(Operator)} {Property2}";
			}
		}
	}
}
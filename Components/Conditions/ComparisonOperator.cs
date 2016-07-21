namespace Conditions
{
	public enum ComparisonOperator : byte
	{
		Equal = 7,				//111
		NotEqual = 0,			//000
		LessThan = 2,			//010
		LessOrEqualThan = 3,	//011
		GreatherThan = 4,		//100
		GreatherOrEqualThan = 5	//101
	}

	public static class ComparisonOperatorHelpers
	{
		public static ComparisonOperator Invert(this ComparisonOperator op)
		{
			return ~op;
		}

		public static ComparisonOperator Mirror(this ComparisonOperator op)
		{
			switch (op)
			{
				case ComparisonOperator.LessThan:
					return ComparisonOperator.GreatherThan;
				case ComparisonOperator.LessOrEqualThan:
					return ComparisonOperator.GreatherOrEqualThan;
				case ComparisonOperator.GreatherThan:
					return ComparisonOperator.LessThan;
				case ComparisonOperator.GreatherOrEqualThan:
					return ComparisonOperator.LessOrEqualThan;
			}
			return op;
		}
	}
}
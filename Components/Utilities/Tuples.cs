namespace Utilities
{
	public static class Tuples
	{
		public static Pair<T1, T2> Create<T1, T2>(T1 value1, T2 value2)
		{
			return new Pair<T1, T2>(value1, value2);
		}

		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
		{
			return new Tuple<T1,T2,T3>(value1, value2, value3);
		}
	}

	public class Pair<T1, T2>
	{
		public Pair(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
	}

	public class Tuple<T1, T2, T3>
	{
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
		}

		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
	}
}
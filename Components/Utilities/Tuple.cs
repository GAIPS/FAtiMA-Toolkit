namespace Utilities
{
	public static class Tuple
	{
		public static Pair<T1, T2> Create<T1, T2>(T1 value1, T2 value2)
		{
			return new Pair<T1, T2>(value1, value2);
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
}
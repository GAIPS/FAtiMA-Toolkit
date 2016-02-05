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

		public override string ToString()
		{
			return string.Format("{{{0}, {1}}}", Item1, Item2);
		}

		public override int GetHashCode()
		{
			return Item1.GetHashCode() ^ Item2.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var p = obj as Pair<T1, T2>;
			if (p == null)
				return false;

			return Item1.Equals(p.Item1) && Item2.Equals(p.Item2);
		}
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

		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}",Item1,Item2,Item3);
		}

		public override int GetHashCode()
		{
			return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2, T3>;
			if (t == null)
				return false;

			return Item1.Equals(t.Item1) && Item2.Equals(t.Item2) && Item3.Equals(t.Item3);
		}
	}
}
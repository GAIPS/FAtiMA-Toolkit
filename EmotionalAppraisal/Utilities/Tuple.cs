using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Utilities
{
	public static class Tuple
	{
		public static Pair<T1, T2> Create<T1, T2>(T1 value1, T2 value2)
		{
			return new Pair<T1, T2>(value1, value2);
		}
	}

	public class Pair<T1,T2>
	{
		public T1 value1 { get; set; }
		public T2 value2 { get; set; }

		public Pair(T1 value1, T2 value2)
		{
			this.value1 = value1;
			this.value2 = value2;
		}
	}
}

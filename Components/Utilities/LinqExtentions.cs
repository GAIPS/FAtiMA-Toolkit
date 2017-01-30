using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
	public static class LinqExtentions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T element)
		{
			foreach (var e in enumerable)
				yield return e;
			yield return element;
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T element)
		{
			yield return element;
			foreach (var e in enumerable)
				yield return e;
		}

#if !PORTABLE

		public static IEnumerable<R> Zip<T1,T2,R>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, R> zipper)
		{
			if (first == null)
				throw new ArgumentNullException(nameof(first));

			if(second == null)
				throw new ArgumentNullException(nameof(second));

			if(zipper == null)
				throw new ArgumentNullException(nameof(zipper));

			using (var it1 = first.GetEnumerator())
			{
				using (var it2 = second.GetEnumerator())
				{
					while (it1.MoveNext() && it2.MoveNext())
					{
						yield return zipper(it1.Current, it2.Current);
					}
				}
			}
		}
		
#endif

		public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
		{
			return !enumerable.GetEnumerator().MoveNext();
		}

		public static T MaxValue<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
		{
			T maxValue = default(T);
			float max = float.NegativeInfinity;
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
				{
					float v = selector(it.Current);
					if(v>max)
					{
						maxValue=it.Current;
						max = v;
					}
				}
			}
			return maxValue;
		}

		public static T MinValue<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
		{
			T maxValue = default(T);
			float min = float.PositiveInfinity;
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
				{
					float v = selector(it.Current);
					if(v<min)
					{
						maxValue=it.Current;
						min = v;
					}
				}
			}
			return maxValue;
		}

		public static string AggregateToString<T>(this IEnumerable<T> enumerable, string separator, string startBraquet, string endBraquet)
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();

			builder.Append(startBraquet);
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				bool first = true;
				while (it.MoveNext())
				{
					if (!first)
						builder.Append(separator);
					first = false;

					builder.Append(it.Current);
				}
			}
			builder.Append(endBraquet);

			string result = builder.ToString();
			builder.Length = 0;
			ObjectPool<StringBuilder>.Recycle(builder);
			return result;
		}

		public static string AggregateToString<T>(this IEnumerable<T> enumerable, string separator)
		{
			return AggregateToString(enumerable, separator, string.Empty, string.Empty);
		}

		public static string AggregateToString<T>(this IEnumerable<T> enumerable)
		{
			return AggregateToString(enumerable, ", ", string.Empty, string.Empty);
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
		{
			return Shuffle(enumerable, new Random());
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, Random random)
		{
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
			if (random == null) throw new ArgumentNullException(nameof(random));

			return internal_shuffle(enumerable, random);
		}

		private static IEnumerable<T> internal_shuffle<T>(IEnumerable<T> enumerable, Random random)
		{
			var buffer = enumerable.ToArray();
			for (int i = 0; i < buffer.Length; i++)
			{
				int j = random.Next(i, buffer.Length);
				yield return buffer[j];
				buffer[j] = buffer[i];
			}
		}

		public static IEnumerable<T> Sort<T>(this IEnumerable<T> enumerable, Func<T, T, int> sortFunction)
		{
			var a = enumerable.ToArray();
			Array.Sort(a, new LambaComparer<T>(sortFunction));
			return a;
		}

		private sealed class LambaComparer<T> : IComparer<T>
		{
			private Func<T, T, int> _lambda;

			public LambaComparer(Func<T, T, int> lambda)
			{
				_lambda = lambda;
			}

			public int Compare(T x, T y)
			{
				return _lambda(x, y);
			}
		}
	}
}

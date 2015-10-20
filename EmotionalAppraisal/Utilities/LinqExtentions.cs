using System;
using System.Text;
using System.Collections.Generic;

namespace Utilities
{
	public static class LinqExtentions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
				{
					action(it.Current);
				}
			}
		}

		public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T element)
		{
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
					yield return it.Current;
			}
			yield return element;
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T element)
		{
			yield return element;
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
					yield return it.Current;
			}
		}

		public static IEnumerable<R> Zip<T1,T2,R>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, R> zipper)
		{
			if (first == null)
				throw new ArgumentNullException("first");

			if(second == null)
				throw new ArgumentNullException("second");

			if(zipper == null)
				throw new ArgumentNullException("zipper");

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

		public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerable) where T : ICloneable
		{
			using (IEnumerator<T> it = enumerable.GetEnumerator())
			{
				while (it.MoveNext())
					yield return (T)it.Current.Clone();
			}
		}

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

		public static IEnumerable<T> ToSequence<T>(params T[] sequence)
		{
			for (int i = 0; i < sequence.Length; i++)
				yield return sequence[i];
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
					if (first)
						first = false;
					else
						builder.Append(separator);

					builder.Append(it.Current.ToString());
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
	}
}

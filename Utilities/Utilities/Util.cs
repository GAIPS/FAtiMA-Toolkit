using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
	public static class Util
	{
        public static void Swap<T>(ref T v1, ref T v2)
		{
			T aux = v1;
			v1 = v2;
			v2 = aux;
		}

        //Extension Method
        public static bool EqualsIgnoreCase(this string str, string str2)
        {
            return StringComparer.OrdinalIgnoreCase.Equals(str, str2);
        }

        //Extension Method
        public static string RemoveWhiteSpace(this string str)
        {
            return new string(str.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
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
                    if (v > max)
                    {
                        maxValue = it.Current;
                        max = v;
                    }
                }
            }
            return maxValue;
        }

        public static T MinValue<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
        {
            T minValue = default(T);
            float min = float.PositiveInfinity;
            using (IEnumerator<T> it = enumerable.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    float v = selector(it.Current);
                    if (v < min)
                    {
                        minValue = it.Current;
                        min = v;
                    }
                }
            }
            return minValue;
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

    }
}
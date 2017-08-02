using System;
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
    }
}
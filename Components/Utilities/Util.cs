using System;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;

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

        /*public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }*/

    }
}
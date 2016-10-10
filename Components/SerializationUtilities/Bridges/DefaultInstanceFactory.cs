using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SerializationUtilities
{
	internal class DefaultInstanceFactory : IInstanceFactory
	{
		public object CreateUninitialized(Type type)
		{
#if PORTABLE
			var c = type.GetConstructors(true).FirstOrDefault(c2 => c2.GetParameters().Length == 0);
			if(c == null)
				throw new Exception("Portable version of the serializer requires a default constructor (can be private)");

			return c.Invoke(new object[0]);
#else
			return FormatterServices.GetSafeUninitializedObject(type);
#endif
		}
	}
}
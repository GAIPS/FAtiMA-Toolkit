using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SerializationUtilities
{
	internal class DefaultInstanceFactory : IInstanceFactory
	{
		public object CreateUninitialized(Type type)
		{
			return FormatterServices.GetSafeUninitializedObject(type);
		}
	}
}
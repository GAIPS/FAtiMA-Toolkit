using System;

namespace SerializationUtilities
{
	public interface IInstanceFactory
	{
		object CreateUninitialized(Type type);
	}
}
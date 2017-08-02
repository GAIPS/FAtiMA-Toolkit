using System;
using System.IO;

namespace SerializationUtilities
{
	public interface ISerializer
	{
		void Serialize(Stream serializationStream, object graph);
		T Deserialize<T>(Stream serializationStream);
		object Deserialize(Stream serializationStream, Type returnType = null);
	}
}
using GAIPS.Serialization.SerializationGraph;
using System;

namespace GAIPS.Serialization
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj, IObjectGraphNode holder);

		void SetObjectData(ref object obj, IObjectGraphNode node);
	}
}
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj, IObjectGraphNode holder);

		void SetObjectData(ref object obj, IObjectGraphNode node);
	}
}
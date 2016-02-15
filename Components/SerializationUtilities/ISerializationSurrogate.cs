using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj, IObjectGraphNode holder);

		void SetObjectData(ref object obj, IObjectGraphNode node);
	}
}
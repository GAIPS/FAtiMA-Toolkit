using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj, ObjectGraphNode holder, Graph graph);

		void SetObjectData(ref object obj, ObjectGraphNode node, Graph graph);
	}
}
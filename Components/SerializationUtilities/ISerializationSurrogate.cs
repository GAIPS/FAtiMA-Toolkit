namespace GAIPS.Serialization
{
	public interface ISerializationSurrogate
	{
		void GetObjectData(object obj, ObjectGraphNode holder, SerializationGraph graph);

		void SetObjectData(ref object obj, ObjectGraphNode node, SerializationGraph graph);
	}
}
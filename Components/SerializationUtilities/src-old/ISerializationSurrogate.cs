public interface ISerializationSurrogate
{
	void GetObjectData(object obj, ObjectGraphNode holder, SerializedGraph graph);
	
	void SetObjectData(ref object obj, ObjectGraphNode node, SerializedGraph graph);
}
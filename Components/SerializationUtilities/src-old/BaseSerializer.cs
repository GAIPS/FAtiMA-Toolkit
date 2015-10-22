public abstract class BaseSerializer : ISerializer
{
	protected abstract void SerializeGraph(System.IO.Stream stream, SerializedGraph graph);
	protected abstract SerializedGraph DeserializeGraph(System.IO.Stream stream);
	
	#region Serialization
	
	public void Serialize (System.IO.Stream stream, object graph)
	{
		SerializedGraph serializedData = new SerializedGraph(graph);
		SerializeGraph(stream,serializedData);
	}
	
	#endregion
	
	#region Deserialize
	
	public object Deserialize (System.IO.Stream stream)
	{
		SerializedGraph serializedData = DeserializeGraph(stream);
		
		if(serializedData.Root==null)
			return null;
		
		return serializedData.Root.RebuildObject(serializedData,null);
	}
	
	#endregion
}
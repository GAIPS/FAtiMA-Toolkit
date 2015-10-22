using System;

public sealed class EnumGraphNode : SerializationGraphNode{
	public Enum e;
	public EnumGraphNode(Enum e){
		this.e = e;
	}
	
	public object RebuildObject (SerializedGraph graph, Type knowType)
	{
		return e;
	}
}
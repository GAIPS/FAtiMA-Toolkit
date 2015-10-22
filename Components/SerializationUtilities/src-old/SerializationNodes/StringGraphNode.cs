using System;

public sealed class StringGraphNode : SerializationGraphNode{
	public string Value;
	public StringGraphNode(string value){
		this.Value = value;
	}
	
	public object RebuildObject (SerializedGraph graph, Type knowType)
	{
		return Value;
	}
}
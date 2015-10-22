using System;

public interface SerializationGraphNode{
	
	object RebuildObject(SerializedGraph graph, Type knowType);
}

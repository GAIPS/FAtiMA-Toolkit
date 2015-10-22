using System;
using System.Collections;
using System.Collections.Generic;

public class ListSerializationSurrogate : ISerializationSurrogate
{
	#region ISerializationSurrogate implementation
	
	public void GetObjectData(object obj, ObjectGraphNode holder, SerializedGraph graph)
	{
		IList e = (IList)obj;
		Type elemType = typeof(object);
		Type objType = e.GetType();
		
		if(objType.IsGenericType)
			elemType = objType.GetGenericArguments()[0];
		
		ArrayGraphNode array = new ArrayGraphNode();
		holder["elements"] = array;
		IEnumerator it = e.GetEnumerator();
		while(it.MoveNext())
		{
			array.Add(graph.BuildNode(it.Current,elemType,holder));
		}
	}
	
	public void SetObjectData (ref object obj, ObjectGraphNode node, SerializedGraph graph)
	{
		IList e = (IList)obj;
		Type elemType = typeof(object);
		Type objType = e.GetType();
		
		if(objType.IsGenericType)
			elemType = objType.GetGenericArguments()[0];
		
		ArrayGraphNode elements = node["elements"] as ArrayGraphNode;
		
		using(IEnumerator<SerializationGraphNode> it = elements.GetEnumerator())
		{
			while(it.MoveNext())
			{
				object elemObj = it.Current.RebuildObject(graph,elemType);
				e.Add(elemObj);
			}
		}
	}
	
	#endregion
}
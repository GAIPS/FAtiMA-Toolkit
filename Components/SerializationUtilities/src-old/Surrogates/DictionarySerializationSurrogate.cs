using System;
using System.Collections;
using System.Collections.Generic;

public class DictionarySerializationSurrogate : ISerializationSurrogate
{
	#region ISerializationSurrogate implementation
	public void GetObjectData(object obj, ObjectGraphNode holder, SerializedGraph graph)
	{
		IDictionary e = (IDictionary)obj;
		Type objType = e.GetType();
		Type keyType = typeof(object);
		Type valueType = typeof(object);
		
		if(objType.IsGenericType)
		{
			Type[] gen = objType.GetGenericArguments();
			keyType = gen[0];
			valueType = gen[1];
		}
		
		ArrayGraphNode elements = new ArrayGraphNode();
		holder["dictionary"] = elements;
		IDictionaryEnumerator it = e.GetEnumerator();
		while(it.MoveNext())
		{
			ObjectGraphNode entry = graph.CreateObjectData();
			entry["key"] = graph.BuildNode(it.Key,keyType,holder);
			entry["value"] = graph.BuildNode(it.Value,valueType,holder);
			elements.Add(entry);
		}
	}
	
	public void SetObjectData (ref object obj, ObjectGraphNode node, SerializedGraph graph)
	{
		IDictionary e = (IDictionary)obj;
		Type objType = e.GetType();
		Type keyType = typeof(object);
		Type valueType = typeof(object);
		
		if(objType.IsGenericType)
		{
			Type[] gen = objType.GetGenericArguments();
			keyType = gen[0];
			valueType = gen[1];
		}
		
		ArrayGraphNode elements = node["dictionary"] as ArrayGraphNode;
		using(IEnumerator<SerializationGraphNode> it = elements.GetEnumerator())
		{
			while(it.MoveNext())
			{
				ObjectGraphNode entry = (ObjectGraphNode)it.Current;
				object key = entry["key"].RebuildObject(graph,keyType);
				object value = entry["value"].RebuildObject(graph,valueType);
				e.Add(key,value);
			}
		}
	}
	#endregion
	
}
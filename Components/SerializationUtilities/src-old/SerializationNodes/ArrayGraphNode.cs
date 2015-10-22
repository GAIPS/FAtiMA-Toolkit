using System;
using System.Collections;
using System.Collections.Generic;

public sealed class ArrayGraphNode : SerializationGraphNode, IEnumerable<SerializationGraphNode>{
	private List<SerializationGraphNode> m_elements = new List<SerializationGraphNode>();
	
	public void Add(SerializationGraphNode node)
	{
		m_elements.Add(node);
	}
	
	public object RebuildObject (SerializedGraph graph, Type knowType)
	{
		Type elemType = null;
		if(knowType!=null)
		{
			if(!knowType.IsArray)
				throw new Exception("Trying to parse array data, but expected an object of type "+knowType);
			
			elemType = knowType.GetElementType();
		}
		
		HashSet<Type> elemsTypes = new HashSet<Type>();
		object[] objs = new object[m_elements.Count];
		int numElems = m_elements.Count; 
		for(int i=0;i<numElems;i++)
		{
			SerializationGraphNode elemNode = m_elements[i];
			if(elemNode == null)
				continue;
			
			object o = m_elements[i].RebuildObject(graph,elemType);
			if(o!=null)
				elemsTypes.Add(o.GetType());
			
			objs[i] = o;
		}
		
		Type arrayType = FindClosestType(elemType,elemsTypes);
		Array array = Array.CreateInstance(arrayType,numElems);
		Array.Copy(objs,array,numElems);
		return array;
	}
	
	private static Type FindClosestType(Type rootType,HashSet<Type> types)
	{
		Type t = null;
		IEnumerator<Type> it = types.GetEnumerator();
		if(it.MoveNext())
		{
			t = it.Current;
			while(it.MoveNext())
			{
				Type ct = it.Current;
				if(!t.IsAssignableFrom(ct))
				{
					if(!ct.IsAssignableFrom(t))
					{
						t = typeof(object);
						break;
					}
					
					t=ct;
				}
			}
		}
		
		if(t == null)
		{
			if(rootType == null)
				return typeof(object);
			return rootType;
		}
		
		if(rootType!=null && !rootType.IsAssignableFrom(t))
			throw new Exception("type "+t+" can not be assign to type "+rootType);
		
		return t;
	}
	
	#region IEnumerable implementation
	
	public IEnumerator<SerializationGraphNode> GetEnumerator ()
	{
		return m_elements.GetEnumerator();
	}
	
	#endregion
	
	#region IEnumerable implementation
	
	IEnumerator IEnumerable.GetEnumerator ()
	{
		return ((IEnumerable<SerializationGraphNode>)this).GetEnumerator();
	}
	
	#endregion
}

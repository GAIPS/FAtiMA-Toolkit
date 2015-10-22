using System;
using System.Collections.Generic;

public abstract class ObjectGraphNode : SerializationGraphNode{
	
	public abstract ITypeGraphNode ObjectType
	{
		get;
	}
	
	public abstract bool IsReference
	{
		get;
	}
	
	public abstract int ReferenceId
	{
		get;
	}
	
	public abstract int ReferenceCount
	{
		get;
	}
	
	public abstract SerializationGraphNode this[string fieldName]{get;set;}	
	
	public abstract ObjectGraphNodeEnumerator GetEnumerator();
	
	public abstract object RebuildObject (SerializedGraph graph, Type knowType);
}

public interface ObjectGraphNodeEnumerator
{
	bool MoveNext();
	
	string FieldName
	{
		get;
	}
	
	SerializationGraphNode FieldNode
	{
		get;
	}
}

public sealed partial class SerializedGraph{
	private sealed class ObjectGraphNode_impl : ObjectGraphNode
	{
		#region Enumerator
		
		private class Enumerator : ObjectGraphNodeEnumerator
		{
			private Dictionary<string,SerializationGraphNode>.Enumerator m_it;
			public Enumerator(Dictionary<string,SerializationGraphNode>.Enumerator it)
			{
				m_it = it;
			}
		
			public bool MoveNext ()
			{
				return m_it.MoveNext();
			}
			
			public string FieldName {
				get {
					return m_it.Current.Key;
				}
			}
			public SerializationGraphNode FieldNode {
				get {
					return m_it.Current.Value;
				}
			}
		}
		
		#endregion
		
		private int m_refId;
		private Dictionary<string,SerializationGraphNode> m_fields = new Dictionary<string,SerializationGraphNode>();
		public object _linkedObject = null;
		private TypeGraphNode m_type=null;
		private List<SerializationGraphNode> m_referencedBy = new List<SerializationGraphNode>();
		
		public override ITypeGraphNode ObjectType {
			get {
				return m_type;
			}
		}
		
		public override bool IsReference {
			get {
				return ReferenceCount>1;
			}
		}
		
		public override int ReferenceId {
			get {
				return m_refId;
			}
		}
		
		public override int ReferenceCount {
			get {
				return m_referencedBy.Count;
			}
		}
		
		public ObjectGraphNode_impl(int refId, SerializedGraph parentGraph)
		{
			m_refId = refId;
			m_type = new TypeGraphNode();
		}
		
		public override SerializationGraphNode this[string fieldName] {
			get{
				SerializationGraphNode node;
				if(m_fields.TryGetValue(fieldName,out node))
					return node;
				return null;
			}
			set{
				if(m_fields.ContainsKey(fieldName))
					throw new Exception("Duplicated field named "+fieldName);
				
				m_fields[fieldName] = value;
			}
		}
		
		public void AddReferencedBy(SerializationGraphNode node)
		{
			m_referencedBy.Add(node);
		}
		
		public override ObjectGraphNodeEnumerator GetEnumerator ()
		{
			return new Enumerator(m_fields.GetEnumerator());
		}
		
		public override object RebuildObject (SerializedGraph graph, Type knowType)
		{
			object obj;
			Type objType = ObjectType.Class;
			
			if(IsReference)
			{
				if(_linkedObject!=null)
					return _linkedObject;
				
				obj = AllocateObjectMemory(ref objType, knowType);
				graph.LinkRefereces(this,obj);
			}
			else
				obj = AllocateObjectMemory(ref objType, knowType);
			
			ISerializationSurrogate surrogate = graph.GetSurrogateForType(objType);
			surrogate.SetObjectData(ref obj,this,graph);
			return obj;
		}
		
		private object AllocateObjectMemory(ref Type objType, Type knowType)
		{
			if(objType==null)
				objType = knowType;
			
			return Activator.CreateInstance(objType,true);
		}
	}
}

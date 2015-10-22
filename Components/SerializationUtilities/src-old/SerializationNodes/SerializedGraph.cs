using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed partial class SerializedGraph{
	
	private static readonly SurrogateChain SURROGATE_CHAIN = CreateSurrogates();
	private static SurrogateChain CreateSurrogates()
	{
		SurrogateChain chain = new SurrogateChain();
		chain.AddSurrogate(typeof(IList),new ListSerializationSurrogate());
		chain.AddSurrogate(typeof(IDictionary),new DictionarySerializationSurrogate());
		chain.AddSurrogate(typeof(DateTime),new TimestampSurrogate());
		return chain;
	}
	
	private List<ObjectGraphNode_impl> m_references;
	private Dictionary<object,int> m_links;
	
	private void init()
	{
		m_references = new List<ObjectGraphNode_impl>();
		m_links = new Dictionary<object, int>();
	}
	
	public SerializedGraph()
	{
		init();
	}
	
	public SerializedGraph(object objectToSerialize)
	{	
		init();
		Root = BuildNode(objectToSerialize,null,null);
	}
	
	public SerializationGraphNode Root{
		get;
		set;
	}
	
	private void LinkRefereces(ObjectGraphNode objRef, object obj)
	{
		if(obj == null)
			throw new NullReferenceException();
			
		int id;
		if(m_links.TryGetValue(obj, out id))
			throw new Exception("The given object is already linked to a ObjectRefGraphNode.");
			
		ObjectGraphNode_impl node = (ObjectGraphNode_impl)objRef;
		node._linkedObject = obj;
		m_links[obj] = node.ReferenceId;
	}
	
	private ObjectGraphNode CreateRef(){
		int count = m_references.Count;
		ObjectGraphNode_impl objRef = new ObjectGraphNode_impl(count,this);
		m_references.Add(objRef);
		return objRef;
	}
	
	public ObjectGraphNode CreateRef(int id, SerializationGraphNode referencedBy){
		while(id>=m_references.Count)	//Resize reference list if too short
			m_references.Add(null);
		
		if(m_references[id] != null)
			throw new Exception("The given id is already allocated to another reference.");
		
		m_references[id] = new ObjectGraphNode_impl(id,this);
		m_references[id].AddReferencedBy(referencedBy);
		return m_references[id];
	}
	
	public ObjectGraphNode CreateObjectData()
	{
		return new ObjectGraphNode_impl(-1,this);
	}
	
	public ObjectGraphNode GetObjRef(int id, SerializationGraphNode referencedBy)
	{
		ObjectGraphNode_impl node = (ObjectGraphNode_impl)m_references[id];
		node.AddReferencedBy(referencedBy);
		return node;
	}
	
	public IEnumerator<ObjectGraphNode> GetReferencesEnumerator(){
		return m_references.Cast<ObjectGraphNode>().Where(e => e!=null).GetEnumerator();
	}
	
	#region Serialization
	
	private static readonly Type STRING_TYPE = typeof(string);
	
	public SerializationGraphNode BuildNode(object obj,Type fieldType, SerializationGraphNode referencedBy){
		if(obj == null)
			return null;
		
		Type type = obj.GetType();
		if(type.IsArray)
			return ConstructArrayNodes((IEnumerable)obj,this);
		
		if(type == STRING_TYPE)
			return new StringGraphNode((string)obj);
		
		ObjectGraphNode objReturnData = null;
		if(type.IsValueType)
		{
			if(type.IsPrimitive)
				return new PrimitiveGraphNode((ValueType)obj);
			if(type.IsEnum)
				return new EnumGraphNode((Enum)obj);
			
			//Struct type
			objReturnData = CreateObjectData();
			ExtractObjectData(obj,type,objReturnData,this);
		}
		
		if(objReturnData==null)
		{
			//Object type
			if(referencedBy!=null){
				int id;
				if(m_links.TryGetValue(obj, out id))
				{
					objReturnData = m_references[id];
				}
				else
				{
					objReturnData = CreateRef();
					LinkRefereces(objReturnData,obj);
					ExtractObjectData(obj,type,objReturnData,this);
				}
				((ObjectGraphNode_impl)objReturnData).AddReferencedBy(referencedBy);
			}
			else
			{
				objReturnData = CreateObjectData();
				ExtractObjectData(obj,type,objReturnData,this);
			}
		}
		
		if((objReturnData.ObjectType.Class == null)&&(fieldType!=type))
			objReturnData.ObjectType.Class = type;
		
		return objReturnData;
	}
	
	private ArrayGraphNode ConstructArrayNodes(IEnumerable enumerable,SerializedGraph graphData){
		ArrayGraphNode array = new ArrayGraphNode();
		IEnumerator it = enumerable.GetEnumerator();
		while(it.MoveNext()){
			SerializationGraphNode elem = BuildNode(it.Current,null,array);
			array.Add(elem);
		}
		return array;
	}
	
	private void ExtractObjectData(object obj, Type objType, ObjectGraphNode holder, SerializedGraph graphData){
		ISerializationSurrogate surrogate = SURROGATE_CHAIN.GetSerializationSurrogate(objType);
		surrogate.GetObjectData(obj,holder,graphData);
	}
	
	#endregion
	
	public ISerializationSurrogate GetSurrogateForType(Type type)
	{
		return SURROGATE_CHAIN.GetSerializationSurrogate(type);
	}
}

using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class JSONSerializer : BaseSerializer {

	#region JSON Nodes
	
	private abstract class JToken{
	
		public void Write(TextWriter writer)
		{
			Write(writer, 0);
		}
		
		public abstract void Write(TextWriter writer, int ident);
		
		protected void writeIdentation(TextWriter writer, int ident)
		{
			while(ident>0)
			{
				writer.Write("\t");
				ident--;
			}
		}
		
		public sealed override string ToString ()
		{
			string str;
			using(StringWriter stream = new StringWriter())
			{
				Write(stream);
				str = stream.ToString();
			}
			return str;
		}
	}
	
	private class JNumber : JToken{
		public string number{
			get;
			private set;
		}
		
		public JNumber(string number){
			this.number = number;
		}
		
		public override void Write(TextWriter writer, int ident)
		{
			writer.Write(number);
		}
	}
	
	private class JString : JToken{
		public string str;
		
		public JString(string str){
			this.str = str;
		}
		
		public override void Write(TextWriter writer, int ident)
		{
			writer.Write('\"');
			StringBuilder builder = new StringBuilder(str);
			builder.Replace("\\","\\\\").Replace("\"","\\\"").Replace("\b","\\b").Replace("\f","\\f").Replace("\n","\\n").Replace("\r","\\r").Replace("\t","\\t");
			writer.Write(builder.ToString());
			writer.Write('\"');
		}
	}
	
	private class JBool : JToken{
		public bool Value;
		public JBool(bool value){
			this.Value = value;
		}
		
		public override void Write(TextWriter writer, int ident)
		{
			writer.Write(Value.ToString());
		}
	}
	
	private class JNull : JToken{
		public override void Write(TextWriter writer, int ident)
		{
			writer.Write("null");
		}
	}
	
	private class JObject : JToken{
		private Dictionary<string,JToken> m_fields;
		
		public JObject(){
			m_fields = new Dictionary<string,JToken>();
		}
		
		public JToken this[string key]{
			get{
				JToken o;
				if(m_fields.TryGetValue(key,out o))
					return o;
				
				return null;
			}
			set{
				m_fields[key] = value;
			}
		}
		
		public IEnumerator<KeyValuePair<string,JToken>> GetEnumerator()
		{
			return m_fields.GetEnumerator();
		}
		
		public override void Write(TextWriter writer, int ident)
		{
			writeIdentation(writer, ident);
			writer.WriteLine("{");
			int max = m_fields.Count;
			int cnt = 0;
			ident++;
			foreach(KeyValuePair<string,JToken> pair in m_fields){
				writeIdentation(writer, ident);
				writer.Write('\"');
				writer.Write(pair.Key);
				writer.Write("\":");
				if(pair.Value is JObject)
				{
					writer.WriteLine();
				}
				else
					writer.Write(' ');
				
				pair.Value.Write(writer,ident+1);
				cnt++;
				if(cnt<max)
					writer.WriteLine(',');
			}
			ident--;
			writer.WriteLine();
			writeIdentation(writer, ident);
			writer.Write("}");
		}
	}
	
	private class JArray : JToken{
		public List<JToken> Elements = new List<JToken>();
		public bool AllElementsInSeparatedLines = false;
		
		public override void Write(TextWriter writer, int ident)
		{
			writer.Write("[");
			int max = Elements.Count;
			int cnt=0;
			ident++;
			foreach(JToken v in Elements){
				if(v is JObject)
				{
					writer.WriteLine();
					v.Write(writer,ident-1);
				}
				else
				{
					if(AllElementsInSeparatedLines)
					{
						writer.WriteLine();
						writeIdentation(writer, ident);
					}
					v.Write(writer,ident);
				}
				
				cnt++;
				if(cnt<max)
					writer.Write(", ");
			}
			ident--;
			if(AllElementsInSeparatedLines)
			{
				writer.WriteLine();
				writeIdentation(writer, ident);
			}
			writer.Write("]");
		}
	}
	
	#endregion
	
	#region JSON Parsing
	
	private JToken ReadValue(StreamReader reader){
		JToken node = null;
		
		readEmptyCharacters(reader);
		char start = (char)reader.Peek();
		switch(start){
		case '{':
			node = ReadObject(reader);
			break;
		case '[':
			node = ReadArray(reader);
			break;
		case '\"':
			{
				string str = ReadString(reader);
				node = new JString(str);
			}
			break;
		default:
			node = ReadPrimitive(reader);
			break;
		}
		return node;
	}
	
	private JToken ReadObject(StreamReader reader){
		JObject obj = new JObject();
		reader.Read();	// read '{'
		readEmptyCharacters(reader);
		char c = (char)reader.Peek();
		if(c=='}')
		{
			reader.Read();
			return obj;
		}
		
		while(!reader.EndOfStream){
			if(c!='"')
				throw new IOException("Invalid JSON format");
			
			string field = ReadString(reader);
			readEmptyCharacters(reader);
			c = (char)reader.Read();
			if(c!=':')
				throw new IOException("Invalid JSON format");
			
			JToken value = ReadValue(reader);
			readEmptyCharacters(reader);
			
			obj[field] = value;
			
			c=(char)reader.Read();
			if(c=='}')
				return obj;
			
			if(c!=',')
				throw new IOException("Invalid JSON format");
			
			readEmptyCharacters(reader);
			c = (char)reader.Peek();
		}
		
		throw new IOException("End of Stream Reached without finishing parsing object");
	}
	
	private JToken ReadArray(StreamReader reader){
		JArray array = new JArray();
		reader.Read();	// read '['
		readEmptyCharacters(reader);
		while((char)reader.Peek() != ']'){
			JToken val = ReadValue(reader);
			array.Elements.Add(val);
			
			readEmptyCharacters(reader);
			char c = (char)reader.Peek();
			if(c==',')
				reader.Read();
			else if(c!=']')
				throw new IOException("Invalid JSON format: Invalid array separator. Expected ',' or ']' - Found'"+c+"'");
		}
		reader.Read(); //Read ']'
		return array;
	}
	
	private const string NUMBER_PATTERN = @"^-?(0|[1-9]\d*)(\.\d+)?(e(-|\+)?\d+)?$";
	private static readonly Regex _numberRegex = new Regex(NUMBER_PATTERN);
	
	private JToken ReadPrimitive(StreamReader reader){
		StringBuilder builder = new StringBuilder();
		while(!reader.EndOfStream){
			char c =(char)reader.Peek();
			if((c == ',')||(c==']')||(c=='}')||Char.IsWhiteSpace(c))
				break;
			
			builder.Append((char)reader.Read());
		}
		
		string primitive = builder.ToString().ToLower();
		if(_numberRegex.IsMatch(primitive)){
			return new JNumber(primitive);
		}
		
		if(primitive == "true")
			return new JBool(true);
		
		if(primitive == "false")
			return new JBool(false);
		
		if(primitive == "null")
			return new JNull();
		
		throw new IOException("Invalid JSON format: Invalid primitive \""+primitive+"\"");
	}
	
	private static readonly char[] hexBuffer = new char[4];
	private string ReadString(StreamReader reader){
		reader.Read();	// read '"'
		StringBuilder builder = new StringBuilder();
		bool isControl=false;
		while(!reader.EndOfStream){
			char c =(char)reader.Read();
			if(isControl){
				switch(c){
				case '"':
					builder.Append('"');
					break;
				case '\\':
					builder.Append('\\');
					break;
				case '/':
					builder.Append('/');
					break;
				case 'b':
					builder.Append('\b');
					break;
				case 'f':
					builder.Append('\f');
					break;
				case 'n':
					builder.Append('\n');
					break;
				case 'r':
					builder.Append('\r');
					break;
				case 't':
					builder.Append('\t');
					break;
				case 'u':
				{
					if(reader.ReadBlock(hexBuffer,0,4)<4)
						throw new IOException("Invalid JSON format.");
					
					string hexString = new string(hexBuffer);
					long result = long.Parse(hexString,System.Globalization.NumberStyles.HexNumber);
					
					char hexChar = (char)result;
					builder.Append(hexChar);
				}
					
					break;
				default:
					throw new IOException("Invalid JSON format.");
				}
				isControl=false;
			}else{
				if(c=='"')
					break;
				else if(c=='\\'){
					isControl=true;
				}else{
					builder.Append(c);
				}
			}
		}
		return builder.ToString();
	}
	
	private void readEmptyCharacters(StreamReader reader){
		while(!reader.EndOfStream && Char.IsWhiteSpace((char)reader.Peek())){
			reader.Read();
		}
	}
	
	#endregion
	
	#region Serialization
	
	private class SerializationHelper
	{
		List<Type> m_typeMapping = new List<Type>();
		//Dictionary<object,int> idMapping = new Dictionary<object, int>();
		
		public int GetTypeID(Type type)
		{
			int id = m_typeMapping.IndexOf(type);
			if(id<0)
			{
				id = m_typeMapping.Count;
				m_typeMapping.Add(type);
			}
			
			return id;
		}
		
		public bool HasTypes
		{
			get{
				return m_typeMapping.Count>0;
			}
		}
		
		public IEnumerator<Type> GetTypeEnumerator()
		{
			return m_typeMapping.GetEnumerator();
		}
	}
	
	protected override void SerializeGraph (Stream stream, SerializedGraph graph)
	{	
		SerializationHelper helper = new SerializationHelper();
	
		JObject jo = new JObject();
		JArray refArray = null;
		{
			List<JToken> nodes = new List<JToken>();
			//Write/count references
			using(IEnumerator<ObjectGraphNode> it = graph.GetReferencesEnumerator()){
				while(it.MoveNext()){
					if(!it.Current.IsReference)
						continue;
					
					JObject o = NodeToJson(it.Current,helper);
					nodes.Add(o);
				}
			}
			
			if(nodes.Count>0)
			{
				refArray = new JArray();
				refArray.Elements = nodes;
			}
		}
		
		JToken root = ToJson(graph.Root,helper); 
		
		if(helper.HasTypes)
		{
			jo["types"] = SerializeTypes(helper.GetTypeEnumerator());
		}
		
		if(refArray!=null)
			jo["refs"] = refArray;
		
		//Write root
		jo["root"] = root;
		
		TextWriter writer = new StreamWriter(stream);
		jo.Write(writer);
		writer.WriteLine();
		writer.Flush();
	}
	
	private JToken SerializeTypes(IEnumerator<Type> typeIt)
	{
		JArray types = new JArray();
		while(typeIt.MoveNext())
		{
			JString str = new JString(typeIt.Current.AssemblyQualifiedName);
			types.Elements.Add(str);
		}
		types.AllElementsInSeparatedLines=true;
		return types;
	/*
		HashSet<System.Reflection.Assembly> ass = new HashSet<System.Reflection.Assembly>();
		while(typeIt.MoveNext())
		{
			Type t = typeIt.Current;
			ass.Add(t.Assembly);
		}
	
		JArray assemblies = new JArray();
		assemblies.AllElementsInSeparatedLines=true;
		foreach(System.Reflection.Assembly a in ass)
		{
			assemblies.Elements.Add(new JString(a.FullName));
		}
		
		return assemblies;
		*/
	}
	
	private JToken ToJson(SerializationGraphNode node, SerializationHelper helper)
	{
		if(node == null)
			return new JNull();
		
		if(node is StringGraphNode)
			return new JString((node as StringGraphNode).Value);
		
		if(node is PrimitiveGraphNode)
			return new JNumber((node as PrimitiveGraphNode).Value.ToString());
			
		if(node is ArrayGraphNode){
			JArray a = new JArray();
			using(IEnumerator<SerializationGraphNode> it = (node as ArrayGraphNode).GetEnumerator())
			{
				while(it.MoveNext()){
					JToken o = ToJson(it.Current,helper);
					a.Elements.Add(o);
				}
			}
			
			return a;
		}
		
		if(node is EnumGraphNode){
			Enum e = (node as EnumGraphNode).e;
			Type eType = e.GetType();
			string format = e.ToString().Replace(", ","|");
			return new JString("enum<"+(helper.GetTypeID(eType)+1)+">:"+format);
		}
		
		ObjectGraphNode objNode = node as ObjectGraphNode;
		
		if(objNode.IsReference)
		{
			return new JString("refId@"+(objNode.ReferenceId+1).ToString());
		}
		
		return NodeToJson(objNode,helper);
	}
	
	private JObject NodeToJson(ObjectGraphNode node, SerializationHelper helper)
	{
		JObject jo = new JObject();
		
		if(node.IsReference)
		{
			jo["refId"] = new JNumber((node.ReferenceId+1).ToString());
		}
		
		if(node.ObjectType.Class!=null)
		{
			int classId = helper.GetTypeID(node.ObjectType.Class)+1;
			jo["classID"] = new JNumber(classId.ToString());
		}
		
		ObjectGraphNodeEnumerator it = node.GetEnumerator();
		while(it.MoveNext()){
			jo[it.FieldName] = ToJson(it.FieldNode,helper);
		}
		
		return jo;
	}
	
	#endregion
	
	#region Deserialization
	
	private class DeserializationHelper
	{
		private Dictionary<int,JObject> m_toLoad = new Dictionary<int, JObject>();
		private List<Type> m_typeList = new List<Type>();
		
		public void AddTypeToRegistry(Type type)
		{
			m_typeList.Add(type);
		}
		
		public Type GetTypeById(int id)
		{
			return m_typeList[id];
		}
		
		public void ScheduleToLoad(int id, JObject data)
		{
			if(m_toLoad.ContainsKey(id))
				throw new Exception("Duplicated object definition for id: "+id);
			
			m_toLoad[id] = data;
		}
		
		public ObjectGraphNode LoadObjectReference(int id, SerializationGraphNode referencedBy, SerializedGraph graph)
		{
			JObject jo;
			if(m_toLoad.TryGetValue(id, out jo))
			{
				m_toLoad.Remove(id);
				ObjectGraphNode objRef = graph.CreateRef(id, referencedBy);
				ReadObjDataNode(objRef,jo,this,graph);
				return objRef;
			}
			
			return graph.GetObjRef(id,referencedBy);
		}
	}
	
	protected override SerializedGraph DeserializeGraph (Stream stream)
	{
		JObject node = (JObject)ReadValue(new StreamReader(stream));
		
		SerializedGraph graph = new SerializedGraph();
		
		DeserializationHelper helper = new DeserializationHelper();
		
		JArray types = node["types"] as JArray;
		if(types != null)
		{
			foreach(JToken t in types.Elements)
			{
				string typeName = (t as JString).str;
				Type loadedType = Type.GetType(typeName);
				helper.AddTypeToRegistry(loadedType);
			}
		}
		
		JArray refs = node["refs"] as JArray;
		if(refs!=null)
		{
			List<JToken> elm = refs.Elements;
			for(int i=0;i<elm.Count;i++)
			{
				JObject jo = (JObject)elm[i];
				int id;
				JToken idToken = jo["refId"];
				if(idToken==null)
				{
					//Id is missing, use element index
					id = i;
				}
				else
				{
					if(!int.TryParse((idToken as JNumber).number,out id))
						throw new Exception("Unable to parse \""+(idToken as JNumber).number+"\" as a refId.");
					id--;
				}
				
				helper.ScheduleToLoad(id,jo);
			}
		}
		
		JToken root = node["root"];
		if(root!=null)
		{
			graph.Root = ReadNode(root,null,helper,graph);
		}
		
		return graph;
	}
	
	private const string REF_MARK_PATTERN = @"^refId@(\d+)$";
	private static readonly Regex _refMarkRegex = new Regex(REF_MARK_PATTERN);
	
	private const string ENUM_PATTERN = @"^enum<(\d+)>:([\w\d\|]+)$";
	private static readonly Regex _enumRegex = new Regex(ENUM_PATTERN);
	
	private static SerializationGraphNode ReadNode(JToken token, SerializationGraphNode referencedBy, DeserializationHelper helper, SerializedGraph graph)
	{
		if(token is JNull)
			return null;
			
		if(token is JString)
		{
			string val = (token as JString).str;
			Match m = _refMarkRegex.Match(val);
			if(m.Success)
			{
				int id = int.Parse(m.Groups[1].Value)-1;
				return helper.LoadObjectReference(id,referencedBy,graph);
			}
			m = _enumRegex.Match(val);
			if(m.Success)
			{
				int typeId = int.Parse(m.Groups[1].Value);
				Type enumType = helper.GetTypeById(typeId-1);
				string value = m.Groups[2].Value;
				bool notFlag = true;
				foreach(object att in enumType.GetCustomAttributes(false))
				{
					if(att is FlagsAttribute)
					{
						notFlag=false;
						break;
					}
				}
				
				if(notFlag)
					return new EnumGraphNode((Enum)Enum.Parse(enumType,value));
				
				ulong bits = 0;
				foreach(string str in value.Split('|'))
				{
					ulong v = Convert.ToUInt64(Enum.Parse(enumType,str));
					bits |= v;
				}
				return new EnumGraphNode((Enum)Enum.ToObject(enumType,bits));
			}
			
			return new StringGraphNode(val);
		}
		
		if(token is JNumber)
		{
			string value = (token as JNumber).number;
			ulong ul;
			if(ulong.TryParse(value,out ul))
				return new PrimitiveGraphNode(ul);
			
			long l;
			if(long.TryParse(value,out l))
				return new PrimitiveGraphNode(l);
			
			double d;
			if(double.TryParse(value, out d))
				return new PrimitiveGraphNode(d);
			
			throw new Exception("Unable to parse value "+value);
		}
		
		if(token is JBool)
		{
			return new PrimitiveGraphNode((token as JBool).Value);
		}
			
		if(token is JArray)
		{
			ArrayGraphNode node = new ArrayGraphNode();
			foreach(JToken t in (token as JArray).Elements)
			{
				node.Add(ReadNode(t,node,helper,graph));
			}
			return node;
		}
		
		ObjectGraphNode objNode = graph.CreateObjectData();
		ReadObjDataNode(objNode,token as JObject,helper,graph);
		return objNode;
	}
	
	private static void ReadObjDataNode(ObjectGraphNode node, JObject jo, DeserializationHelper helper, SerializedGraph graph)
	{
		IEnumerator<KeyValuePair<string,JToken>> it = jo.GetEnumerator();
		while(it.MoveNext())
		{
			if(it.Current.Key == "classID")
			{
				int classId = int.Parse((it.Current.Value as JNumber).number);
				node.ObjectType.Class = helper.GetTypeById(classId-1);
			}
			else if(it.Current.Key != "refId")
			{
				node[it.Current.Key] = ReadNode(it.Current.Value,node,helper,graph);
			}
		}
	}
	
	#endregion
}

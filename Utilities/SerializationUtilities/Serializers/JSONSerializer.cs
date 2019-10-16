using System;
using System.Globalization;
using System.IO;
using System.Linq;
using SerializationUtilities.SerializationGraph;
using Utilities.Json;

namespace SerializationUtilities
{
	public class JSONSerializer : BaseSerializer
	{
		private const string TYPES_FIELD = "types";
		private const string ROOT_FIELD = "root";
		private const string REFERENCES_FIELD = "references";

		public enum EnumRepresentationMode : byte
		{
			Explicit,
			Numeric
		}

		public bool AllowIdentation = true;
		public EnumRepresentationMode EnumRepresentation = EnumRepresentationMode.Explicit;

		public JSONSerializer()
		{
			FormatSelector.AddFormatter(typeof(Enum), true, new EnumGraphFormatter { parent = this });
			FormatSelector.AddFormatter(typeof(DateTime),false, new DateTimeFormatter());
			FormatSelector.AddFormatter(typeof(Guid),false,new GUIDFormatter());
		}

		#region Serialization

		public JsonObject SerializeToJson(object graph)
		{
			Graph serGraph = ComputeGraph(graph);
			return ToJson(serGraph);
		}

		protected override void SerializeDataGraph(Stream serializationStream, Graph graph)
		{
			JsonObject json = ToJson(graph);

			var w = new StreamWriter(serializationStream);
			json.Write(w, 0, AllowIdentation);
			w.Flush();
		}

		private JsonObject ToJson(Graph graph)
		{
			JsonObject json = new JsonObject();

			//Collect types
			var types = CollectAssemblyData(graph);

			json[ROOT_FIELD] = ToJson(graph.Root);

			var nodes = graph.GetReferences().Select(NodeToJson).Cast<JsonToken>();
			if (nodes.Any())
				json[REFERENCES_FIELD] = new JsonArray(nodes);

			json[TYPES_FIELD] = types;

			return json;
		}

		private JsonToken CollectAssemblyData(Graph graph)
		{
			var array = new JsonArray();
			foreach (var typeEntry in graph.GetRegistedTypes())
			{
				var entry = new JsonObject();
				entry["TypeId"] = new JsonNumber(typeEntry.TypeId);
				entry["ClassName"] = new JsonString(typeEntry.ClassType.AssemblyQualifiedName);
				array.Add(entry);
			}
			return array;
		}

		private JsonToken ToJson(IGraphNode node)
		{
			if (node == null)
				return null;

			switch (node.DataType)
			{
				case SerializedDataType.Boolean:
					return new JsonBool((bool)((IPrimitiveGraphNode)node).Value);
				case SerializedDataType.Number:
					return new JsonNumber((node as IPrimitiveGraphNode).Value);
				case SerializedDataType.String:
					return new JsonString((node as IStringGraphNode).Value);
				case SerializedDataType.DataSequence:
					return new JsonArray((node as ISequenceGraphNode).Select(n => ToJson(n)));
				case SerializedDataType.Type:
					return new JsonString("class@"+(node as ITypeGraphNode).TypeId);
			}

			IObjectGraphNode objNode = node as IObjectGraphNode;
			if (objNode.IsReferedMultipleTimes)
			{
				return new JsonString("refId@" + objNode.RefId.ToString());
			}
			
			return NodeToJson(objNode);
		}

		private JsonObject NodeToJson(IObjectGraphNode node)
		{
			JsonObject json = new JsonObject();

			if (node.IsReferedMultipleTimes)
			{
				if (node.RefId >= 0)
					json["refId"] = new JsonNumber(node.RefId);
			}

			if (node.ObjectType != null)
			{
				json["classId"] = new JsonNumber(node.ObjectType.TypeId);
			}

			using (var it = node.GetEnumerator())
			{
				while (it.MoveNext())
				{
					json[it.Current.FieldName] = ToJson(it.Current.FieldNode);
				}
			}

			return json;
		}

		#endregion

		#region Deserialization

		public T DeserializeFromJson<T>(JsonObject json)
		{
			return (T)DeserializeFromJson(json, typeof(T));
		}

		public object DeserializeFromJson(JsonObject json, Type returnType)
		{
			if (!json.ContainsField(ROOT_FIELD))
			{
				var baseJson = new JsonObject();
				baseJson[ROOT_FIELD] = json;
				json = baseJson;
			}

			var graph = new Graph(FormatSelector, Context);
			if (!json.ContainsField(TYPES_FIELD))
			{
				var obj = (JsonObject) json[ROOT_FIELD];
				if (obj.ContainsField("classId"))
					graph.RegistTypeEntry(returnType, (byte)((JsonNumber)obj["classId"]).Value);
			}
			
			DeserializeDataGraphFromJson(json,graph);
			return graph.DeserializeObject(returnType);
		}

		protected override void DeserializeDataGraph(Stream serializationStream, Graph serGraph)
		{
			JsonObject json = JsonParser.Parse(serializationStream) as JsonObject;
			if (json == null)
				throw new Exception("Unable to deserialize"); //TODO get a better exception

			DeserializeDataGraphFromJson(json,serGraph);
		}

		private void DeserializeDataGraphFromJson(JsonObject json, Graph serGraph)
		{
			if (json == null)
				throw new Exception("Unable to deserialize"); //TODO get a better exception

			if (json.ContainsField(TYPES_FIELD))
			{
				JsonArray types = json[TYPES_FIELD] as JsonArray;
				foreach (var t in types)
				{
					var typeEntry = t as JsonObject;
					if (typeEntry == null)
						throw new Exception("Unable to deserialize"); //TODO get a better exception

					byte typeId = (byte)(typeEntry["TypeId"] as JsonNumber);
                    
					Type loadedType = Type.GetType((typeEntry["ClassName"] as JsonString).String);
					serGraph.RegistTypeEntry(loadedType, typeId);
				}
			}

			if (json.ContainsField(REFERENCES_FIELD))
			{
				JsonArray refs = json[REFERENCES_FIELD] as JsonArray;
				foreach (var e in refs)
				{
					var refEntry = e as JsonObject;
					if (refEntry == null)
						throw new Exception("Unable to deserialize"); //TODO get a better exception

					JsonToObjectNode(refEntry, serGraph);
				}
			}

			serGraph.Root = ReadNode(json[ROOT_FIELD] as JsonToken, serGraph);
		}

		private IObjectGraphNode JsonToObjectNode(JsonObject json, Graph parentGraph)
		{
			IObjectGraphNode node;
			
			if(json.ContainsField("refId"))
			{
				node = parentGraph.GetObjectDataForRefId((int)(json["refId"] as JsonNumber));
			}
			else
				node = parentGraph.CreateObjectData();

			foreach (var field in json)
			{
				if (field.Key == "refId")
					continue;

				if (field.Key == "classId")
				{
					var id = (byte)(field.Value as JsonNumber);
					node.ObjectType = parentGraph.GetTypeEntry(id);
				}
				else
				{
					node[field.Key] = ReadNode(field.Value, parentGraph);
				}
			}

			return node;
		}

		private IGraphNode ReadNode(JsonToken json, Graph parentGraph)
		{
			if (json == null)
				return null;

			if (json is JsonString)
			{
				string val = ((JsonString)json).String;
				if (val.StartsWith("refId@"))
				{
					int id = int.Parse(val.Substring(6));
					return parentGraph.GetObjectDataForRefId(id);
				}

				if (val.StartsWith("class@"))
				{
					byte id = byte.Parse(val.Substring(6));
					return parentGraph.GetTypeEntry(id);
				}

				return parentGraph.BuildStringNode(val);
			}

			if (json is JsonNumber)
				return parentGraph.BuildPrimitiveNode((json as JsonNumber).Value);

			if (json is JsonBool)
				return parentGraph.BuildPrimitiveNode((json as JsonBool).Value);

			if (json is JsonArray)
			{
				ISequenceGraphNode node = parentGraph.BuildSequenceNode();
				foreach (JsonToken t in ((JsonArray)json))
				{
					var elem = ReadNode(t, parentGraph);
					node.Add(elem);
				}
				return node;
			}

			return JsonToObjectNode(json as JsonObject, parentGraph);
		}

		#endregion

		private class EnumGraphFormatter : IGraphFormatter
		{
			public JSONSerializer parent;

			public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
			{
				Enum enumValue = (Enum)value;
				if (parent.EnumRepresentation == EnumRepresentationMode.Numeric)
					return serializationGraph.BuildPrimitiveNode((ValueType)Convert.ChangeType(enumValue, enumValue.GetType()));
				return serializationGraph.BuildStringNode(enumValue.ToString().Replace(", ", "|"));
			}

			public object GraphNodeToObject(IGraphNode node, Type objectType)
			{
				if (node.DataType == SerializedDataType.Number)
					return Convert.ChangeType((node as IPrimitiveGraphNode).Value, objectType);
				if (node.DataType != SerializedDataType.String)
					throw new Exception("invalid enum type");	//TODO better exception

				var str = (node as IStringGraphNode).Value;
				str = str.Replace("|", ",");
				return Enum.Parse(objectType, str, true);
			}
		}

		private class DateTimeFormatter : IGraphFormatter
		{
			private static readonly string[] TIME_FORMATS = {
				@"d/M/yyyy@H:m:s.fffffff",
				@"d/M/yyyy@H:m:s",
				@"d/M/yyyy@H:m",
				@"d/M/yyyy",
			};

			public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
			{
				DateTime time = (DateTime) value;
				time = time.ToUniversalTime();
				string format = @"d/M/yyyy";
				if (time.Hour > 0 || time.Minute > 0 || time.Second > 0 || time.Millisecond > 0)
				{
					format += @"@H:m";
					if (time.Second > 0 || time.Millisecond > 0)
					{
						format += @":s";
						if (time.Millisecond > 0)
							format += @".fffffff";
					}
				}
				string timestamp = time.ToString(format);
				return serializationGraph.BuildStringNode(timestamp);
			}

			public object GraphNodeToObject(IGraphNode node, Type objectType)
			{
				var timestamp = (IStringGraphNode) node;
				DateTime t = DateTime.ParseExact(timestamp.Value, TIME_FORMATS, null, DateTimeStyles.None);
				return DateTime.SpecifyKind(t, DateTimeKind.Utc);
			}
		}

		private class GUIDFormatter: IGraphFormatter
		{
			public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
			{
				return serializationGraph.BuildStringNode(((Guid) value).ToString("D"));
			}

			public object GraphNodeToObject(IGraphNode node, Type objectType)
			{
				var str = ((IStringGraphNode) node).Value;
				return new Guid(str);
			}
		}
	}
}

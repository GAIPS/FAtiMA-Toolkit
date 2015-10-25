using GAIPS.Serialization.SerializationGraph;
using System;
using System.IO;
using System.Linq;
using Utilities;
using Utilities.Json;

namespace GAIPS.Serialization
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

		#region Serialization

		protected override void SerializeDataGraph(Stream serializationStream, Graph graph)
		{
			JsonObject json = new JsonObject();

			//Collect types
			json[TYPES_FIELD] = CollectAssemblyData(graph);

			json[ROOT_FIELD] = ToJson(graph.Root);

			var nodes = graph.GetReferences().Select(n => NodeToJson(n)).Cast<JsonToken>();
			if (!nodes.IsEmpty())
				json[REFERENCES_FIELD] = new JsonArray(nodes);

			var w = new StreamWriter(serializationStream);
			json.Write(w, 0, AllowIdentation);
			w.Flush();
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
			}

			IObjectGraphNode objNode = node as IObjectGraphNode;
			if (objNode.IsReference)
			{
				return new JsonString("refId@" + objNode.RefId.ToString());
			}
			
			return NodeToJson(objNode);
		}

		private JsonObject NodeToJson(IObjectGraphNode node)
		{
			JsonObject json = new JsonObject();

			if (node.IsReference)
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

		protected override Graph DeserializeDataGraph(Stream serializationStream)
		{
			JsonObject json = JsonParser.Parse(serializationStream) as JsonObject;
			if (json == null)
				throw new Exception("Unable to deserialize"); //TODO get a better exception

			Graph serGraph = new Graph(this);
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

					var node = JsonToObjectNode(refEntry, serGraph);
				}
			}

			serGraph.Root = JsonToObjectNode(json[ROOT_FIELD] as JsonObject, serGraph);
			return serGraph;
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

		public override IGraphNode EnumToGraphNode(Enum enumValue, Graph serializationGraph)
		{
			if (EnumRepresentation == EnumRepresentationMode.Numeric)
				return serializationGraph.BuildPrimitiveNode((ValueType)Convert.ChangeType(enumValue, enumValue.GetTypeCode()));
			return serializationGraph.BuildStringNode(enumValue.ToString().Replace(',','|'));
		}

		public override Enum GraphNodeToEnum(IGraphNode node, Type enumType)
		{
			if (node.DataType == SerializedDataType.Number)
				return (Enum)Convert.ChangeType((node as IPrimitiveGraphNode).Value, enumType);
			if (node.DataType != SerializedDataType.String)
				throw new Exception("invalid enum type");	//TODO better exception

			var str = (node as IStringGraphNode).Value;
			str = str.Replace("|", ",");
			return (Enum)Enum.Parse(enumType, str, true);
		}
	}
}

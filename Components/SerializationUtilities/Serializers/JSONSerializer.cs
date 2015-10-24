using GAIPS.Serialization.SerializationGraph;
using GAIPS.Serialization.SerializationGraph.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities;
using Utilities.Json;

namespace GAIPS.Serialization
{
	public class JSONSerializer : BaseSerializer
	{
		//#region JSON Parsing

		//private JsonToken ReadValue(StreamReader reader)
		//{
		//	JsonToken node = null;

		//	readEmptyCharacters(reader);
		//	char start = (char)reader.Peek();
		//	switch (start)
		//	{
		//		case '{':
		//			node = ReadObject(reader);
		//			break;
		//		case '[':
		//			node = ReadArray(reader);
		//			break;
		//		case '\"':
		//			{
		//				string str = ReadString(reader);
		//				node = new JsonString(str);
		//			}
		//			break;
		//		default:
		//			node = ReadPrimitive(reader);
		//			break;
		//	}
		//	return node;
		//}

		//private JsonToken ReadObject(StreamReader reader)
		//{
		//	JsonObject obj = new JsonObject();
		//	reader.Read();	// read '{'
		//	readEmptyCharacters(reader);
		//	char c = (char)reader.Peek();
		//	if (c == '}')
		//	{
		//		reader.Read();
		//		return obj;
		//	}

		//	while (!reader.EndOfStream)
		//	{
		//		if (c != '"')
		//			throw new IOException("Invalid JSON format");

		//		string field = ReadString(reader);
		//		readEmptyCharacters(reader);
		//		c = (char)reader.Read();
		//		if (c != ':')
		//			throw new IOException("Invalid JSON format");

		//		JsonToken value = ReadValue(reader);
		//		readEmptyCharacters(reader);

		//		obj[field] = value;

		//		c = (char)reader.Read();
		//		if (c == '}')
		//			return obj;

		//		if (c != ',')
		//			throw new IOException("Invalid JSON format");

		//		readEmptyCharacters(reader);
		//		c = (char)reader.Peek();
		//	}

		//	throw new IOException("End of Stream Reached without finishing parsing object");
		//}

		//private JsonToken ReadArray(StreamReader reader)
		//{
		//	JsonArray array = new JsonArray();
		//	reader.Read();	// read '['
		//	readEmptyCharacters(reader);
		//	while ((char)reader.Peek() != ']')
		//	{
		//		JsonToken val = ReadValue(reader);
		//		array.Add(val);

		//		readEmptyCharacters(reader);
		//		char c = (char)reader.Peek();
		//		if (c == ',')
		//			reader.Read();
		//		else if (c != ']')
		//			throw new IOException("Invalid JSON format: Invalid array separator. Expected ',' or ']' - Found'" + c + "'");
		//	}
		//	reader.Read(); //Read ']'
		//	return array;
		//}

		//private const string NUMBER_PATTERN = @"^-?(0|[1-9]\d*)(\.\d+)?(e(-|\+)?\d+)?$";
		//private static readonly Regex _numberRegex = new Regex(NUMBER_PATTERN);

		//private JsonToken ReadPrimitive(StreamReader reader)
		//{
		//	StringBuilder builder = new StringBuilder();
		//	while (!reader.EndOfStream)
		//	{
		//		char c = (char)reader.Peek();
		//		if ((c == ',') || (c == ']') || (c == '}') || Char.IsWhiteSpace(c))
		//			break;

		//		builder.Append((char)reader.Read());
		//	}

		//	string primitive = builder.ToString().ToLower();
		//	if (_numberRegex.IsMatch(primitive))
		//	{
		//		return new JsonNumber(primitive);
		//	}

		//	if (primitive == "true")
		//		return new JsonBool(true);

		//	if (primitive == "false")
		//		return new JsonBool(false);

		//	if (primitive == "null")
		//		return null;

		//	throw new IOException("Invalid JSON format: Invalid primitive \"" + primitive + "\"");
		//}

		//private static readonly char[] hexBuffer = new char[4];
		//private string ReadString(StreamReader reader)
		//{
		//	reader.Read();	// read '"'
		//	StringBuilder builder = new StringBuilder();
		//	bool isControl = false;
		//	while (!reader.EndOfStream)
		//	{
		//		char c = (char)reader.Read();
		//		if (isControl)
		//		{
		//			switch (c)
		//			{
		//				case '"':
		//					builder.Append('"');
		//					break;
		//				case '\\':
		//					builder.Append('\\');
		//					break;
		//				case '/':
		//					builder.Append('/');
		//					break;
		//				case 'b':
		//					builder.Append('\b');
		//					break;
		//				case 'f':
		//					builder.Append('\f');
		//					break;
		//				case 'n':
		//					builder.Append('\n');
		//					break;
		//				case 'r':
		//					builder.Append('\r');
		//					break;
		//				case 't':
		//					builder.Append('\t');
		//					break;
		//				case 'u':
		//					{
		//						if (reader.ReadBlock(hexBuffer, 0, 4) < 4)
		//							throw new IOException("Invalid JSON format.");

		//						string hexString = new string(hexBuffer);
		//						long result = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

		//						char hexChar = (char)result;
		//						builder.Append(hexChar);
		//					}

		//					break;
		//				default:
		//					throw new IOException("Invalid JSON format.");
		//			}
		//			isControl = false;
		//		}
		//		else
		//		{
		//			if (c == '"')
		//				break;
		//			else if (c == '\\')
		//			{
		//				isControl = true;
		//			}
		//			else
		//			{
		//				builder.Append(c);
		//			}
		//		}
		//	}
		//	return builder.ToString();
		//}

		//private void readEmptyCharacters(StreamReader reader)
		//{
		//	while (!reader.EndOfStream && Char.IsWhiteSpace((char)reader.Peek()))
		//	{
		//		reader.Read();
		//	}
		//}

		//#endregion

		public bool AllowIdentation = true;

		#region Serialization

		protected override void SerializeDataGraph(Stream serializationStream, Graph graph)
		{
			JsonObject root = new JsonObject();

			//Collect types
			root["types"] = CollectAssemblyData(graph);

			root["root"] = ToJson(graph.Root);

			var nodes = graph.GetReferences().Select(n => NodeToJson(n)).Cast<JsonToken>();
			if (!nodes.IsEmpty())
				root["references"] = new JsonArray(nodes);

			var w = new StreamWriter(serializationStream);
			root.Write(w, 0, AllowIdentation);
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

		private JsonToken ToJson(GraphNode node)
		{
			if (node == null)
				return null;

			switch (node.DataType)
			{
				case SerializedDataType.Boolean:
					return new JsonBool((bool)((PrimitiveGraphNode)node).Value);
				case SerializedDataType.Number:
					return new JsonNumber((node as PrimitiveGraphNode).Value);
				case SerializedDataType.String:
					return new JsonString((node as StringGraphNode).Value);
				case SerializedDataType.Enum:
					{
						Enum e = (node as EnumGraphNode).Value;
						Type eType = e.GetType();
						string format = e.ToString().Replace(", ", "|");
						return new JsonString("enum<" + (eType.AssemblyQualifiedName) + ">:" + format);
					}
					break;
				case SerializedDataType.DataSequence:
					return new JsonArray((node as SequenceGraphNode).Select(n => ToJson(n)));
			}

			ObjectGraphNode objNode = node as ObjectGraphNode;
			if (objNode.IsReference)
			{
				return new JsonString("refId@" + objNode.RefId.ToString());
			}
			
			return NodeToJson(objNode);
		}

		private JsonObject NodeToJson(ObjectGraphNode node)
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
					json[it.Current.Key] = ToJson(it.Current.Value);
				}
			}

			return json;
		}

		#endregion

		protected override Graph DeserializeDataGraph(Stream serializationStream)
		{
			throw new System.NotImplementedException();
		}
	}
}

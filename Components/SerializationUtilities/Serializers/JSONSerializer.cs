using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Utilities;

namespace GAIPS.Serialization
{
	public class JSONSerializer : BaseSerializer
	{
		#region JSON Nodes

		private abstract class JToken
		{
			public void Write(TextWriter writer, bool allowIndent)
			{
				Write(writer, 0,allowIndent);
			}

			public abstract void Write(TextWriter writer, int ident, bool allowIndent);

			protected void writeIdentation(TextWriter writer, int ident)
			{
				while (ident > 0)
				{
					writer.Write("\t");
					ident--;
				}
			}

			public sealed override string ToString()
			{
				string str;
				using (StringWriter stream = new StringWriter())
				{
					Write(stream,false);
					str = stream.ToString();
				}
				return str;
			}
		}

		private class JNumber : JToken
		{
			public string number
			{
				get;
				private set;
			}

			public JNumber(string number)
			{
				this.number = number;
			}

			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				writer.Write(number);
			}
		}

		private class JString : JToken
		{
			public string str;

			public JString(string str)
			{
				this.str = str;
			}

			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				writer.Write('\"');
				StringBuilder builder = new StringBuilder(str);
				builder.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\b", "\\b").Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
				writer.Write(builder.ToString());
				writer.Write('\"');
			}
		}

		private class JBool : JToken
		{
			public bool Value;
			public JBool(bool value)
			{
				this.Value = value;
			}

			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				writer.Write(Value.ToString());
			}
		}

		private class JNull : JToken
		{
			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				writer.Write("null");
			}
		}

		private class JObject : JToken
		{
			private Dictionary<string, JToken> m_fields;

			public JObject()
			{
				m_fields = new Dictionary<string, JToken>();
			}

			public JToken this[string key]
			{
				get
				{
					JToken o;
					if (m_fields.TryGetValue(key, out o))
						return o;

					return null;
				}
				set
				{
					m_fields[key] = value;
				}
			}

			public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
			{
				return m_fields.GetEnumerator();
			}

			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				if (allowIndent)
					writeIdentation(writer, ident);
				writer.Write('{');
				if (allowIndent)
					writer.WriteLine();
				
				int max = m_fields.Count;
				int cnt = 0;
				ident++;
				foreach (KeyValuePair<string, JToken> pair in m_fields)
				{
					if (allowIndent)
						writeIdentation(writer, ident);
					writer.Write('"');
					writer.Write(pair.Key);
					writer.Write("\":");
					if (allowIndent)
					{
						if (pair.Value is JObject)
						{
							writer.WriteLine();
						}
						else
							writer.Write(' ');
					}
					pair.Value.Write(writer, ident + 1, allowIndent);
					cnt++;
					if (cnt < max)
					{
						writer.Write(',');
						if (allowIndent)
							writer.WriteLine();
					}
				}
				ident--;
				if (allowIndent)
				{
					writer.WriteLine();
					writeIdentation(writer, ident);
				}
				writer.Write("}");
			}
		}

		private class JArray : JToken, IList<JToken>
		{
			private List<JToken> m_elements;
			public bool AllElementsInSeparatedLines = false;

			public JArray()
			{
				m_elements = new List<JToken>();
			}

			public JArray(IEnumerable<JToken> elements)
			{
				m_elements = new List<JToken>(elements);
			}

			public int IndexOf(JToken item)
			{
				return m_elements.IndexOf(item);
			}

			public void Insert(int index, JToken item)
			{
				m_elements.Insert(index, item);
			}

			public void RemoveAt(int index)
			{
				m_elements.RemoveAt(index);
			}

			public JToken this[int index]
			{
				get
				{
					return m_elements[index];
				}
				set
				{
					m_elements[index] = value;
				}
			}

			public void Add(JToken item)
			{
				m_elements.Add(item);
			}

			public void Clear()
			{
				m_elements.Clear();
			}

			public bool Contains(JToken item)
			{
				return m_elements.Contains(item);
			}

			public void CopyTo(JToken[] array, int arrayIndex)
			{
				m_elements.CopyTo(array, arrayIndex);
			}

			public int Count
			{
				get { return m_elements.Count; }
			}

			public bool IsReadOnly
			{
				get { return false; }
			}

			public bool Remove(JToken item)
			{
				return m_elements.Remove(item);
			}

			public IEnumerator<JToken> GetEnumerator()
			{
				return m_elements.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public override void Write(TextWriter writer, int ident, bool allowIndent)
			{
				writer.Write("[");
				int max = m_elements.Count;
				int cnt = 0;
				ident++;
				foreach (JToken v in m_elements)
				{
					if (v is JObject)
					{
						writer.WriteLine();
						v.Write(writer, ident - 1, allowIndent);
					}
					else
					{
						if (AllElementsInSeparatedLines)
						{
							writer.WriteLine();
							writeIdentation(writer, ident);
						}
						v.Write(writer, ident, allowIndent);
					}

					cnt++;
					if (cnt < max)
						writer.Write(", ");
				}
				ident--;
				if (AllElementsInSeparatedLines)
				{
					writer.WriteLine();
					writeIdentation(writer, ident);
				}
				writer.Write("]");
			}
		}

		#endregion

		#region JSON Parsing

		private JToken ReadValue(StreamReader reader)
		{
			JToken node = null;

			readEmptyCharacters(reader);
			char start = (char)reader.Peek();
			switch (start)
			{
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

		private JToken ReadObject(StreamReader reader)
		{
			JObject obj = new JObject();
			reader.Read();	// read '{'
			readEmptyCharacters(reader);
			char c = (char)reader.Peek();
			if (c == '}')
			{
				reader.Read();
				return obj;
			}

			while (!reader.EndOfStream)
			{
				if (c != '"')
					throw new IOException("Invalid JSON format");

				string field = ReadString(reader);
				readEmptyCharacters(reader);
				c = (char)reader.Read();
				if (c != ':')
					throw new IOException("Invalid JSON format");

				JToken value = ReadValue(reader);
				readEmptyCharacters(reader);

				obj[field] = value;

				c = (char)reader.Read();
				if (c == '}')
					return obj;

				if (c != ',')
					throw new IOException("Invalid JSON format");

				readEmptyCharacters(reader);
				c = (char)reader.Peek();
			}

			throw new IOException("End of Stream Reached without finishing parsing object");
		}

		private JToken ReadArray(StreamReader reader)
		{
			JArray array = new JArray();
			reader.Read();	// read '['
			readEmptyCharacters(reader);
			while ((char)reader.Peek() != ']')
			{
				JToken val = ReadValue(reader);
				array.Add(val);

				readEmptyCharacters(reader);
				char c = (char)reader.Peek();
				if (c == ',')
					reader.Read();
				else if (c != ']')
					throw new IOException("Invalid JSON format: Invalid array separator. Expected ',' or ']' - Found'" + c + "'");
			}
			reader.Read(); //Read ']'
			return array;
		}

		private const string NUMBER_PATTERN = @"^-?(0|[1-9]\d*)(\.\d+)?(e(-|\+)?\d+)?$";
		private static readonly Regex _numberRegex = new Regex(NUMBER_PATTERN);

		private JToken ReadPrimitive(StreamReader reader)
		{
			StringBuilder builder = new StringBuilder();
			while (!reader.EndOfStream)
			{
				char c = (char)reader.Peek();
				if ((c == ',') || (c == ']') || (c == '}') || Char.IsWhiteSpace(c))
					break;

				builder.Append((char)reader.Read());
			}

			string primitive = builder.ToString().ToLower();
			if (_numberRegex.IsMatch(primitive))
			{
				return new JNumber(primitive);
			}

			if (primitive == "true")
				return new JBool(true);

			if (primitive == "false")
				return new JBool(false);

			if (primitive == "null")
				return new JNull();

			throw new IOException("Invalid JSON format: Invalid primitive \"" + primitive + "\"");
		}

		private static readonly char[] hexBuffer = new char[4];
		private string ReadString(StreamReader reader)
		{
			reader.Read();	// read '"'
			StringBuilder builder = new StringBuilder();
			bool isControl = false;
			while (!reader.EndOfStream)
			{
				char c = (char)reader.Read();
				if (isControl)
				{
					switch (c)
					{
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
								if (reader.ReadBlock(hexBuffer, 0, 4) < 4)
									throw new IOException("Invalid JSON format.");

								string hexString = new string(hexBuffer);
								long result = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

								char hexChar = (char)result;
								builder.Append(hexChar);
							}

							break;
						default:
							throw new IOException("Invalid JSON format.");
					}
					isControl = false;
				}
				else
				{
					if (c == '"')
						break;
					else if (c == '\\')
					{
						isControl = true;
					}
					else
					{
						builder.Append(c);
					}
				}
			}
			return builder.ToString();
		}

		private void readEmptyCharacters(StreamReader reader)
		{
			while (!reader.EndOfStream && Char.IsWhiteSpace((char)reader.Peek()))
			{
				reader.Read();
			}
		}

		#endregion

		public bool AllowIdentation = true;

		#region Serialization

		protected override void SerializeDataGraph(Stream serializationStream, SerializationGraph graph)
		{
			JObject root = new JObject();
			root["root"] = ToJson(graph.Root);

			var nodes = graph.GetReferences().Select(n => NodeToJson(n)).Cast<JToken>();
			if (!nodes.IsEmpty())
				root["references"] = new JArray(nodes);

			var w = new StreamWriter(serializationStream);
			root.Write(w, 0, AllowIdentation);
			w.Flush();
		}

		private JToken ToJson(SerializationGraphNode node)
		{
			if (node == null)
				return new JNull();

			if (node is StringGraphNode)
				return new JString((node as StringGraphNode).Value);

			if (node is PrimitiveGraphNode)
				return new JNumber((node as PrimitiveGraphNode).Value.ToString());
			
			if (node is SequenceGraphNode)
				return new JArray((node as SequenceGraphNode).Select(n => ToJson(n)));
			
			if (node is EnumGraphNode)
			{
				Enum e = (node as EnumGraphNode).Value;
				Type eType = e.GetType();
				string format = e.ToString().Replace(", ", "|");
				return new JString("enum<" + (eType.AssemblyQualifiedName) + ">:" + format);
			}

			ObjectGraphNode objNode = node as ObjectGraphNode;
			if (objNode.IsReference)
			{
				return new JString("refId@" + objNode.RefId.ToString());
			}
			
			return NodeToJson(objNode);
		}

		private JObject NodeToJson(ObjectGraphNode node)
		{
			JObject json = new JObject();

			if (node.IsReference)
			{
				if (node.RefId >= 0)
					json["refId"] = new JNumber(node.RefId.ToString());
			}

			if (node.Class != null)
			{
				json["class"] = new JString(node.Class.AssemblyQualifiedName);
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

		protected override SerializationGraph DeserializeDataGraph(Stream serializationStream)
		{
			throw new System.NotImplementedException();
		}
	}
}

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AtomicPanda.Serialization.SerializationGraph;

namespace AtomicPanda.Serialization
{
	public class BinarySerializer : BaseSerializer
	{
		#region Serialization

		protected override void SerializeDataGraph(Stream serializationStream, Graph graph)
		{
			var writer = new BinaryWriter(serializationStream, Encoding.UTF8);
			//Write type collections
			WriteTypes(writer,graph);
			//Write referencedObjects and root
			var nodes = graph.GetReferences().Cast<IGraphNode>().Append(graph.Root).ToArray();
			writer.Write((uint)nodes.Length);
			foreach (var n in nodes)
			{
				WriteNode(writer, n);
			}
			writer.Flush();
		}

		private void WriteTypes(BinaryWriter writer, Graph graph)
		{
			var group = graph.GetRegistedTypes().GroupBy(t => t.ClassType.Assembly).ToArray();
			writer.Write((byte)group.Length);
			foreach (var g in group)
			{
				writer.Write(g.Key.FullName);
				var types = g.ToArray();
				writer.Write((byte)types.Length);
				foreach (var t in types)
				{
					writer.Write(t.TypeId);
					writer.Write(t.ClassType.FullName);
				}
			}
		}

		private void WriteNode(BinaryWriter writer, IGraphNode node)
		{
			if (node == null)
			{
				writer.Write((byte)0);
				return;
			}

			switch (node.DataType)
			{
				case SerializedDataType.Boolean:
				case SerializedDataType.Number:
				{
					var n = node as IPrimitiveGraphNode;
					byte code = 1 << 5;
					var value = n.Value;
					TypeCode typeCode = Type.GetTypeCode(value.GetType());
					code |= (byte) typeCode;
					writer.Write(code);
					switch (typeCode)
					{
						case TypeCode.Boolean:
							writer.Write((bool)value);
							break;
						case TypeCode.Char:
							writer.Write((char)value);
							break;
						case TypeCode.SByte:
							writer.Write((sbyte)value);
							break;
						case TypeCode.Byte:
							writer.Write((byte)value);
							break;
						case TypeCode.Int16:
							writer.Write((short)value);
							break;
						case TypeCode.UInt16:
							writer.Write((ushort)value);
							break;
						case TypeCode.Int32:
							writer.Write((int)value);
							break;
						case TypeCode.UInt32:
							writer.Write((uint)value);
							break;
						case TypeCode.Int64:
							writer.Write((long)value);
							break;
						case TypeCode.UInt64:
							writer.Write((ulong)value);
							break;
						case TypeCode.Single:
							writer.Write((float)value);
							break;
						case TypeCode.Double:
							writer.Write((double)value);
							break;
						case TypeCode.Decimal:
							writer.Write((decimal)value);
							break;
						case TypeCode.DateTime:
						{
							var d = (DateTime) value;
							writer.Write(d.ToFileTimeUtc());
						}
							break;
						default:
							throw new ArgumentOutOfRangeException("Not supported "+typeCode);
					}
				}
					break;
				case SerializedDataType.String:
				{
					var n = node as IStringGraphNode;
					byte code = 2 << 5;
					writer.Write(code);
					writer.Write(n.Value);
				}
				break;
				case SerializedDataType.DataSequence:
				{
					var nodes = node as ISequenceGraphNode;
					byte code = 3 << 5;
					writer.Write(code);
					writer.Write(nodes.Length);
					foreach (var n in nodes)
						WriteNode(writer,n);
				}
				break;
				case SerializedDataType.Type:
				{
					var n = node as ITypeGraphNode;
					byte code = 4 << 5;
					writer.Write(code);
					writer.Write(n.TypeId);
				}
				break;
				case SerializedDataType.Object:
				{
					var n = node as IObjectGraphNode;
					byte code = 5 << 5;
					if (n.IsReferedMultipleTimes)
					{
						code |= 1 << 4;
						writer.Write(code);
						writer.Write(n.RefId);
					}
					else
					{
						writer.Write(code);
						WriteObjectNode(writer, n);
					}
				}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void WriteObjectNode(BinaryWriter writer, IObjectGraphNode node)
		{
			byte code = 0;
			if (node.IsReferedMultipleTimes && node.RefId >= 0)
				code |= 0x1;
			if (node.ObjectType != null)
				code |= 0x2;

			writer.Write(code);
			if (node.IsReferedMultipleTimes && node.RefId >= 0)
				writer.Write(node.RefId);
			if (node.ObjectType != null)
				writer.Write(node.ObjectType.TypeId);

			writer.Write(node.NumOfFields);
			using (var it = node.GetEnumerator())
			{
				while (it.MoveNext())
				{
					writer.Write(it.Current.FieldName);
					WriteNode(writer, it.Current.FieldNode);
				}
			}
		}

		#endregion

		#region Deserialization

		protected override void DeserializeDataGraph(Stream serializationStream, Graph graph)
		{
			var reader = new BinaryReader(serializationStream,Encoding.UTF8);
			//Read header to retrive saved types
			ReadTypes(reader,graph);

			//Read serialized data array
			//Last Serialized node is root. The rest are references
			uint numOfNodesStored = reader.ReadUInt32();
			for (uint i = 0; i < numOfNodesStored-1; i++)
			{
				ReadNode(reader, graph);
			}
			graph.Root = ReadNode(reader, graph);
		}

		private void ReadTypes(BinaryReader reader, Graph graph)
		{
			byte numOfAssemblies = reader.ReadByte();
			for (byte i = 0; i < numOfAssemblies; i++)
			{
				var assemblyName = reader.ReadString();
				var assembly = Assembly.Load(assemblyName);
				var numOfTypes = reader.ReadByte();
				for (byte j = 0; j < numOfTypes; j++)
				{
					var typeId = reader.ReadByte();
					var typeName = reader.ReadString();
					var type = assembly.GetType(typeName);
					graph.RegistTypeEntry(type,typeId);
				}
			}
		}

		private IGraphNode ReadNode(BinaryReader reader, Graph graph)
		{
			byte code = reader.ReadByte();
			byte nodeCode = (byte) (code >> 5);
			byte codeData = (byte)(code & 0x1f);

			switch (nodeCode)
			{
				case 0:
					return null;
				case 1: //SerializedDataType.Boolean or SerializedDataType.Number
				{
					TypeCode typeCode = (TypeCode) codeData;
					ValueType value;
					switch (typeCode)
					{
						case TypeCode.Boolean:
							value = reader.ReadBoolean();
							break;
						case TypeCode.Char:
							value = reader.ReadChar();
							break;
						case TypeCode.SByte:
							value = reader.ReadSByte();
							break;
						case TypeCode.Byte:
							value = reader.ReadByte();
							break;
						case TypeCode.Int16:
							value = reader.ReadInt16();
							break;
						case TypeCode.UInt16:
							value = reader.ReadUInt16();
							break;
						case TypeCode.Int32:
							value = reader.ReadInt32();
							break;
						case TypeCode.UInt32:
							value = reader.ReadUInt32();
							break;
						case TypeCode.Int64:
							value = reader.ReadInt64();
							break;
						case TypeCode.UInt64:
							value = reader.ReadUInt64();
							break;
						case TypeCode.Single:
							value = reader.ReadSingle();
							break;
						case TypeCode.Double:
							value = reader.ReadDouble();
							break;
						case TypeCode.Decimal:
							value = reader.ReadDecimal();
							break;
						case TypeCode.DateTime:
						{
							long time = reader.ReadInt64();
							value = DateTime.FromFileTimeUtc(time);
						}
							break;
						default:
							throw new ArgumentOutOfRangeException("Not supported " + typeCode);
					}
					return graph.BuildPrimitiveNode(value);
				}
				case 2: //SerializedDataType.String
					return graph.BuildStringNode(reader.ReadString());
				case 3: //SerializedDataType.DataSequence
				{
					var n = graph.BuildSequenceNode();
					int numOfElements = reader.ReadInt32();
					for (int i = 0; i < numOfElements; i++)
					{
						var node = ReadNode(reader, graph);
						n.Add(node);
					}
					return n;
				}
				case 4: //SerializedDataType.Type
				{
					return graph.GetTypeEntry(reader.ReadByte());
				}
				case 5: //SerializedDataType.Object
				{
					codeData = (byte)(codeData >> 4);
					if (codeData == 1)
					{
						int refId = reader.ReadInt32();
						return graph.GetObjectDataForRefId(refId);
					}
					if (codeData == 0)
						return ReadObjectNode(reader, graph);

					throw new Exception("Invalid bytecode");
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private IObjectGraphNode ReadObjectNode(BinaryReader reader, Graph graph)
		{
			byte code = reader.ReadByte();

			IObjectGraphNode node;
			if ((code & 0x1) == 0x1) //IsReference
			{
				int refId = reader.ReadInt32();
				node = graph.GetObjectDataForRefId(refId);
			}
			else
				node = graph.CreateObjectData();

			if ((code & 0x2) == 0x2)
			{
				byte typeId = reader.ReadByte();
				node.ObjectType = graph.GetTypeEntry(typeId);
			}
			else
				node.ObjectType = null;

			int numOfFields = reader.ReadInt32();
			for (int i = 0; i < numOfFields; i++)
			{
				var fieldName = reader.ReadString();
				var fieldNode = ReadNode(reader, graph);
				node[fieldName] = fieldNode;
			}
			return node;
		}

		#endregion
	}
}
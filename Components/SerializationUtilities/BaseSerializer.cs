using GAIPS.Serialization.SerializationGraph;
using System;
using System.IO;

namespace GAIPS.Serialization
{
	public abstract partial class BaseSerializer
	{
		public BaseSerializer(){}

		public T Deserialize<T>(Stream serializationStream)
		{
			return (T)Deserialize(serializationStream, typeof(T));
		}

		public object Deserialize(Stream serializationStream, Type returnType = null)
		{
			var serGraph = DeserializeDataGraph(serializationStream);
			return serGraph.RebuildObject(returnType);
		}

		public void Serialize(Stream serializationStream, object graph)
		{
			Graph serGraph = new Graph(graph,this);
			SerializeDataGraph(serializationStream, serGraph);
		}

		public abstract IGraphNode EnumToGraphNode(Enum enumValue, Graph serializationGraph);
		public abstract Enum GraphNodeToEnum(IGraphNode node, Type enumType);

		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract Graph DeserializeDataGraph(Stream serializationStream);
	}
}

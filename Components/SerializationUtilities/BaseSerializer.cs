using GAIPS.Serialization.SerializationGraph;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace GAIPS.Serialization
{
	public abstract class BaseSerializer
	{
		public GraphFormatterSelector FormatSelector { get; private set; }

		protected BaseSerializer()
		{
			FormatSelector = new GraphFormatterSelector();
		}

		public T Deserialize<T>(Stream serializationStream)
		{
			return (T)Deserialize(serializationStream, typeof(T));
		}

		public object Deserialize(Stream serializationStream, Type returnType = null)
		{
			var graph = new Graph(FormatSelector);
			DeserializeDataGraph(serializationStream,graph);
			return graph.RebuildObject(returnType);
		}

		public void Serialize(Stream serializationStream, object graph)
		{
			Graph serGraph = new Graph(graph,FormatSelector);
			SerializeDataGraph(serializationStream, serGraph);
		}
		/*
		public abstract IGraphNode EnumToGraphNode(Enum enumValue, Graph serializationGraph);
		public abstract Enum GraphNodeToEnum(IGraphNode node, Type enumType);
		*/
		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract void DeserializeDataGraph(Stream serializationStream, Graph graph);
	}
}

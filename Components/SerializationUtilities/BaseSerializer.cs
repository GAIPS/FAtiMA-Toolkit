using System;
using System.IO;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public abstract class BaseSerializer : ISerializer
	{
		public GraphFormatterSelector FormatSelector { get; }
		public ISerializationContext Context { get; }

		protected BaseSerializer()
		{
			FormatSelector = new GraphFormatterSelector();
			Context = new SerializationContext();
		}

		public T Deserialize<T>(Stream serializationStream)
		{
			return (T)Deserialize(serializationStream, typeof(T));
		}

		public object Deserialize(Stream serializationStream, Type returnType = null)
		{
			var graph = new Graph(FormatSelector,Context);
			DeserializeDataGraph(serializationStream, graph);
			return graph.DeserializeObject(returnType);
		}

		public void Serialize(Stream serializationStream, object graph)
		{
			if (!graph.GetType().IsSerializable)
				throw new Exception($"Instances of {graph.GetType()} are not serializable.");  //TODO add a better exception

			Graph serGraph = new Graph(graph, FormatSelector,Context);
			SerializeDataGraph(serializationStream, serGraph);
		}

		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract void DeserializeDataGraph(Stream serializationStream, Graph graph);
	}
}

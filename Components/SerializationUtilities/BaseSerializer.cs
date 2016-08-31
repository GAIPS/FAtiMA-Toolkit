using System;
using System.IO;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities
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
			Graph serGraph = ComputeGraph(graph);
			SerializeDataGraph(serializationStream, serGraph);
		}

		protected Graph ComputeGraph(object graph)
		{
			if (!graph.GetType().IsSerializable())
				throw new Exception($"Instances of {graph.GetType()} are not serializable.");  //TODO add a better exception

			return new Graph(graph, FormatSelector, Context);
		}

		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract void DeserializeDataGraph(Stream serializationStream, Graph graph);
	}
}

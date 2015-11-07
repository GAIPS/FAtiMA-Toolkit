using GAIPS.Serialization.SerializationGraph;
using System;
using System.IO;

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
			if(!graph.GetType().IsSerializable)
				throw new Exception(string.Format("Instances of {0} are not serializable.",graph.GetType()));	//TODO add a better exception

			Graph serGraph = new Graph(graph,FormatSelector);
			SerializeDataGraph(serializationStream, serGraph);
		}

		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract void DeserializeDataGraph(Stream serializationStream, Graph graph);
	}
}

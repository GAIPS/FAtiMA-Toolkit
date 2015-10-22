using System;
using System.IO;

namespace GAIPS.Serialization
{
	public abstract partial class BaseSerializer
	{
		public BaseSerializer(){}

		public object Deserialize(Stream serializationStream)
		{
			var serGraph = DeserializeDataGraph(serializationStream);
			throw new NotImplementedException();
		}

		public void Serialize(Stream serializationStream, object graph)
		{
			SerializationGraph serGraph = new SerializationGraph();
			serGraph.Root = SerializationServices.BuildNode(graph, null, null, serGraph);
			SerializeDataGraph(serializationStream, serGraph);
		}

		protected abstract void SerializeDataGraph(Stream serializationStream, SerializationGraph graph);
		protected abstract SerializationGraph DeserializeDataGraph(Stream serializationStream);
	}
}

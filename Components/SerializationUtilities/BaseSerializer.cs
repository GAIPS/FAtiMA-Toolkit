using GAIPS.Serialization.SerializationGraph;
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
			Graph serGraph = new Graph();
			serGraph.Root = SerializationServices.BuildNode(graph, null, null, serGraph);
			SerializeDataGraph(serializationStream, serGraph);
		}

		protected abstract void SerializeDataGraph(Stream serializationStream, Graph graph);
		protected abstract Graph DeserializeDataGraph(Stream serializationStream);
	}
}

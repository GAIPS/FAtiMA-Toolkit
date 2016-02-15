using System;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public interface IGraphFormatter
	{
		IGraphNode ObjectToGraphNode(object value, Graph serializationGraph);
		object GraphNodeToObject(IGraphNode node, Type objectType);
	}
}
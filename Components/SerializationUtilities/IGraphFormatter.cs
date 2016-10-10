using System;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities
{
	public interface IGraphFormatter
	{
		IGraphNode ObjectToGraphNode(object value, Graph serializationGraph);
		object GraphNodeToObject(IGraphNode node, Type objectType);
	}
}
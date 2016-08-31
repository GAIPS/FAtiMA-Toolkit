using System;
using SerializationUtilities;
using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;

namespace Conditions
{
	[DefaultSerializationSystem(typeof(Condition),true)]
	sealed class ConditionSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			return serializationGraph.BuildStringNode(value.ToString());
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			return Condition.Parse(((IStringGraphNode) node).Value);
		}
	}
}
using System;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

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
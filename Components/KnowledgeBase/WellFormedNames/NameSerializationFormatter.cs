using System;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace KnowledgeBase.WellFormedNames
{
	[DefaultSerializationSystem(typeof(Name),true)]
	public class NameSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			return serializationGraph.BuildStringNode(value.ToString());
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			return Name.Parse(((IStringGraphNode)node).Value);
		}
	}
}
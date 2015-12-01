using System;
using System.Linq;
using System.Runtime.InteropServices;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace KnowledgeBase.WellFormedNames
{
	[DefaultSerializationSystem(typeof(Name), true)]
	class NameSerializationFormatter : IGraphFormatter
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

	[DefaultSerializationSystem(typeof(Substitution))]
	class SubstitutionSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			return serializationGraph.BuildStringNode(value.ToString());
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			return new Substitution(((IStringGraphNode) node).Value);
		}
	}

	[DefaultSerializationSystem(typeof(SubstitutionSet))]
	class SubstitutionSetSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			var seq = serializationGraph.BuildSequenceNode();
			seq.AddRange(((SubstitutionSet)value).Select(s => serializationGraph.BuildNode(s, typeof(Substitution))));
			return seq;
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			var seq = (ISequenceGraphNode) node;
			return new SubstitutionSet(seq.Select(g => g.RebuildObject<Substitution>()));
		}
	}
}
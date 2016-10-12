using System;
using System.Linq;
using SerializationUtilities;
using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;
using Utilities;

namespace WellFormedNames
{
	[DefaultSerializationSystem(typeof(Name), true)]
	class NameSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			var n = (Name) value;
			if (n.IsPrimitive)
			{
				var obj = n.GetValue();
				if (obj.GetType().GetTypeCode() == Utilities.TypeCode.String)
					return serializationGraph.BuildStringNode((string)obj);
				return serializationGraph.BuildPrimitiveNode((ValueType)obj);
			}
			return serializationGraph.BuildStringNode(n.ToString());
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			var n = node as IStringGraphNode;
			if (n != null)
				return Name.BuildName(n.Value);
			return Name.BuildName(((IPrimitiveGraphNode)node).Value);
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
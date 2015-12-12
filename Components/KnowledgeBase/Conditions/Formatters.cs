﻿using System;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace KnowledgeBase.Conditions
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

	[DefaultSerializationSystem(typeof(ConditionEvaluatorSet))]
	sealed class ConditionSetSerializationFormatter : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			ConditionEvaluatorSet evaluatorSet = (ConditionEvaluatorSet) value;
			ISequenceGraphNode seq = serializationGraph.BuildSequenceNode();
			seq.AddRange(evaluatorSet.Select(c => serializationGraph.BuildNode(c, typeof (Condition))));
			return seq;
		}

		public object GraphNodeToObject(IGraphNode node, Type objectType)
		{
			ISequenceGraphNode seq = (ISequenceGraphNode) node;
			var c = new ConditionEvaluatorSet();
			if (seq.Length != 0)
				c.UnionWith(seq.Select(n => (Condition)n.RebuildObject(typeof(Condition))));
			
			return c;
		}
	}
}
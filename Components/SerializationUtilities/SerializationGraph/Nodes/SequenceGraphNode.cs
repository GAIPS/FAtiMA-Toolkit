using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections.Generic;

namespace GAIPS.Serialization.SerializationGraph
{
	public interface ISequenceGraphNode : IGraphNode, IEnumerable<IGraphNode>
	{
		int Length {get;}
		void Add(IGraphNode node);
		void AddRange(IEnumerable<IGraphNode> nodes);
	}

	public partial class Graph
	{
		private sealed class SequenceGraphNode : BaseGraphNode, ISequenceGraphNode
		{
			private List<IGraphNode> m_elements = new List<IGraphNode>();

			public override SerializedDataType DataType
			{
				get { return SerializedDataType.DataSequence; }
			}

			public SequenceGraphNode(Graph parentGraph) : base(parentGraph){}

			public int Length
			{
				get { return m_elements.Count; }
			}

			public void Add(IGraphNode node)
			{
				m_elements.Add(node);
			}

			public void AddRange(IEnumerable<IGraphNode> nodes)
			{
				m_elements.AddRange(nodes);
			}

			public IEnumerator<IGraphNode> GetEnumerator()
			{
				return m_elements.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return m_elements.GetEnumerator();
			}

			public override object ExtractObject(Type requestedType)
			{
				throw new NotImplementedException();
			}
		}
	}
}

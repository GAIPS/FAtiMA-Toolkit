using GAIPS.Serialization.SerializationGraph;
using System.Collections.Generic;

namespace GAIPS.Serialization
{
	public sealed class SequenceGraphNode : GraphNode, IEnumerable<GraphNode>
	{
		private List<GraphNode> m_elements = new List<GraphNode>();

		public override SerializedDataType DataType
		{
			get { return SerializedDataType.DataSequence; }
		}

		public int Lenght
		{
			get { return m_elements.Count; }
		}

		public void Add(GraphNode node)
		{
			m_elements.Add(node);
		}

		public IEnumerator<GraphNode> GetEnumerator()
		{
			return m_elements.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

using System.Collections.Generic;

namespace GAIPS.Serialization
{
	public sealed class SequenceGraphNode : SerializationGraphNode, IEnumerable<SerializationGraphNode>
	{
		private List<SerializationGraphNode> m_elements = new List<SerializationGraphNode>();

		public void Add(SerializationGraphNode node)
		{
			m_elements.Add(node);
		}

		public IEnumerator<SerializationGraphNode> GetEnumerator()
		{
			return m_elements.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

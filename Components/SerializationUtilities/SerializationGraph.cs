using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAIPS.Serialization
{
	public class SerializationGraph
	{
		private int m_idCounter = 0;
		private SortedDictionary<long, ObjectGraphNode> m_refs = new SortedDictionary<long, ObjectGraphNode>();
		private Dictionary<object, long> m_links = new Dictionary<object, long>();
		public SerializationGraphNode Root { get; set; }

		public ObjectGraphNode CreateObjectData()
		{
			return new ObjectGraphNode(-1);
		}

		public bool GetObjectNode(object obj, out ObjectGraphNode dataNode)
		{
			long id;
			if (m_links.TryGetValue(obj, out id))
			{
				dataNode = m_refs[id];
				return false;
			}

			dataNode = new ObjectGraphNode(m_idCounter++);
			m_refs[dataNode.RefId] = dataNode;
			m_links[obj] = dataNode.RefId;
			return true;
		}

		public IEnumerable<ObjectGraphNode> GetReferences()
		{
			return m_refs.Values.Where(n => n.IsReference);
		}
	}
}

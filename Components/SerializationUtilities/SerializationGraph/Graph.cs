using System;
using System.Collections.Generic;
using System.Linq;

namespace GAIPS.Serialization.SerializationGraph
{
	public partial class Graph
	{
		private int m_idCounter = 0;
		private SortedDictionary<long, ObjectGraphNode> m_refs = new SortedDictionary<long, ObjectGraphNode>();
		private Dictionary<object, long> m_links = new Dictionary<object, long>();
		public GraphNode Root { get; set; }

		private byte m_typeidCounter = 0;
		private Dictionary<Type, TypeEntry> m_registedTypes = new Dictionary<Type, TypeEntry>();

		public TypeEntry GetTypeEntry(Type type)
		{
			TypeEntry t;
			if (m_registedTypes.TryGetValue(type, out t))
				return t;

			t = new TypeEntry(type, m_typeidCounter++);
			m_registedTypes[type] = t;
			return t;
		}

		public IEnumerable<TypeEntry> GetRegistedTypes()
		{
			return m_registedTypes.Values;
		}

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

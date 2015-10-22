using System;
using System.Collections.Generic;

namespace GAIPS.Serialization
{
	public sealed class ObjectGraphNode : SerializationGraphNode
	{
		private Dictionary<string, SerializationGraphNode> m_fields = new Dictionary<string, SerializationGraphNode>();
		private HashSet<SerializationGraphNode> m_referencedBy = new HashSet<SerializationGraphNode>();

		public readonly int RefId;
		public Type Class;
		public int ReferenceCount
		{
			get
			{
				return m_referencedBy.Count;
			}
		}
		public bool IsReference
		{
			get
			{
				return ReferenceCount > 1;
			}
		}

		public ObjectGraphNode(int refId)
		{
			this.Class = null;
			this.RefId = refId;
		}

		public SerializationGraphNode this[string fieldName]
		{
			get
			{
				SerializationGraphNode node;
				if (m_fields.TryGetValue(fieldName, out node))
					return node;
				return null;
			}
			set
			{
				if (m_fields.ContainsKey(fieldName))
					throw new Exception("Duplicated field named " + fieldName);

				m_fields[fieldName] = value;
			}
		}

		public void AddReferencedBy(SerializationGraphNode node)
		{
			m_referencedBy.Add(node);
		}

		public IEnumerator<KeyValuePair<string, SerializationGraphNode>> GetEnumerator()
		{
			return m_fields.GetEnumerator();
		}
	}
}

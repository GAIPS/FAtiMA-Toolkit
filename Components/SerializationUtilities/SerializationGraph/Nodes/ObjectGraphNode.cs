using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections.Generic;

namespace GAIPS.Serialization
{
	public sealed class ObjectGraphNode : GraphNode
	{
		private Dictionary<string, GraphNode> m_fields = new Dictionary<string, GraphNode>();
		private HashSet<GraphNode> m_referencedBy = new HashSet<GraphNode>();

		public TypeEntry ObjectType
		{
			get;
			set;
		}

		public override SerializedDataType DataType
		{
			get { return SerializedDataType.Object; }
		}

		public readonly int RefId;
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
			this.ObjectType = null;
			this.RefId = refId;
		}

		public GraphNode this[string fieldName]
		{
			get
			{
				GraphNode node;
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

		public void AddReferencedBy(GraphNode node)
		{
			m_referencedBy.Add(node);
		}

		public IEnumerator<KeyValuePair<string, GraphNode>> GetEnumerator()
		{
			return m_fields.GetEnumerator();
		}
	}
}

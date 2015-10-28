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

			public override bool CanMatchType(Type requestedType)
			{
				return requestedType == null || requestedType.IsArray;
			}

			public override object ExtractObject(Type requestedType)
			{
				if(requestedType!=null && !requestedType.IsArray)
					throw new Exception("requested type is not a array");	//TODO better exception

				Type elementType = requestedType!=null?requestedType.GetElementType():null;
				object[] elements = new object[Length];
				for (int i = 0; i < m_elements.Count; i++)
				{
					if(m_elements[i]==null)
						continue;
					if (elementType != null)
					{
						while (elementType != null && !m_elements[i].CanMatchType(elementType))
						{
							elementType = elementType.BaseType;
						}
					}
					var o = m_elements[i].RebuildObject(elementType);
					if(elementType==null)
						elementType = o.GetType();
					elements[i] = o;
				}
				var a = Array.CreateInstance(elementType, elements.Length);
				elements.CopyTo(a,0);
				return a;
			}
		}
	}
}

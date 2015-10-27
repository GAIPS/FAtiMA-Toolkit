using GAIPS.Serialization.SerializationGraph;
using System;
using Utilities;

namespace GAIPS.Serialization.SerializationGraph
{
	public interface IPrimitiveGraphNode : IGraphNode
	{
		ValueType Value {get;}
	}

	public partial class Graph
	{
		private sealed class PrimitiveGraphNode : BaseGraphNode, IPrimitiveGraphNode
		{
			private ValueType m_value;
			private readonly bool m_isNumber;

			public PrimitiveGraphNode(ValueType value, Graph parentGraph) : base(parentGraph)
			{
				m_isNumber = value.GetType().IsNumeric();
				m_value = value;
			}

			public ValueType Value
			{
				get { return m_value; }
			}

			public override SerializedDataType DataType
			{
				get { return m_isNumber ? SerializedDataType.Number : SerializedDataType.Boolean; }
			}

			public override object ExtractObject(Type requestedType)
			{
				if (requestedType == null)
					return m_value;
				return Convert.ChangeType(m_value, requestedType);
			}
		}
	}
}

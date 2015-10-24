using GAIPS.Serialization.SerializationGraph;
using System;
using Utilities;

namespace GAIPS.Serialization
{
	public sealed class PrimitiveGraphNode : GraphNode
	{
		private readonly bool m_isNumber;
		public readonly ValueType Value;

		public override SerializedDataType DataType
		{
			get { return m_isNumber ? SerializedDataType.Number : SerializedDataType.Boolean; }
		}

		public PrimitiveGraphNode(ValueType value)
		{
			this.m_isNumber = value.GetType().IsNumeric();
			this.Value = value;
		}
	}
}

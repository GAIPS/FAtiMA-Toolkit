using System;

namespace GAIPS.Serialization
{
	public sealed class PrimitiveGraphNode : SerializationGraphNode
	{
		public readonly ValueType Value;

		public PrimitiveGraphNode(ValueType value)
		{
			this.Value = value;
		}
	}
}

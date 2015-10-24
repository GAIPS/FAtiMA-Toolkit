using System;

namespace GAIPS.Serialization.SerializationGraph.Nodes
{
	public sealed class EnumGraphNode : GraphNode
	{
		public readonly Enum Value;

		public override SerializedDataType DataType
		{
			get { return SerializedDataType.Enum; }
		}

		public EnumGraphNode(Enum value)
		{
			this.Value = value;
		}
	}
}

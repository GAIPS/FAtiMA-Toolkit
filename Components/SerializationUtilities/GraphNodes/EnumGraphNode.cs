using System;

namespace GAIPS.Serialization
{
	public sealed class EnumGraphNode : SerializationGraphNode
	{
		public readonly Enum Value;

		public EnumGraphNode(Enum value)
		{
			this.Value = value;
		}
	}
}

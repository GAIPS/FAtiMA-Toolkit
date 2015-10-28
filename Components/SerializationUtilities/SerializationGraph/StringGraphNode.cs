using System;

namespace GAIPS.Serialization.SerializationGraph
{
	public interface IStringGraphNode : IGraphNode
	{
		string Value { get; }
	}

	public partial class Graph
	{
		private sealed class StringGraphNode : BaseGraphNode, IStringGraphNode
		{
			public StringGraphNode(string value, Graph parent) : base(parent)
			{
				Value = value;
			}

			public string Value { get; private set; }

			public override SerializedDataType DataType
			{
				get { return SerializedDataType.String; }
			}

			public override bool CanMatchType(Type requestedType)
			{
				return requestedType == null || requestedType.IsAssignableFrom(typeof (string));
			}

			public override object ExtractObject(Type requestedType)
			{
				return Value;
			}
		}
	}
}
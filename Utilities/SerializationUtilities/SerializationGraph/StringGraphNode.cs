using System;

namespace SerializationUtilities.SerializationGraph
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

			public string Value { get; }

			public override SerializedDataType DataType
			{
				get { return SerializedDataType.String; }
			}

			public override bool CanMatchType(Type requestedType)
			{
				return requestedType == null || TypeTools.IsAssignableFrom(requestedType, typeof (string));
			}

			public override object ExtractObject(Type requestedType)
			{
				return Value;
			}
		}
	}
}
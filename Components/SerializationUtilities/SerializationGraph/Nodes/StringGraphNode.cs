using GAIPS.Serialization.SerializationGraph;
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
			public string Value
			{
				get;
				private set;
			}

			public override SerializedDataType DataType
			{
				get { return SerializedDataType.String; }
			}

			public StringGraphNode(string value, Graph parent) : base(parent)
			{
				this.Value = value;
			}

			public override object ExtractObject(Type requestedType)
			{
				return Value;
			}
		}
	}
}

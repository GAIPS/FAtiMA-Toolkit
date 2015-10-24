using GAIPS.Serialization.SerializationGraph;
namespace GAIPS.Serialization
{
	public sealed class StringGraphNode : GraphNode
	{
		public readonly string Value;

		public override SerializedDataType DataType
		{
			get { return SerializationGraph.SerializedDataType.String; }
		}

		public StringGraphNode(string value)
		{
			this.Value = value;
		}
	}
}

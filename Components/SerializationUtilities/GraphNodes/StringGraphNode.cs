namespace GAIPS.Serialization
{
	public sealed class StringGraphNode : SerializationGraphNode
	{
		public readonly string Value;

		public StringGraphNode(string value)
		{
			this.Value = value;
		}
	}
}

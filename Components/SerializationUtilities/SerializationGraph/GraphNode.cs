namespace GAIPS.Serialization.SerializationGraph
{
	public abstract class GraphNode
	{
		public abstract SerializedDataType DataType
		{
			get;
		}
		/*
		protected SerializationGraph ParentGraph
		{
			get;
			private set;
		}

		protected SerializationGraphNode(SerializationGraph parentGraph)
		{
			this.ParentGraph = parentGraph;
		}
		*/
	}
}

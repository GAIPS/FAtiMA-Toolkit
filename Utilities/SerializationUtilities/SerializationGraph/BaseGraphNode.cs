using System;

namespace SerializationUtilities.SerializationGraph
{
	public partial class Graph
	{
		private abstract class BaseGraphNode : IGraphNode
		{
			internal bool IsRoot;

			protected BaseGraphNode(Graph parentGraph)
			{
				ParentGraph = parentGraph;
			}

			public abstract SerializedDataType DataType { get; }

			public Graph ParentGraph { get; private set; }

			public object RebuildObject(Type requestedType)
			{
				return ParentGraph.RebuildObject(this, requestedType);
			}

			public T RebuildObject<T>()
			{
				return (T)RebuildObject(typeof (T));
			}

			public abstract bool CanMatchType(Type requestedType);

			public abstract object ExtractObject(Type requestedType);
		}
	}
}
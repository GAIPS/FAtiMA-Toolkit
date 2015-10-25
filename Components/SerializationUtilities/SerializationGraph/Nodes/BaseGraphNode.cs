using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAIPS.Serialization.SerializationGraph
{
	public partial class Graph
	{
		private abstract class BaseGraphNode : IGraphNode
		{
			public abstract SerializedDataType DataType {get;}

			public Graph ParentGraph
			{
				get;
				private set;
			}

			public BaseGraphNode(Graph parentGraph)
			{
				this.ParentGraph = parentGraph;
			}

			public object RebuildObject(Type requestedType)
			{
				return ParentGraph.RebuildObject(this, requestedType);
			}

			public abstract object ExtractObject(Type requestedType);
		}
	}
}

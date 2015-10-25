using System;

namespace GAIPS.Serialization.SerializationGraph
{
	public interface IGraphNode
	{
		SerializedDataType DataType
		{
			get;
		}

		Graph ParentGraph
		{
			get;
		}

		object RebuildObject(Type requestedType);
	}
}

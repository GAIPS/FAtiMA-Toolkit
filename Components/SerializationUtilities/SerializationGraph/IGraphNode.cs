using System;

namespace SerializationUtilities.SerializationGraph
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

		bool CanMatchType(Type requestedType);

		object RebuildObject(Type requestedType);
		T RebuildObject<T>();
	}
}

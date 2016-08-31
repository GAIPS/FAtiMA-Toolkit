using System;

namespace SerializationUtilities.SerializationGraph
{
	public interface ITypeGraphNode : IGraphNode
	{
		byte TypeId { get; }
		Type ClassType { get; }
	}

	public partial class Graph
	{
		private class TypeGraphNode : ITypeGraphNode
		{
			public byte TypeId
			{
				get;
				private set;
			}

			public Type ClassType
			{
				get;
			}

			public TypeGraphNode(Type type, byte typeId, Graph parentGraph)
			{
				this.ClassType = type;
				this.TypeId = typeId;
				this.ParentGraph = parentGraph;
			}

			public SerializedDataType DataType
			{
				get { return SerializedDataType.Type;}
			}

			public Graph ParentGraph {get; private set; }

			public bool CanMatchType(Type requestedType)
			{
				return requestedType == null || TypeTools.IsAssignableFrom(typeof (Type), requestedType);
			}

			public object RebuildObject(Type requestedType)
			{
				return ClassType;
			}
			
			public T RebuildObject<T>()
			{
				return (T) RebuildObject(typeof (T));
			}
		}
	}
}

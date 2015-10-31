using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace GAIPS.Serialization.SerializationGraph
{
	public struct FieldEnty
	{
		public readonly string FieldName;
		public readonly IGraphNode FieldNode;

		internal FieldEnty(string fieldName, IGraphNode nodeValue)
		{
			this.FieldName = fieldName;
			this.FieldNode = nodeValue;
		}
	}

	public interface IObjectGraphNode : IGraphNode, IEnumerable<FieldEnty>
	{
		int RefId { get; }
		TypeEntry ObjectType
		{
			get;
			set;
		}

		int ReferenceCount{ get; }
		bool IsReferedMultipleTimes { get;}

		IGraphNode this[string fieldName]{get;set;}
	}

	public partial class Graph
	{
		private sealed class ObjectGraphNode : BaseGraphNode, IObjectGraphNode
		{
			private readonly Dictionary<string, IGraphNode> m_fields = new Dictionary<string, IGraphNode>();
			private readonly HashSet<IGraphNode> m_referencedBy = new HashSet<IGraphNode>();

			public ObjectGraphNode(int refId, Graph parentGraph) : base(parentGraph)
			{
				this.ObjectType = null;
				this.RefId = refId;
			}
			
			public IGraphNode this[string fieldName]
			{
				get
				{
					IGraphNode node;
					return m_fields.TryGetValue(fieldName, out node) ? node : null;
				}
				set
				{
					if (m_fields.ContainsKey(fieldName))
						throw new Exception("Duplicated field named " + fieldName);

					m_fields[fieldName] = value;

					var node = value as ObjectGraphNode;
					if (node != null)
						node.m_referencedBy.Add(this);
				}
			}

			public int RefId
			{
				get;
				private set;
			}

			public TypeEntry ObjectType
			{
				get;
				set;
			}

			public int ReferenceCount
			{
				get { return m_referencedBy.Count; }
			}

			public bool IsReferedMultipleTimes
			{
				get {
					return ReferenceCount > 1 || (ReferenceCount == 1 && IsRoot);
				}
			}

			public IEnumerator<FieldEnty> GetEnumerator()
			{
				return m_fields.Select(p => new FieldEnty(p.Key,p.Value)).GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public override SerializedDataType DataType
			{
				get { return SerializedDataType.Object; }
			}

			public override bool CanMatchType(Type requestedType)
			{
				if (requestedType == null)
					return true;

				if (ObjectType == null)
					return true;

				return requestedType.IsAssignableFrom(ObjectType.ClassType);
			}

			public override object ExtractObject(Type requestedType)
			{
				object buildObject;
				if (IsReferedMultipleTimes)
				{
					if (ParentGraph.TryGetObjectForRefId(RefId, out buildObject))
						return buildObject;
				}

				Type typeToBuild = requestedType;
				if (ObjectType != null)
				{
					Type myType = ObjectType.ClassType;
					if (requestedType != null && !requestedType.IsAssignableFrom(myType))
						throw new Exception("Unable to build object. Requested on type but data has another type");	//TODO better exception
					typeToBuild = myType;
				}

				if (typeToBuild == null)
					throw new Exception("Missing type information. Unable to build object");	//TODO better exception

				if (typeToBuild.IsAbstract || typeToBuild.IsInterface)
					throw new Exception("Cannot create a direct instance of a abstract or interface");	//TODO better exception

				if (typeToBuild.IsArray || typeToBuild.IsPrimitiveData())
				{
					//Handle Boxed Value Types
					IGraphNode boxedValue = m_fields["boxedValue"];
					return boxedValue.RebuildObject(typeToBuild);
				}

				buildObject = SerializationServices.GetUninitializedObject(typeToBuild);
				ParentGraph.LinkObjectToNode(this, buildObject);

				var surrogate = SerializationServices.GetDefaultSerializationSurrogate(typeToBuild);
				surrogate.SetObjectData(ref buildObject, this);
				return buildObject;
			}
		}
	}
}

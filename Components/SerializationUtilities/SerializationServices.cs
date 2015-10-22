using GAIPS.Serialization.Surrogates;
using System;
using System.Collections;
using System.Reflection;

namespace GAIPS.Serialization
{
	public static class SerializationServices
	{
		static SerializationServices()
		{
			SurrogateSelector = new ChainedSurrogateSelector();
			SurrogateSelector.AddSurrogate(typeof(IDictionary), new DictionarySerializationSurrogate());
			SurrogateSelector.AddSurrogate(typeof(IEnumerable), new EnumerableSerializationSurrogate());
		}

		public static ChainedSurrogateSelector SurrogateSelector { get; private set; }

		public static FieldInfo[] GetFieldsToSerialize(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		}

		public static SerializationGraphNode BuildNode(object obj, Type fieldType, SerializationGraphNode referencedBy, SerializationGraph parentGraph)
		{
			if (obj == null)
				return null;

			var objType = obj.GetType();
			if (objType.IsArray)
				return ConstructArrayNodes(obj as IEnumerable, parentGraph);

			if (objType == typeof(string))
				return new StringGraphNode(obj as string);

			ObjectGraphNode objReturnData = null;
			if (objType.IsValueType)
			{
				if (objType.IsPrimitive)
					return new PrimitiveGraphNode(obj as ValueType);

				if (objType.IsEnum)
					return new EnumGraphNode(obj as Enum);

				//Struct type
				objReturnData = parentGraph.CreateObjectData();
				ExtractSerializationData(obj, objType, objReturnData, parentGraph);
			}

			if (objReturnData == null)
			{
				if (referencedBy != null)
				{
					if (parentGraph.GetObjectNode(obj, out objReturnData))
						ExtractSerializationData(obj, objType, objReturnData, parentGraph);

					objReturnData.AddReferencedBy(referencedBy);
				}
				else
				{
					objReturnData = parentGraph.CreateObjectData();
					ExtractSerializationData(obj, objType, objReturnData, parentGraph);
				}
			}

			if ((objReturnData.Class == null) && (objType != fieldType))
				objReturnData.Class = objType;

			return objReturnData;
		}

		private static SequenceGraphNode ConstructArrayNodes(IEnumerable enumerable, SerializationGraph parentGraph)
		{
			SequenceGraphNode array = new SequenceGraphNode();
			IEnumerator it = enumerable.GetEnumerator();
			while (it.MoveNext())
			{
				SerializationGraphNode elem = BuildNode(it.Current, null, array, parentGraph);
				array.Add(elem);
			}
			return array;
		}

		private static void ExtractSerializationData(object obj, Type objType, ObjectGraphNode holder, SerializationGraph parentGraph)
		{
			var surrogate = SurrogateSelector.GetSurrogate(objType);
			surrogate.GetObjectData(obj, holder, parentGraph);
		}

	}
}

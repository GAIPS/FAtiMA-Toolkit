using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections;

namespace GAIPS.Serialization.Surrogates
{
	public sealed class DictionarySerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			IDictionary e = (IDictionary)obj;
			Type objType = e.GetType();
			Type keyType = typeof(object);
			Type valueType = typeof(object);

			if (objType.IsGenericType)
			{
				Type[] gen = objType.GetGenericArguments();
				keyType = gen[0];
				valueType = gen[1];
			}

			ISequenceGraphNode elements = holder.ParentGraph.BuildSequenceNode();
			holder["dictionary"] = elements;
			IDictionaryEnumerator it = e.GetEnumerator();
			while (it.MoveNext())
			{
				IObjectGraphNode entry = holder.ParentGraph.CreateObjectData();
				entry["key"] = holder.ParentGraph.BuildNode(it.Key, keyType, holder);
				entry["value"] = holder.ParentGraph.BuildNode(it.Value, valueType, holder);
				elements.Add(entry);
			}

			//TODO missing add comparer
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			IDictionary d = (IDictionary)obj;
			Type objType = d.GetType();
			Type keyType = typeof(object);
			Type valueType = typeof(object);

			if (objType.IsGenericType)
			{
				Type[] gen = objType.GetGenericArguments();
				keyType = gen[0];
				valueType = gen[1];
			}

			//TODO missing add comparer

			ISequenceGraphNode elements = node["dictionary"] as ISequenceGraphNode;
			foreach (var e in elements)
			{
				IObjectGraphNode entry = e as IObjectGraphNode;
				object key = entry["key"].RebuildObject(keyType);
				object value = entry["value"].RebuildObject(valueType);
				d.Add(key, value);
			}
		}
	}
}

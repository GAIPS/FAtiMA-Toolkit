using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections;

namespace GAIPS.Serialization.Surrogates
{
	public sealed class DictionarySerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, ObjectGraphNode holder, Graph graph)
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

			SequenceGraphNode elements = new SequenceGraphNode();
			holder["dictionary"] = elements;
			IDictionaryEnumerator it = e.GetEnumerator();
			while (it.MoveNext())
			{
				ObjectGraphNode entry = graph.CreateObjectData();
				entry["key"] = SerializationServices.BuildNode(it.Key, keyType, holder,graph);
				entry["value"] = SerializationServices.BuildNode(it.Value, valueType, holder,graph);
				elements.Add(entry);
			}

			//TODO missing add comparer
		}

		public void SetObjectData(ref object obj, ObjectGraphNode node, Graph graph)
		{
			throw new NotImplementedException();
		}
	}
}

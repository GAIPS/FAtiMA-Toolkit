using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities.Surrogates
{
	[DefaultSerializationSystem(typeof(IList), true)]
	public class ListSerializationSurrogate : IGraphFormatter
	{
		public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
		{
			var type = value.GetType();
			Type elemType = null;
			if (type.IsArray)
				elemType = type.GetElementType();
			else if (TypeTools.IsAssignableToGenericType(type,typeof(IList<>)))
				elemType = TypeTools.GetGenericArguments(type)[0];

			var seq = serializationGraph.BuildSequenceNode();
			seq.AddRange(((IList)value).Cast<object>().Select(o => serializationGraph.BuildNode(o, elemType)));
			return seq;
		}

		public object GraphNodeToObject(IGraphNode node, Type type)
		{
			var seq = (ISequenceGraphNode) node;
			Type elemType = null;
			if (type.IsArray)
				elemType = type.GetElementType();
			else if (TypeTools.IsAssignableToGenericType(type, typeof(IList<>)))
				elemType = TypeTools.GetGenericArguments(type)[0];

			var elements = seq.Select(n => n?.RebuildObject(elemType));
			if (type.IsArray)
			{
				var a = Array.CreateInstance(elemType, seq.Length);
				int i = 0;
				foreach (var e in elements)
					a.SetValue(e, i++);

				return a;
			}

			var instance = (IList)Activator.CreateInstance(type);
			foreach (var e in elements)
				instance.Add(e);

			return instance;
		}
	}
}
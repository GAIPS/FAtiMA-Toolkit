using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities.Surrogates
{
	//TODO improve this code

	[DefaultSerializationSystem(typeof(HashSet<>), true)]
	public class HashSetSerializationSurrogate : ISerializationSurrogate
	{
		private static readonly Regex CompareRegex = new Regex(@"^\w*comparer\w*$", RegexOptions.IgnoreCase);
		private static readonly Type[] DefaultComparatorTypes = new[]
		{
			Type.GetType("System.Collections.Generic.ObjectEqualityComparer`1"),
			Type.GetType("System.Collections.Generic.GenericEqualityComparer`1"),
			Type.GetType("System.Collections.Generic.EqualityComparer`1+DefaultComparer")
		}; 

		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			Type objType = obj.GetType();
			var allFields = TypeTools.GetRuntimeFields(objType);
			var f = allFields.FirstOrDefault(field => CompareRegex.IsMatch(field.Name));
			if (f != null)
			{
				var comparator = f.GetValue(obj);
				Type comparatorType = comparator.GetType();

				if (!(comparatorType.IsGenericType() && DefaultComparatorTypes.Contains(comparatorType.GetGenericTypeDefinition())))
					holder["comparer"] = holder.ParentGraph.BuildNode(comparator, null);
			}

			Type elementType = TypeTools.GetGenericArguments(objType)[0];
			var nodeSequence = ((IEnumerable) obj).Cast<object>().Select(o => holder.ParentGraph.BuildNode(o, elementType));
			ISequenceGraphNode sequence = holder.ParentGraph.BuildSequenceNode();
			sequence.AddRange(nodeSequence);
			holder["elements"] = sequence;
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			Type objType = obj.GetType();
			Type elemType = TypeTools.GetGenericArguments(objType)[0];

			IGraphNode comparerData = node["comparer"];
			if (comparerData != null)
			{
				Type comparerType = typeof (IEqualityComparer<>).MakeGenericType(elemType);
				var comparer = comparerData.RebuildObject(comparerType);
				objType.GetConstructor(false, comparerType).Invoke(obj, new[] {comparer});
			}
			else
				objType.GetConstructor(false).Invoke(obj, null);

			ISequenceGraphNode elements = node["elements"] as ISequenceGraphNode;
			if (elements != null)
			{
				var m = TypeTools.GetRuntimeMethod(objType,"UnionWith");
				var a = Array.CreateInstance(elemType, elements.Length);
				elements.Select(e => e.RebuildObject(elemType)).ToArray().CopyTo(a, 0);
				m.Invoke(obj, new object[] {a});
			}
		}
	}
}

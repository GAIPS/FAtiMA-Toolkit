using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GAIPS.Serialization.Attributes;
using Utilities;

namespace GAIPS.Serialization.Surrogates
{
	//TODO improve this code

	[DefaultSerializationSystem(typeof(HashSet<>), true)]
	public class HashSetSerializationSurrogate : ISerializationSurrogate
	{
		private readonly static Regex CompareRegex = new Regex(@"^\w*comparer\w*$", RegexOptions.IgnoreCase);
		private static readonly Type[] DefaultComparatorTypes = new[]
		{
			Type.GetType("System.Collections.Generic.ObjectEqualityComparer`1"),
			Type.GetType("System.Collections.Generic.GenericEqualityComparer`1"),
			Type.GetType("System.Collections.Generic.EqualityComparer`1+DefaultComparer")
		}; 

		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			Type objType = obj.GetType();
			var allFields = objType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			var f = allFields.FirstOrDefault(field => CompareRegex.IsMatch(field.Name));
			if (f != null)
			{
				var comparator = f.GetValue(obj);
				Type comparatorType = comparator.GetType();

				if (!(comparatorType.IsGenericType && DefaultComparatorTypes.Contains(comparatorType.GetGenericTypeDefinition())))
					holder["comparer"] = holder.ParentGraph.BuildNode(comparator, null);
			}

			Type elementType = objType.GetGenericArguments()[0];
			var nodeSequence = ((IEnumerable) obj).Cast<object>().Select(o => holder.ParentGraph.BuildNode(o, elementType));
			ISequenceGraphNode sequence = holder.ParentGraph.BuildSequenceNode();
			sequence.AddRange(nodeSequence);
			holder["elements"] = sequence;
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			Type objType = obj.GetType();
			Type elemType = objType.GetGenericArguments()[0];

			IGraphNode comparerData = node["comparer"];
			if (comparerData != null)
			{
				Type comparerType = typeof(IEqualityComparer<>).MakeGenericType(elemType);
				var comparerObject = comparerData.RebuildObject(comparerType);
				var f = objType.GetField("m_comparer", BindingFlags.NonPublic | BindingFlags.Instance);
				f.SetValue(obj, comparerObject);
			}

			ISequenceGraphNode elements = node["elements"] as ISequenceGraphNode;
			if (elements != null)
			{
				var m = objType.GetMethod("UnionWith");
				var a = Array.CreateInstance(elemType, elements.Length);
				elements.Select(e => e.RebuildObject(elemType)).ToArray().CopyTo(a, 0);
				m.Invoke(obj, new object[] {a});
			}
		}
	}
}

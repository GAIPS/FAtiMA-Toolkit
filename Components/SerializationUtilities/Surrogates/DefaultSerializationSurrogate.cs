using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace GAIPS.Serialization.Surrogates
{
	public sealed class DefaultSerializationSurrogate : ISerializationSurrogate
	{
		private static readonly IFormatterConverter FORMAT_CONVERTER = new FormatterConverter();

		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			Type objType = obj.GetType();
			if (obj is ISerializable)
			{
				SerializationInfo info = new SerializationInfo(objType, FORMAT_CONVERTER);
				var ser = obj as ISerializable;
				ser.GetObjectData(info, new StreamingContext(StreamingContextStates.All));
				var it = info.GetEnumerator();
				while (it.MoveNext())
				{
					IGraphNode fieldNode = holder.ParentGraph.BuildNode(it.Current.Value, it.Current.ObjectType);
					holder[it.Name] = fieldNode;
				}
			}
			else
			{
				FieldInfo[] fields = SerializationServices.GetSerializableFields(objType);
				for (int i = 0; i < fields.Length; i++)
				{
					var f = fields[i];
					if (f.IsPublic)
					{
						if (f.IsNotSerialized)
							continue;
					}
					else
					{
						if (!f.GetCustomAttributes(typeof(SerializeFieldAttribute), false).Any())
							continue;
					}

					var value = f.GetValue(obj);
					if(value == null)
						continue;

					holder[f.Name] = holder.ParentGraph.BuildNode(value, f.FieldType);
				}
			}
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			Type objType = obj.GetType();
			if (obj is ISerializable)
			{
				SerializationInfo info = new SerializationInfo(objType, FORMAT_CONVERTER);
				foreach (var entry in node)
				{
					info.AddValue(entry.FieldName, entry.FieldNode.RebuildObject(null));
				}
				var c = objType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(SerializationInfo),typeof(StreamingContext)}, null);
				c.Invoke(obj, new object[] { info,new StreamingContext()});
				/*
				SerializationInfo info = new SerializationInfo(objType, FORMAT_CONVERTER);
				var ser = obj as ISerializable;
				ser.GetObjectData(info, new StreamingContext(StreamingContextStates.All));
				var it = info.GetEnumerator();
				while (it.MoveNext())
				{
					GraphNode fieldNode = SerializationServices.BuildNode(it.Current.Value, null, holder, graph);
					holder[it.Name] = fieldNode;
				}
				*/
			}
			else
			{
				FieldInfo[] fields = SerializationServices.GetSerializableFields(objType);
				for (int i = 0; i < fields.Length; i++)
				{
					var f = fields[i];
					if (f.IsPublic)
					{
						if (f.IsNotSerialized)
							continue;
					}
					else
					{
						if (!f.GetCustomAttributes(typeof(SerializeFieldAttribute), false).Any())
							continue;
					}

					string fieldName = f.Name;
					object fieldValue;
					IGraphNode nodeValue = node[fieldName];
					if (nodeValue == null)
						fieldValue = SerializationServices.GetDefaultValueForType(f.FieldType);
					else
						fieldValue = nodeValue.RebuildObject(f.FieldType);

					f.SetValue(obj, fieldValue);
				}
			}
		}
	}
}

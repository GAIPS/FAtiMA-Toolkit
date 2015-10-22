using GAIPS.Serialization.Attributes;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace GAIPS.Serialization.Surrogates
{
	public sealed class DefaultSerializationSurrogate : ISerializationSurrogate
	{
		private static readonly IFormatterConverter FORMAT_CONVERTER = new FormatterConverter();

		public void GetObjectData(object obj, ObjectGraphNode holder, SerializationGraph graph)
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
					SerializationGraphNode fieldNode = SerializationServices.BuildNode(it.Current.Value, null, holder, graph);
					holder[it.Name] = fieldNode;
				}
			}
			else
			{
				FieldInfo[] fields = SerializationServices.GetFieldsToSerialize(objType);
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

					holder[f.Name] = SerializationServices.BuildNode(value, f.FieldType, holder, graph);
				}
			}
		}

		public void SetObjectData(ref object obj, ObjectGraphNode node, SerializationGraph graph)
		{
			throw new NotImplementedException();
		}
	}
}

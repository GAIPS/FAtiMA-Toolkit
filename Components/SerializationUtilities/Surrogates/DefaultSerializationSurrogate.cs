using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization.Surrogates
{
	[DefaultSerializationSystem(typeof(object),true)]
	public sealed class DefaultSerializationSurrogate : ISerializationSurrogate
	{
		private static readonly IFormatterConverter FormatConverter;
		private static readonly Regex BackingFieldNameRegex;

		static DefaultSerializationSurrogate()
		{
			FormatConverter = new FormatterConverter();
			BackingFieldNameRegex = new Regex(@"^<([a-zA-Z_]\w*)>k__BackingField$");
		}

		private static string FormatFieldName(string fieldName)
		{
			var m = BackingFieldNameRegex.Match(fieldName);
			if (m.Success)
				return m.Groups[1].Value;
			return fieldName;
		}

		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			var objType = obj.GetType();
			var serializable = obj as ISerializable;
			if (serializable != null)
			{
				//Dictionary<int, int> b;
				var info = new SerializationInfo(objType, FormatConverter);
				var ser = serializable;
				ser.GetObjectData(info, new StreamingContext(StreamingContextStates.All));
				var it = info.GetEnumerator();
				while (it.MoveNext())
				{
					var fieldNode = holder.ParentGraph.BuildNode(it.Current.Value, null);
					holder[it.Name] = fieldNode;
				}
			}
			else
			{
				var fields = SerializationServices.GetSerializableFields(objType);
				foreach (var f in fields)
				{
					var value = f.GetValue(obj);
					var fieldType = f.FieldType;
					if (value == null || value.Equals(SerializationServices.GetDefaultValueForType(fieldType)))
						continue;

					var fieldName = FormatFieldName(f.Name);
					holder[fieldName] = holder.ParentGraph.BuildNode(value, f.FieldType);
				}
			}
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			var objType = obj.GetType();
			if (obj is ISerializable)
			{
				var info = new SerializationInfo(objType, FormatConverter);
				foreach (var entry in node)
				{
					info.AddValue(entry.FieldName, entry.FieldNode.RebuildObject(null));
				}
				var c = objType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
					new[] {typeof (SerializationInfo), typeof (StreamingContext)}, null);
				c.Invoke(obj, new object[] {info, new StreamingContext()});
				var des = obj as IDeserializationCallback;
				if (des != null)
					des.OnDeserialization(this);
			}
			else
			{
				var fields = SerializationServices.GetSerializableFields(objType);
				foreach (var f in fields)
				{
					if (f.IsNotSerialized)
						continue;

					var fieldName = FormatFieldName(f.Name);
					object fieldValue;
					var nodeValue = node[fieldName];
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
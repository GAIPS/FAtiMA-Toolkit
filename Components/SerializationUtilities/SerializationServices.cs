using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public static class SerializationServices
	{
		static SerializationServices()
		{
			_isDirty = true;
			AppDomain.CurrentDomain.AssemblyLoad += delegate(object sender, AssemblyLoadEventArgs args)
			{
				_isDirty = true;
			};
		}

		private static bool _isDirty;
		private static readonly IFormatterConverter DEFAULT_FORMATER = new FormatterConverter();
		private static readonly Regex BackingFieldNameRegex = new Regex(@"^<([a-zA-Z_]\w*)>k__BackingField$");
		private static TypeSelector<ISerializationSurrogate> _surrogateSelector = new TypeSelector<ISerializationSurrogate>();
		private static TypeSelector<IGraphFormatter> _formatterSelector = new TypeSelector<IGraphFormatter>();
		private static readonly Type[] _validTypes = new[] {typeof (ISerializationSurrogate), typeof (IGraphFormatter)};

		private static void UpdateSerializationSystems()
		{
			if(!_isDirty)
				return;

			var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => !(t.IsAbstract || t.IsInterface));
			var candidates = allTypes.ToLookup(t => _validTypes.FirstOrDefault(i => i.IsAssignableFrom(t)));
		
			RecalcTypeTrees(_surrogateSelector,candidates);
			RecalcTypeTrees(_formatterSelector, candidates);
			_isDirty = false;
		}

		private static void RecalcTypeTrees<T>(TypeSelector<T> selector, ILookup<Type, Type> group) where T: class
		{
			var validDefaults = group[typeof(T)]
				.Select(t => new{type = t, atts = t.GetCustomAttributes(typeof(DefaultSerializationSystemAttribute),false)})
				.Where(e => e.atts!=null && e.atts.Length==1);

			selector.Clear();
			foreach (var entry in validDefaults)
			{
				var att = (DefaultSerializationSystemAttribute)entry.atts[0];
				var ist = Activator.CreateInstance(entry.type);
				selector.AddValue(att.AssociatedType,att.UseInChildren,(T)ist);
			}
		}

		public static ISerializationSurrogate GetDefaultSerializationSurrogate(Type type)
		{
			UpdateSerializationSystems();
			return _surrogateSelector.GetValue(type);
		}

		public static IGraphFormatter GetDefaultSerializationFormatter(Type type)
		{
			UpdateSerializationSystems();
			return _formatterSelector.GetValue(type);
		}

		public static IEnumerable<FieldInfo> GetSerializableFields(Type type, bool includeBases)
		{
			while (type!=null && type.BaseType!=null && type.IsSerializable)
			{
				var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (var f in fields.Where(f => f.FieldType.IsSerializable && !f.IsNotSerialized))
					yield return f;

				if(!includeBases)
					break;
				type = type.BaseType;
			}
		}

		private static ConstructorInfo GetBaseConstructor(Type type)
		{
			if (type == typeof (object))
				return null;

			var c = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (c != null)
				return c;

			return GetBaseConstructor(type.BaseType);
		}

		public static object GetUninitializedObject(Type type)
		{
			var mem = FormatterServices.GetSafeUninitializedObject(type);
			var c = GetBaseConstructor(type);
			if (c == null)
				return mem;
			c.Invoke(mem, null);
			return mem;
		}

		public static object GetDefaultValueForType(Type type)
		{
			return type.IsValueType ? Activator.CreateInstance(type) : null;
		}

		private static string FormatFieldName(string fieldName)
		{
			var m = BackingFieldNameRegex.Match(fieldName);
			if (m.Success)
				return m.Groups[1].Value;
			return fieldName;
		}

		public static void PopulateWithFieldData(ISerializationData holder, object obj, bool includeBases, bool writeDefaults)
		{
			var objType = obj.GetType();
			var serializable = obj as ISerializable;
			if (serializable != null)
			{
				//Dictionary<int, int> b;
				var info = new SerializationInfo(objType,DEFAULT_FORMATER);
				var ser = serializable;
				ser.GetObjectData(info, new StreamingContext(StreamingContextStates.All));
				var it = info.GetEnumerator();
				while (it.MoveNext())
				{
					holder.SetValue(it.Name,it.Value,it.ObjectType);
				}
			}
			else
			{
				var fields = GetSerializableFields(objType, includeBases);
				foreach (var f in fields)
				{
					var value = f.GetValue(obj);
					var fieldType = f.FieldType;
					if (!writeDefaults && (value == null || value.Equals(GetDefaultValueForType(fieldType))))
						continue;

					var fieldName = FormatFieldName(f.Name);
					holder.SetValue(fieldName,value,f.FieldType);
				}
			}
		}

		public static void ExtractFromFieldData(ISerializationData holder, ref object obj, bool includeBases)
		{
			var objType = obj.GetType();
			if (obj is ISerializable)
			{
				var info = new SerializationInfo(objType, DEFAULT_FORMATER);
				using (var it = holder.GetEnumerator())
				{
					while (it.MoveNext())
					{
						info.AddValue(it.FieldName, it.BuildValue());
					}
				}

				var c = objType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
					new[] { typeof(SerializationInfo), typeof(StreamingContext) }, null);
				c.Invoke(obj, new object[] { info, new StreamingContext() });
			}
			else
			{
				var fields = GetSerializableFields(objType, true);
				foreach (var f in fields)
				{
					if (f.IsNotSerialized)
						continue;

					var fieldName = FormatFieldName(f.Name);
					object fieldValue;
					var nodeValue = holder.GetValueGraphNode(fieldName);
					if (nodeValue == null)
						fieldValue = GetDefaultValueForType(f.FieldType);
					else
						fieldValue = nodeValue.RebuildObject(f.FieldType);

					f.SetValue(obj, fieldValue);
				}
			}
		}

		public static T GetFieldOrDefault<T>(IObjectGraphNode node, string fieldName, T defaultValue)
		{
			IGraphNode target;
			if (node.TryGetField(fieldName, out target))
				return target.RebuildObject<T>();
			return defaultValue;
		}
	}
}

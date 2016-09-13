using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;
using Utilities;
#if !PORTABLE
using System.Runtime.Serialization;
#endif

namespace SerializationUtilities
{
	using Pair = Pair<DefaultSerializationSystemAttribute, Type>;

	public static class SerializationServices
	{
		private static IAssemblyLoader _assemblyLoader;
		public static IAssemblyLoader AssemblyLoader {
			get { return _assemblyLoader; }
#if PORTABLE
			set
			{
				if(value==null)
					throw new ArgumentException();

				if (_assemblyLoader != null)
				{
					UnbindAssemblyLoader(_assemblyLoader);
				}

				_assemblyLoader = value;
				BindAssemblyLoader(_assemblyLoader);
			}
#endif
		}

		private static IInstanceFactory _factory;

		public static IInstanceFactory InstanceFactory
		{
			get { return _factory; }
#if PORTABLE
			set
			{
				if(value == null)
					throw new ArgumentNullException();
				if(_factory == value)
					return;

				_factory = value;
			}
#endif
		}

		static SerializationServices()
		{
			if (_assemblyLoader == null)
				_assemblyLoader=new DefaultAssemblyLoader();
			BindAssemblyLoader(_assemblyLoader);

			_factory = new DefaultInstanceFactory();
		}

		private static readonly Regex BackingFieldNameRegex = new Regex(@"^<([a-zA-Z_]\w*)>k__BackingField$");
		private static TypeSelector<ISerializationSurrogate> _surrogateSelector = new TypeSelector<ISerializationSurrogate>();
		private static TypeSelector<IGraphFormatter> _formatterSelector = new TypeSelector<IGraphFormatter>();
		private static readonly Type[] _validTypes = new[] {typeof (ISerializationSurrogate), typeof (IGraphFormatter)};
		private static bool _isDirty;

#if !PORTABLE
		private static readonly IFormatterConverter DEFAULT_FORMATER = new FormatterConverter();
#endif

		private static void BindAssemblyLoader(IAssemblyLoader loader)
		{
			loader.OnAssemblyLoaded += SetDirty;
			loader.OnBind();
			_isDirty = true;
		}

		private static void UnbindAssemblyLoader(IAssemblyLoader loader)
		{
			loader.OnUnbind();
			loader.OnAssemblyLoaded -= SetDirty;
		}

		private static void SetDirty(Assembly assembly)
		{
			_isDirty = true;
		}

		private static void UpdateSerializationSystems()
		{
			if(!_isDirty)
				return;

			_assemblyLoader.OnAssemblyLoaded -= SetDirty;

			Dictionary<Type,List<Pair>> candidates = new Dictionary<Type, List<Pair>>();

			var searchSet = new Queue<Assembly>(_assemblyLoader.GetAssemblies());
			var foundSet = new HashSet<Assembly>();

			Action<Assembly> onAssemblyLoaded = a =>
			{
				if(foundSet.Contains(a))
					return;

				searchSet.Enqueue(a);
			};
			_assemblyLoader.OnAssemblyLoaded += onAssemblyLoaded;

			while (searchSet.Count>0)
			{
				var ass = searchSet.Dequeue();
				foundSet.Add(ass);

				foreach (var type in TypeTools.GetAllTypes(ass).Where(t => !(t.IsAbstract() || t.IsInterface())))
				{
					var key = _validTypes.FirstOrDefault(i => TypeTools.IsAssignableFrom(i, type));
					if(key==null)
						continue;

					var att = type.GetTypeAttributes<DefaultSerializationSystemAttribute>(false).FirstOrDefault();
					if(att==null)
						continue;

					List<Pair> set;
					if (!candidates.TryGetValue(key, out set))
					{
						set = new List<Pair>();
						candidates[key] = set;
					}

					set.Add(Tuples.Create(att,type));
				}
			}

			_assemblyLoader.OnAssemblyLoaded -= onAssemblyLoaded;

			RecalcTypeTrees(_surrogateSelector,candidates);
			RecalcTypeTrees(_formatterSelector, candidates);

			_assemblyLoader.OnAssemblyLoaded += SetDirty;
			_isDirty = false;
		}

		private static void RecalcTypeTrees<T>(TypeSelector<T> selector, Dictionary<Type, List<Pair>> group) where T: class
		{
			var validDefaults = group[typeof (T)];

			selector.Clear();
			foreach (var entry in validDefaults)
			{
				var att = entry.Item1;
				var ist = Activator.CreateInstance(entry.Item2);
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
			while (type?.GetBaseType() != null && type.IsSerializable())
			{
				var fields = TypeTools.GetRuntimeFields(type);
				foreach (var f in fields.Where(f => f.FieldType.IsSerializable() && !f.IsNotSerialized()))
					yield return f;

				if(!includeBases)
					break;
				type = type.GetBaseType();
			}
		}

		//private static ConstructorInfo GetBaseConstructor(Type type)
		//{
		//	if (type == typeof(object))
		//		return null;

		//	var c = type.GetConstructors(true).FirstOrDefault(c2 => c2.GetParameters().Length == 0);
		//	if (c != null)
		//		return c;

		//	return GetBaseConstructor(type.GetBaseType());
		//}

		//public static object InstatiateMinimalObject(Type type)
		//{
		//	bool calledConst;
		//	object mem = InstanceFactory.CreateUninitialized(type,out calledConst);
		//	if (calledConst)
		//		return mem;

		//	var c = GetBaseConstructor(type);
		//	if (c == null)
		//		return mem;
		//	c.Invoke(mem, null);
		//	return mem;
		//}

		public static object GetDefaultValueForType(Type type)
		{
			return type.IsValueType() ? Activator.CreateInstance(type) : null;
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
#if !PORTABLE
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
				return;
			}
#endif

			var fields = GetSerializableFields(objType, includeBases);
			foreach (var f in fields)
			{
				var value = f.GetValue(obj);
				var fieldType = f.FieldType;
				if (!writeDefaults && (value == null || value.Equals(GetDefaultValueForType(fieldType))))
					continue;

				var fieldName = FormatFieldName(f.Name);
				holder.SetValue(fieldName, value, f.FieldType);
			}
		}

		public static void ExtractFromFieldData(ISerializationData holder, ref object obj, bool includeBases)
		{
			var objType = obj.GetType();
#if !PORTABLE
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
				return;
			}
#endif
			var fields = GetSerializableFields(objType, true);
			foreach (var f in fields)
			{
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

		public static T GetFieldOrDefault<T>(IObjectGraphNode node, string fieldName, T defaultValue)
		{
			IGraphNode target;
			if (node.TryGetField(fieldName, out target))
				return target.RebuildObject<T>();
			return defaultValue;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using GAIPS.Serialization.Attributes;

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

		public static IEnumerable<FieldInfo> GetSerializableFields(Type type)
		{
			var result = Enumerable.Empty<FieldInfo>();

			while (type!=null && type.BaseType!=null && type.IsSerializable)
			{
				var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				result = result.Union(fields.Where(f => f.FieldType.IsSerializable && !f.IsNotSerialized));
				type = type.BaseType;
			}

			return result;
		}
		
		public static object GetUninitializedObject(Type type)
		{
			var c = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (c == null)
				return FormatterServices.GetSafeUninitializedObject(type);
			return c.Invoke(null);
		}

		public static object GetDefaultValueForType(Type type)
		{
			return type.IsValueType ? Activator.CreateInstance(type) : null;
		}
	}
}

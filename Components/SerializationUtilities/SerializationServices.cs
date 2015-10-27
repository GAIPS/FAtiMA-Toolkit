using GAIPS.Serialization.SerializationGraph;
using GAIPS.Serialization.Surrogates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace GAIPS.Serialization
{
	public static class SerializationServices
	{
		static SerializationServices()
		{
			SurrogateSelector = new ChainedSurrogateSelector();
			SurrogateSelector.AddSurrogate(typeof(IDictionary), new DictionarySerializationSurrogate());
			SurrogateSelector.AddSurrogate(typeof(HashSet<>),new HashSetSerializationSurrogate());
		}

		public static ChainedSurrogateSelector SurrogateSelector { get; private set; }

		public static FieldInfo[] GetSerializableFields(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		}
		/*
		public static void ExtractSerializationData(object obj, Type objType, IObjectGraphNode holder)
		{
			var surrogate = SurrogateSelector.GetSurrogate(objType);
			surrogate.GetObjectData(obj, holder);
		}

		public static void PopulateObjectWithNodeData(ref object obj, IObjectGraphNode holder)
		{
			var surrogate = SurrogateSelector.GetSurrogate(obj.GetType());
			surrogate.SetObjectData(ref obj, holder);
		}
		*/
		public static object GetUninitializedObject(Type type)
		{
			var c = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (c == null)
				return FormatterServices.GetSafeUninitializedObject(type);
			return c.Invoke(null);
		}

		private static Dictionary<Type, object> m_defaultInstances = new Dictionary<Type, object>();
		public static object GetDefaultValueForType(Type type)
		{
			if (!type.IsValueType)
				return null;

			object instance;
			if (!m_defaultInstances.TryGetValue(type, out instance))
			{
				instance = Activator.CreateInstance(type);
				m_defaultInstances[type] = instance;
			}
			return instance;
		}
	}
}

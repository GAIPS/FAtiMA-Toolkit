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

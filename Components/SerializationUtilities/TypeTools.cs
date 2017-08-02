using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SerializationUtilities
{
	internal static class TypeTools
	{
		public static IEnumerable<Type> GetAllTypes(Assembly assembly)
		{
			return assembly.GetTypes();
		}

		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		public static bool IsSerializable(this Type type)
		{
			bool result;
			result = type.IsSerializable;
			return result || type.GetTypeAttributes<SerializationUtilities.Attributes.SerializableAttribute>().Any();
		}

		public static bool IsNotSerialized(this FieldInfo member)
		{
			return member.GetCustomAttributes(false).Any(a => string.Equals(a.GetType().Name, "NonSerializedAttribute", StringComparison.OrdinalIgnoreCase));
		}

		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		public static Type GetGenericTypeDefinition(this Type type)
		{
			return type.GetGenericTypeDefinition();
		}

		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		public static IEnumerable<TA> GetTypeAttributes<TA>(this Type type, bool inherit = true)
		{
			return type.GetCustomAttributes(typeof (TA), inherit).OfType<TA>();
		}

		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		public static IEnumerable<Type> GetAllInterfaces(this Type type)
		{
			return type.GetInterfaces();
		}

		public static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		public static IEnumerable<Type> GetInterfaces(Type type)
		{
			return type.GetInterfaces();
		}

		public static IEnumerable<FieldInfo> GetRuntimeFields(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static MethodInfo GetRuntimeMethod(Type type, string methodName)
		{
			return type.GetMethod(methodName);
		}

		public static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		public static bool IsAssignableFrom(Type baseType, Type assignableType)
		{
			return baseType.IsAssignableFrom(assignableType);
		}

		public static bool IsAssignableToGenericType(Type givenType, Type genericType)
		{
			if (givenType.GetAllInterfaces().Any(it => it.IsGenericType() && it.GetGenericTypeDefinition() == genericType))
				return true;

			if (givenType.IsGenericType() && givenType.GetGenericTypeDefinition() == genericType)
				return true;

			var baseType = givenType.GetBaseType();
			return baseType != null && IsAssignableToGenericType(baseType, genericType);
		}

		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, bool getPrivate=false)
		{
			var flags = BindingFlags.Instance | BindingFlags.Public;
			if (getPrivate)
				flags |= BindingFlags.NonPublic;

			return type.GetConstructors(flags);
		}

		public static ConstructorInfo GetConstructor(this Type type, bool getPrivate, params Type[] parameterTypes)
		{
			return GetConstructors(type, getPrivate).FirstOrDefault(info =>
			{
				var p = info.GetParameters();
				if (p.Length != parameterTypes.Length)
					return false;

				return !p.Where((t, i) => !t.ParameterType.TypeHandle.Equals(parameterTypes[i].TypeHandle)).Any();
			});
		}
	}
}
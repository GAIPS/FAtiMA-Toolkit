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
#if PORTABLE
			return assembly.DefinedTypes.Select(t => t.AsType());
#else
			return assembly.GetTypes();
#endif
		}

		public static bool IsEnum(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsEnum;
#else
			return type.IsEnum;
#endif
		}

		public static bool IsAbstract(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsAbstract;
#else
			return type.IsAbstract;
#endif
		}

		public static bool IsValueType(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsValueType;
#else
			return type.IsValueType;
#endif
		}

		public static bool IsSerializable(this Type type)
		{
			bool result;
#if PORTABLE
			result = type.GetTypeInfo().IsSerializable;
#else
			result = type.IsSerializable;
#endif
			return result || type.GetTypeAttributes<SerializationUtilities.Attributes.SerializableAttribute>().Any();
		}

		public static bool IsNotSerialized(this FieldInfo member)
		{
#if PORTABLE
			return member.CustomAttributes.Any(a => string.Equals(a.AttributeType.Name, "NonSerializedAttribute", StringComparison.OrdinalIgnoreCase));
#else
			return member.GetCustomAttributes(false).Any(a => string.Equals(a.GetType().Name, "NonSerializedAttribute", StringComparison.OrdinalIgnoreCase));
#endif
		}

		public static bool IsGenericType(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsGenericType;
#else
			return type.IsGenericType;
#endif
		}

		public static bool IsGenericTypeDefinition(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsGenericTypeDefinition;
#else
			return type.IsGenericTypeDefinition;
#endif
		}

		public static Type GetGenericTypeDefinition(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().GetGenericTypeDefinition();
#else
			return type.GetGenericTypeDefinition();
#endif
		}

		public static Type[] GetGenericArguments(Type type)
		{
#if PORTABLE
			return type.GenericTypeArguments;
#else
			return type.GetGenericArguments();
#endif
		}

		public static IEnumerable<TA> GetTypeAttributes<TA>(this Type type, bool inherit = true)
		{
#if PORTABLE
			return type.GetTypeInfo().GetCustomAttributes(typeof (TA), inherit).OfType<TA>();
#else
			return type.GetCustomAttributes(typeof (TA), inherit).OfType<TA>();
#endif
		}

		public static bool IsInterface(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().IsInterface;
#else
			return type.IsInterface;
#endif
		}

		public static IEnumerable<Type> GetAllInterfaces(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().ImplementedInterfaces;
#else
			return type.GetInterfaces();
#endif
		}

		public static Type GetBaseType(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().BaseType;
#else
			return type.BaseType;
#endif
		}

		public static IEnumerable<Type> GetInterfaces(Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().ImplementedInterfaces;
#else
			return type.GetInterfaces();
#endif
		}

		public static IEnumerable<FieldInfo> GetRuntimeFields(Type type)
		{
#if PORTABLE
			return type.GetRuntimeFields().Where(f => !f.IsStatic && !f.IsLiteral);
#else
			return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
#endif
		}

		public static MethodInfo GetRuntimeMethod(Type type, string methodName)
		{
#if PORTABLE
			return type.GetRuntimeMethods().FirstOrDefault(m => m.Name == methodName);
#else
			return type.GetMethod(methodName);
#endif
		}

		public static Assembly GetAssembly(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().Assembly;
#else
			return type.Assembly;
#endif
		}

		public static bool IsAssignableFrom(Type baseType, Type assignableType)
		{
#if PORTABLE
			return baseType.GetTypeInfo().IsAssignableFrom(assignableType.GetTypeInfo());
#else
			return baseType.IsAssignableFrom(assignableType);
#endif
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
#if PORTABLE
			var e = type.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic);

			return getPrivate ? e : e.Where(c => c.IsPublic);
#else
			var flags = BindingFlags.Instance | BindingFlags.Public;
			if (getPrivate)
				flags |= BindingFlags.NonPublic;

			return type.GetConstructors(flags);
#endif
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
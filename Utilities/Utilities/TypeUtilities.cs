using System;

namespace Utilities
{
	public enum TypeCode : byte
	{
		Empty = 0,
		Object = 1,
		Boolean = 3,
		Char = 4,
		SByte = 5,
		Byte = 6,
		Int16 = 7,
		UInt16 = 8,
		Int32 = 9,
		UInt32 = 10,
		Int64 = 11,
		UInt64 = 12,
		Single = 13,
		Double = 14,
		Decimal = 15,
		DateTime = 16,
		String = 18
	}

	public static class TypeUtilities
	{
		public static TypeCode GetTypeCode(this Type type)
		{
			if(type == null)
				return TypeCode.Empty;

			if(type == typeof(bool))
				return TypeCode.Boolean;

			if(type == typeof(char))
				return TypeCode.Char;
			if (type == typeof(string))
				return TypeCode.String;

			if (type == typeof (DateTime))
				return TypeCode.DateTime;

			if (type == typeof(sbyte))
				return TypeCode.SByte;
			if(type == typeof(byte))
				return TypeCode.Byte;
			if(type == typeof(short))
				return TypeCode.Int16;
			if (type == typeof (ushort))
				return TypeCode.UInt16;
			if(type == typeof(int))
				return TypeCode.Int32;
			if(type == typeof(uint))
				return TypeCode.UInt32;
			if(type == typeof(long))
				return TypeCode.Int64;
			if(type == typeof(ulong))
				return TypeCode.UInt64;
			if(type == typeof(float))
				return TypeCode.Single;
			if(type == typeof(double))
				return TypeCode.Double;
			if(type == typeof(decimal))
				return TypeCode.Decimal;
			
			return TypeCode.Object;
		}

		public static Type GetUnderlyingType(this TypeCode code)
		{
			switch (code)
			{
				case TypeCode.Empty:
					return null;
				case TypeCode.Boolean:
					return typeof (bool);
				case TypeCode.Char:
					return typeof(char);
				case TypeCode.SByte:
					return typeof(sbyte);
				case TypeCode.Byte:
					return typeof(byte);
				case TypeCode.Int16:
					return typeof(short);
				case TypeCode.UInt16:
					return typeof(ushort);
				case TypeCode.Int32:
					return typeof(int);
				case TypeCode.UInt32:
					return typeof(uint);
				case TypeCode.Int64:
					return typeof(long);
				case TypeCode.UInt64:
					return typeof(ulong);
				case TypeCode.Single:
					return typeof(float);
				case TypeCode.Double:
					return typeof(double);
				case TypeCode.Decimal:
					return typeof(decimal);
				case TypeCode.String:
					return typeof(string);
				case TypeCode.DateTime:
					return typeof (DateTime);
			}
			throw new ArgumentException($"{TypeCode.Object} does not have a defined underlying type.",nameof(code));
		}

		public static bool IsNumeric(this Type type)
		{
			return IsNumeric(type.GetTypeCode());
		}

		public static bool IsNumeric(this TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
			}
			return IsUnsignedNumeric(typeCode);
		}

		public static bool IsUnsignedNumeric(this Type type)
		{
			return IsUnsignedNumeric(type.GetTypeCode());
		}

		public static bool IsUnsignedNumeric(this TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
			}
			return false;
		}

		public static bool IsPrimitiveData(this Type type)
		{
			return IsPrimitiveData(type.GetTypeCode());
		}

		public static bool IsPrimitiveData(this TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Boolean:
				case TypeCode.String:
					return true;
			}
			return IsNumeric(typeCode);
		}
	}
}

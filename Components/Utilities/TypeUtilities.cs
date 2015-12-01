using System;

namespace Utilities
{
	public static class TypeUtilities
	{
		public static bool IsNumeric(this Type type)
		{
			return IsNumeric(Type.GetTypeCode(type));
		}

		public static bool IsNumeric(this TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Byte:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.Single:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
			}
			return false;
		}

		public static bool IsUnsignedNumeric(this Type type)
		{
			return IsUnsignedNumeric(Type.GetTypeCode(type));
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
			return IsPrimitiveData(Type.GetTypeCode(type));
		}

		public static bool IsPrimitiveData(this TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Byte:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.Single:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Boolean:
				case TypeCode.String:
					return true;
			}
			return false;
		}

		//public static Type ToType(this TypeCode code)
		//{
		//	switch (code)
		//	{
		//		case TypeCode.Empty:
		//			return null;
		//		case TypeCode.Object:
		//			return typeof (object);
		//		case TypeCode.DBNull:
		//			return typeof (DBNull);
		//		case TypeCode.Boolean:
		//			return typeof (bool);
		//		case TypeCode.Char:
		//			return typeof (char);
		//		case TypeCode.SByte:
		//			return typeof (sbyte);
		//		case TypeCode.Byte:
		//			return typeof (byte);
		//		case TypeCode.Int16:
		//			return typeof (short);
		//		case TypeCode.UInt16:
		//			return typeof (ushort);
		//		case TypeCode.Int32:
		//			return typeof (int);
		//		case TypeCode.UInt32:
		//			return typeof (uint);
		//		case TypeCode.Int64:
		//			return typeof (long);
		//		case TypeCode.UInt64:
		//			return typeof (ulong);
		//		case TypeCode.Single:
		//			return typeof (float);
		//		case TypeCode.Double:
		//			return typeof (double);
		//		case TypeCode.Decimal:
		//			return typeof (decimal);
		//		case TypeCode.DateTime:
		//			return typeof(DateTime);
		//		case TypeCode.String:
		//			return typeof (string);
		//		default:
		//			throw new ArgumentOutOfRangeException("code", code, null);
		//	}
		//}
	}
}

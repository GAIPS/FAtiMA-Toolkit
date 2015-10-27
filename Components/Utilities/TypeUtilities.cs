using System;

namespace Utilities
{
	public static class TypeUtilities
	{
		public static bool IsNumeric(this Type type)
		{
			var typeCode = Type.GetTypeCode(type);
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

		public static bool IsPrimitiveData(this Type type)
		{
			var typeCode = Type.GetTypeCode(type);
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
	}
}

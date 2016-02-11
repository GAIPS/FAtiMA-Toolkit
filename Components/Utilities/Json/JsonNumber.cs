using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
	public class JsonNumber : JsonToken
	{
		public readonly ValueType Value;

		#region Constructors

		public JsonNumber(ValueType value)
		{
			if (!value.GetType().IsNumeric())
				throw new ArgumentException("The give value is not a primitive number data type.", "value");

			Value = value;
		}

		public JsonNumber(byte value)
		{
			Value = value;
		}

		public JsonNumber(sbyte value)
		{
			Value = value;
		}

		public JsonNumber(short value)
		{
			Value = value;
		}

		public JsonNumber(int value)
		{
			Value = value;
		}

		public JsonNumber(long value)
		{
			Value = value;
		}

		public JsonNumber(ushort value)
		{
			Value = value;
		}

		public JsonNumber(uint value)
		{
			Value = value;
		}

		public JsonNumber(ulong value)
		{
			Value = value;
		}

		public JsonNumber(float value)
		{
			Value = value;
		}

		public JsonNumber(double value)
		{
			Value = value;
		}

		public JsonNumber(decimal value)
		{
			Value = value;
		}

		#endregion

		public static explicit operator byte(JsonNumber json)
		{
			return Convert.ToByte(json.Value);
		}

		public static explicit operator sbyte(JsonNumber json)
		{
			return Convert.ToSByte(json.Value);
		}

		public static explicit operator short(JsonNumber json)
		{
			return Convert.ToInt16(json.Value);
		}

		public static explicit operator int(JsonNumber json)
		{
			return Convert.ToInt32(json.Value);
		}

		public static explicit operator long(JsonNumber json)
		{
			return Convert.ToInt64(json.Value);
		}

		public static explicit operator ushort(JsonNumber json)
		{
			return Convert.ToUInt16(json.Value);
		}

		public static explicit operator uint(JsonNumber json)
		{
			return Convert.ToUInt32(json.Value);
		}

		public static explicit operator ulong(JsonNumber json)
		{
			return Convert.ToUInt64(json.Value);
		}

		public static explicit operator float(JsonNumber json)
		{
			return Convert.ToSingle(json.Value);
		}

		public static explicit operator double(JsonNumber json)
		{
			return Convert.ToDouble(json.Value);
		}

		public static explicit operator decimal(JsonNumber json)
		{
			return Convert.ToDecimal(json.Value);
		}

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
		    var invariantCultureString = Convert.ToString(Value, CultureInfo.InvariantCulture);
            writer.Write(invariantCultureString);
		}
	}

		
}

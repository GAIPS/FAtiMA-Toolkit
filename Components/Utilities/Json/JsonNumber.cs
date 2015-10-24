using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
	public class JsonNumber : JsonToken
	{
		public string NumberString
		{
			get;
			private set;
		}

		#region Constructors

		public JsonNumber(ValueType value)
		{
			if (!value.GetType().IsNumeric())
				throw new ArgumentException("The give value is not a primitive number data type.", "value");

			this.NumberString = value.ToString();
		}

		public JsonNumber(byte value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(sbyte value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(short value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(int value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(long value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(ushort value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(uint value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(ulong value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(float value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(double value)
		{
			this.NumberString = value.ToString();
		}

		public JsonNumber(decimal value)
		{
			this.NumberString = value.ToString();
		}

		#endregion

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
			writer.Write(NumberString);
		}
	}

		
}

using System;
using System.Globalization;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;
using Utilities;

namespace KnowledgeBase
{
	[Serializable]
	public abstract class PrimitiveValue : IEquatable<PrimitiveValue>
	{
		#region Holder Classes

		private class ConcreteValue<T> : PrimitiveValue, IOpenable
		{
			public readonly T value;

			public ConcreteValue(T value)
			{
				this.value = value;
			}

			public override Type ValueType
			{
				get { return typeof(T); }
			}

			public object Open()
			{
				return value;
			}

			public override TypeCode TypeCode
			{
				get
				{
					return Type.GetTypeCode(typeof(T));
				}
			}

			public override bool Equals(PrimitiveValue obj)
			{
				ConcreteValue<T> other = obj as ConcreteValue<T>;
				if (other == null)
					return false;

				return Equals(value, other.value);
			}

			public override int GetHashCode()
			{
				return value.GetHashCode();
			}

			public override string ToString()
			{
				IFormattable f = value as IFormattable;
				if(f!=null)
					return f.ToString(null, CultureInfo.InvariantCulture);

				return value.ToString();
			}
		}

		private interface INumber
		{
			TypeCode TypeCode { get; }
			object Value { get; }
			TResult Cast<TResult>();

			int Diff(INumber other);
		}

		private interface IOpenable
		{
			object Open();
		}

		private class StringValue : ConcreteValue<string>
		{
			public StringValue(string value) : base(value)
			{
			}

			public override bool Equals(PrimitiveValue obj)
			{
				ConcreteValue<string> other = obj as ConcreteValue<string>;
				if (other == null)
					return false;

				return StringComparer.InvariantCultureIgnoreCase.Equals(value, other.value);
			}
		}

		private class NumberValue<T> : ConcreteValue<T>, INumber
		{
			public NumberValue(T value) : base(value)
			{
			}

			public TResult Cast<TResult>()
			{
				return (TResult)Convert.ChangeType(value, typeof(TResult));
			}

			public object Value => value;

			public override bool Equals(PrimitiveValue obj)
			{
				INumber n = obj as INumber;
				if (n == null)
					return false;

				return Diff(n) == 0;
			}

			public int Diff(INumber other)
			{
				const float SINGLE_ERROR_MARGIN = 0.0001f;
				const double DOUBLE_ERROR_MARGIN = 0.0001;
				const decimal DECIMAL_ERROR_MARGIN = 0.0001m;

				var ta = TypeCode;
				var tb = other.TypeCode;

				var castType = GetBestNumberTypeComparison(ta, tb);

				var a = ta==castType?value:Convert.ChangeType(value, castType);
				var b = tb==castType?other.Value:Convert.ChangeType(other.Value, castType);
				switch (castType)
				{
					case TypeCode.SByte:
					case TypeCode.Byte:
					case TypeCode.Int16:
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
						return ((IComparable)a).CompareTo(b);
					case TypeCode.Single:
						{
							var diff = (float)a - (float)b;
							var absDiff = Math.Abs(diff);
							if (absDiff < SINGLE_ERROR_MARGIN)
								return 0;
							return (int)(diff / absDiff);
						}
					case TypeCode.Double:
						{
							var diff = (double)a - (double)b;
							var absDiff = Math.Abs(diff);
							if (absDiff < DOUBLE_ERROR_MARGIN)
								return 0;
							return (int)(diff / absDiff);
						}
					case TypeCode.Decimal:
						{
							var diff = (decimal)a - (decimal)b;
							var absDiff = Math.Abs(diff);
							if (absDiff < DECIMAL_ERROR_MARGIN)
								return 0;
							return (int)(diff / absDiff);
						}
				}

				return 0;
			}

			private static TypeCode GetBestNumberTypeComparison(TypeCode a, TypeCode b)
			{
				if (a == b)
					return a;

				var ua = a.IsUnsignedNumeric();
				var ub = b.IsUnsignedNumeric();

				if (ua == ub)
					return (TypeCode)Math.Max((int)a, (int)b);

				var unsignedCode = ua ? a : b;
				var signedCode = ua ? b : a;

				if (signedCode > unsignedCode)
					return signedCode;

				switch (unsignedCode)
				{
					case TypeCode.Byte:
						return TypeCode.Int16;
					case TypeCode.UInt16:
						return TypeCode.Int32;
					case TypeCode.UInt32:
						return TypeCode.Int64;
				}
				return TypeCode.Double;
			}
		}

		#endregion

		private PrimitiveValue()
		{
		}

		public abstract TypeCode TypeCode { get; }
		public abstract Type ValueType { get; }

		public static PrimitiveValue Cast(object v)
		{
			if (v == null)
				return null;

			switch (Type.GetTypeCode(v.GetType()))
			{
				case TypeCode.Boolean:
					return (bool)v;
				case TypeCode.SByte:
					return (sbyte)v;
				case TypeCode.Byte:
					return (byte)v;
				case TypeCode.Int16:
					return (short)v;
				case TypeCode.UInt16:
					return (ushort)v;
				case TypeCode.Int32:
					return (int)v;
				case TypeCode.UInt32:
					return (uint)v;
				case TypeCode.Int64:
					return (long)v;
				case TypeCode.UInt64:
					return (ulong)v;
				case TypeCode.Single:
					return (float)v;
				case TypeCode.Double:
					return (double)v;
				case TypeCode.Decimal:
					return (decimal)v;
				case TypeCode.String:
					return (string)v;
			}

			return null;
		}

		private const NumberStyles NUMBER_STYLE_FLAGS =
			NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;
		private static readonly IFormatProvider NUMBER_FORMAT = CultureInfo.InvariantCulture.NumberFormat;

		public static PrimitiveValue Parse(string str)
		{
			str = str.TrimStart();
			if (str[0] != '-')
			{
				byte b;
				if (byte.TryParse(str, out b))
					return b;

				ushort us;
				if (ushort.TryParse(str, out us))
					return us;

				uint ui;
				if (uint.TryParse(str, out ui))
					return ui;

				ulong ul;
				if (ulong.TryParse(str, out ul))
					return ul;
			}

			bool bl;
			if (bool.TryParse(str, out bl))
				return bl;

			sbyte sb;
			if (sbyte.TryParse(str, out sb))
				return sb;

			short s;
			if (short.TryParse(str, out s))
				return s;

			int i;
			if (int.TryParse(str, out i))
				return i;

			long l;
			if (long.TryParse(str, out l))
				return l;

			float f;
			if (float.TryParse(str,NUMBER_STYLE_FLAGS, NUMBER_FORMAT,out f))
				return f;

			double d;
			if (double.TryParse(str, NUMBER_STYLE_FLAGS, NUMBER_FORMAT, out d))
				return d;

			decimal m;
			if (decimal.TryParse(str, NUMBER_STYLE_FLAGS, NUMBER_FORMAT, out m))
				return m;

			return str;
		}

		public static object Extract(PrimitiveValue value)
		{
			return ((IOpenable) value).Open();
		}

		private static TResult Open<TResult>(PrimitiveValue value) where TResult : IConvertible
		{
			ConcreteValue<TResult> r = value as ConcreteValue<TResult>;
			if (r != null)
				return r.value;

			INumber n = value as INumber;
			if (n != null)
				return n.Cast<TResult>();

			throw new InvalidCastException($"Unable to convert {value.ValueType} to {typeof (TResult)}");
		}

		public abstract bool Equals(PrimitiveValue other);

		public sealed override bool Equals(object obj)
		{
			var v = Cast(obj);
			if (obj == null)
				return false;
			return Equals(v);
		}

		public abstract override int GetHashCode();

		public abstract override string ToString();

		#region Converters

		public static implicit operator PrimitiveValue(bool value)
		{
			return new ConcreteValue<bool>(value);
		}

		public static implicit operator bool(PrimitiveValue value)
		{
			return Open<bool>(value);
		}

		public static implicit operator PrimitiveValue(string value)
		{
			return new StringValue(value);
		}

		public static implicit operator string(PrimitiveValue value)
		{
			return Open<string>(value);
		}

		//Numbers

		public static implicit operator PrimitiveValue(byte value)
		{
			return new NumberValue<byte>(value);
		}

		public static implicit operator byte(PrimitiveValue value)
		{
			return Open<byte>(value);
		}

		public static implicit operator PrimitiveValue(sbyte value)
		{
			return new NumberValue<sbyte>(value);
		}

		public static implicit operator sbyte(PrimitiveValue value)
		{
			return Open<sbyte>(value);
		}

		public static implicit operator PrimitiveValue(short value)
		{
			return new NumberValue<short>(value);
		}

		public static implicit operator short(PrimitiveValue value)
		{
			return Open<short>(value);
		}

		public static implicit operator PrimitiveValue(ushort value)
		{
			return new NumberValue<ushort>(value);
		}

		public static implicit operator ushort(PrimitiveValue value)
		{
			return Open<ushort>(value);
		}

		public static implicit operator PrimitiveValue(int value)
		{
			return new NumberValue<int>(value);
		}

		public static implicit operator int(PrimitiveValue value)
		{
			return Open<int>(value);
		}

		public static implicit operator PrimitiveValue(uint value)
		{
			return new NumberValue<uint>(value);
		}

		public static implicit operator uint(PrimitiveValue value)
		{
			return Open<uint>(value);
		}

		public static implicit operator PrimitiveValue(long value)
		{
			return new NumberValue<long>(value);
		}

		public static implicit operator long(PrimitiveValue value)
		{
			return Open<long>(value);
		}

		public static implicit operator PrimitiveValue(ulong value)
		{
			return new NumberValue<ulong>(value);
		}

		public static implicit operator ulong(PrimitiveValue value)
		{
			return Open<ulong>(value);
		}

		public static implicit operator PrimitiveValue(float value)
		{
			return new NumberValue<float>(value);
		}

		public static implicit operator float(PrimitiveValue value)
		{
			return Open<float>(value);
		}

		public static implicit operator PrimitiveValue(double value)
		{
			return new NumberValue<double>(value);
		}

		public static implicit operator double(PrimitiveValue value)
		{
			return Open<double>(value);
		}

		public static implicit operator PrimitiveValue(decimal value)
		{
			return new NumberValue<decimal>(value);
		}

		public static implicit operator decimal(PrimitiveValue value)
		{
			return Open<decimal>(value);
		}

		#endregion

		#region Operators

		public static bool operator ==(PrimitiveValue a, PrimitiveValue b)
		{
			if (ReferenceEquals(a, b))
				return true;

			if (ReferenceEquals(a, null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(PrimitiveValue a, PrimitiveValue b)
		{
			return !(a == b);
		}

		public static bool operator <(PrimitiveValue a, PrimitiveValue b)
		{
			INumber na = a as INumber;
			INumber nb = b as INumber;

			if (na != null && nb != null)
			{
				var r = na.Diff(nb);
				return r < 0;
			}
			return false;
		}

		public static bool operator <=(PrimitiveValue a, PrimitiveValue b)
		{
			INumber na = a as INumber;
			INumber nb = b as INumber;

			if (na != null && nb != null)
			{
				var r = na.Diff(nb);
				return r <= 0;
			}
			return false;
		}

		public static bool operator >(PrimitiveValue a, PrimitiveValue b)
		{
			return !(a <= b);
		}

		public static bool operator >=(PrimitiveValue a, PrimitiveValue b)
		{
			return !(a < b);
		}

		#endregion

		#region Serializer

		[DefaultSerializationSystem(typeof (PrimitiveValue), true)]
		private class PrimitiveValueSerializer : IGraphFormatter
		{
			public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
			{
				PrimitiveValue v = (PrimitiveValue) value;
				switch (v.TypeCode)
				{
					case TypeCode.String:
						return serializationGraph.BuildStringNode(((ConcreteValue<string>) v).value);
					case TypeCode.Boolean:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<bool>) v).value);
					case TypeCode.SByte:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<sbyte>) v).value);
					case TypeCode.Byte:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<byte>) v).value);
					case TypeCode.Int16:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<short>) v).value);
					case TypeCode.UInt16:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<ushort>) v).value);
					case TypeCode.Int32:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<int>) v).value);
					case TypeCode.UInt32:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<uint>) v).value);
					case TypeCode.Int64:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<long>) v).value);
					case TypeCode.UInt64:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<ulong>) v).value);
					case TypeCode.Single:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<float>) v).value);
					case TypeCode.Double:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<double>) v).value);
					case TypeCode.Decimal:
						return serializationGraph.BuildPrimitiveNode(((ConcreteValue<decimal>) v).value);
				}
				throw new InvalidOperationException("Unexpected PrimitiveValue type. " + v.ValueType + " is not a primitive v.");
			}

			public object GraphNodeToObject(IGraphNode node, Type objectType)
			{
				switch (node.DataType)
				{
					case SerializedDataType.String:
						return new ConcreteValue<string>(((IStringGraphNode) node).Value);
					case SerializedDataType.Boolean:
					case SerializedDataType.Number:
						return Cast(((IPrimitiveGraphNode) node).Value);
				}
				throw new InvalidOperationException("Unable to deserialize non-primitive data type as " + typeof (PrimitiveValue));
			}
		}

		#endregion
	}
}
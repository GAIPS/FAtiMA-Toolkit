using System;
using System.Globalization;
using SerializationUtilities;
using SerializationUtilities.SerializationGraph;
using Utilities;
using TypeCode = Utilities.TypeCode;
#if PORTABLE
using SerializationUtilities.Attributes;
#endif

[Serializable]
internal abstract class PrimitiveValue : IEquatable<PrimitiveValue>
{
	#region Holder Classes

	private abstract class ConcreteValue<T> : PrimitiveValue, IOpenable
	{
		public readonly T value;

		protected ConcreteValue(T value)
		{
			this.value = value;
		}

		public sealed override Type ValueType => typeof(T);

		public object Open()
		{
			return value;
		}

		public sealed override TypeCode TypeCode => ValueType.GetTypeCode();
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

	private class BoolValue : ConcreteValue<bool>
	{
		public BoolValue(bool value) : base(value)
		{
		}

		public override string ToString()
		{
#if PORTABLE
			return value.ToString();
#else
			return value.ToString(CultureInfo.InvariantCulture);
#endif
		}

		public sealed override bool Equals(PrimitiveValue other)
		{
			ConcreteValue<bool> obj = other as ConcreteValue<bool>;
			if (obj == null)
				return false;

			return value == obj.value;
		}

		public sealed override int GetHashCode()
		{
			return value.GetHashCode();
		}
	}

	private class StringValue : ConcreteValue<string>
	{
		public StringValue(string value) : base(value)
		{
		}

		public sealed override bool Equals(PrimitiveValue other)
		{
			ConcreteValue<string> obj = other as ConcreteValue<string>;
			if (obj == null)
				return false;

			StringComparer c;
#if PORTABLE
			c = StringComparer.OrdinalIgnoreCase;
#else
			c = StringComparer.InvariantCultureIgnoreCase;
#endif

			return c.Equals(value, obj.value);
		}

		public override string ToString()
		{
			return value;
		}

		public sealed override int GetHashCode()
		{
			return value.ToUpperInvariant().GetHashCode();
		}
	}

	private class NumberValue<T> : ConcreteValue<T>, INumber
		where T: IFormattable
	{
		private const float SINGLE_ERROR_MARGIN = 0.0001f;
		private const double DOUBLE_ERROR_MARGIN = 0.0001;
		private const decimal DECIMAL_ERROR_MARGIN = 0.0001m;

		public NumberValue(T value) : base(value)
		{
		}

		public TResult Cast<TResult>()
		{
			return (TResult)Convert.ChangeType(value, typeof(TResult));
		}

		public object Value => value;

		public sealed override bool Equals(PrimitiveValue other)
		{
			INumber n = other as INumber;
			if (n == null)
				return false;

			return Diff(n) == 0;
		}
		
		public int Diff(INumber other)
		{
			var ta = TypeCode;
			var tb = other.TypeCode;

			var castType = GetBestNumberTypeComparison(ta, tb);
			var ct = castType.GetUnderlyingType();
			var a = ta == castType ? value : Convert.ChangeType(value, ct);
			var b = tb == castType ? other.Value : Convert.ChangeType(other.Value, ct);
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

		public override string ToString()
		{
			return value.ToString(null,CultureInfo.InvariantCulture);
		}

		public sealed override int GetHashCode()
		{
			return value.GetHashCode();
		}
	}

	#endregion

	private PrimitiveValue()
	{
	}

	public abstract TypeCode TypeCode { get; }
	public abstract Type ValueType { get; }

	public static PrimitiveValue Cast(object obj)
	{
		if (obj == null)
			return null;

		switch (obj.GetType().GetTypeCode())
		{
			case TypeCode.Boolean:
				return new BoolValue((bool)obj);
			case TypeCode.SByte:
				return new NumberValue<sbyte>((sbyte)obj);
			case TypeCode.Byte:
				return new NumberValue<byte>((byte)obj);
			case TypeCode.Int16:
				return new NumberValue<short>((short)obj);
			case TypeCode.UInt16:
				return new NumberValue<ushort>((ushort)obj);
			case TypeCode.Int32:
				return new NumberValue<int>((int)obj);
			case TypeCode.UInt32:
				return new NumberValue<uint>((uint)obj);
			case TypeCode.Int64:
				return new NumberValue<long>((long)obj);
			case TypeCode.UInt64:
				return new NumberValue<ulong>((ulong)obj);
			case TypeCode.Single:
				return new NumberValue<float>((float)obj);
			case TypeCode.Double:
				return new NumberValue<double>((double)obj);
			case TypeCode.Decimal:
				return new NumberValue<decimal>((decimal)obj);
			case TypeCode.String:
				return new StringValue((string)obj);
		}

		throw new ArgumentException("The given object must be of primitive data type.",nameof(obj));
	}

	private const NumberStyles NUMBER_STYLE_FLAGS =
		NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;
	private static readonly IFormatProvider NUMBER_FORMAT = CultureInfo.InvariantCulture.NumberFormat;

	public static bool TryParse(string str, out PrimitiveValue value)
	{
		str = str.Trim();

		bool bl;
		if (bool.TryParse(str, out bl))
		{
			value = new BoolValue(bl);
			return true;
		}

		//Unsigned
		if (str[0] != '-')
		{
			byte b;
			if (byte.TryParse(str, out b))
			{
				value = new NumberValue<byte>(b);
				return true;
			}

			ushort us;
			if (ushort.TryParse(str, out us))
			{
				value = new NumberValue<ushort>(us);
				return true;
			}

			uint ui;
			if (uint.TryParse(str, out ui))
			{
				value = new NumberValue<uint>(ui);
				return true;
			}

			ulong ul;
			if (ulong.TryParse(str, out ul))
			{
				value = new NumberValue<ulong>(ul);
				return true;
			}
		}

		//Signed
		sbyte sb;
		if (sbyte.TryParse(str, out sb))
		{
			value = new NumberValue<sbyte>(sb);
			return true;
		}

		short s;
		if (short.TryParse(str, out s))
		{
			value = new NumberValue<short>(s);
			return true;
		}

		int i;
		if (int.TryParse(str, out i))
		{
			value = new NumberValue<int>(i);
			return true;
		}

		long l;
		if (long.TryParse(str, out l))
		{
			value = new NumberValue<long>(l);
			return true;
		}

		float f;
		if (float.TryParse(str, NUMBER_STYLE_FLAGS, NUMBER_FORMAT, out f))
		{
			value = new NumberValue<float>(f);
			return true;
		}

		decimal m;
		if (decimal.TryParse(str, NUMBER_STYLE_FLAGS, NUMBER_FORMAT, out m))
		{
			value = new NumberValue<decimal>(m);
			return true;
		}

		double d;
		if (double.TryParse(str, NUMBER_STYLE_FLAGS, NUMBER_FORMAT, out d))
		{
			value = new NumberValue<double>(d);
			return true;
		}

		value = new StringValue(str);
		return true;
	}

	public static object Extract(PrimitiveValue value)
	{
		return ((IOpenable)value).Open();
	}

	public static bool TryOpen<TResult>(PrimitiveValue value, out TResult v)
	{
		ConcreteValue<TResult> r = value as ConcreteValue<TResult>;
		if (r != null)
		{
			v = r.value;
			return true;
		}

		INumber n = value as INumber;
		if (n != null)
		{
			v = n.Cast<TResult>();
			return true;
		}

		v = default(TResult);
		return false;
	}

	//public static TResult Open<TResult>(PrimitiveValue value) where TResult : IConvertible
	//{
	//	ConcreteValue<TResult> r = value as ConcreteValue<TResult>;
	//	if (r != null)
	//		return r.value;

	//	INumber n = value as INumber;
	//	if (n != null)
	//		return n.Cast<TResult>();

	//	throw new InvalidCastException($"Unable to convert {value.ValueType} to {typeof(TResult)}");
	//}

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

	public static bool TryCompare(PrimitiveValue a, PrimitiveValue b, out int delta)
	{
		INumber na = a as INumber;
		INumber nb = b as INumber;

		if (na != null && nb != null)
		{
			delta = na.Diff(nb);
			return true;
		}
		delta = 0;
		return false;
	}

	//#region Serializer

	//[DefaultSerializationSystem(typeof(PrimitiveValue), true)]
	//private class PrimitiveValueSerializer : IGraphFormatter
	//{
	//	public IGraphNode ObjectToGraphNode(object value, Graph serializationGraph)
	//	{
	//		PrimitiveValue v = (PrimitiveValue)value;
	//		switch (v.TypeCode)
	//		{
	//			case TypeCode.String:
	//				return serializationGraph.BuildStringNode(((ConcreteValue<string>)v).value);
	//			case TypeCode.Boolean:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<bool>)v).value);
	//			case TypeCode.SByte:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<sbyte>)v).value);
	//			case TypeCode.Byte:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<byte>)v).value);
	//			case TypeCode.Int16:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<short>)v).value);
	//			case TypeCode.UInt16:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<ushort>)v).value);
	//			case TypeCode.Int32:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<int>)v).value);
	//			case TypeCode.UInt32:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<uint>)v).value);
	//			case TypeCode.Int64:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<long>)v).value);
	//			case TypeCode.UInt64:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<ulong>)v).value);
	//			case TypeCode.Single:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<float>)v).value);
	//			case TypeCode.Double:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<double>)v).value);
	//			case TypeCode.Decimal:
	//				return serializationGraph.BuildPrimitiveNode(((ConcreteValue<decimal>)v).value);
	//		}
	//		throw new InvalidOperationException($"Unexpected PrimitiveValue type. {v.ValueType} is not a primitive object.");
	//	}

	//	public object GraphNodeToObject(IGraphNode node, Type objectType)
	//	{
	//		switch (node.DataType)
	//		{
	//			case SerializedDataType.String:
	//				return new StringValue(((IStringGraphNode)node).Value);
	//			case SerializedDataType.Boolean:
	//			case SerializedDataType.Number:
	//				return Cast(((IPrimitiveGraphNode)node).Value);
	//		}
	//		throw new InvalidOperationException("Unable to deserialize non-primitive data type as " + typeof(PrimitiveValue));
	//	}
	//}

	//#endregion
}
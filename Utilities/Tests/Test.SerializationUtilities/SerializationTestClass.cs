using System;
using System.Collections.Generic;

namespace Test.SerializationUtilities
{
	[Flags]
	public enum SerializationEnumTest : int
	{
		Ok1 = 1,
		Ok2 = 2,
		Ok3 = 4
	}

	[Serializable]
	public class SerializationTestClass
	{
		private int[] ArrayField1 = { 4, 5, 3, 2, 7, 5, 8 };

		private object[] ArrayField2 =
		{
				4, 5.5, 1m/3, null, "teste", new object(), DateTime.Now, SerializationEnumTest.Ok2,
				new[] {4, 5, 6}
			};

		public SerializationEnumTest EnumField;
		public decimal FloatValue;
		public double FloatValue2;
		private SerializationTestClass m_circlePoiter;
		private SerializationTestClass m_nullPointer;
		public ulong NumValue;
		public byte NumValue2;

		public readonly Dictionary<int, string> TestDic;

		public readonly HashSet<float> TestHash;
		public readonly HashSet<object> TestHash2;

		public DateTime TimeField;

		[NonSerialized]
		public string VolatileField = "this string should not be serialized";

		public string BackingProperty { get; set; }

		public Type SerializeType { get; set; }

		public SerializationTestClass()
		{
			NumValue = ulong.MaxValue / 3;
			NumValue2 = 123;
			FloatValue = 4.565e+25m;
			FloatValue2 = double.MaxValue / 5;
			EnumField = SerializationEnumTest.Ok3 | SerializationEnumTest.Ok1;
			TimeField = DateTime.UtcNow;

			TestHash2 = new HashSet<object>();
			TestHash2.Add(new object());
			TestHash2.Add(3);
			TestHash2.Add(5);
			TestHash2.Add(SerializationEnumTest.Ok2);

			TestHash = new HashSet<float>();
			TestHash.Add(1);
			TestHash.Add(2);
			TestHash.Add(3);

			TestDic = new Dictionary<int, string>();
			TestDic[1] = "one";
			TestDic[2] = "two";
			TestDic[3] = "three";

			m_nullPointer = null;
			m_circlePoiter = this;

			BackingProperty = "this is a property";

			SerializeType = typeof(DateTime);
		}
	}
}
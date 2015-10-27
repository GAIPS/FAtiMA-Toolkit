using EmotionalAppraisal;
using GAIPS.Serialization;
using GAIPS.Serialization.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCCModelAppraisal.OCCModel;
using System;
using System.IO;

namespace UnitTest
{
	[TestClass]
	public class SerializationTests
	{
		private enum SerializationEnumTest
		{
			Ok1,
			Ok2,
			Ok3
		}

		private class SerializationTestClass
		{
			[SerializeField]
			private SerializationTestClass m_nullPointer;
			[SerializeField]
			private SerializationTestClass m_circlePoiter;
			public decimal FloatValue;
			public double FloatValue2;
			public ulong NumValue;
			public byte NumValue2;

			public DateTime TimeField;
			public SerializationEnumTest EnumField;

			[SerializeField]
			private int[] ArrayField1 = new int[] { 4, 5, 3, 2, 7, 5, 8 };

			[SerializeField]
			private object[] ArrayField2 = new object[] { 4, 5.5, 1m/3, null, "teste", new object(), DateTime.Now, SerializationEnumTest.Ok2, new int[]{4,5,6}};

			[NonSerialized]
			public string VolatileField = "this string should not be serialized";

			public SerializationTestClass()
			{
				NumValue = ulong.MaxValue/3;
				NumValue2 = 123;
				FloatValue = 4.565e+25m;
				FloatValue2 = double.MaxValue / 5;
				EnumField = SerializationEnumTest.Ok3;
				TimeField = DateTime.UtcNow;

				m_nullPointer = null;
				m_circlePoiter = this;
			}
		}

		[TestMethod]
		public void BasicSerializationTest()
		{
			var asset = new SerializationTestClass();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);

				stream.Seek(0, SeekOrigin.Begin);
				System.Diagnostics.Trace.Write(new StreamReader(stream).ReadToEnd());
			}
		}

		[TestMethod]
		public void BasicDeserializationTest()
		{
			var asset = new SerializationTestClass();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				System.Diagnostics.Trace.Write(new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formater.Deserialize(stream);
			}
		}
	}
}

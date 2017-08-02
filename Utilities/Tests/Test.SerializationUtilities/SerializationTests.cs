using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SerializationUtilities;
using Utilities;

namespace Test.SerializationUtilities
{
	[TestFixture]
	public class SerializationTests
	{
		private static InstanceFactoryImplementation factory = new InstanceFactoryImplementation();
		private static TestAssemblyLoader assemblyLoader = new TestAssemblyLoader();

		private class TestCasesGenerator
		{
			public static IEnumerable TestCases()
			{
				foreach (var serType in new []{ Tuples.Create<Type,Action<MemoryStream>>(typeof(JSONSerializer),TextLogger), Tuples.Create<Type, Action<MemoryStream>>(typeof(BinarySerializer), BinaryLogger) })
				{
					foreach (var tests in TestCases2())
					{
						var ser = Activator.CreateInstance(serType.Item1);
						yield return new TestCaseData(new [] {ser,serType.Item2}.Union(tests.Arguments).ToArray());
					}
				}
			}

			public static IEnumerable<TestCaseData> TestCases2()
			{
				var r = new Random();
				yield return new TestCaseData(r.Next(),typeof(int));
				yield return new TestCaseData(Math.Round(r.NextDouble(), 15), typeof(double));
				yield return new TestCaseData("test string",typeof(string));
				yield return new TestCaseData(DateTime.UtcNow,typeof(DateTime));

				var len = r.Next(5, 30);
				var array = Enumerable.Repeat(0, len).Select(a => r.Next()).ToArray();
				yield return new TestCaseData(array,typeof(int[]));

				var list = new List<int>(array);
				yield return new TestCaseData(list,list.GetType());

				var list2 = new ArrayList(array);
				yield return new TestCaseData(list2,list2.GetType());

				var hashset = new HashSet<double>();
				hashset.UnionWith(Enumerable.Repeat(0,r.Next(5,50)).Select(a => Math.Round(r.NextDouble(), 15)));

				yield return new TestCaseData(hashset,typeof(HashSet<double>));

				var hashset2 = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				hashset2.UnionWith(Enumerable.Repeat(0,r.Next(5,30)).Select(i => RandomString(r.Next(5,10))));
				yield return new TestCaseData(hashset2,typeof(HashSet<string>));

				var dic = new Dictionary<string,int>();
				var entries = r.Next(3, 10);
				for (int i = 0; i < entries; i++)
				{
					string key;
					do
					{
						key = RandomString(6);
					} while (dic.ContainsKey(key));
					dic.Add(key,r.Next());
				}
				yield return new TestCaseData(dic,typeof(Dictionary<string, int>));

				var dic2 = new Dictionary<string,int>(StringComparer.InvariantCultureIgnoreCase);
				var entries2 = r.Next(3, 10);
				for (int i = 0; i < entries2; i++)
				{
					string key;
					do
					{
						key = RandomString(6);
					} while (dic2.ContainsKey(key));
					dic2.Add(key, r.Next());
				}
				yield return new TestCaseData(dic2, typeof(Dictionary<string, int>));
			}

			private static Random random = new Random();
			public static string RandomString(int length)
			{
				const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
				return new string(Enumerable.Repeat(chars, length)
				  .Select(s => s[random.Next(s.Length)]).ToArray());
			}

			private static void TextLogger(MemoryStream stream)
			{
				var reader = new StreamReader(stream);
				Console.Out.WriteLine(reader.ReadToEnd());
			}

			private static void BinaryLogger(MemoryStream stream)
			{
				byte[] buffer = new byte[16];
				Console.WriteLine("Total byte: "+stream.Length);
				while (stream.Position<stream.Length)
				{
					var total = stream.Read(buffer, 0, 16);
					Console.WriteLine(BitConverter.ToString(buffer,0,total).Replace('-', ' '));
				}
			}
		}

		[Test,TestCaseSource(typeof(TestCasesGenerator),nameof(TestCasesGenerator.TestCases))]
		public static void Test(BaseSerializer serializer, Action<MemoryStream> logger, object value,Type type)
		{
            //TODO: Ask Pedro
			/*SerializationServices.AssemblyLoader = assemblyLoader;
			SerializationServices.InstanceFactory = factory;*/

			object other;
			using (var mem = new MemoryStream())
			{
				serializer.Serialize(mem,value);
				mem.Position = 0;
				logger(mem);
				mem.Position = 0;
				other = serializer.Deserialize(mem, type);
			}

			Assert.AreEqual(value,other);
		}

		[Test]
		public static void CustomClassSerializationTest()
		{
            //TODO: Ask Pedro
            /*SerializationServices.AssemblyLoader = assemblyLoader;
			SerializationServices.InstanceFactory = factory;*/

			var value = new SerializationTestClass();

			var serializer = new JSONSerializer();
			var json = serializer.SerializeToJson(value);
			json.Write(Console.Out, true);
			var other = serializer.DeserializeFromJson<SerializationTestClass>(json);

			Assert.IsNull(other.VolatileField);
		}
	}
}
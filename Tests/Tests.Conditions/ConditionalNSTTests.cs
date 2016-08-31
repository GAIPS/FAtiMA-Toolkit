using System;
using System.IO;
using Conditions;
using SerializationUtilities;
using KnowledgeBase;
using NUnit.Framework;
using WellFormedNames;

namespace Tests.Conditions
{
	[TestFixture]
	public class ConditionalNSTTests
	{
		 //test case
		private static ConditionalNST<int> _testCase = CreateTestCase();
		private static ConditionalNST<int> CreateTestCase()
		{
			var map = new ConditionalNST<int>();
			map.Add((Name)"Event([x],Silence,SELF)",null,0);
			map.Add((Name)"EVENT([x],Speak,[y])",new ConditionSet(LogicalQuantifier.Universal,
				new []
				{
					Condition.Parse("[type]=Informal"),
					Condition.Parse("[y]=SELF")
				}),1);
			map.Add((Name)"EVENT([x],Speak,[y])", new ConditionSet(
				new[]
				{
					Condition.Parse("[type]=Formal"),
					Condition.Parse("[y]=SELF")
				}), 2);
			map.Add((Name)"EVENT(Self,Remind_Problem,-)", new ConditionSet(), 3);
			map.Add((Name)"EVENT(*,Problem_Solved,*)", new ConditionSet(
					new []
					{
						Condition.Parse("[subject]=Player"),
						Condition.Parse("[TARGET]=Self"),
					}
				), 4);
			return map;
		}

		private const string DESERIALIZATION_TEST_CASE = @"{""root"":{""classId"": 0,""values"": [{""key"": ""Event(Self, Remind_Problem, -)"",""value"": 3}, {""key"": ""Event([x], Silence, SELF)"",""value"": 0}, {""key"": ""Event([x], Speak, [y])"",""conditions"":{""Quantifier"": ""Universal"",""Set"": [""Informal = [type]"", ""SELF = [y]""]},""value"": 1}, {""key"": ""Event([x], Speak, [y])"",""conditions"":{""Set"": [""Formal = [type]"", ""SELF = [y]""]},""value"": 2}, {""key"": ""Event(*, Problem_Solved, *)"",""conditions"":{""Set"": [""Player = [subject]"", ""Self = [TARGET]""]},""value"": 4}]},""types"": [{""TypeId"": 0,""ClassName"": ""KnowledgeBase.ConditionalNST`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], Conditions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null""}]}";

		[Test]
		public void Test_ConditionalNST_Serialization()
		{
			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, _testCase);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
			}
		}

		[TestCase]
		public void Test_ConditionalNST_Deserialization()
		{
			using (var stream = new MemoryStream())
			{
				var writer = new StreamWriter(stream);
				writer.Write(DESERIALIZATION_TEST_CASE);
				writer.Flush();
				stream.Seek(0, SeekOrigin.Begin);

				var formater = new JSONSerializer();
				var obj = formater.Deserialize(stream);
				Assert.AreEqual(obj, _testCase);
			}
		}
	}
}
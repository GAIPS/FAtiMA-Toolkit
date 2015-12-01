using System;
using System.IO;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.Conditions
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
			map.Add((Name)"EVENT([x],Speak,[y])",new ConditionSet(
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

		private const string DESERIALIZATION_TEST_CASE = @"{""root"":{""classId"": 0,""values"": [{""key"": ""Event(Self, Remind_Problem, -)"",""value"": 3}, {""key"": ""Event([x], Silence, SELF)"",""value"": 0}, {""key"": ""Event([x], Speak, [y])"",""conditions"": [""[type] = Informal"", ""[y] = SELF""],""value"": 1}, {""key"": ""Event([x], Speak, [y])"",""conditions"": [""[type] = Formal"", ""[y] = SELF""],""value"": 2}, {""key"": ""Event(*, Problem_Solved, *)"",""conditions"": [""[subject] = Player"", ""[TARGET] = Self""],""value"": 4}]},""types"": [{""TypeId"": 0,""ClassName"": ""KnowledgeBase.ConditionalNST`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], KnowledgeBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null""}]}";

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
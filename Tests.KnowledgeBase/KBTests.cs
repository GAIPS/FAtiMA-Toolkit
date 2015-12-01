using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase
{
	[TestFixture]
	public class KBTests
	{
		private class TestFactory
		{
			public static IEnumerable<TestCaseData> Test_Simple_Tell_Valid_Cases()
			{
				yield return new TestCaseData((Name)"x",false,false,KnowledgeVisibility.Universal);
				yield return new TestCaseData((Name)"like(self,john)", true, false, KnowledgeVisibility.Universal);
				yield return new TestCaseData((Name)"like(self,Amy)", true, true, KnowledgeVisibility.Universal);
				yield return new TestCaseData((Name)"like(self,Make)", true, false, KnowledgeVisibility.Self);
				yield return new TestCaseData((Name)"like(self,Steven)", true, true, KnowledgeVisibility.Self);
				yield return new TestCaseData((Name)"Color(id_2433)", "Blue", true, KnowledgeVisibility.Self);
			}

			public static IEnumerable<TestCaseData> Test_Simple_Tell_Invalid_Cases()
			{
				yield return new TestCaseData((Name)"[x]", false).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"like(self,[x])", false).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"like(self,Color(Ball))", false).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"like(self,Color(A(B,D)))", false).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"10", 35).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"10", 10).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"true", 25).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"false", true).Throws(typeof(Exception));
			}

			public static IEnumerable<TestCaseData> MemoryData()
			{
				yield return new TestCaseData((Name)"Strength(John)", 10,false);
				yield return new TestCaseData((Name)"Strength(Name(Self))", 7).Throws(typeof(Exception));
				yield return new TestCaseData((Name)"Name(Self)", "Titus",true);
				yield return new TestCaseData((Name)"Strength(Name(Self))", 7,false);
				yield return new TestCaseData((Name)"Color(Strength(Name(Self)))", "Blue",true);
			}

			public static int NumOfPersistentEntries()
			{
				return MemoryData().Count(d => 
					d.Arguments.Length > 2 && (bool) d.Arguments[2]
					);
			}

			public static KB PopulatedTestMemory()
			{
				KB kb = new KB();
				foreach (var t in MemoryData())
				{
					try
					{
						if(t.Arguments.Length>2)
							kb.Tell((Name)t.Arguments[0], t.Arguments[1],(bool)t.Arguments[2]);
						else
							kb.Tell((Name)t.Arguments[0], t.Arguments[1]);
					}
					catch (Exception e)
					{
						if (t.ExpectedException != null)
							Assert.AreEqual(e.GetType(), t.ExpectedException);
						else
							Assert.Fail("An exception was thrown unexpectedly.");
					}
				}
				return kb;
			}
		}

		[TestCaseSource(typeof(TestFactory), "Test_Simple_Tell_Valid_Cases")]
		public void Test_Simple_Tell_Valid(Name name,object value, bool isPersitent, KnowledgeVisibility visibility)
		{
			var kb = new KB();
			kb.Tell(name,value,isPersitent,visibility);
		}

		[TestCaseSource(typeof(TestFactory), "Test_Simple_Tell_Invalid_Cases")]
		public void Test_Simple_Tell_Invalid(Name name, object value)
		{
			var kb = new KB();
			kb.Tell(name, value);
		}

		[Test]
		public void Test_Acculm_Tell_Valid()
		{
			KB kb = TestFactory.PopulatedTestMemory();
		}

		[Test]
		public void Test_RemoveNonPersistent()
		{
			KB kb = TestFactory.PopulatedTestMemory();
			kb.RemoveNonPersistent();
			var n = TestFactory.NumOfPersistentEntries();
			Assert.AreEqual(kb.NumOfEntries,n);
		}

		[TestCase("35",35)]
		[TestCase("-9223372036854775807", -9223372036854775807)]
		[TestCase("-9.43",-9.43)]
		[TestCase("-9.43e-1", -9.43e-1)]
		[TestCase("true",true)]
		[TestCase("FALSE",false)]
		[TestCase("3.40282347E+39", 3.40282347E+39)]
		public void Test_PrimitiveValuesAsk(string str,object expect)
		{
			Name v = (Name) str;
			KB kb = new KB();
			var value = kb.AskProperty(v);
			Assert.NotNull(value);

			Assert.True(Util.CompareObjects(value, expect));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;
using Utilities;

namespace Tests.KnowledgeBase
{
	[TestFixture]
	public class KBTests
	{
		private class TestFactory
		{
			public static IEnumerable<TestCaseData> Test_Simple_Tell_Valid_Cases()
			{
				yield return new TestCaseData((Name)"like(self,john)", true, false, KnowledgeVisibility.Universal);
				yield return new TestCaseData((Name)"like(self,Amy)", true, true, KnowledgeVisibility.Universal);
				yield return new TestCaseData((Name)"like(self,Make)", true, false, KnowledgeVisibility.Self);
				yield return new TestCaseData((Name)"like(self,Steven)", true, true, KnowledgeVisibility.Self);
				yield return new TestCaseData((Name)"Color(id_2433)", "Blue", true, KnowledgeVisibility.Self);
			}

			public static IEnumerable<TestCaseData> Test_OperatorRegist_Cases()
			{
				DynamicPropertyCalculator p = (kb2, args,subs) =>
				{
					return Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();
				};
				yield return new TestCaseData(PopulatedTestMemory(), (Name)"Count([x])", p, (Name)"Count(IsAlive([x]))", null);
				yield return new TestCaseData(PopulatedTestMemory(), (Name)"Count([x])", p, (Name)"Count([y])", new SubstitutionSet(new Substitution("[y]/IsAlive([x])")));
			}

			public static IEnumerable<TestCaseData> MemoryData()
			{
				yield return new TestCaseData((Name)"Strength(John)", (byte)5);
				yield return new TestCaseData((Name)"Strength(Mary)", (sbyte)3);
				yield return new TestCaseData((Name)"Strength(Leonidas)", (short)500);
				yield return new TestCaseData((Name)"Strength(Goku)", (uint)9001f);
				yield return new TestCaseData((Name)"Strength(SuperMan)", ulong.MaxValue);
				yield return new TestCaseData((Name)"Strength(Saitama)", float.MaxValue);
				yield return new TestCaseData((Name)"Race(Saitama)", "human");
				yield return new TestCaseData((Name)"Race(Superman)", "kriptonian");
				yield return new TestCaseData((Name)"Race(Goku)", "sayian");
				yield return new TestCaseData((Name)"Race(Leonidas)", "human");
				yield return new TestCaseData((Name)"Race(Mary)", "human");
				yield return new TestCaseData((Name)"Race(John)", "human");
				yield return new TestCaseData((Name)"Job(Saitama)", "super-hero");
				yield return new TestCaseData((Name)"Job(Superman)", "super-hero");
				yield return new TestCaseData((Name)"Job(Leonidas)", "Spartan");
				yield return new TestCaseData((Name)"AKA(Saitama)", "One-Punch_Man");
				yield return new TestCaseData((Name)"AKA(Superman)", "Clark_Kent");
				yield return new TestCaseData((Name)"AKA(Goku)", "Kakarot");
				yield return new TestCaseData((Name)"Hobby(Saitama)", "super-hero");
				yield return new TestCaseData((Name)"Hobby(Goku)", "training");
				yield return new TestCaseData((Name)"IsAlive(Leonidas)", false);
				yield return new TestCaseData((Name)"IsAlive(Saitama)", true);
				yield return new TestCaseData((Name)"IsAlive(Superman)", true);
				yield return new TestCaseData((Name)"IsAlive(John)", true);

				yield return new TestCaseData((Name)"Strength(Name(Self))", 7,typeof(Exception));
				yield return new TestCaseData((Name)"Name(Self)", "Titus",true);
				yield return new TestCaseData((Name)"Strength(Name(Self))", 7,false);
			}

			public static IEnumerable<TestCaseData> Test_Simple_Tell_Invalid_Cases()
			{
				yield return new TestCaseData((Name)"[x]", false,typeof(Exception));
				yield return new TestCaseData((Name)"like(self,[x])", false,typeof(Exception));
				yield return new TestCaseData((Name)"like(self,Color(Ball))", false,typeof(Exception));
				yield return new TestCaseData((Name)"like(self,Color(A(B,D)))", false,typeof(Exception));
				yield return new TestCaseData((Name)"10", 35,typeof(Exception));
				yield return new TestCaseData((Name)"10", 10,typeof(Exception));
				yield return new TestCaseData((Name)"true", 25,typeof(Exception));
				yield return new TestCaseData((Name)"false", true,typeof(Exception));
			}

			//public static int NumOfPersistentEntries()
			//{
			//	return MemoryData().Count(d => 
			//		d.Arguments.Length > 2 && (bool) d.Arguments[2]
			//		);
			//}

			public static KB PopulatedTestMemory()
			{
				KB kb = new KB();
				foreach (var t in MemoryData())
				{
					try
					{
						//if(t.Arguments.Length>2)
						//	kb.Tell((Name)t.Arguments[0], PrimitiveValue.Cast(t.Arguments[1]),(bool)t.Arguments[2]);
						//else
							kb.Tell((Name)t.Arguments[0], PrimitiveValue.Cast(t.Arguments[1]));
					}
					catch (Exception e)
					{
						if (t.Arguments[2] != null)
							Assert.AreEqual(e.GetType(), t.Arguments[2]);
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
			kb.Tell(name,PrimitiveValue.Cast(value),isPersitent,visibility);
		}

		[TestCaseSource(typeof(TestFactory), "Test_Simple_Tell_Invalid_Cases")]
		public void Test_Simple_Tell_Invalid(Name name, object value, Type expectedException)
		{
			var kb = new KB();
			Assert.Throws(expectedException,() => kb.Tell(name, PrimitiveValue.Cast(value)));
		}

		[Test]
		public void Test_Acculm_Tell_Valid()
		{
			KB kb = TestFactory.PopulatedTestMemory();
		}

		//[Test]
		//public void Test_RemoveNonPersistent()
		//{
		//	KB kb = TestFactory.PopulatedTestMemory();
		//	kb.RemoveNonPersistent();
		//	var n = TestFactory.NumOfPersistentEntries();
		//	Assert.AreEqual(kb.NumOfEntries,n);
		//}

		//[TestCase("35",35)]
		//[TestCase("-9223372036854775807", -9223372036854775807)]
		//[TestCase("-9.43",-9.43)]
		//[TestCase("-9.43e-1", -9.43e-1)]
		//[TestCase("true",true)]
		//[TestCase("FALSE",false)]
		//[TestCase("3.40282347E+39", 3.40282347E+39)]
		//public void Test_PrimitiveValuesAsk(string str,object expect)
		//{
		//	Name v = (Name) str;
		//	KB kb = new KB();
		//	var value = kb.AskProperty(v);
		//	Assert.NotNull(value);
		//	Assert.That(Equals(value,expect));
		//}

		[Test]
		public void Test_OperatorRegist_Fail_Duplicate()
		{
			var kb = new KB();
			Assert.Throws<ArgumentException>(
				() => kb.RegistDynamicProperty((Name) "Count([y])", ((kb1, args, constraints) => null), null));
		}

		[Test]
		public void Test_OperatorRegist_Fail_Same_Template()
		{
			var kb = new KB();
			Assert.Throws<ArgumentException>(() => kb.RegistDynamicProperty((Name)"Count([x])", ((kb1, args, constraints) => null), null));
		}

		[Test]
		public void Test_OperatorRegist_Fail_GroundedTemplate()
		{
			var kb = new KB();
			Assert.Throws<ArgumentException>(() => kb.RegistDynamicProperty((Name)"Count(John)", ((kb1, args, constraints) => null), null));
		}

		[Test]
		public void Test_OperatorRegist_Fail_Null_Surogate()
		{
			var kb = new KB();
			Assert.Throws<ArgumentNullException>(() => kb.RegistDynamicProperty((Name)"Count(John)", null, null));
		}

		[Test]
		public void Test_OperatorRegist_Fail_ConstantProperties()
		{
			var kb = new KB();
			Assert.Throws<ArgumentException>(() =>
			{
				kb.Tell((Name)"Count(John)", 3);
				kb.RegistDynamicProperty((Name)"Count([x])", ((kb1, args, constraints) => null), null);
			});
		}

		[Test]
		public void Test_Tell_Fail_OperatorRegist()
		{
			var kb = new KB();
			Assert.Throws<ArgumentException>(() =>
			{
				kb.RegistDynamicProperty((Name) "Count([x])", ((kb1, args, constraints) => null), null);
				kb.Tell((Name) "Count(John)", 3);
			});
		}
	}
}
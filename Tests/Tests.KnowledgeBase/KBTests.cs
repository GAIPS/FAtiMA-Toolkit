using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerializationUtilities;
using KnowledgeBase;
using NUnit.Framework;
using WellFormedNames;
using Conditions;

namespace Tests.KnowledgeBase
{
	[TestFixture]
	public class KBTests
	{
		[TestCase("true")]
		[TestCase("10")]
		[TestCase("Johan")]
		[TestCase("10e+34")]
		public void Test_Tell_Fail_Primitive_Property(string primitiveName)
		{
			var kb = new KB((Name)"John");
			Assert.Throws<ArgumentException>(() => kb.Tell((Name)primitiveName, Name.BuildName(true)));
		}

		[TestCase("Likes(*)")]
		[TestCase("Has([x])")]
		public void Test_Tell_Fail_NonConstant_Property(string propertyName)
		{
			var kb = new KB((Name)"John");
			Assert.Throws<ArgumentException>(() => kb.Tell((Name)propertyName, Name.BuildName(true)));
		}

		[Test]
		public void Test_Tell_Fail_Add_Self_To_Universal_Context()
		{
			var kb = new KB((Name)"John");
			Assert.Throws<InvalidOperationException>(() => kb.Tell((Name)"Likes(Self)", Name.BuildName(true),Name.UNIVERSAL_SYMBOL));
		}

		[TestCase("[x]", typeof(ArgumentException))]
		[TestCase("John([x])",typeof(ArgumentException))]
		[TestCase("John(Mary,Steve)",typeof(ArgumentException))]
		[TestCase("John(*)", typeof(ArgumentException))]
		[TestCase("*(John)", typeof(ArgumentException))]
		[TestCase("John(*(Steven([x])))", typeof(ArgumentException))]
		[TestCase("John(Mary(Steve(Self)))", typeof(ArgumentException))]
		public void Test_Tell_Fail_Assert_Perspective(string perspective,Type exceptionType)
		{
			var kb = new KB((Name)"John");

			Assert.Throws(exceptionType,() => kb.Tell((Name)"Likes(Mary)", Name.BuildName(true), (Name)perspective));
		}

		[TestCase("ToM(Mary,Likes(Self))", "Mary(Self)")]
		[TestCase("ToM(Mary,Likes(Self),Marty)", "Self")]
		[TestCase("ToM(John,ToM(Mary,Likes(John)))", "Mary")]
		public void Test_Tell_Fail_Property_ToM_Transform(string property,string perspective)
		{
			var kb = new KB((Name)"John");
			Assert.Throws<ArgumentException>(() => kb.Tell((Name) property, Name.BuildName(true),(Name)perspective));
		}

	    [TestCase("Has(Floor) = Player", "Has(Floor)", 0)]
	    [TestCase("Has(Floor) = Player", "-", 1)]
	    [TestCase("Has(Floor) = Player, DialogueState(P) = Start", "Has(Floor)", 1)]
	    [TestCase("Has(Floor) = Player, DialogueState(P) = Start", "Has(Floor), DialogueState(P)", 0)]
	    public void Test_KB_RemoveBelief(string toAdd, string toRemove, int total)
	    {
	        var kb = new KB((Name)"John");

	        var tell = toAdd.Split(',');

	        foreach (var b in tell)
	        {
	            var value = b.Split('=');
	            kb.Tell((Name)value[0], (Name)value[1]);
	        }

	        var rem = toRemove.Split(',');

	        foreach (var r in rem)
	        {
	            kb.removeBelief((Name)r);
	        }

	        Assert.AreEqual(kb.GetAllBeliefs().Count(), total);
	    }

		[TestCase("CountSubs(Tiger)", "Self")]
		[TestCase("CountSubs(Tiger)", "*")]
		[TestCase("CountSubs(Tiger)", "Mary")]
		[TestCase("CountSubs(Tiger)", "Mary(Self)")]
		[TestCase("ToM(Mary,CountSubs(Tiger))", "Self")]
		[TestCase("ToM(Mary,ToM(Self,CountSubs(Tiger)))", "Self")]
		[TestCase("ToM(John,CountSubs(Tiger))", "Mary")]
		public void Test_Tell_Fail_DynamicProperty(string property, string perspective)
		{
			var kb = new KB((Name)"John");
			Assert.Throws<ArgumentException>(() => kb.Tell((Name)property, Name.BuildName(true), (Name)perspective));
		}

		[Test]
		public void Test_Tell_Pass_Basic_Property_With_ToM()
		{
			const string property = "ToM(Mary,ToM(Self,Has(Ball)))";
			var kb = new KB((Name)"John");
			kb.Tell((Name) property, Name.BuildName(true));
		}

		[Test]
		public void Test_AskProperty_Self()
		{
			var kb = new KB((Name)"John");
			var value = Name.BuildName(kb.AskProperty(Name.SELF_SYMBOL).Value.ToString());
			Assert.AreEqual(value, kb.Perspective);
		}

		private class TestFactory
		{
			public static IEnumerable<TestCaseData> Test_Simple_Tell_Valid_Cases()
			{
				yield return new TestCaseData((Name)"like(Ann,john)", true);
				yield return new TestCaseData((Name)"like(Ann,Amy)", true);
				yield return new TestCaseData((Name)"like(Ann,Mike)", true);
				yield return new TestCaseData((Name)"like(Ann,Steven)", true);
				yield return new TestCaseData((Name)"Color(id_2433)", "Blue");
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

				yield return new TestCaseData((Name)"Strength(Name(Self))", 7, typeof(Exception));
				yield return new TestCaseData((Name)"Name(Self)", "Titus", typeof(Exception));
				yield return new TestCaseData((Name)"Name(Titus)", "Titus");
				yield return new TestCaseData((Name)"Strength(Name(Self))", 7);
			}

			public static IEnumerable<TestCaseData> Test_Simple_Tell_Invalid_Cases()
			{
				yield return new TestCaseData((Name)"[x]", false, typeof(ArgumentException));
				yield return new TestCaseData((Name)"like(self,[x])", false, typeof(ArgumentException));
				yield return new TestCaseData((Name)"10", 35, typeof(ArgumentException));
				yield return new TestCaseData((Name)"10", 10, typeof(ArgumentException));
				yield return new TestCaseData((Name)"true", 25, typeof(ArgumentException));
				yield return new TestCaseData((Name)"false", true, typeof(ArgumentException));
			}

			//public static int NumOfPersistentEntries()
			//{
			//	return MemoryData().Count(d => 
			//		d.Arguments.Length > 2 && (bool) d.Arguments[2]
			//		);
			//}

			public static KB PopulatedTestMemory()
			{
				KB kb = new KB((Name)"Me");
				foreach (var t in MemoryData())
				{
					Name property = (Name) t.Arguments[0];
					Name value = Name.BuildName(t.Arguments[1]);
					try
					{
						//if(t.Arguments.Length>2)
						//	kb.Tell((Name)t.Arguments[0], PrimitiveValue.Cast(t.Arguments[1]),(bool)t.Arguments[2]);
						//else
						kb.Tell(property, value);
					}
					catch (Exception e)
					{
						if (t.Arguments.Length > 2)
							Assert.AreEqual(e.GetType(), t.Arguments[2]);
						else
							Assert.Fail($"An exception was thrown unexpectedly while evaluating {property} = {value}: {e}");
					}
				}
				return kb;
			}
		}

		[TestCaseSource(typeof(TestFactory), nameof(TestFactory.Test_Simple_Tell_Valid_Cases))]
		public void Test_Simple_Tell_Valid(Name name, object value)
		{
			var kb = new KB((Name)"Me");
			kb.Tell(name, Name.BuildName(value));
		}

		[TestCaseSource(typeof(TestFactory), nameof(TestFactory.Test_Simple_Tell_Invalid_Cases))]
		public void Test_Simple_Tell_Invalid(Name name, object value, Type expectedException)
		{
			var kb = new KB((Name)"Me");
			Assert.Throws(expectedException, () => kb.Tell(name, Name.BuildName(value)));
		}

		[Test]
		public void Test_Acculm_Tell_Valid()
		{
			TestFactory.PopulatedTestMemory();
		}

		//[Test]
		//public void Test_RemoveProperty()
		//{
		//	throw new NotImplementedException();
		//}

		[TestCase("35", 35)]
		[TestCase("-9223372036854775807", -9223372036854775807)]
		[TestCase("-9.43", -9.43)]
		[TestCase("-9.43e-1", -9.43e-1)]
		[TestCase("true", true)]
		[TestCase("FALSE", false)]
		[TestCase("3.40282347E+39", 3.40282347E+39)]
		public void Test_PrimitiveValuesAsk(string str, object expect)
		{
			Name v = (Name)str;
			KB kb = new KB((Name)"Me");
			var value = kb.AskProperty(v).Value;
			Assert.NotNull(value);

			Assert.AreEqual(value, Name.BuildName(expect));
		}

		[TestCase("Matt", "IsPerson(Matt)", "*", "IsPerson(Matt)", "Self")]
		[TestCase("Matt", "IsPerson(Matt)", "*", "IsPerson(Matt)", "Mary")]
		[TestCase("Matt", "IsPerson(Self)", "Self", "IsPerson(Self)", "Self")]
		[TestCase("Matt", "IsPerson(Matt)", "Self", "IsPerson(Self)", "Matt")]
		[TestCase("Matt", "IsPerson(Self)", "Mary", "IsPerson(Mary)", "Mary")]
		[TestCase("Matt", "IsPerson(Self)", "Matt", "IsPerson(Self)", "Matt")]
		public void Test_Tell_Pass_Add_With_Perspective(string nativePerspective, string tellPerdicate, string tellPerspective, string queryPerdicate, string queryPerspective)
		{
			var kb = new KB(Name.BuildName(nativePerspective));
			kb.Tell(Name.BuildName(tellPerdicate), Name.BuildName(true), Name.BuildName(tellPerspective));

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, kb);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
			}

			var r = kb.AskProperty(Name.BuildName(queryPerdicate), Name.BuildName(queryPerspective));
			bool b;
			if (!r.Value.TryConvertToValue(out b))
				Assert.Fail();

			Assert.IsTrue(b);
		}

		[Test]
		public void Test_Fail_Tell_With_Nil_Perspective()
		{
			var kb = new KB(Name.BuildName("Mark"));
			Assert.Throws<ArgumentException>(() => kb.Tell(Name.BuildName("IsPerson(Self)"), Name.BuildName(true), Name.NIL_SYMBOL));
		}

		[Test]
		public void Test_Tell_Pass_Add_Self_Belief_and_Change_Perspective_01()
		{
			var kb = new KB(Name.BuildName("Mark"));
			kb.Tell(Name.BuildName("IsPerson(Self)"), Name.BuildName(true));

			kb.SetPerspective(Name.BuildName("Mary"));

		    var result = kb.AskProperty(Name.BuildName("IsPerson(Mark)"));

		    Assert.AreEqual(Name.NIL_STRING, result.Value.ToString());

			var n = kb.AskProperty(Name.BuildName("IsPerson(Mary)"));
			bool b;
			if(!n.Value.TryConvertToValue(out b))
				Assert.Fail();
			Assert.True(b);
		}

		[Test]
		public void Test_Tell_Pass_Add_Self_Belief_and_Change_Perspective_02()
		{
			var kb = new KB(Name.BuildName("Mark"));
			kb.Tell(Name.BuildName("IsPerson(Self)"), Name.BuildName(true),Name.BuildName("John(Self)"));

			kb.SetPerspective(Name.BuildName("Mary"));

		    Assert.AreEqual(Name.NIL_STRING, kb.AskProperty(Name.BuildName("IsPerson(Mark)"), Name.BuildName("John(Self)")).Value.ToString());

			var n = kb.AskProperty(Name.BuildName("IsPerson(Mary)"), Name.BuildName("John(Self)"));
			bool b;
			if(!n.Value.TryConvertToValue(out b))
				Assert.Fail();
			Assert.True(b);
		}

		[Test]
		public void Test_Fail_Change_Perspective_Conflict()
		{
			var kb = new KB(Name.BuildName("Mark"));
			kb.Tell(Name.BuildName("IsPerson(Self)"), Name.BuildName(true), Name.BuildName("John(Self)"));

			Assert.Throws<ArgumentException>(()=> kb.SetPerspective(Name.BuildName("John")));
		}

	    [Test]
	    public void Test_Tell_SameBelief_DifferentPerspective()
	    {
	        var kb = new KB(Name.BuildName("Mark"));

	        kb.Tell(Name.BuildName("Likes(Bread)"), Name.BuildName(true), Name.BuildName("Self"));

	        kb.Tell(Name.BuildName("Likes(Bread)"), Name.BuildName(false), Name.BuildName("Jose"));

	        var bs = kb.GetAllBeliefs();

	        var count = bs.Count();

            Assert.AreEqual(2, count);

	    }

		[TestCase("*")]
		[TestCase("-")]
		[TestCase("Test(Mark)")]
		[TestCase("Self")]
		public void Test_Fail_Change_Perspective_To_Invalid_Perspective(string perspective)
		{
			var kb = new KB(Name.BuildName("Mark"));
			kb.Tell(Name.BuildName("IsPerson(Self)"), Name.BuildName(true), Name.BuildName("John(Self)"));

			Assert.Throws<ArgumentException>(() => kb.SetPerspective(Name.BuildName(perspective)));
		}

		[Test]
		public void Test_Tell_Fail_Add_Self_To_Universal()
		{
			var kb = new KB(Name.BuildName("Matt"));
			Assert.Throws<InvalidOperationException>(() => { kb.Tell((Name)"IsPerson(Self)", Name.BuildName(true), Name.UNIVERSAL_SYMBOL); });
		}

		#region  Dynamic Property Tests

		private static IEnumerable<DynamicPropertyResult> DummyCount(IQueryContext context, Name x)
		{
			throw new NotImplementedException();
		}

		[Test]
		public void Test_DynamicProperty_Regist_Fail_Duplicate()
		{
			var kb = new KB((Name)"Me");
			Assert.Throws<ArgumentException>(
				() => kb.RegistDynamicProperty((Name)"CountSubs", "", DummyCount));
		}

		[Test]
		public void Test_DynamicProperty_Regist_Fail_InvalidTemplate()
		{
			var kb = new KB((Name)"Me");
			Assert.Throws<ArgumentException>(() => kb.RegistDynamicProperty((Name)"CountSubs(John)", "", DummyCount));
		}

		[Test]
		public void Test_DynamicProperty_Regist_Fail_Null_Surogate()
		{
			var kb = new KB((Name)"Me");
			Assert.Throws<ArgumentNullException>(() => kb.RegistDynamicProperty((Name)"CountSubs", "", (DynamicPropertyCalculator_T1)null));
		}

		[Test]
		public void Test_DynamicProperty_Regist_Fail_ConstantProperties()
		{
			var kb = new KB((Name)"Me");
			Assert.Throws<ArgumentException>(() =>
			{
				kb.Tell((Name)"CountSubs(John)", Name.BuildName(3));
				kb.RegistDynamicProperty((Name)"CountSubs", "", DummyCount);
			});
		}

		[Test]
		public void Test_Tell_Fail_DynamicProperty_Regist()
		{
			var kb = new KB((Name)"Me");
			Assert.Throws<ArgumentException>(() =>
			{
				kb.RegistDynamicProperty((Name)"CountSubs", "",DummyCount);
				kb.Tell((Name)"CountSubs(John)", Name.BuildName(3));
			});
		}

		private static IEnumerable<DynamicPropertyResult> Test_Concat_Dynamic_Property(IQueryContext context, Name x, Name y)
		{
			foreach (var v1 in context.AskPossibleProperties(x))
			{
				foreach (var v2 in context.AskPossibleProperties(y))
				{
					var c2 = Name.BuildName((Name)"Con", v1.Item1.Value, v2.Item1.Value);
					foreach (var s in v2.Item2)
					{
						yield return new DynamicPropertyResult(new ComplexValue(c2), s);
					}
				}
			}
		}

		[TestCase("Concat(x,y)",new[] {"Con(x,y)"})]
		[TestCase("Concat(true,Job([x]))", new[] {"Con(true,Spartan)", "Con(true,super-hero)" })]
		public void Test_DynamicProperty_NewDP_Pass(string expression, string[] expectedResult)
		{
			var kb = TestFactory.PopulatedTestMemory();
			kb.RegistDynamicProperty((Name)"Concat", "",Test_Concat_Dynamic_Property);

			var results = new HashSet<Name>(kb.AskPossibleProperties((Name)expression, Name.SELF_SYMBOL, null).Select(r => r.Item1.Value));
			var expected = new HashSet<Name>(expectedResult.Select(s => (Name) s));
			Assert.True(results.SetEquals(expected));
		}

		[TestCase("[x]","[y]",0)]
		[TestCase("[x]", "John", 0)]
		[TestCase("[x]", "Meaning(Toast,Baegle)", 3)]
		[TestCase("Toast", "Meaning(Toast,Baegle)", 1)]
		[TestCase("Dougnut", "Meaning(Toast,Baegle)", 0)]
		[TestCase("Dougnut", "Meaning([x],Baegle)", 0)]
		public void Test_DynamicProperty_HasLiteral(string x, string y, int num)
		{
			var kb = TestFactory.PopulatedTestMemory();
			var prop = (Name) string.Format("HasLiteral({0},{1})", x, y);

			var result = kb.AskPossibleProperties(prop, Name.SELF_SYMBOL, null).SelectMany(p => p.Item2).ToArray();
			Assert.AreEqual(result.Length,num);
		}



        [TestCase("[x] = 3", "Math(3, Plus, 1) = [re]", 4.0f)]
        [TestCase("[x] = 3", "Math([x], Plus, 1) = [re]", 4.0f)]
        [TestCase("[x] = 3, [y] = 1", "Math([x], Plus, [y]) = [re]", 4.0f)]
        [TestCase("[x] = 3", "Math([x], Minus, 1) = [re]", 2.0f)]
        [TestCase("[x] = 3, [y] = 1", "Math([x], Minus, [y]) = [re]", 2.0f)]
        [TestCase("[x] = 4, [y] = 2", "Math([x], Div, [y]) = [re]", 2.0f)]
        [TestCase("[x] = 4, [y] = 2", "Math([x], Times, [y]) = [re]", 8.0f)]
        [TestCase("[x] = 4, [y] = 200", "Math([x], Times, [y]) = [re]", 800.0f)]
        [TestCase("[x1] = 0, [y1] = 0, [x2] = 10, [y2] = 0", "SquareDistance([x1],[y1],[x2],[y2]) = [re]", 100.0f)]
        [TestCase("[x1] = 0, [y1] = 1, [x2] = 2, [y2] = 3", "SquareDistance([x1],[y1],[x2],[y2]) = [re]", 8.0f)]
        [TestCase("[x1] = -3, [y1] = 1, [x2] = 2, [y2] = -3", "SquareDistance([x1],[y1],[x2],[y2]) = [re]", 41.0f)]
        [Test]
        public void Test_DP_Math_Match(string contraints, string methodCall, float result)
        {
            var me = (Name)"Ana";
            var kb = new KB(me);


            var resultVariable = methodCall.Split('=')[1];


            var conditions = contraints.Split(',');

            IEnumerable<SubstitutionSet> resultingConstraints;

            var condSet = new ConditionSet();

            var cond = Condition.Parse(conditions[0]);

            // Apply conditions to RPC
            foreach (var c in conditions)
            {
                cond = Condition.Parse(c);
                condSet = condSet.Add(cond);


            }

            resultingConstraints = condSet.Unify(kb, Name.SELF_SYMBOL, null);

            condSet = new ConditionSet();
            cond = Condition.Parse(methodCall);
            condSet = condSet.Add(cond);


            var res = condSet.Unify(kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(res);

            Name actualResult = Name.BuildName(0);

            foreach(var v in res.FirstOrDefault())
            {
               if(v.Variable == (Name)resultVariable)
                {
                    actualResult = v.SubValue.Value;
                    break;
                }
            }

          

            Assert.AreEqual(actualResult, Name.BuildName(result) );
        }

        
        [TestCase("[y] = 1", "Math([x], Plus, 1) = [re]")]
        [TestCase("[x] != 1", "Math([x], Plus, 1) = [re]")]
        [TestCase("[x] = 3", "Math([x], Plus, [y]) = [re]")]
        [TestCase("[x] = 2", "Math([x], [y], 1) = [re]")]
        [TestCase("[y] = 1", "Math(3, Plus, [x]) = [re]")]
        [TestCase("[x] != 1", "Math(3, Plus, [x]) = [re]")]
        [TestCase("isAgent([x]) = True", "Math([x], Plus, 1) = [re]")]
        [TestCase("Mood([x]) = 0", "Math([x], Plus, 1) = [re]")]
        [Test]
        public void Test_DP_Math_NoMatch(string contraints, string methodCall)
        {
            var me = (Name)"Ana";
            var kb = new KB(me);


            var resultVariable = methodCall.Split('=')[1];


            var conditions = contraints.Split(',');

            IEnumerable<SubstitutionSet> resultingConstraints;

            var condSet = new ConditionSet();

            var cond = Condition.Parse(conditions[0]);

            // Apply conditions to RPC
            foreach (var c in conditions)
            {
                cond = Condition.Parse(c);
                condSet = condSet.Add(cond);


            }

            resultingConstraints = condSet.Unify(kb, Name.SELF_SYMBOL, null);

            condSet = new ConditionSet();
            cond = Condition.Parse(methodCall);
            condSet = condSet.Add(cond);


            var res = condSet.Unify(kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsEmpty(res);

        
        }

        #endregion

        [Test]
		public void Test_Self_Property()
		{
			var me = (Name) "Ana";
			var kb = new KB(me);
			kb.Tell((Name)"A(B)",Name.SELF_SYMBOL);
			Assert.AreEqual(kb.AskProperty((Name) "A(B)").Value, me);

			me = (Name) "John";
			kb.SetPerspective(me);
			Assert.AreEqual(kb.AskProperty((Name)"A(B)").Value, me);

			kb.Tell((Name)"A(B)", Name.SELF_SYMBOL,(Name)"Ana");
			Assert.AreEqual(kb.AskProperty((Name)"A(B)", (Name)"Ana").Value, (Name)"Ana");
		}





	}
}
using System;
using System.Linq;
using Conditions;
using KnowledgeBase;
using NUnit.Framework;
using WellFormedNames;
using WellFormedNames.Exceptions;

namespace Tests.Conditions
{
	[TestFixture]
	public class ConditionsTests
	{
		[TestCase("Value(X)>4")]
		[TestCase("Value(y)<  6")]
		[TestCase("Prop(Value)  <6")]
		[TestCase("Value(z)<  -65.54e10")]
		[TestCase("Prop(Value)  !=  true\t")]
		[TestCase("\t\t9  >= \t [y]")]
		[TestCase("[x]<=[y]")]
		[TestCase("#[x]<=#[y]")]
		[TestCase("#[z]!=4")]
		public void Condition_Valid_Parse(string str)
		{
			var c = Condition.Parse(str);
		}

		[TestCase("A=A",typeof(InvalidOperationException))]
		[TestCase("4>3",typeof(InvalidOperationException))]
		[TestCase("true!=false",typeof(InvalidOperationException))]
		[TestCase("true=false",typeof(InvalidOperationException))]
		[TestCase("-65.54e10<-1e",typeof(ParsingException))]
		[TestCase("?-65.54e10<+-1e",typeof(ParsingException))]
		[TestCase("#Like([x]) = 4",typeof(ParsingException))]
		[TestCase("#4 = 4",typeof(ParsingException))]
		[TestCase("#4 % 4", typeof(ParsingException))]
		public void Condition_Invalid_Parse(string str, Type expectedException)
		{
			Assert.Throws(expectedException, () =>
			{
				var c = Condition.Parse(str);
			});
		}

		[Test]
		public void Test_Fail_FalsePerdicate()
		{
			var kb = new KB((Name)"John");
			var c = Condition.Parse("Likes(Formal) = false");

			Assert.False(c.Evaluate(kb, Name.SELF_SYMBOL, new[] {new SubstitutionSet()}));
		}

		//Test KB
		private static KB _kb = CreateKB();
		private static KB CreateKB()
		{
			var kb = new KB((Name)"Agatha");

			kb.Tell((Name)"Strength(John)",Name.BuildName(5));
			kb.Tell((Name)"Strength(Mary)", Name.BuildName(3));
			kb.Tell((Name)"Strength(Leonidas)", Name.BuildName(500));
			kb.Tell((Name)"Strength(Goku)", Name.BuildName(9001f));
			kb.Tell((Name)"Strength(SuperMan)", Name.BuildName(ulong.MaxValue));
			kb.Tell((Name)"Strength(Saitama)", Name.BuildName(float.MaxValue));
			kb.Tell((Name)"Race(Saitama)", Name.BuildName("human"));
			kb.Tell((Name)"Race(Superman)", Name.BuildName("kriptonian"));
			kb.Tell((Name)"Race(Goku)",Name.BuildName("sayian"));
			kb.Tell((Name)"Race(Leonidas)", Name.BuildName("human"));
			kb.Tell((Name)"Race(Mary)", Name.BuildName("human"));
			kb.Tell((Name)"Race(John)", Name.BuildName("human"));
			kb.Tell((Name)"Job(Saitama)", Name.BuildName("super-hero"));
			kb.Tell((Name)"Job(Superman)", Name.BuildName("super-hero"));
			kb.Tell((Name)"Job(Leonidas)", Name.BuildName("Spartan"));
			kb.Tell((Name)"AKA(Saitama)", Name.BuildName("One-Punch_Man"));
			kb.Tell((Name)"AKA(Superman)", Name.BuildName("Clark_Kent"));
			kb.Tell((Name)"AKA(Goku)", Name.BuildName("Kakarot"));
			kb.Tell((Name)"Hobby(Saitama)", Name.BuildName("super-hero"));
			kb.Tell((Name)"Hobby(Goku)", Name.BuildName("training"));
			kb.Tell((Name)"IsAlive(Leonidas)", Name.BuildName(false));
			kb.Tell((Name)"IsAlive(Saitama)", Name.BuildName(true));
			kb.Tell((Name)"IsAlive(Superman)", Name.BuildName(true));
			kb.Tell((Name)"IsAlive(John)", Name.BuildName(true));

			return kb;
		}

		[TestCase("Strength(John)=5",true)]
		[TestCase("34.7!=Strength(John)", true)]
		[TestCase("Strength(Saitama)>=Strength(SuperMan)", true)]
		[TestCase("Strength([x])<Strength(Goku)", true)]
		[TestCase("Strength(Saitama)<Strength([x])", false)]
		[TestCase("Strength([x])>Strength([y])", true)]
		[TestCase("Race(Saitama)=human", true)]
		[TestCase("Race(goku)=sayian", true)]
		[TestCase("IsAlive(Saitama)=true", true)]
		[TestCase("IsAlive(Goku)!=true", false)] //Closed world assumption
		[TestCase("#[x] = 0", true)]
		[TestCase("#[x] = #[y]", true)]
		[TestCase("1 = #[y]", false)]
		public void Test_Condition(string conditionStr, bool result)
		{
			var c = Condition.Parse(conditionStr);
			var v = c.Evaluate(_kb, Name.SELF_SYMBOL, null);
			Assert.AreEqual(v, result);
		}

		[TestCase("Strength(John)","5",ComparisonOperator.Equal)]
		[TestCase("34.7","Strength(John)", ComparisonOperator.NotEqual)]
		[TestCase("Strength(Saitama)","Strength(SuperMan)", ComparisonOperator.GreatherOrEqualThan)]
		[TestCase("Strength([x])","Strength(Goku)", ComparisonOperator.LessThan)]
		[TestCase("Strength(Saitama)","Strength([x])", ComparisonOperator.LessThan)]
		[TestCase("Strength([x])","Strength([y])", ComparisonOperator.GreatherThan)]
		public void Test_Condition_HashCode(string str1, string str2, ComparisonOperator op)
		{
			var n1 = (Name) str1;
			var n2 = (Name)str2;

			var c1 = Condition.BuildCondition(n1,n2,op);
			var c2 = Condition.BuildCondition(n2, n1, op.Mirror());

			Assert.AreEqual(c1.GetHashCode(),c2.GetHashCode());
		}

		[TestCase("Strength(John)", "5", ComparisonOperator.Equal)]
		[TestCase("34.7", "Strength(John)", ComparisonOperator.NotEqual)]
		[TestCase("Strength(Saitama)", "Strength(SuperMan)", ComparisonOperator.GreatherOrEqualThan)]
		[TestCase("Strength([x])", "Strength(Goku)", ComparisonOperator.LessThan)]
		[TestCase("Strength(Saitama)", "Strength([x])", ComparisonOperator.LessThan)]
		[TestCase("Strength([x])", "Strength([y])", ComparisonOperator.GreatherThan)]
		public void Test_Condition_Equals(string str1, string str2, ComparisonOperator op)
		{
			var n1 = (Name)str1;
			var n2 = (Name)str2;

			var c1 = Condition.BuildCondition(n1, n2, op);
			var c2 = Condition.BuildCondition(n2, n1, op.Mirror());

			Assert.AreEqual(c1, c2);
		}

		[TestCase(new[] { "Strength([x])<=Strength(Saitama)", "Strength([x])>=Strength(Goku)", "[x]!=Saitama", "[x]!=goku" }, true, null)]
		//[TestCase(new[] { "Race([y])!=Race([x])", "Strength([x])>=Strength([y])", "[x]!=[y]" }, true, null)]
		//[TestCase(new[] { "Race([y])!=Race([x])", "Strength([x])>=Strength([y])", "[x]!=[y]", "#[x]=3" }, true, null)]
		//[TestCase(new[] { "Race([y])!=Race([x])", "Strength([x])>=Strength([y])", "[x]!=[y]", "Count([x])=3", "Count([y])=5" }, true, null)]
		//[TestCase(new[] { "Count(Like([x]))=0" }, true, null)]
		public void Test_ConditionSet(string[] conditions, bool result, string[] constraints)
		{
			var set = constraints!=null?new[]{new SubstitutionSet(constraints.Select(c => new Substitution(c)))}:null;
			var conds = new ConditionSet(conditions.Select(Condition.Parse));
			Assert.AreEqual(result, conds.Unify(_kb, Name.SELF_SYMBOL, set).Any());
		}

		[Test]
		public void Test_SelfCompare_Equal()
		{
			var c = Condition.Parse("[x] = Self");
			var s = new Substitution("[x]/Agatha");
			var res = c.Evaluate(_kb, Name.SELF_SYMBOL, new []{ new SubstitutionSet(s) });
			Assert.IsTrue(res);
		}

		[Test]
		public void Test_SelfCompare_Diferent()
		{
			var c = Condition.Parse("[x] != Self");
			var s = new Substitution("[x]/Agatha");
			var res = c.Evaluate(_kb, Name.SELF_SYMBOL, new[] { new SubstitutionSet(s) });
			Assert.IsFalse(res);
		}
	}
}
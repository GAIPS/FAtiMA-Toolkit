using System;
using System.Linq;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.Conditions
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
		public void Condition_Valid_Parse(string str)
		{
			var c = Condition.Parse(str);
		}

		[TestCase("A=A",ExpectedException = typeof(InvalidOperationException))]
		[TestCase("4>3",ExpectedException = typeof(InvalidOperationException))]
		[TestCase("true!=false", ExpectedException = typeof(InvalidOperationException))]
		[TestCase("true=false", ExpectedException = typeof(InvalidOperationException))]
		[TestCase("-65.54e10<-1e", ExpectedException = typeof(ParsingException))]
		public void Condition_Invalid_Parse(string str)
		{
			var c = Condition.Parse(str);
		}

		//Test KB
		private static KB _kb = CreateKB();
		private static KB CreateKB()
		{
			var kb = new KB();

			kb.Tell((Name)"Strength(John)",(byte)5);
			kb.Tell((Name)"Strength(Mary)", (sbyte)3);
			kb.Tell((Name)"Strength(Leonidas)", (short)500);
			kb.Tell((Name)"Strength(Goku)", (uint)9001f);
			kb.Tell((Name)"Strength(SuperMan)", ulong.MaxValue);
			kb.Tell((Name)"Strength(Saitama)", float.MaxValue);
			kb.Tell((Name)"Race(Saitama)", "human");
			kb.Tell((Name)"Race(Superman)", "kriptonian");
			kb.Tell((Name)"Race(Goku)","sayian");
			kb.Tell((Name)"Race(Leonidas)", "human");
			kb.Tell((Name)"Race(Mary)", "human");
			kb.Tell((Name)"Race(John)", "human");
			kb.Tell((Name)"Job(Saitama)", "super-hero");
			kb.Tell((Name)"Job(Superman)", "super-hero");
			kb.Tell((Name)"Job(Leonidas)", "Spartan");
			kb.Tell((Name)"AKA(Saitama)", "One-Punch_Man");
			kb.Tell((Name)"AKA(Superman)", "Clark_Kent");
			kb.Tell((Name)"AKA(Goku)", "Kakarot");
			kb.Tell((Name)"Hobby(Saitama)", "super-hero");
			kb.Tell((Name)"Hobby(Goku)", "training");
			kb.Tell((Name)"IsAlive(Leonidas)", false);
			kb.Tell((Name)"IsAlive(Saitama)", true);
			kb.Tell((Name)"IsAlive(Superman)", true);
			kb.Tell((Name)"IsAlive(John)", true);

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
		[TestCase("IsAlive(Goku)!=true", true)]
		public void Test_Condition(string conditionStr, bool result)
		{
			var c = Condition.Parse(conditionStr);
			var v = c.Evaluate(_kb, null);
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

		[TestCase(new[] { "Strength([x])<=Strength(Saitama)", "Strength([x])>=Strength(Goku)","[x]!=Saitama","[x]!=goku" }, true,null)]
		[TestCase(new[] { "Race([y])!=Race([x])", "Strength([x])>=Strength([y])", "[x]!=[y]" }, true, null)]
		public void Test_ConditionSet(string[] conditions, bool result, string[] constraints)
		{
			var set = constraints!=null?new SubstitutionSet(constraints.Select(c => new Substitution(c))):null;
			var conds = new ConditionEvaluatorSet(conditions.Select(Condition.Parse));

			Assert.AreEqual(result, conds.Evaluate(_kb, set));
		}
	}
}
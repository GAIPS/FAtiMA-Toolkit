using System.Linq;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.Conditions
{
	[TestFixture]
	public class ConditionEvaluatorSetTests
	{
		private static readonly Condition[] TEST_CONDITIONS = new Condition[]
		{
			Condition.Parse("Likes([x]) = true"),
			Condition.Parse("Has([x]) = true")
		};

		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):true", "Has(Health):true")]
		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):true", "Has(Health):false")]
		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):false", "Has(Health):true")]
		public void ConditionEvaluatorSet_Test_Exists_Pass(params string[] beliefs)
		{
			var kb = new KB((Name)"Me");
			foreach (var s in beliefs.Select(b => b.Split(':')))
				kb.Tell((Name)s[0], PrimitiveValue.Parse(s[1]));

			var set = new ConditionSet(LogicalQuantifier.Existential,TEST_CONDITIONS);
			Assert.True(set.Evaluate(kb, Name.SELF_SYMBOL, null));
		}

		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):false", "Has(Health):false")]
		public void ConditionEvaluatorSet_Test_Exists_Fail(params string[] beliefs)
		{
			var kb = new KB((Name)"Me");
			foreach (var s in beliefs.Select(b => b.Split(':')))
				kb.Tell((Name)s[0], PrimitiveValue.Parse(s[1]));

			var set = new ConditionSet(LogicalQuantifier.Existential, TEST_CONDITIONS);
			Assert.False(set.Evaluate(kb, Name.SELF_SYMBOL, null));
		}

		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):false", "Has(Health):false")]
		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):true", "Has(Health):false")]
		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):false", "Has(Health):true")]
		public void ConditionEvaluatorSet_Test_Universal_Fail(params string[] beliefs)
		{
			var kb = new KB((Name)"Me");
			foreach (var s in beliefs.Select(b => b.Split(':')))
				kb.Tell((Name)s[0], PrimitiveValue.Parse(s[1]));

			var set = new ConditionSet(LogicalQuantifier.Universal, TEST_CONDITIONS);
			Assert.False(set.Evaluate(kb, Name.SELF_SYMBOL, null));
		}

		[TestCase("Likes(Money):true", "Likes(Health):true", "Has(Money):true", "Has(Health):true")]
		public void ConditionEvaluatorSet_Test_Universal_Pass(params string[] beliefs)
		{
			var kb = new KB((Name)"Me");
			foreach (var s in beliefs.Select(b => b.Split(':')))
				kb.Tell((Name)s[0], PrimitiveValue.Parse(s[1]));

			var set = new ConditionSet(LogicalQuantifier.Universal, TEST_CONDITIONS);
			Assert.True(set.Evaluate(kb, Name.SELF_SYMBOL, null));
		}
	}
}
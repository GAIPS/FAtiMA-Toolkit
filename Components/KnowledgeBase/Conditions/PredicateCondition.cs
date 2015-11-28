using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public sealed class PredicateCondition : ICondition
	{
		public Name Predicate { get; private set; }

		public PredicateCondition(Name predicate)
		{
			this.Predicate = predicate;
		}

		public bool Evaluate(Memory kb)
		{
			return kb.AskPredicate(Predicate);
		}
	}
}
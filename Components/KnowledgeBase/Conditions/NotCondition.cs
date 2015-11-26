namespace KnowledgeBase.Conditions
{
	public class NotCondition : ICondition
	{
		public ICondition Condition { get; set; }

		public NotCondition(ICondition condition)
		{
			this.Condition = condition;
		}

		public bool Evaluate(KnowledgeBase kb)
		{
			return !this.Condition.Evaluate(kb);
		}
	}
}
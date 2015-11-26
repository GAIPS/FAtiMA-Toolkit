namespace KnowledgeBase.Conditions
{
	public interface ICondition
	{
		bool Evaluate(KnowledgeBase kb);
	}
}
namespace KnowledgeBase.Conditions
{
	public interface ICondition
	{
		bool Evaluate(Memory kb);
	}
}
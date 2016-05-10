namespace KnowledgeBase.WellFormedNames.Interfaces
{
	public interface IVariableRenamer<T> where T : IVariableRenamer<T>
	{
		T ReplaceUnboundVariables(string id);

		T RemoveBoundedVariables(string id);
	}
}
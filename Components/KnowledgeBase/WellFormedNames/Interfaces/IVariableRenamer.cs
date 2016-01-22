namespace KnowledgeBase.WellFormedNames.Interfaces
{
	public interface IVariableRenamer<T> where T : IVariableRenamer<T>
	{
		/// <summary>
		/// Returns a copy of this object with all unbound variables in the object by applying an identifier to each one.
		/// For example, the variable [x] becomes [x4] if the received ID is "4".
		/// </summary>
		/// <param name="variableId">the identifier to be applied</param>
		T ReplaceUnboundVariables(string id);

		T RemoveBoundedVariables(string id);
	}
}
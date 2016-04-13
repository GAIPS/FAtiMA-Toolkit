using KnowledgeBase.WellFormedNames;
using Utilities;

namespace ActionLibrary
{
	public static class ActionsExtentedOperations
	{
		public static Name ToNameRepresentation(this IAction action)
		{
			return Name.BuildName(action.Parameters.Prepend(action.ActionName));
		}
	}
}
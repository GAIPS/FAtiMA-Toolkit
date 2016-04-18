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

		private static readonly Name EVT_NAME = Name.BuildName("Event");
		private static readonly Name ACTION_NAME = Name.BuildName("Action");
		public static Name ToEventName(this IAction action, Name subject)
		{
			var a = ToNameRepresentation(action);
			return Name.BuildName(EVT_NAME, ACTION_NAME, subject, a, action.Target);
		}
	}
}
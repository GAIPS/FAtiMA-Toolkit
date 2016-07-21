using WellFormedNames;
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
		private static readonly Name ACTION_START_NAME = Name.BuildName("Action-Start");
		private static readonly Name ACTION_FINISHED_NAME = Name.BuildName("Action-Finished");

		public static Name ToStartEventName(this IAction action, Name subject)
		{
			var a = ToNameRepresentation(action);
			return Name.BuildName(EVT_NAME, ACTION_START_NAME, subject, a, action.Target);
		}

		public static Name ToFinishedEventName(this IAction action, Name subject)
		{
			var a = ToNameRepresentation(action);
			return Name.BuildName(EVT_NAME, ACTION_FINISHED_NAME, subject, a, action.Target);
		}
	}
}
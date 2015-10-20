using EmotionalAppraisal.Interfaces;
using System.Collections.Generic;

public static class EventOperations
{
	public static bool MatchEvents(IEvent matchRule, IEvent eventPerception)
	{
		if (matchRule.Action != null && !matchRule.Action.Equals(eventPerception.Action))
			return false;

		if (matchRule.Subject != null && eventPerception.Subject != null)
		{
			if (!matchRule.Subject.Equals(eventPerception.Subject))
				return false;
		}

		if (matchRule.Target != null && !matchRule.Target.Equals(eventPerception.Target))
			return false;

		IEnumerator<IEventParameter> it1 = matchRule.Parameters.GetEnumerator();
		IEnumerator<IEventParameter> it2 = eventPerception.Parameters.GetEnumerator();

		while (it1.MoveNext() && it2.MoveNext())
		{
			if (!it1.Current.ParameterName.Equals(it2.Current.ParameterName))
				return false;

			if (!it1.Current.Value.Equals("*") && !it1.Current.Value.Equals(it2.Current.Value))
				return false;
		}

		if (it1.MoveNext() || it2.MoveNext())
			return false;

		return true;
	}
}

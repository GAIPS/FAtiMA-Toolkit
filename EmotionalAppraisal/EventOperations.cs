using System;
using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
    public static class EventOperations
    {
	    private static bool MatchStrings(string str1, string str2)
	    {
		    if (str1 == null || str2 == null) return true;
		    return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
	    }

	    public static bool MatchEvents(IEvent matchRule, IEvent eventPerception)
	    {
		    if (!MatchStrings(matchRule.Action, eventPerception.Action))
			    return false;

			if (!MatchStrings(matchRule.Subject, eventPerception.Subject))
				return false;

			if (!MatchStrings(matchRule.Target, eventPerception.Target))
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

		//public static Name ToName(this IEvent evt)
		//{
		//	//if (bindings != null)
		//	//{
		//	//	if (evt.Parameters.Any())
		//	//	{
		//	//		var subs = evt.Parameters.Select(p => new Substitution(new Symbol(p.ParameterName), Name.Parse(p.Value.ToString())));
		//	//		bindings.AddSubstitutions(subs);
		//	//	}
		//	//}

		//	Name n = new ComposedName(new Symbol("Event"),
		//		new Symbol(evt.Subject),
		//		new Symbol(evt.Action),
		//		evt.Target == null ? Symbol.NIL_SYMBOL : new Symbol(evt.Target));
		//	return n;
		//}

	    /// <summary>
	    /// Generates a set of bindings that associate the Variables
	    /// [Subject],[Action],[Target],[P1_Name],[P2_Name],... respectively to the event's subject, action, target and parameters 
	    /// </summary>
	    /// <returns>the mentioned set of substitutions</returns>
	    public static IEnumerable<Substitution> GenerateBindings(this IEvent evt)
	    {
			yield return new Substitution("[Subject]", evt.Subject);
			yield return new Substitution("[Action]", evt.Action);
			yield return new Substitution("[Target]", evt.Target ?? Symbol.UNIVERSAL_STRING);

			foreach (var p in evt.Parameters)
			{
				yield return new Substitution("[" + p.ParameterName + "]", p.Value.ToString());
			}
	    }
    }
}

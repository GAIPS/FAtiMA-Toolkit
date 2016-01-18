using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
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
				if (!string.Equals(it1.Current.ParameterName, it2.Current.ParameterName, StringComparison.InvariantCultureIgnoreCase))
                    return false;

                if (!it1.Current.Value.Match(it2.Current.Value))
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

	    public static Name ToIdentifierName(this IEvent evt)
	    {
		    return Name.BuildName((Name) "Event", (Name) evt.Subject, (Name) evt.Action, (Name) evt.Target);
	    }

	    /// <summary>
	    /// Generates a set of bindings that associate the Variables
	    /// [Subject],[Action],[Target],[P1_Name],[P2_Name],... respectively to the event's subject, action, target and parameters 
	    /// </summary>
	    /// <returns>the mentioned set of substitutions</returns>
	    public static IEnumerable<Substitution> GenerateBindings(this IEvent evt)
	    {
			yield return new Substitution("[Subject]", evt.Subject);
			yield return new Substitution("[Action]", evt.Action);
			yield return new Substitution("[Target]", evt.Target ?? Name.UNIVERSAL_STRING);

		    var parameters = evt.Parameters;
		    if (parameters != null)
		    {
				foreach (var p in evt.Parameters)
				{
					yield return new Substitution("[" + p.ParameterName + "]", p.Value.ToString());
				}
		    }
	    }

		public static IEnumerable<Substitution> GenerateParameterBindings(this IEvent evt)
		{
			return evt.Parameters.Select(p => new Substitution("[" + p.ParameterName + "]", p.Value.ToString()));
		}
		/*
	    public static IEvent ApplyPerspective(IEvent evt, string perspective)
	    {
			return new internalEventDef(evt,perspective);
	    }
		
		private class internalEventDef : IEvent
		{
			public internalEventDef(IEvent evt, string perspective)
			{
				Action = evt.Action;
				Subject = Name.ApplyPerspective(evt.Subject, perspective);
				Target = evt.Target == null ? null : Name.ApplyPerspective(evt.Target, perspective);
				Timestamp = evt.Timestamp;
				this.Parameters = null;
				if (evt.Parameters != null && evt.Parameters.Any())
					this.Parameters = evt.Parameters.ToArray();
			}

			public string Action { get; private set; }

			public string Subject { get; private set; }

			public string Target { get; private set; }

			public DateTime Timestamp { get; private set; }

			public IEnumerable<IEventParameter> Parameters { get; private set; }
		}
		*/
    }
}

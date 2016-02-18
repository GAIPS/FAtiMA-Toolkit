using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public sealed partial class ActionTendency
	{
		private class Action : IAction
		{
			public Name ActionName { get; private set; }
			public Name Target { get; private set; }

			public IList<Name> Parameters { get; }

			public Action(Name actionNameName, Name target, IList<Name> parameters)
			{
				ActionName = actionNameName;
				Target = target;
				Parameters = parameters;
			}
		}
	}
}
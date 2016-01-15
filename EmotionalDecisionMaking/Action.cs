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
			private Dictionary<string, Name> m_parameters;

			public Name ActionName{ get; private set; }

			public IEnumerable<IActionParameter> Parameters
			{
				get { return m_parameters.Select(p => new ActionParameter(p.Key, p.Value)).Cast<IActionParameter>(); }
			}

			public Action(Name actionNameName, IEnumerable<IActionParameter> parameters)
			{
				ActionName = actionNameName;
				m_parameters = parameters.ToDictionary(p => p.ParameterName, p => p.Value,StringComparer.InvariantCultureIgnoreCase);
			}

			public Name this[string parameterName]
			{
				get
				{
					Name value;
					if (!m_parameters.TryGetValue(parameterName, out value))
						return Name.NIL_SYMBOL;
					return value;
				}
			}
		}

		private class ActionParameter : IActionParameter
		{
			public ActionParameter(string paramName, Name value)
			{
				ParameterName = paramName;
				Value = value;
			}

			public string ParameterName { get; private set; }

			public Name Value { get; private set; }
		}
	}
}
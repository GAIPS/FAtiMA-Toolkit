using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public sealed partial class ReactiveActions
	{
		private class Action_impl : IAction
		{
			private Dictionary<string, Name> m_parameters;

			public string Action{ get; private set; }

			public string Subject { get; private set; }

			public string Target { get; private set; }

			public IEnumerable<IActionParameter> Parameters
			{
				get { return m_parameters.Select(p => new ActionParameter(p.Key, p.Value)).Cast<IActionParameter>(); }
			}

			public Action_impl(string actionName, string subject, string target, IEnumerable<IActionParameter> parameters)
			{
				Action = actionName;
				Subject = subject;
				Target = target;
				m_parameters = parameters.ToDictionary(p => p.ParameterName, p => p.Value);
			}

			public Name this[string parameterName]
			{
				get
				{
					Name value;
					if (!m_parameters.TryGetValue(parameterName, out value))
						return null;
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
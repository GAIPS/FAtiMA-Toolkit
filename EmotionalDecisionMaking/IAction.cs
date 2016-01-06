using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public interface IAction
	{
		string Subject { get; }
		string Action { get; }
		string Target { get; }
		IEnumerable<IActionParameter> Parameters { get; }

		Name this[string parameterName] { get; }
	}

	public interface IActionParameter
	{
		string ParameterName { get; }
		Name Value { get; }
	}
}
using EmotionalAppraisal.Interfaces;
using System.Collections.Generic;

namespace EmotionalAppraisal
{
	public interface IAppraisalFrame
	{
		IEvent AppraisedEvent { get; }
		IEnumerable<string> AppraisalVariables { get; }
		bool IsEmpty { get; }
		long LastChange { get; }

		float GetAppraisalVariable(string appraisalVariable);
		bool ContainsAppraisalVariable(string appraisalVariable);
		bool TryGetAppraisalVariable(string appraisalVariable, out float value);
	}
}

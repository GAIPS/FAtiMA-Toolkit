using System.Collections.Generic;
using AutobiographicMemory;
using WellFormedNames;

namespace EmotionalAppraisal
{
	public interface IAppraisalFrame
	{
		IBaseEvent AppraisedEvent { get; }
		IEnumerable<string> AppraisalVariables { get; }
		bool IsEmpty { get; }
        Name Perspective { get; set; }

		float GetAppraisalVariable(string appraisalVariable);
		bool ContainsAppraisalVariable(string appraisalVariable);
        void SetAppraisalVariable(string appraisalVariableName, float value);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmotionalAppraisal
{
	public interface IWritableAppraisalFrame : IAppraisalFrame
	{
		void SetAppraisalVariable(string appraisalVariableName, float value);
	}
}

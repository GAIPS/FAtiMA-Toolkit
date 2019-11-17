using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using WellFormedNames;

namespace EmotionalAppraisal
{
	public partial class EmotionalAppraisalAsset
	{
        private class InternalAppraisalFrame : IAppraisalFrame
		{
			private Dictionary<string, float> appraisalVariables = new Dictionary<string, float>();

			public IBaseEvent AppraisedEvent { get;  set; }

			public IEnumerable<string> AppraisalVariables
			{
				get { return this.appraisalVariables.Keys; }
			}

			public bool IsEmpty
			{
				get { return this.appraisalVariables.Count == 0; }
			}


            public Name Perspective
            {
                get;
                set;
            }

            public InternalAppraisalFrame()
			{
				AppraisedEvent = null;
			}


			public float GetAppraisalVariable(string appraisalVariable)
			{
                if (this.appraisalVariables.ContainsKey(appraisalVariable))
                    return appraisalVariables[appraisalVariable];
                else return 0f;
			}

			public bool ContainsAppraisalVariable(string appraisalVariable)
			{
				return this.appraisalVariables.ContainsKey(appraisalVariable);
			}

            public void SetAppraisalVariable(string appraisalVariableName, float value)
            {
                this.appraisalVariables[appraisalVariableName] = value;
            }
        }
	}
}

using System;
using System.Collections.Generic;
using EmotionalAppraisal.DTOs;
using SerializationUtilities;
using WellFormedNames;

namespace EmotionalAppraisal
{

    [Serializable]
    public class AppraisalVariables : ICustomSerialization
    {
        
        public List<AppraisalVariableDTO> appraisalVariables;


        public AppraisalVariables(List<AppraisalVariableDTO> _Dto)
        {

            appraisalVariables = _Dto;

        }

        public AppraisalVariables()
        {
            appraisalVariables = new List<AppraisalVariableDTO>();
        }
        

        public override string ToString()
        {

            string ret = "";
        
            foreach(var app in appraisalVariables){

                if(app.Target != null  && app.Target != (Name)"-")

                ret += string.Format(" {0}({2})={1} ", app.Name, app.Value, app.Target);

                else   ret += string.Format(" {0}={1} ", app.Name, app.Value, app.Target);
            }

            return ret;
                               
    }


        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AppraisalVariables", this.appraisalVariables);
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			this.appraisalVariables = dataHolder.GetValue<List<AppraisalVariableDTO>>("AppraisalVariables");

			
		}

}
}

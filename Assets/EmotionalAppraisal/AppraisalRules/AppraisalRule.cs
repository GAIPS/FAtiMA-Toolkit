using System;
using System.Collections.Generic;
using System.Linq;
using Conditions;
using EmotionalAppraisal.DTOs;
using WellFormedNames;
using AutobiographicMemory;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	///     Represents an Emotional AppraisalRule based in Construal Frames that specify values
	///     for some of OCC's appraisal variables: Desirability, DesirabilityForOther, Like and
	///     Praiseworthiness.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class AppraisalRule
	{
		[NonSerialized]
		private Guid m_id;

		public Guid Id { get { return m_id; } set { m_id = value; } }
        public Name EventName { get; set; }
        public ConditionSet Conditions { get; set; }

        
        public AppraisalVariables AppraisalVariables{ get; set; }
        
	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
		    m_id = (appraisalRuleDTO.Id == Guid.Empty)?Guid.NewGuid() : appraisalRuleDTO.Id;
            EventName = (Name)appraisalRuleDTO.EventMatchingTemplate;

            if(appraisalRuleDTO.AppraisalVariables != null)
	        AppraisalVariables = appraisalRuleDTO.AppraisalVariables;
            else 	        AppraisalVariables = new AppraisalVariables();

            
			Conditions = appraisalRuleDTO.Conditions==null ? new ConditionSet() : new ConditionSet(appraisalRuleDTO.Conditions);
	    }

		/// <summary>
		///     Clone Constructor
		/// </summary>
		/// <param name="other">the reaction to clone</param>
		public AppraisalRule(AppraisalRule other)
		{
			m_id = other.m_id;
			EventName = other.EventName;
            if(other.Conditions==null || other.Conditions.Count == 0)
                Conditions = new ConditionSet();
            else Conditions = new ConditionSet(other.Conditions);
		    AppraisalVariables = other.AppraisalVariables;
		}


          public void AddOrUpdateAppraisalVariables(AppraisalVariableDTO newVar)
        {

            if(this.AppraisalVariables.appraisalVariables.Contains(newVar))
                  this.AppraisalVariables.appraisalVariables.Remove(newVar);
            this.AppraisalVariables.appraisalVariables.Add(newVar);



		}

        public void RemoveAppraisalVariables(AppraisalVariableDTO newVars)
        {
           this.AppraisalVariables.appraisalVariables.Remove(newVars);
		}
        
           public AppraisalVariableDTO getAppraisalVar(Name app)
        {
                if(AppraisalVariables != null)
            return this.AppraisalVariables.appraisalVariables.Find(x=>x.Name == app.ToString());
                   else return null;
                }

        public List<AppraisalVariableDTO> getAppraisalVariables()
        {
            if(AppraisalVariables != null)
                return this.AppraisalVariables.appraisalVariables;
            else return  null;
        }

	}
}
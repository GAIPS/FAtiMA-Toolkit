using System;
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

        /// <summary>
        ///     Desirability of the event
        /// </summary>
        public Name Desirability { get; set; }

        /// <summary>
        ///     Praiseworthiness of the event
        /// </summary>
        public Name Praiseworthiness { get; set; }


	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
		    m_id = (appraisalRuleDTO.Id == Guid.Empty)?Guid.NewGuid() : appraisalRuleDTO.Id;
            EventName = (Name)appraisalRuleDTO.EventMatchingTemplate;

            if (appraisalRuleDTO.Desirability == null)
            {
                Desirability = (Name)"0";
            }
            else
            {
                Desirability = appraisalRuleDTO.Desirability;
            }

            if (appraisalRuleDTO.Praiseworthiness == null)
            {
                Praiseworthiness = (Name)"0";
            }
            else
            {
                Praiseworthiness = appraisalRuleDTO.Praiseworthiness;
            }

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
			Conditions = new ConditionSet(other.Conditions);
			Desirability = other.Desirability;
			Praiseworthiness = other.Praiseworthiness;
		}

	}
}
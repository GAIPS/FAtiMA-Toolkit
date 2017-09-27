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

        public AppraisalRule(Name eventName, ConditionSet conditions = null)
		{
			m_id = Guid.NewGuid();
			EventName = eventName;
			Conditions = conditions ?? new ConditionSet();
			Desirability = Praiseworthiness = (Name)"0";
		}

	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
		    m_id = (appraisalRuleDTO.Id == Guid.Empty)?Guid.NewGuid() : appraisalRuleDTO.Id;
            EventName = (Name)appraisalRuleDTO.EventMatchingTemplate;
	        Desirability = appraisalRuleDTO.Desirability;
	        Praiseworthiness = appraisalRuleDTO.Praiseworthiness;
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
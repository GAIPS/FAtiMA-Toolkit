using System;
using System.Collections.Generic;
using Conditions.DTOs;
using WellFormedNames;

namespace EmotionalAppraisal.DTOs
{
    /// <summary>
    /// Data Type Object Class for the representation of an Appraisal Rule.
    /// Appraisal rules determines how emotions are generated based on perceived events.
    /// </summary>
    [Serializable]
    public class AppraisalRuleDTO 
    {
		/// <summary>
		/// Unique indentifier of the appraisal rule
		/// </summary>
		public Guid Id { get; set; }
        /// <summary>
        /// The matching template for the events we want to appraise with this rule.
        /// </summary>
        public Name EventMatchingTemplate { get; set; }
      
		/// The conditions in which this event must be appraised.
		/// If the conditions are not met, the event appraisal is ignored.
		/// </summary>
	    public ConditionSetDTO Conditions { get; set; }

         /// <summary>
        /// The list of appraisal variables used for this rule
        /// </summary>
        public AppraisalVariables AppraisalVariables  { get; set; }
    }
}

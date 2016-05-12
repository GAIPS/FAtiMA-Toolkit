using System;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisal.DTOs
{
	/// <summary>
	/// Data Type Object Class for the representation of an Appraisal Rule.
	/// Appraisal rules determines how emotions are generated based on perceived events.
	/// </summary>
	public class AppraisalRuleDTO
    {
		/// <summary>
		/// Unique indentifier of the appraisal rule
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// The matching template for the events we want to appraise with this rule.
		/// </summary>
		public string EventMatchingTemplate { get; set; }
		/// <summary>
		/// The desirability of the event
		/// </summary>
        public float Desirability { get; set; }
		/// <summary>
		/// The praisewothiness of the event.
		/// </summary>
        public float Praiseworthiness { get; set; }
		/// <summary>
		/// The conditions in which this event must be appraised.
		/// If the conditions are not met, the event appraisal is ignored.
		/// </summary>
	    public ConditionSetDTO Conditions { get; set; }
    }
}

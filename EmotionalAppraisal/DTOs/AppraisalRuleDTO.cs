using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisal.DTOs
{
    public class AppraisalRuleDTO : BaseDTO
    {
        public string EventMatchingTemplate { get; set; }
        public float Desirability { get; set; }
        public float Praiseworthiness { get; set; }
	    public ConditionSetDTO Conditions { get; set; }
    }
}

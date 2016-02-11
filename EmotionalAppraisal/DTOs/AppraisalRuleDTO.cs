using System.Collections.Generic;

namespace EmotionalAppraisal.DTOs
{
    public class AppraisalRuleDTO : BaseDTO
    {
        public string EventName { get; set; }
        public float Desirability { get; set; }
        public float Praiseworthiness { get; set; }
        public IEnumerable<ConditionDTO> Conditions { get; set; }
    }
}

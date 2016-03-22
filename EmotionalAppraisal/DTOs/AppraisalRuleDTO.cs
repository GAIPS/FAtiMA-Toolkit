using System;
using System.Collections.Generic;

namespace EmotionalAppraisal.DTOs
{
    public class AppraisalRuleDTO : BaseDTO
    {
        public string EventMatchingTemplate { get; set; }
        public float Desirability { get; set; }
        public float Praiseworthiness { get; set; }
        public string Description { get; set; }
        public IEnumerable<ConditionDTO> Conditions { get; set; }

        public AppraisalRuleDTO()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

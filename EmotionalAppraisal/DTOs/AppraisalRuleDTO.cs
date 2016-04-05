using System;
using System.Collections.Generic;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisal.DTOs
{
    public class AppraisalRuleDTO : BaseDTO
    {
        public string EventMatchingTemplate { get; set; }
        public float Desirability { get; set; }
        public float Praiseworthiness { get; set; }
        public IList<ConditionDTO> Conditions { get; set; }

        public AppraisalRuleDTO()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

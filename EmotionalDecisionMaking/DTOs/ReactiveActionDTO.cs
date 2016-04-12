using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalDecisionMaking.DTOs
{
    public class ReactiveActionDTO
    {
        public string Action { get; set; }
        public string Target { get; set; }
        public float Cooldown { get; set; }
        public IList<ConditionDTO> Conditions { get; set; }
    }
}

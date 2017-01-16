using System;
using Conditions.DTOs;
using WellFormedNames;

namespace CommeillFaut.DTOs
{
    [Serializable]
    public class InfluenceRuleDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The rule name/description string.
        /// Optional attribute of an Attribution Rule.
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// The condition variable that represents the target name in the rule condition set.
        /// </summary>
        public string Target { get; set; }

        public string Initiator { get; set; }
        /// <summary>
        /// The value to be attributed to the target, if all conditions are valid.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The condition set used to validate this rule.
        /// </summary>
        public ConditionSetDTO RuleConditions { get; set; }
    }
}
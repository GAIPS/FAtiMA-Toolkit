using System;
using System.Collections.Generic;
using ActionLibrary.DTOs;
using Conditions.DTOs;
using WellFormedNames;

namespace CommeillFaut.DTOs
{
    [Serializable]
    public class SocialExchangeDTO 
    {
        public Guid Id { get; set; }

        /// The Social Exchange Name
        /// </summary>
        public Name Name { get; set; }

        /// The description of the Social Exchange
        /// </summary>
        public string Description { get; set; }

        /// The Social Exchange Name
        /// </summary>
        public Name Initiator { get; set; }

        /// The Social Exchange Name
        /// </summary>
        public Name Target { get; set; }

        /// <summary>
        /// The condition set used to validate this rule.
        /// </summary>
        public ConditionSetDTO Conditions { get; set; }



    }
}
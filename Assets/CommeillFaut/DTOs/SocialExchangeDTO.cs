using System;
using System.Collections.Generic;
using ActionLibrary.DTOs;
using Conditions.DTOs;

namespace CommeillFaut.DTOs
{
    [Serializable]
    public class SocialExchangeDTO : ActionDefinitionDTO 
    {
       // public Guid Id { get; set; }

        /// <summary>
        /// The social exchange name/description string.
       
        /// </summary>
        public string SocialExchangeName { get; set; }

        /// <summary>
        /// The condition variable that represents the initiator name 
        /// </summary>
        public string Initiator { get; set; }

        /// <summary>
        /// The condition variable that represents the targe name
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The description od the social exchange
        /// </summary>
        public string Intent { get; set; }


        /// <summary>
        /// The instanciation of a social exchange, if all conditions are valid.
        /// </summary>
        public string Instantiation { get; set; } // care

        /// <summary>
        /// The condition set used to validate this rule.
        /// </summary>
        public List<InfluenceRuleDTO> InfluenceRules { get; set; }


        public List<InfluenceRuleDTO> Effects { get; set; }
    }
}
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
        /// The condition set used to validate this rule.
        /// </summary>
        public InfluenceRuleDTO InfluenceRule { get; set; }

      
     
    }
}
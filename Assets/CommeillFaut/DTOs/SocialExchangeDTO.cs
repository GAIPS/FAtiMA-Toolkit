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

        /// The Social Exchange different steps
        /// </summary>
       
        //public List<Name> Steps { get; set; }



        public string Steps { get; set; }
        
        /// The Social Exchange Target
        /// </summary>
        public Name Target { get; set; }

            /// <summary>
        /// The conditions that need to be true in order to start the social exchange.
        /// </summary>
        public ConditionSetDTO StartingConditions { get; set; }

            /// <summary>
        /// The condition set used to calculate the will of this action
        /// </summary>
        public List<InfluenceRuleDTO> InfluenceRules { get; set; }


    }
}
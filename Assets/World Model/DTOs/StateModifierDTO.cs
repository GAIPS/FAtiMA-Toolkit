using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conditions.DTOs;
using WellFormedNames;

namespace WorldModel.DTOs
{
    public class StateModifierDTO
    {
        /// <summary>
        /// Unique indentifier of the appraisal rule
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The matching template for the events we want to appraise with this rule.
        /// </summary>
        public Name EventMatchingTemplate { get; set; }
        /// <summary>
        /// The desirability of the event
        /// </summary>
        public Name Desirability { get; set; }
        /// <summary>
        /// The praisewothiness of the event.
        /// </summary>
        public Name Praiseworthiness { get; set; }
        /// <summary>
        /// The conditions in which this event must be appraised.
        /// If the conditions are not met, the event appraisal is ignored.
        /// </summary>
        public ConditionSetDTO Conditions { get; set; }

        //Moditifications to the world
        //
        public ConditionSetDTO Effects { get; set; }
    }
}


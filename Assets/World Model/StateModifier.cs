using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conditions;
using WorldModel.DTOs;
using WellFormedNames;

namespace WorldModel
{
    /// <summary>
    ///     Represents a rule that is triggered when something happens in the world
    /// </summary>
    /// @author Manuel Guimaraes

    [Serializable]
    public class StateModifier
    {
        [NonSerialized]
        private Guid m_id;

        public Guid Id { get { return m_id; } set { m_id = value; } }
        public Name EventName { get; set; }
        public ConditionSet Conditions { get; set; }

        public ConditionSet Effects { get; set; }
        /// <summary>
        ///     Desirability of the event
        /// </summary>
        public Name Desirability { get; set; }

        /// <summary>
        ///     Praiseworthiness of the event
        /// </summary>
        public Name Praiseworthiness { get; set; }


        public StateModifier(StateModifierDTO stateModifierDTO)
        {
            m_id = (stateModifierDTO.Id == Guid.Empty) ? Guid.NewGuid() : stateModifierDTO.Id;
            EventName = (Name)stateModifierDTO.EventMatchingTemplate;

            if (stateModifierDTO.Desirability == null)
            {
                Desirability = (Name)"0";
            }
            else
            {
                Desirability = stateModifierDTO.Desirability;
            }

            if (stateModifierDTO.Praiseworthiness == null)
            {
                Praiseworthiness = (Name)"0";
            }
            else
            {
                Praiseworthiness = stateModifierDTO.Praiseworthiness;
            }

            Conditions = stateModifierDTO.Conditions == null ? new ConditionSet() : new ConditionSet(stateModifierDTO.Conditions);
            Effects = stateModifierDTO.Effects == null ? new ConditionSet() : new ConditionSet(stateModifierDTO.Effects);
        }

        /// <summary>
        ///     Clone Constructor
        /// </summary>
        /// <param name="other">the reaction to clone</param>
        public StateModifier(StateModifier other)
        {
            m_id = other.m_id;
            EventName = other.EventName;
            Conditions = new ConditionSet(other.Conditions);
            Desirability = other.Desirability;
            Praiseworthiness = other.Praiseworthiness;
            Effects = other.Effects;
        }



    }
   

}

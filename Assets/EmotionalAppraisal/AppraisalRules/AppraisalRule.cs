using System;
using Conditions;
using EmotionalAppraisal.DTOs;
using WellFormedNames;
using AutobiographicMemory;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	///     Represents an Emotional AppraisalRule based in Construal Frames that specify values
	///     for some of OCC's appraisal variables: Desirability, DesirabilityForOther, Like and
	///     Praiseworthiness.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class AppraisalRule
	{
		[NonSerialized]
		private Guid m_id;

		public Guid Id { get { return m_id; } set { m_id = value; } }
		public Name EventName { get; set; }
		public ConditionSet Conditions { get; set; }

        public static Name BuildEventName(AppraisalRuleDTO appraisalRuleDTO)
        {
            if (appraisalRuleDTO.EventType == AMConsts.PROPERTY_CHANGE)
            {
                return Name.BuildName(
                    (Name)AMConsts.EVENT,
                    (Name)appraisalRuleDTO.EventType,
                    (Name)appraisalRuleDTO.Subject,
                    (Name)appraisalRuleDTO.Property,
                    (Name)appraisalRuleDTO.NewValue);
            }
            else
            {
                return Name.BuildName(
                  (Name)AMConsts.EVENT,
                  (Name)appraisalRuleDTO.EventType,
                  (Name)appraisalRuleDTO.Subject,
                  (Name)appraisalRuleDTO.Action,
                  (Name)appraisalRuleDTO.Target);
            }
        }

		public AppraisalRule(Name eventName, ConditionSet conditions = null)
		{
			m_id = Guid.NewGuid();
			EventName = eventName;
			Conditions = conditions ?? new ConditionSet();
			Desirability = Praiseworthiness = 0;
		}

	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
		    m_id = (appraisalRuleDTO.Id == Guid.Empty)?Guid.NewGuid() : appraisalRuleDTO.Id;
            EventName = BuildEventName(appraisalRuleDTO);
	        Desirability = appraisalRuleDTO.Desirability;
	        Praiseworthiness = appraisalRuleDTO.Praiseworthiness;
			Conditions = appraisalRuleDTO.Conditions==null ? new ConditionSet() : new ConditionSet(appraisalRuleDTO.Conditions);
	    }

		/// <summary>
		///     Clone Constructor
		/// </summary>
		/// <param name="other">the reaction to clone</param>
		public AppraisalRule(AppraisalRule other)
		{
			m_id = other.m_id;
			EventName = other.EventName;
			Conditions = new ConditionSet(other.Conditions);
			Desirability = other.Desirability;
			Praiseworthiness = other.Praiseworthiness;
			//DesirabilityForOther = other.DesirabilityForOther;
			//Like = other.Like;
			//ReferencedEventName = (Name)other.ReferencedEventName.Clone();
			//if (Other != null)
			//	Other = (Name) other.Other.Clone();
		}

		/// <summary>
		///     Desirability of the event
		/// </summary>
		public float Desirability { get; set; }

	    /// <summary>
		///     Praiseworthiness of the event
		/// </summary>
		public float Praiseworthiness { get; set; }

	}
}
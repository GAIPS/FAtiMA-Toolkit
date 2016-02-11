using System;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal.DTOs;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

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
	public class AppraisalRule : BaseDomainObject
	{
        public Name EventName { get; private set; }
		public ConditionEvaluatorSet Conditions { get; private set; }
		public bool TriggersOnFailedActivation { get; set; }

		/// <summary>
		///     Creates a new empty Emotional AppraisalRule
		/// </summary>
		public AppraisalRule(IEvent evt)
		{
			EventName = evt.ToIdentifierName();
			Conditions = new ConditionEvaluatorSet();
			if (evt.Parameters != null && evt.Parameters.Any())
			{
				Conditions.UnionWith(evt.Parameters.Select(
						p => Condition.BuildCondition((Name)("[" + p.ParameterName + "]"), p.Value, ComparisonOperator.Equal)));
			}

			Desirability = Praiseworthiness = 0;//DesirabilityForOther = Like = 0;
			//ReferencedEventName = null;
			//Other = null;
		}

		public AppraisalRule(Name eventName, ConditionEvaluatorSet conditions)
		{
			EventName = eventName;
			Conditions = conditions ?? new ConditionEvaluatorSet();
			Desirability = Praiseworthiness = 0;
		}

	    public AppraisalRule(AppraisalRuleDTO appraisalRuleDTO)
	    {
	        EventName = Name.BuildName(appraisalRuleDTO.EventMatchingTemplate);
	        Desirability = appraisalRuleDTO.Desirability;
	        Praiseworthiness = appraisalRuleDTO.Praiseworthiness;
            Conditions = new ConditionEvaluatorSet(appraisalRuleDTO.Conditions.Select(c => Condition.Parse(c.Condition)));
	    }

		/// <summary>
		///     Clone Constructor
		/// </summary>
		/// <param name="other">the reaction to clone</param>
		public AppraisalRule(AppraisalRule other)
		{
			Desirability = other.Desirability;
			//DesirabilityForOther = other.DesirabilityForOther;
			Praiseworthiness = other.Praiseworthiness;
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
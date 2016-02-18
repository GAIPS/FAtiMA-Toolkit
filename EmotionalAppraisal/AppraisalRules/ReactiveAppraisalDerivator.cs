using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	/// Default reactive module implementation.
	/// It evaluates events through a evaluatorSet of rules, and determines the emotional reaction of that event.
	/// It then generates appropriate actions base on the agent's emotional state.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class ReactiveAppraisalDerivator : IAppraisalDerivator, ICustomSerialization
	{
		private const short DEFAULT_APPRAISAL_WEIGHT = 1;
		
		private readonly NameSearchTree<HashSet<AppraisalRule>> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new NameSearchTree<HashSet<AppraisalRule>>();
		}
		
		public AppraisalRule Evaluate(IEventRecord evt, KB kb)
		{
			foreach (var possibleAppraisals in Rules.Unify(evt.EventName))
			{
				var conditions = new[] {possibleAppraisals.Item2};
				foreach (var appraisal in possibleAppraisals.Item1)
				{
					var result = appraisal.Conditions.Evaluate(kb, conditions);
					if (result == !appraisal.TriggersOnFailedActivation)
						return appraisal;	
				}
			}
			return null;
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
		public void AddAppraisalRule(AppraisalRuleDTO emotionalAppraisalRuleDTO)
		{
            this.AddEmotionalReaction(new AppraisalRule(emotionalAppraisalRuleDTO));
		}

	    private void AddEmotionalReaction(AppraisalRule appraisalRule)
	    {
            var name = appraisalRule.EventName;
            HashSet<AppraisalRule> ruleSet;
            if (!Rules.TryGetValue(name, out ruleSet))
            {
                ruleSet = new HashSet<AppraisalRule>();
                Rules.Add(name, ruleSet);
            }
            ruleSet.Add(appraisalRule);
        }

	    public IEnumerable<AppraisalRule> GetAppraisalRules()
	    {
		    return Rules.Values.SelectMany(set => set);
	    }
        
		#region IAppraisalDerivator Implementation

		public short AppraisalWeight
		{
			get;
			set;
		}

		public void Appraisal(EmotionalAppraisalAsset emotionalModule, IEventRecord evt, IWritableAppraisalFrame frame)
		{
			AppraisalRule selfEvaluation = Evaluate(evt,emotionalModule.Kb);
			if (selfEvaluation != null)
			{
				if (selfEvaluation.Desirability != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY, selfEvaluation.Desirability);

				//if (selfEvaluation.DesirabilityForOther != 0)
				//{
				//	string other;
				//	if (selfEvaluation.Other != null)
				//		other = selfEvaluation.Other.ToString();
				//	else if (evt.Target != null)
				//		other = evt.Target;
				//	else
				//		other = evt.Subject;

				//	frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER + other, selfEvaluation.DesirabilityForOther);
				//}

				if (selfEvaluation.Praiseworthiness != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS, selfEvaluation.Praiseworthiness);

				//if (selfEvaluation.Like != 0)
				//	frame.SetAppraisalVariable(OCCAppraisalVariables.LIKE, selfEvaluation.Like);
			}
		}

		public void InverseAppraisal(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame)
		{
			float desirability = frame.GetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY);
			float praiseworthiness = frame.GetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS);

			if (desirability != 0 || praiseworthiness != 0)
			{
				var eventName = frame.AppraisedEvent.EventName.ApplyPerspective(emotionalModule.Perspective);
				AppraisalRule r = new AppraisalRule(eventName,null);
				r.Desirability = desirability;
				r.Praiseworthiness = praiseworthiness;
				//r.EventObject = frame.AppraisedEvent.ToIdentifierName().RemovePerspective(emotionalModule.Perspective);
				AddEmotionalReaction(r);
			}
		}

		public IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule)
		{
			return null;
		}

		#endregion

		#region Custom Serializer

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("AppraisalWeight",AppraisalWeight);
			dataHolder.SetValue("Rules",Rules.Values.SelectMany(set => set).ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			AppraisalWeight = dataHolder.GetValue<short>("AppraisalWeight");
			var rules = dataHolder.GetValue<AppraisalRule[]>("Rules");
			Rules.Clear();
			foreach (var r in rules)
				AddEmotionalReaction(r);
		}

		#endregion
	}
}

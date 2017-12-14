using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using Conditions;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using SerializationUtilities;
using KnowledgeBase;
using WellFormedNames;
using WellFormedNames.Collections;
using IQueryable = WellFormedNames.IQueryable;

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
		
		private NameSearchTree<HashSet<AppraisalRule>> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new NameSearchTree<HashSet<AppraisalRule>>();
		}
		
        
		public AppraisalRule Evaluate(IBaseEvent evt, IQueryable kb, Name perspective)
		{
          //  var eventInPerspective = evt.EventName.ApplySelfPerspective(perspective);

            var newRules = new NameSearchTree<HashSet<AppraisalRule>>();
            foreach (var rule in Rules)
            {
               var key = rule.Key.ApplyToTerms(x=>x.SwapTerms(Name.SELF_SYMBOL, perspective));
                newRules.Add(key, rule.Value);
            }

            //evt.EventName.SwapTerms(Name.SELF_SYMBOL, perspective);

            foreach (var possibleAppraisals in newRules.Unify(evt.EventName))
			{
				var substitutions = new[] { possibleAppraisals.Item2 }; //this adds the subs found in the eventName
				foreach (var appRule in possibleAppraisals.Item1)
				{
                    var finalSubsList = appRule.Conditions.Unify(kb, Name.SELF_SYMBOL, substitutions);

                    //The appraisal will only consider the substitution set that it has the most certainty in
                    var mostCertainSubSet = this.DetermineSubstitutionSetWithMostCertainty(finalSubsList);
                    if (mostCertainSubSet != null)
                    {
                        appRule.Desirability = appRule.Desirability.MakeGround(mostCertainSubSet);
                        appRule.Praiseworthiness = appRule.Praiseworthiness.MakeGround(mostCertainSubSet);
                        if (!appRule.Desirability.IsGrounded || !appRule.Praiseworthiness.IsGrounded)
                        {
                           return null;
                        }

                        //Modify the appraisal variables based on the certainty of the substitutions
                        var minCertainty = mostCertainSubSet.FindMinimumCertainty();

                        var aux = float.Parse(appRule.Desirability.ToString()) * minCertainty;
                        appRule.Desirability = Name.BuildName(aux);

                        aux = float.Parse(appRule.Praiseworthiness.ToString()) * minCertainty;
                        appRule.Praiseworthiness = Name.BuildName(aux);

                        return appRule;
                    }
						
				}
			}
			return null;
		}



        private SubstitutionSet DetermineSubstitutionSetWithMostCertainty(IEnumerable<SubstitutionSet> subSets)
        {
            SubstitutionSet result = null;
            var max = float.MinValue;
            foreach (var subSet in subSets)
            {
                var minCertainty = subSet.FindMinimumCertainty();
                if(minCertainty > max)
                {
                    max = minCertainty;
                    result = subSet;
                }
            }
            return result;
        }

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
		public void AddOrUpdateAppraisalRule(AppraisalRuleDTO emotionalAppraisalRuleDTO)
		{
			AppraisalRule existingRule = GetAppraisalRule(emotionalAppraisalRuleDTO.Id);
		    if (existingRule != null)
		    {
				RemoveAppraisalRule(existingRule);
				existingRule.Desirability = emotionalAppraisalRuleDTO.Desirability;
				existingRule.Praiseworthiness = emotionalAppraisalRuleDTO.Praiseworthiness;
                existingRule.EventName = emotionalAppraisalRuleDTO.EventMatchingTemplate;
				existingRule.Conditions = new ConditionSet(emotionalAppraisalRuleDTO.Conditions);
		    }
		    else
		    {
			    existingRule = new AppraisalRule(emotionalAppraisalRuleDTO);
		    }
			AddEmotionalReaction(existingRule);
		}

        public void AddEmotionalReaction(AppraisalRule appraisalRule)
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

		public void RemoveAppraisalRule(AppraisalRule appraisalRule)
		{
			HashSet<AppraisalRule> ruleSet;
			if (Rules.TryGetValue(appraisalRule.EventName, out ruleSet))
			{
				AppraisalRule ruleToRemove = null;
				foreach (var rule in ruleSet)
				{
					if (rule.Id == appraisalRule.Id)
					{
						ruleToRemove = rule;
					}
				}
				if (ruleToRemove != null)
				{
					ruleSet.Remove(ruleToRemove);
				}
			}
		}

		public AppraisalRule GetAppraisalRule(Guid id)
		{
			return Rules.SelectMany(r => r.Value).FirstOrDefault(a => a.Id == id);
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

		public void Appraisal(KB kb, IBaseEvent evt, IWritableAppraisalFrame frame)
		{
			AppraisalRule activeRule = Evaluate(evt, kb, kb.Perspective);
			if (activeRule != null)
			{
				if (activeRule.Desirability != null)
                {
                    float des;
                    if (!float.TryParse(activeRule.Desirability.ToString(), out des))
                    {
                        throw new ArgumentException("Desirability can only be a float value");
                    }
                    frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY, des);
                }

                if (activeRule.Praiseworthiness != null)
                {
                    float p;
                    if (!float.TryParse(activeRule.Praiseworthiness.ToString(), out p))
                    {
                        throw new ArgumentException("Desirability can only be a float value");
                    }
                    frame.SetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS, p);
                }
			}
		}

		#endregion

		#region Custom Serializer

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AppraisalWeight",AppraisalWeight);
			dataHolder.SetValue("Rules",Rules.Values.SelectMany(set => set).ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			AppraisalWeight = dataHolder.GetValue<short>("AppraisalWeight");
			var rules = dataHolder.GetValue<AppraisalRule[]>("Rules");

			if(Rules==null)
				Rules = new NameSearchTree<HashSet<AppraisalRule>>();
			else
				Rules.Clear();

		    foreach (var r in rules)
		    {
				r.Id = Guid.NewGuid();
                if (r.Desirability == null)
                {
                    r.Desirability = (Name)"0";
                }
                if (r.Praiseworthiness == null)
                {
                    r.Praiseworthiness = (Name)"0";
                }
                AddEmotionalReaction(r);
            }
		}

		#endregion
	}
}

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
using IQueryable = WellFormedNames.IQueryable;
using System.Globalization;

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
		
		private List<AppraisalRule> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new List<AppraisalRule>();
		}
		
        
		public IEnumerable<AppraisalRule> Evaluate(IBaseEvent evt, IQueryable kb, Name perspective)
		{
            // Switching the SELF term for the correct name withint the event

            var auxEvt = evt.EventName.SwapTerms(perspective, Name.SELF_SYMBOL);
            var result = new List<AppraisalRule>();
            foreach (var r in this.Rules)
			{
                // Trying to find all possible initial substitutions;
                var initialSubSet = Unifier.Unify(r.EventName, auxEvt);
           

                if (initialSubSet == null)
                    initialSubSet = new SubstitutionSet();


                if (auxEvt.Match(r.EventName) || initialSubSet.Any())
                {
                    var finalSubSet = r.Conditions.Unify(kb, perspective, new List<SubstitutionSet>() {new SubstitutionSet(initialSubSet)});
                    if (finalSubSet != null)
                    {
                        //TODO: Handle uncertainty in beliefs
                        foreach(var set in finalSubSet)
                        {
                            var a = new AppraisalRule(r);
                            a.EventName.MakeGround(set);
                            foreach (var variable in a.getAppraisalVariables())
                            {
                                variable.Value = variable.Value.MakeGround(set);
                                if (variable.Target != null && variable.Target != (Name)"-")
                                {
                                    variable.Target = variable.Target.MakeGround(set);
                                }
                            }
                            result.Add(a);
                        }
                    }
                }
			}
			return result;
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
				RemoveAppraisalRule(existingRule.Id);
                existingRule.EventName = emotionalAppraisalRuleDTO.EventMatchingTemplate;
				existingRule.Conditions = new ConditionSet(emotionalAppraisalRuleDTO.Conditions);
                existingRule.AppraisalVariables = emotionalAppraisalRuleDTO.AppraisalVariables;
		    }
		    else
		    {
			    existingRule = new AppraisalRule(emotionalAppraisalRuleDTO);
		    }
			AddAppraisalRule(existingRule);
		}

        public void AddAppraisalRule(AppraisalRule appraisalRule)
        {
            Rules.Add(appraisalRule);
        }

		public void RemoveAppraisalRule(Guid id)
		{
            Rules.RemoveAll(r => r.Id == id);
		}

		public AppraisalRule GetAppraisalRule(Guid id)
		{
			return Rules.FirstOrDefault(a => a.Id == id);
		}

        public IEnumerable<AppraisalRule> GetAppraisalRules()
	    {
	        return Rules;
	    }
        
		#region IAppraisalDerivator Implementation

		public short AppraisalWeight
		{
			get;
			set;
		}

		public void Appraisal(KB kb, IBaseEvent evt, IAppraisalFrame frame)
		{
			IEnumerable<AppraisalRule> activeRules = Evaluate(evt, kb, kb.Perspective);
            foreach(var rule in activeRules)
			{
                foreach(var appVar in rule.getAppraisalVariables())
                {
                     float des;
                    if (!float.TryParse(appVar.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out des))
                    {
                        throw new ArgumentException(appVar.Name + " can only be a float value");
                    }

                    else if (appVar.Name == OCCAppraisalVariables.DESIRABILITY_FOR_OTHER)
                        frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER + " " + appVar.Target, des);

                    else if (appVar.Name == OCCAppraisalVariables.GOALSUCCESSPROBABILITY)
                          frame.SetAppraisalVariable(OCCAppraisalVariables.GOALSUCCESSPROBABILITY + " " + appVar.Target, des);
                     
                    else if(appVar.Name == OCCAppraisalVariables.PRAISEWORTHINESS)
                          frame.SetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS + " " + appVar.Target, des);
                   
                    else  frame.SetAppraisalVariable(appVar.Name, des);
                }
			}
		}

		#endregion

		#region Custom Serializer

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AppraisalWeight",AppraisalWeight);
			dataHolder.SetValue("Rules", GetAppraisalRules().ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			AppraisalWeight = dataHolder.GetValue<short>("AppraisalWeight");
            Rules = dataHolder.GetValue<AppraisalRule[]>("Rules").ToList();
            foreach(var r in Rules)
            {
                r.Id = Guid.NewGuid();
            }
		}

		#endregion
	}
}

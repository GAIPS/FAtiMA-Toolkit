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
		
		private NameSearchTree<HashSet<AppraisalRule>> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new NameSearchTree<HashSet<AppraisalRule>>();
		}
		
        
		public AppraisalRule Evaluate(IBaseEvent evt, IQueryable kb, Name perspective)
		{
            var auxEvt = evt.EventName.SwapTerms(perspective, Name.SELF_SYMBOL);

            foreach (var possibleAppraisals in this.Rules.Unify(auxEvt))
			{
                //This next for loop is to prevent a problem with using appraisal rules that contain SELF
                //This will replace all the subs with SELF with the name of the perspective  
                foreach (var sub in possibleAppraisals.Item2)
                {
                    if (sub.SubValue.Value == Name.SELF_SYMBOL)
                    {
                        sub.SubValue = new ComplexValue(perspective);
                    }
                }
				var substitutions = new[] { possibleAppraisals.Item2 }; //this adds the subs found in the eventName
				foreach (var appRule in possibleAppraisals.Item1)
				{
                    var finalSubsList = appRule.Conditions.Unify(kb, Name.SELF_SYMBOL, substitutions);

                    //The appraisal will only consider the substitution set that it has the most certainty in
                    var mostCertainSubSet = this.DetermineSubstitutionSetWithMostCertainty(finalSubsList);
                    if (mostCertainSubSet != null)
                    {

                        foreach(var appVariable in appRule.getAppraisalVariables())
                        {
                            appVariable.Value =  appVariable.Value.MakeGround(mostCertainSubSet);

                             var minCertainty = mostCertainSubSet.FindMinimumCertainty();

                            if(appVariable.Target != null && appVariable.Target != (Name)"-")
                            {
                                appVariable.Target = appVariable.Target.MakeGround(mostCertainSubSet);
                            }

                            float f;

                            if (float.TryParse(appVariable.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out f))
                            {

                                var aux = f * minCertainty;
                                appVariable.Value = Name.BuildName(aux);
                            }
                            else continue;

                        }
                     
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
                existingRule.EventName = emotionalAppraisalRuleDTO.EventMatchingTemplate;
				existingRule.Conditions = new ConditionSet(emotionalAppraisalRuleDTO.Conditions);
                existingRule.AppraisalVariables = emotionalAppraisalRuleDTO.AppraisalVariables;
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
			    
                foreach(var appVar in activeRule.getAppraisalVariables())
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

                //     if(appVar.Target != null && appVar.Target != (Name)"-" && appVar.Target != (Name)"SELF")
                //          frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER + "" + appVar.Target, des);
                }
                
               
			}
		}

		#endregion

		#region Custom Serializer

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AppraisalWeight",AppraisalWeight);
			dataHolder.SetValue("Rules", GetAppraisalRules());
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
                AddEmotionalReaction(r);
            }
		}

		#endregion
	}
}

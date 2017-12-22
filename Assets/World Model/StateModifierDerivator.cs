using AutobiographicMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase;
using WellFormedNames;
using WellFormedNames.Collections;
using IQueryable = WellFormedNames.IQueryable;
using WorldModel;
using WorldModel.DTOs;
using Conditions;

namespace WorldModel
{
    class StateModifierDerivator
    {

        // private const short DEFAULT_APPRAISAL_WEIGHT = 1;

        private NameSearchTree<HashSet<StateModifier>> Rules;

        public StateModifierDerivator()
        {
          //  this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
            this.Rules = new NameSearchTree<HashSet<StateModifier>>();
        }


        public void AddOrUpdateStateModifier(StateModifierDTO stateModifierDTO)
        {
            var existingRule = GetStateModifier(stateModifierDTO.Id);

            if (existingRule != null)
            {
                RemoveStateModifier(existingRule);
                existingRule.Desirability = stateModifierDTO.Desirability;
                existingRule.Praiseworthiness = stateModifierDTO.Praiseworthiness;
                existingRule.EventName = stateModifierDTO.EventMatchingTemplate;
                existingRule.Conditions = new ConditionSet(stateModifierDTO.Conditions);
                existingRule.Effects = new ConditionSet(stateModifierDTO.Effects);
            }
            else
            {
                existingRule = new StateModifier(stateModifierDTO);
            }
            AddStateModifier(existingRule);
        }

        public void AddStateModifier(StateModifier appraisalRule)
        {
            var name = appraisalRule.EventName;

            HashSet<StateModifier> ruleSet;
            if (!Rules.TryGetValue(name, out ruleSet))
            {
                ruleSet = new HashSet<StateModifier>();
                Rules.Add(name, ruleSet);
            }
            ruleSet.Add(appraisalRule);
        }

        public void RemoveStateModifier(StateModifier sm)
        {
            HashSet<StateModifier> ruleSet;
            if (Rules.TryGetValue(sm.EventName, out ruleSet))
            {
                StateModifier ruleToRemove = null;
                foreach (var rule in ruleSet)
                {
                    if (rule.Id == sm.Id)
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

        public StateModifier GetStateModifier(Guid id)
        {
            return Rules.SelectMany(r => r.Value).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<StateModifier> GetStateModifiers()
        {
            return Rules.Values.SelectMany(set => set);
        }

        #region Evaluate

        public StateModifier Evaluate(IBaseEvent evt, IQueryable kb, Name perspective)
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
                if (minCertainty > max)
                {
                    max = minCertainty;
                    result = subSet;
                }
            }
            return result;
        }
#endregion

    }
}

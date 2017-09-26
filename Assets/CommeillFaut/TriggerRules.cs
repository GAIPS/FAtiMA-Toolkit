using System;
using System.Collections.Generic;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using KnowledgeBase;
using SerializationUtilities;
using WellFormedNames;


namespace CommeillFaut
{
    [Serializable]
    public class TriggerRules
    {
        [NonSerialized] public readonly Guid GUID;


        public Dictionary<InfluenceRuleDTO, string> _triggerRules { get; private set; }


        public TriggerRules()
        {
            _triggerRules = new Dictionary<InfluenceRuleDTO, string>();
            GUID = Guid.NewGuid();
        }


        public TriggerRules(TriggerRulesDTO dto) : this()
        {
            SetData(dto);
        }

        public void SetData(TriggerRulesDTO dto)
        {
            _triggerRules = dto._TriggerRules;
        }

        public TriggerRulesDTO ToDTO()
        {
            return new TriggerRulesDTO() {_TriggerRules = _triggerRules};
        }


        public void Verify(KB subject)
        {

            foreach (var rule in _triggerRules.Keys)
            {
                var rul = new InfluenceRule(rule);
                if (rul.Result("", "", subject) > 0)
                    ApplyEffect(subject, _triggerRules[rule]);

            }

        }

        public void ApplyEffect(KB subject, string effect)
        {

            // apply effect...
        }


        public Guid AddTriggerRule(InfluenceRuleDTO rule, string cond)
        {

            if (_triggerRules != null)

                _triggerRules.Add(rule, cond);

            else
            {

                _triggerRules = new Dictionary<InfluenceRuleDTO, string>();
                _triggerRules.Add(rule, cond);
            }

            return new Guid();
        }

        public void UpdateTriggerRule(InfluenceRuleDTO _rule, string _cond)
        {

            InfluenceRuleDTO index = new InfluenceRuleDTO();
            bool found = false;
            foreach (var rule in _triggerRules.Keys)
            {
                if (rule.RuleName != _rule.RuleName) continue;
                index = rule;
                found = true;
                break;
            }
            if (found)
            {
                _triggerRules.Remove(index);


            }
            else _triggerRules.Add(_rule, _cond);


        }

        public void RemoveTriggerRule(InfluenceRuleDTO rule)
        {

            _triggerRules.Remove(rule);
        }

        public void RemoveTriggerRuleByName(string RuleName)
        {

            InfluenceRuleDTO index = new InfluenceRuleDTO();
            bool found = false;
            foreach (var rule in _triggerRules.Keys)
            {
                if (rule.RuleName != RuleName) continue;
                index = rule;
                found = true;
                break;
            }
            if (found)
                _triggerRules.Remove(index);



        }


    }
}
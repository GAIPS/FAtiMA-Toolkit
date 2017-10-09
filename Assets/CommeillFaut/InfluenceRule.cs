using System;
using CommeillFaut.DTOs;
using Conditions;
using KnowledgeBase;
using WellFormedNames;
using System.Linq;

namespace CommeillFaut
{
    [Serializable]
    public class InfluenceRule
    {
        [NonSerialized]
        public readonly Guid GUID;
        public string RuleName { get; private set; }
        public string Target { get; private set; }
        public ConditionSet RuleConditions { get; private set; }

        protected InfluenceRule()
        {
            GUID = Guid.NewGuid();
        }

        public float Result(string init, string targ, KB m_Kb)
        {
            float toRet = 0.0f;
            var totalCertainty = 0.0f;
            int totalConds = RuleConditions.Count();
            var toEvaluate = new ConditionSet(RuleConditions);
            var sub = new Substitution(Name.BuildName(Target), new ComplexValue(Name.BuildName(targ)));
            var eval = toEvaluate.Unify(m_Kb, Name.BuildName(init), new[] { new SubstitutionSet(sub) });
            if (eval.Any())
            {
                var cond = "";
                var toAsk = new string[] { };
                foreach (var c in toEvaluate)
                {
                    cond = c.ToString();
                    toAsk = cond.Split(')');
                    toAsk[0] += ")";
                   toAsk[0] =  toAsk[0].Replace("[x]", targ);
                    var certainty = m_Kb.AskProperty(Name.BuildName(toAsk[0])).Certainty;
                    totalCertainty += certainty;
                }

                toRet = totalCertainty / totalConds;

                return toRet;
               
            }

            else return 0;


        }


        public InfluenceRule(InfluenceRuleDTO dto)
        {
            SetData(dto);
        }

        public void SetData(InfluenceRuleDTO dto)
        {
            if (dto == null)
            {
                RuleName = "";
                Target = "[x]";
                RuleConditions = new ConditionSet();
            }
            else
            {
                if (dto.RuleName != null)
                    RuleName = dto.RuleName;
                else RuleName = "";
                if (dto.Target != null)
                    Target = dto.Target;
                else Target = "[x]";
                if (dto.RuleConditions != null)
                    RuleConditions = new ConditionSet(dto.RuleConditions);
                else RuleConditions = new ConditionSet();
            }
          
        }

      

        public InfluenceRuleDTO ToDTO()
        {
            return new InfluenceRuleDTO() { Id = GUID, RuleName = RuleName, Target = Target, RuleConditions = RuleConditions.ToDTO() };
        }

    }

}
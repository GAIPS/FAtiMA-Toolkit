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

            var toEvaluate = new ConditionSet(RuleConditions);
            var sub = new Substitution(Name.BuildName(Target), new ComplexValue(Name.BuildName(targ)));

            var eval = toEvaluate.Unify(m_Kb, Name.BuildName(init), new[] { new SubstitutionSet(sub) }).Any();

            if (eval)
            {
                var cond = toEvaluate.ToDTO().ConditionSet.GetValue(0).ToString().Split(new[] { '=' });
                cond = cond[0].Split(new[] { ',' });
                var certainty = m_Kb.AskProperty(Name.BuildName(cond[0] + "," + targ + ")")).Certainty;

                return certainty;
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
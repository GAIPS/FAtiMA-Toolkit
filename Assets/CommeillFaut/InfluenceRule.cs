using System;
using System.Collections.Generic;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using EmotionalAppraisal;
using KnowledgeBase;
using SerializationUtilities;
using WellFormedNames;


namespace CommeillFaut
{
    [Serializable]
    public class InfluenceRule
    {
        [NonSerialized]
        public readonly Guid GUID;

        public string RuleName { get; private set; }
        public string Target { get; private set; }
        public string Initiator { get; private set; }
        public int Value { get; private set; }
      public ConditionSet RuleConditions { get; private set; }


    protected InfluenceRule()
        {
            GUID = Guid.NewGuid();
        }

        public int Result(string init, string targ, KB m_Kb)
        {
           
            var toEvaluate = new ConditionSet(RuleConditions);
            var sub = new Substitution(Name.BuildName(Target), Name.BuildName(targ));
            if (toEvaluate.Evaluate(m_Kb, Name.BuildName(init), new [] { new SubstitutionSet(sub) }))
                return Value;
            else return 0;

            //  (a.Conditions.Evaluate(m_ea, perspective, new[] { new SubstitutionSet(sub) }))
        }
        public InfluenceRule(InfluenceRuleDTO dto) : this()
		{
            SetData(dto);
        }

        public void SetData(InfluenceRuleDTO dto)
        {
            RuleName = dto.RuleName;
            Initiator = dto.Initiator;
            Target = dto.Target;
            Value = dto.Value;;
            RuleConditions = new ConditionSet(dto.RuleConditions);
        }

        public InfluenceRuleDTO ToDTO()
        {
            return new InfluenceRuleDTO() {Id = GUID, RuleName = RuleName, Initiator = Initiator, Target = Target, Value = Value, RuleConditions = RuleConditions.ToDTO()};
        }

     /*   public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {


            dataHolder.SetValue("RuleName", this.RuleName);
            dataHolder.SetValue("Initiator", this.Initiator);
            dataHolder.SetValue("Target", this.Target);
            dataHolder.SetValue("RuleConditions", this.RuleConditions);
            


        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
           
            RuleName = dataHolder.GetValue<string>("RuleName");
            Initiator = dataHolder.GetValue<string>("Initiator");
            Target = dataHolder.GetValue<string>("Target");
            RuleConditions = dataHolder.GetValue<ConditionSet>("RuleConditions");
        }*/
    }


}
using System;
using CommeillFaut.DTOs;
using Conditions;
using KnowledgeBase;
using WellFormedNames;
using System.Collections.Generic;
using SerializationUtilities;
using System.Linq;

namespace CommeillFaut
{
    [Serializable]
    public class InfluenceRule : ICustomSerialization
    {
        public Guid Id { get; set; }

        public ConditionSet Rule{ get; set; }
            
        public int Value{ get; set; }

        public Name Mode{ get; set; }

            public InfluenceRule(InfluenceRuleDTO inf) 
        {
            Id = Guid.NewGuid();
           Rule = new ConditionSet( inf.Rule);
            Value = inf.Value;
            Mode = inf.Mode;
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
             dataHolder.SetValue("Rules", this.Rule);
            dataHolder.SetValue("Value", this.Value);
               dataHolder.SetValue("Mode", this.Mode);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            Rule = dataHolder.GetValue<ConditionSet>("Rules");
            Value = dataHolder.GetValue<int>("Value");
             Mode = dataHolder.GetValue<Name>("Mode");
        }


          public InfluenceRuleDTO ToDTO()
        {
            return new InfluenceRuleDTO()
            {
                Rule = this.Rule.ToDTO(),
              Value = this.Value,
                Id = this.Id,
                Mode = this.Mode
            };
        }



        public float EvaluateInfluenceRule(KB m_Kb, SubstitutionSet constraints)
        {
            float ret = 0.0f;

            var resultingConstraints = Rule.Unify(m_Kb, m_Kb.Perspective, new[] { constraints });

                if(resultingConstraints.Count() > 0)
                    ret = Value;
            

            
            return ret;
        }
        
    }
}

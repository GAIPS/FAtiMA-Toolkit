using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using ActionLibrary.DTOs;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using KnowledgeBase;
using WellFormedNames;
using SerializationUtilities;

namespace CommeillFaut
{

    [Serializable]
    public class SocialExchange : ActionRule
    {
    
        public String Intent { get; set; }

        public Guid GUID;

        public string Initiator { get; set; }

       public string targ { get; set; }

        int Response { get; set; }

        public InfluenceRule InfluenceRule { get; set; }

        public SocialExchange(String name) : base(WellFormedNames.Name.BuildName(name), Name.NIL_SYMBOL, Name.BuildName(1), Name.NIL_SYMBOL, new ConditionSet(new ConditionSetDTO()))
        {
            Intent = "";
            InfluenceRule = new InfluenceRule(new InfluenceRuleDTO());
        }


        public SocialExchange(SocialExchangeDTO s) : base(s)
        {



            InfluenceRule = new InfluenceRule(s.InfluenceRule);
            //      Name = s.SocialExchangeName;

            Intent = s.Intent;

        }

        public SocialExchange(SocialExchange other) : base(other)
		{
         
            Intent = other.Intent;
            InfluenceRule = other.InfluenceRule;
		}



      
        public float CalculateVolition(string init, string _targ, KB m_Kb)
        {
            float counter = 0;
           

              
                counter += InfluenceRule.Result(init, _targ, m_Kb);
            
            
            Console.WriteLine("Calculating Volition for " + this.ActionName.ToString() + " init: " + init + " targ: " + _targ + " result: " + counter);
            return counter;
        }


        private float CalculateResponse(string Init, string _Targ, KB m_Kb)
        {

            return this.CalculateVolition(_Targ, Init, m_Kb);

           
        }

        public override String ToString()
        {

            return base.ActionName + " " + Intent + " " + this.Id + "\n";
        }


    

        public void SetData(SocialExchangeDTO dto)
        {
            
            Intent = dto.Intent;
            if (dto.InfluenceRule != null)
                InfluenceRule = new InfluenceRule(dto.InfluenceRule);
            else InfluenceRule = new InfluenceRule(new InfluenceRuleDTO()); 
           
            SetFromDTO(dto);
        }

        public SocialExchangeDTO ToDTO()
        {
        
           
            return new SocialExchangeDTO() {Action = ActionName.ToString(), Intent = Intent, InfluenceRule = InfluenceRule.ToDTO()};
        }


       public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {

            base.GetObjectData(dataHolder, context);
            dataHolder.SetValue("Description", this.Intent);
    
            dataHolder.SetValue("InfluenceRule", this.InfluenceRule);
     
        }

        public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            base.SetObjectData(dataHolder, context);
           
            Intent = dataHolder.GetValue<string>("Description");
        
            InfluenceRule = dataHolder.GetValue<InfluenceRule>("InfluenceRule");
         
        }

        
        public void AddInfluenceRule(InfluenceRule rule)
        {
            this.InfluenceRule = rule;
        }

        public void RemoveInfluenceRule(InfluenceRule rule)
        {
            InfluenceRule = null;
        }

       public void SetInfluenceRule(InfluenceRule rule)
        {
            InfluenceRule = rule;
        }

        public void SetInfluenceRule(InfluenceRuleDTO rule)
        {
            InfluenceRule = new InfluenceRule(rule);
        }


    }
}

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
    public class SocialExchange : ICustomSerialization
    {
        public String Intent { get; set; }

        
        public Guid Id { get; private set; }
        public Name Target { get; private set; }

        private Name m_actionTemplate;
        public Name ActionName
        {
            get { return m_actionTemplate.GetFirstTerm(); }
        }


        int Response { get; set; }

        public InfluenceRule InfluenceRule { get; set; }

        public SocialExchange(String name) 
        {
            Intent = "";
            InfluenceRule = new InfluenceRule(new InfluenceRuleDTO());
        }


        public SocialExchange(SocialExchangeDTO s) 
        {



            InfluenceRule = new InfluenceRule(s.InfluenceRule);
            //      Name = s.SocialExchangeName;

            Intent = s.Intent;

        }

        public SocialExchange(SocialExchange other)
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
            return ActionName + " " + Intent + " " + this.Id + "\n";
        }

        public SocialExchangeDTO ToDTO()
        {
            return new SocialExchangeDTO() {Action = ActionName.ToString(), Intent = Intent, InfluenceRule = InfluenceRule.ToDTO()};
        }
        
       public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("Description", this.Intent);
            dataHolder.SetValue("InfluenceRule", this.InfluenceRule);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
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

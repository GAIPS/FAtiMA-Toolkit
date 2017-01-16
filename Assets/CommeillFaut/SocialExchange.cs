using System;
using System.Collections.Generic;
using ActionLibrary;
using ActionLibrary.DTOs;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using EmotionalAppraisal;
using WellFormedNames;
using SerializationUtilities;

namespace CommeillFaut
{

    [Serializable]
    public class SocialExchange : BaseActionDefinition
    {
    


        public String Intent { get; set; }

     

        public string Initiator { get; set; }

       public string targ { get; set; }

        int Response { get; set; }

        public String State { get; set; }

        public String Instantiation { get; set; }

        public List<InfluenceRuleDTO> InfluenceRules { get; set; }



        public SocialExchange(String name) : base(WellFormedNames.Name.BuildName(name), WellFormedNames.Name.BuildName("Empty"), new ConditionSet(new ConditionSetDTO()))
        
            {
            State = "Initialized";
     
            Intent = "";
            Instantiation = "";

            InfluenceRules = new List<InfluenceRuleDTO>();

        }


        public SocialExchange(SocialExchangeDTO s) : base(s)
        {

         

            InfluenceRules = new List<InfluenceRuleDTO>();
            //      Name = s.SocialExchangeName;
            if(s.InfluenceRules!=null)
            foreach (var inf in s.InfluenceRules)
            {
                InfluenceRules.Add(inf);
            }
            
           
            Intent = s.Intent;
            Instantiation = s.Instantiation;
        }

        public SocialExchange(SocialExchange other) : base(other)
		{
         
            Intent = other.Intent;
		    Instantiation = other.Instantiation;

            if (other.InfluenceRules != null)
                foreach (var inf in other.InfluenceRules)
                {
                    InfluenceRules.Add(inf);
                }

        }



        private void Instatiate()
        {
            var write = "Instantiating SocialExchange... \n";


            write += Initiator + " " + Intent + " with " + targ + "\n";

            Console.WriteLine(write);


            if (CalculateResponse(Initiator, targ) > 0)
            {
                write = targ + " accepted the" + this.ActionName + " Social Exchange \n";
                Console.WriteLine(write);


            }
            write = " Social Exchange" + base.ActionName + " completed \n";
            Console.WriteLine(write);
            //System.Threading.Thread.Sleep(2000);
        }

        public int CalculateVolition(string init, string _targ, EmotionalAppraisalAsset m_ea)
        {

            int counter = 0;
            foreach (var rule in InfluenceRules)
            {
                var result = new InfluenceRule(rule);
                counter += result.Result(init, _targ, m_ea);

            }
            return counter;
        }


        private int CalculateResponse(string Init, string _Targ)
        {
           // var write = "Calculating SocialExchangeResponse:";// Target.CalculateResponse(Name, Initiator) + "\n";

            return this.CalculateVolition(_Targ, Init);

           
        }

        public override String ToString()
        {

            return base.ActionName + " " + Intent + " " + "\n";
        }


        public void LaunchSocialExchange(string init, string _targ)
        {
            Initiator = init;
            targ = _targ;

            var write = "Launching SocialExchange: " + base.ActionName + " Initator: " + init + " Target: " + targ + "\n";

            Console.WriteLine(write);

            CalculateResponse(init, targ);

            Instatiate();

            ApplyConsequences();

            State = "Completed";
        }



        public void ApplyConsequences()
        {


       /*     Initiator.AddBelief();.SocialBeliefs[Target] += 2;
            Target.SocialBeliefs[Initiator] += 2;

            Initiator.SocialExchangeEnded();*/
          
        }

        public void SetData(SocialExchangeDTO dto)
        {
            
            Intent = dto.Intent;
            Instantiation = dto.Instantiation;

            foreach (var go in dto.InfluenceRules)
            {
                InfluenceRules.Add(go);
            }
        
            SetFromDTO(dto);
        }

        public SocialExchangeDTO ToDTO()
        {
            List<InfluenceRuleDTO> ret = new List<InfluenceRuleDTO>();

            foreach (var inf in InfluenceRules)
            {
               ret.Add(inf);
            }
            return new SocialExchangeDTO() {Action = ActionName.ToString(), Intent = Intent, Instantiation = Instantiation, InfluenceRules = ret };
        }


       public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {

         
            dataHolder.SetValue("Intent", this.Intent);
            dataHolder.SetValue("Instantiation", this.Instantiation);
            dataHolder.SetValue("InfluenceRules", this.InfluenceRules);

        
            foreach (var inf in this.InfluenceRules)
            {

                dataHolder.SetValue(inf.RuleName, inf.RuleConditions);

            }
            
            base.GetObjectData(dataHolder, context);
           
           
        }

        public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            base.SetObjectData(dataHolder, context);
           
            Intent = dataHolder.GetValue<string>("Intent");
            Instantiation = dataHolder.GetValue<string>("Instantiation");
            InfluenceRules = dataHolder.GetValue<List<InfluenceRuleDTO>>("InfluenceRules");

            int index = 0;
            foreach (var inf in InfluenceRules)
            {

              var InfConditions = dataHolder.GetValue<ConditionSetDTO>(inf.RuleName);
               inf.RuleConditions =InfConditions;
            }
        }

        public void AddInfluenceRule(InfluenceRule infrul)
        {

            InfluenceRules.Add(infrul.ToDTO());

        }

        public void EditInfluenceRule(InfluenceRule infrul)
        {
            InfluenceRules.Remove(InfluenceRules.Find(x => x.RuleName == infrul.RuleName));
            InfluenceRules.Add(infrul.ToDTO());

        }


        public void RemoveInfluenceRule(InfluenceRule infrul)
        {
            InfluenceRules.Remove(InfluenceRules.Find(x => x.RuleName == infrul.RuleName));
          

        }

        protected override float CalculateActionUtility(IAction a)
        {
            throw new NotImplementedException();
        }


    }
}

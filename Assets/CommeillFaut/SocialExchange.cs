using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using ActionLibrary.DTOs;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using EmotionalAppraisal;
using KnowledgeBase;
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

        public List<InfluenceRule> InfluenceRules { get; set; }

        public Dictionary<string,List<string>> EffectsList { get; set; }

        public SocialExchange(String name) : base(WellFormedNames.Name.BuildName(name), WellFormedNames.Name.BuildName("Empty"), new ConditionSet(new ConditionSetDTO()))
        
            {
            State = "Initialized";
     
            Intent = "";
            Instantiation = "";

            InfluenceRules = new List<InfluenceRule>();
            EffectsList = new Dictionary<string,List<string>>();

        }


        public SocialExchange(SocialExchangeDTO s) : base(s)
        {

         

            InfluenceRules = new List<InfluenceRule>();
            //      Name = s.SocialExchangeName;
            if(s.InfluenceRules!=null)
            foreach (var inf in s.InfluenceRules)
            {
                InfluenceRules.Add(new InfluenceRule(inf));
            }
            EffectsList = s.Effects ?? new Dictionary<string, List<string>>();
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

		    EffectsList = other.EffectsList;
		}



        private void Instatiate(int response)
        {
            var write = "Instantiating SocialExchange " + ActionName + " \n";


            write += Initiator + " wants to " + Intent + " with " + targ + "\n";

            Console.WriteLine(write);


           if(response > 0)
                write = targ + " accepted the " + this.ActionName + " Social Exchange \n";
           else if (response == 0)
                write = targ + " is neutral to the " + this.ActionName + " Social Exchange \n";
           else 
                write = targ + " rejected the " + this.ActionName + " Social Exchange \n";


            Console.WriteLine(write);


          
            write = " Social Exchange" + base.ActionName + " completed \n";
            Console.WriteLine(write);
            //System.Threading.Thread.Sleep(2000);
        }

        public int CalculateVolition(string init, string _targ, KB m_Kb)
        {

            int counter = 0;
            foreach (var rule in InfluenceRules)
            {

                var result = rule;
                counter += result.Result(init, _targ, m_Kb);

            }
            return counter;
        }


        private int CalculateResponse(string Init, string _Targ, KB m_Kb)
        {

            return this.CalculateVolition(_Targ, Init, m_Kb);

           
        }

        public override String ToString()
        {

            return base.ActionName + " " + Intent + " " + "\n";
        }


        public void LaunchSocialExchange(string init, string _targ, KB init_Kb, KB targ_Kb)
        {
            Initiator = init;
            targ = _targ;

            var write = "Launching SocialExchange: " + base.ActionName + " Initator: " + init + " Target: " + targ + "\n";

            Console.WriteLine(write);

           
            var response = CalculateResponse(init, targ, targ_Kb);
            Console.WriteLine("Response result: " + response );

            Instatiate(response);

         //   ApplyConsequences(init_Kb, targ_Kb, response);

            State = "Completed";


            write = " SocialExchange Completed: " + base.ActionName + " Initator: " + init + " Target: " + targ + " result : " + response + "\n";

            Console.WriteLine(write);

        }



        public void ApplyConsequences(KB me, Name initiator, Name Target, string response, bool isSpectator)
        {
            int resp = 0;

            response = response.Trim();
            
            
           
            
            Console.WriteLine(" Much effects, such response: " + response);
            var newEffectList = new List<String>();

            foreach (var effect in EffectsList)
            {
              
                if (effect.Key == response)
                {
                    newEffectList = effect.Value;
                   
                }
            }
                                     // Ideally we would be able to insert any 


            foreach (var ef in newEffectList)
            {
                Console.WriteLine("Effects: " + me + " " + initiator + " "+ Target + " "+ resp + " " + ef + " " + isSpectator);
                ApplyKeywordEffects(me, initiator, Target, resp, ef, isSpectator);
            }
          
            
           
        }

        public void ApplyKeywordEffects(KB me, Name initiator, Name other, int result, string keyword, bool spectator)
        {
            char[] delimitedChars = {'(', ')', ','};
            bool isInitiator = (initiator == me.Perspective);
            string[] words = keyword.Split(delimitedChars);
            var value = 0;
            Console.WriteLine("Effects Keyword: " +  keyword);
                                       // social network but we don't store them just yet   Attraction(Initiator,Target,3)
                if (words[1] == "Initiator")
                {
                    if (isInitiator)
                    {

                        if (me.AskProperty((Name) (words[0] + "(" + me.Perspective + ","  + other.ToString() + ")")) != null)
                        {
                            value =
                                Convert.ToInt32(
                                    me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + other.ToString() + ")")).ToString());
                            value += Convert.ToInt32(words[3]);

                        }
                        else
                        {
                            value = Convert.ToInt32(words[3]);
                        }

                        var insert = "" + value;
                        me.Tell((Name)(words[0] + "(" + me.Perspective + "," + other.ToString() + ")"), (Name) insert);
                        return;
                    }
                else if (spectator)
                {
                    if (me.AskProperty((Name)(words[0] + "(" + initiator + "," + other + ")")) != null)
                    {
                        value =
                            Convert.ToInt32(
                                me.AskProperty(me.AskProperty((Name)(words[0] + "(" + initiator + "," + other + ")")))
                                    .ToString());
                        value += Convert.ToInt32(words[3]);
                    }

                    else
                    {
                        value = Convert.ToInt32(words[3]);
                    }

                    var insert = "" + value;
                    me.Tell((Name)(words[0] + "(" + initiator + "," + other.ToString() + ")"), (Name)insert);
                }



            }

            if (words[1] == "Target")
            {
                if (!isInitiator && !spectator)
                {
                    if (me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")")) != null)
                    {
                        value =
                            Convert.ToInt32(
                                me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")")).ToString());
                        value += Convert.ToInt32(words[3]);

                    }
                    else
                    {
                        value = Convert.ToInt32(words[3]);
                    }

                    var insert = "" + value;
                    me.Tell((Name)(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")"), (Name)insert);
                    
                }
                else if (spectator)
                {
                    if (me.AskProperty((Name) (words[0] + "(" + other + "," + initiator + ")")) != null)
                    {
                        value =
                            Convert.ToInt32(
                                me.AskProperty(me.AskProperty((Name) (words[0] + "(" + other + "," + initiator + ")")))
                                    .ToString());
                        value += Convert.ToInt32(words[3]);
                    }

                    else
                    {
                        value = Convert.ToInt32(words[3]);
                    }

                      var insert = "" + value;
                    me.Tell((Name)(words[0] + "(" + other + "," + initiator + ")"), (Name)insert);
                }
            }




        }

        public void SetData(SocialExchangeDTO dto)
        {
            
            Intent = dto.Intent;
            Instantiation = dto.Instantiation;

            foreach (var go in dto.InfluenceRules)
            {
                InfluenceRules.Add(new InfluenceRule(go));
            }
        
            SetFromDTO(dto);
        }

        public SocialExchangeDTO ToDTO()
        {
            List<InfluenceRuleDTO> ret = new List<InfluenceRuleDTO>();

            foreach (var inf in InfluenceRules)
            {
               ret.Add(inf.ToDTO());
            }
            return new SocialExchangeDTO() {Action = ActionName.ToString(), Intent = Intent, Instantiation = Instantiation, InfluenceRules = ret, Effects = EffectsList};
        }


       public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {

            base.GetObjectData(dataHolder, context);
            dataHolder.SetValue("Intent", this.Intent);
            dataHolder.SetValue("Instantiation", this.Instantiation);
            dataHolder.SetValue("InfluenceRules", this.InfluenceRules);
            dataHolder.SetValue("EffectsList", this.EffectsList);

    
           
        }

        public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            base.SetObjectData(dataHolder, context);
           
            Intent = dataHolder.GetValue<string>("Intent");
            Instantiation = dataHolder.GetValue<string>("Instantiation");
            InfluenceRules = dataHolder.GetValue<List<InfluenceRule>>("InfluenceRules");
            EffectsList = dataHolder.GetValue<Dictionary<string,List<string>>>("EffectsList");
         
        }

        public void AddInfluenceRule(InfluenceRule infrul)
        {

            InfluenceRules.Add(infrul);

        }

        public void EditInfluenceRule(InfluenceRule infrul)
        {
            InfluenceRules.Remove(InfluenceRules.Find(x => x.RuleName == infrul.RuleName));
            InfluenceRules.Add(infrul);

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

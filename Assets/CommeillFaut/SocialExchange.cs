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
    public class SocialExchange : BaseActionDefinition
    {
    


        public String Intent { get; set; }

        public Guid GUID;

        public string Initiator { get; set; }

       public string targ { get; set; }

        int Response { get; set; }

        public InfluenceRule InfluenceRule { get; set; }

        public SocialExchange(String name) : base(WellFormedNames.Name.BuildName(name), WellFormedNames.Name.BuildName("Empty"), new ConditionSet(new ConditionSetDTO()))
        
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



        private void Instatiate(float response)
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

        public float CalculateVolition(string init, string _targ, KB m_Kb)
        {
            float counter = 0;
           

              
                counter += InfluenceRule.Result(init, _targ, m_Kb);

            
         //   Console.WriteLine("Calculating Volition for " + this.ActionName.ToString() + " init: " + init + " targ: " + _targ + " result: " + counter);
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


            write = " SocialExchange Completed: " + base.ActionName + " Initator: " + init + " Target: " + targ + " result : " + response + "\n";

            Console.WriteLine(write);

        }



        public Dictionary<Name, Name> ApplyConsequences(KB me, Name initiator, Name Target, string response, bool isSpectator)
        {
            int resp = 0;

            response = response.Trim();
            
            
           
            
          //  Console.WriteLine(" Much effects, such response: " + response);
            var newEffectList = new List<String>();

                                 // Ideally we would be able to insert any 

            var retList = new Dictionary<Name, Name>();
            foreach (var ef in newEffectList)
            {
                Console.WriteLine("Effects: " + me + " " + initiator + " "+ Target + " "+ resp + " " + ef + " " + isSpectator);
                var retPair = ApplyKeywordEffects(me, initiator, Target, resp, ef, isSpectator);
                if(retPair.Key!=null)
                retList.Add(retPair.Key, retPair.Value);
            }

            return retList;
           
        }

        public KeyValuePair<Name,Name> ApplyKeywordEffects(KB me, Name initiator, Name other, int result, string keyword, bool spectator)
        {
            char[] delimitedChars = {'(', ')', ','};
            bool isInitiator = (initiator == me.Perspective);
            string[] words = keyword.Split(delimitedChars);
            var value = 0.0f;
            // Console.WriteLine("Effects Keyword: " +  keyword);
            // social network but we don't store them just yet   Attraction(Initiator,Target,3)
            if (words[1] == "Initiator")
            {
                if (isInitiator)
                {

                    if (me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + other.ToString() + ")")) != null)
                    {
                        value = me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + other.ToString() + ")")).Certainty;
                        if (value <= 1)
                            value += 0.2f;

                    }
                    else
                    {
                        value = 0.2f;
                    }

                    // var insert = "" + value;

                    me.Tell((Name.BuildName(words[0] + "(" + me.Perspective + "," + other.ToString() + ")")), Name.BuildName("True"), me.Perspective, value);

                    return new KeyValuePair<Name, Name>();

                    //return new KeyValuePair<Name, Name>(Name.BuildName(words[0] + "(" + me.Perspective + "," + other.ToString() + ")"), Name.BuildName(insert));
                }
                else if (spectator)
                {
                    if (me.AskProperty((Name)(words[0] + "(" + initiator + "," + other + ")")) != null)
                    {
                        value = me.AskProperty((Name)(words[0] + "(" + initiator + "," + other + ")")).Certainty;
                        if (value <= 1)
                            value += 0.2f;

                    }

                    else
                    {
                        value = 0.2f;

                        me.Tell((Name)(words[0] + "(" + initiator + "," + other + ")"), Name.BuildName("True"), initiator, value);

                        //    return new KeyValuePair<Name, Name>(Name.BuildName(words[0] + "(" + initiator + "," + other.ToString() + ")"), Name.BuildName(insert));

                        return new KeyValuePair<Name, Name>();
                    }


                }
            }

            if (words[1] == "Target")
            {
                if (!isInitiator && !spectator)
                {
                    if (me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")")) != null)
                    {
                        value =  me.AskProperty((Name)(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")")).Certainty;
                            if (value <= 1)
                                value += 0.2f;

                        }
                    else
                    {
                            value = 0.2f;
                    }

                        //   var insert = "" + value;

                        me.Tell(Name.BuildName(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")"), Name.BuildName("True"), me.Perspective, value);

                        //    return new KeyValuePair<Name, Name>(Name.BuildName(words[0] + "(" + me.Perspective + "," + initiator.ToString() + ")"), Name.BuildName(insert));

                        return new KeyValuePair<Name, Name>();

                    }
                else if (spectator)
                {
                    if (me.AskProperty((Name) (words[0] + "(" + other + "," + initiator + ")")) != null)
                    {
                        value = me.AskProperty((Name) (words[0] + "(" + other + "," + initiator + ")")).Certainty;
                            if (value <= 1)
                                value += 0.2f;
                        }

                    else
                    {
                        value =0.2f;
                    }

                        me.Tell(Name.BuildName(words[0] + "(" + other + "," + initiator + ")"), Name.BuildName("True"), other,  value);



                        //   return new KeyValuePair<Name, Name>(Name.BuildName(words[0] + "(" + other + "," + initiator + ")"), Name.BuildName(insert));

                        return new KeyValuePair<Name, Name>();

                    }
            }



            return new KeyValuePair<Name, Name>();

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

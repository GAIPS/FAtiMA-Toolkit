using System;
using System.Collections.Generic;
using ActionLibrary;
using ActionLibrary.DTOs;
using CommeillFaut.DTOs;
using WellFormedNames;


namespace CommeillFaut
{


    public class SocialExchange : BaseActionDefinition
    {


        public String Name { get; set; }

        String Intent { get; set; }

        public Name Initiator { get; set; }

       public Name Target { get; set; }

        int Response { get; set; }

        public String State { get; set; }



        public List<InfluenceRule> InfluenceRules { get; set; }



        public SocialExchange(String name, String intent) : base(new ActionDefinitionDTO())
        {
            State = "Initialized";
            Name = name;
            Intent = intent;
          //  Initiator = init;
          //  Target = targ;

            InfluenceRules = new List<InfluenceRule>();

        }


        public SocialExchange(SocialExchangeDTO s) : base(s)
        {

        }


        protected override float CalculateActionUtility(IAction a)
        {
            throw new NotImplementedException();
        }

        private void Instatiate()
        {
            var write = "Instantiating SocialExchange... \n";


            write += Initiator + " " + Intent + " with " + Target + "\n";

            Console.WriteLine(write);


            if (CalculateResponse(Initiator, Target) > 0)
            {
                write = Target + " accepted the" + this.Name + " Social Exchange \n";
                Console.WriteLine(write);


            }
            write = " Social Exchange" + Name + " completed \n";
            Console.WriteLine(write);
            //System.Threading.Thread.Sleep(2000);
        }

        public int CalculateVolition(Name init, Name targ)
        {

            int counter = 0;
            foreach (var rule in InfluenceRules)
            {
                counter += rule.Result(init, targ);

            }
            return counter;
        }


        private int CalculateResponse(Name Init, Name Targ)
        {
            var write = "Calculating SocialExchangeResponse:";// Target.CalculateResponse(Name, Initiator) + "\n";

            return this.CalculateVolition(Targ, Init);

            Console.WriteLine(write);
        }

        public override String ToString()
        {

            return Name + " " + Intent + " " + "\n" + "Initiator: "+ Initiator.ToString() + "\n" + "Target: "+ Target.ToString() + "\n";
        }


        public void LaunchSocialExchange(Name init, Name targ)
        {
            Initiator = init;
            Target = targ;

            var write = "Launching SocialExchange: " + Name + " Initator: " + init + " Target: " + targ + "\n";

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
    }
}

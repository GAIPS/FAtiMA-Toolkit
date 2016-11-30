using System;
using System.Linq;
using ActionLibrary;
using AssetManagerPackage;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Rage;
using WellFormedNames;


namespace EmotionalDecisionMakingTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Emotional Decision Making Asset
        static void Main(string[] args)
        {
			AssetManager.Instance.Bridge = new BasicIOBridge();
			//First we construct a new instance of the EmotionalDecisionMakingAsset class
			var edm = new EmotionalDecisionMakingAsset();

            //Second, we need to associate an existing EmotionalAppraisalAsset to the new instance
            //Since there is none existing we create a new EmotionalAppraisalAsset as well
            var ea = new EmotionalAppraisalAsset("John");
            edm.RegisterEmotionalAppraisalAsset(ea);

            //create a reaction rule
            var reaction = new ReactionDTO {Action = "Kick", Priority = 0, Target = "Player"};
            var reaction2 = new ReactionDTO { Action = "Punch", Priority = 1, Target = "Player" };

            //add the reaction rule
            var id = edm.AddReaction(reaction);
            var id2 = edm.AddReaction(reaction2);

            edm.AddReactionCondition(id, "Mood(SELF) = 0");


            edm.AddReactionCondition(id2, "Mood(SELF) = 0");


            //the method decide will now trigger the previous reaction defined (since the default value of mood is 0) 
            var actions = edm.Decide();
            Console.WriteLine("Decision: " + actions.ToList()[0].ToStartEventName((Name)"John"));
            //Console.WriteLine("Decision: " + string.Concat(actions.Select(a => a.ToStartEventName((Name)"John"))));

            //this is how you can load the asset from a file
            edm = EmotionalDecisionMakingAsset.LoadFromFile("../../../Examples/EDMTest.edm");
            edm.RegisterEmotionalAppraisalAsset(ea);
            
            Console.ReadKey();
        }
    }
}


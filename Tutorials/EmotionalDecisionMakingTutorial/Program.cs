using System;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using KnowledgeBase.WellFormedNames;


namespace EmotionalDecisionMakingTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Emotional Decision Making Asset
        static void Main(string[] args)
        {
            //First we construct a new instance of the EmotionalDecisionMakingAsset class
            var edm = new EmotionalDecisionMakingAsset();

            //Second, we need to associate an existing EmotionalAppraisalAsset to the new instance
            //Since there is none existing we create a new EmotionalAppraisalAsset as well
            var ea = new EmotionalAppraisalAsset("John");
            edm.RegisterEmotionalAppraisalAsset(ea);

            //create a reaction rule
            var reaction = new ReactionDTO {Action = "Kick", Priority = 0, Target = "Player"};
            
            //add the reaction rule
            var id = edm.AddReaction(reaction);
            edm.AddReactionCondition(id, "Mood(SELF) = 0");

            //the method decide will now trigger the previous reaction defined (since the default value of mood is 0) 
            var actions = edm.Decide();
            Console.WriteLine("Decision: " + string.Concat(actions.Select(a => a.ToEventName((Name)"John"))));

            Console.ReadKey();
        }
    }
}


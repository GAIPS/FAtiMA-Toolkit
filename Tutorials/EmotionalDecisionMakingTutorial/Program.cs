using System;
using System.Linq;
using ActionLibrary;
using AssetManagerPackage;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Rage;
using WellFormedNames;
using KnowledgeBase;

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

            //Then, we have to register an existing knowledge base to the asset so it can check for 
            //beliefs are true
            var kb = new KB((Name)"John");
            kb.Tell((Name)"LikesToFight(SELF)", (Name)"True");
            edm.RegisterKnowledgeBase(kb);

            //create a reaction rule
            var reaction = new ReactionDTO {Action = "Kick", Priority = 0, Target = "Player" };

            //add the reaction rule
            var id = edm.AddReaction(reaction);
            
            edm.AddReactionCondition(id, "LikesToFight(SELF) = True");
            var actions = edm.Decide();
            Console.WriteLine("Decisions: ");
            foreach (var a in actions)
            {
                Console.WriteLine(a.Name.ToString());
            }
            
            //this is how you can load the asset from a file
            edm = EmotionalDecisionMakingAsset.LoadFromFile("../../../Examples/EDMTest.edm");
                        
            Console.ReadKey();
        }
    }
}


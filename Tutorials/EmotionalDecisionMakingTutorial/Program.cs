using System;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using GAIPS.Rage;
using WellFormedNames;
using KnowledgeBase;
using ActionLibrary.DTOs;

namespace EmotionalDecisionMakingTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Emotional Decision Making Asset
        static void Main(string[] args)
        {
			//First we construct a new instance of the EmotionalDecisionMakingAsset class
			var edm = new EmotionalDecisionMakingAsset();

            //Then, we have to register an existing knowledge base to the asset so it can check for 
            //beliefs are true
            var kb = new KB((Name)"John");
            kb.Tell((Name)"LikesToFight(SELF)", (Name)"True");
            edm.RegisterKnowledgeBase(kb);

            //create an action rule
            var actionRule = new ActionRuleDTO {Action = Name.BuildName("Kick"), Priority = Name.BuildName("4"), Target = (Name)"Player" };

            //add the reaction rule
            var id = edm.AddActionRule(actionRule);
            
            edm.AddRuleCondition(id, "LikesToFight(SELF) = True");
            var actions = edm.Decide(Name.UNIVERSAL_SYMBOL);

            Console.WriteLine("Decisions: ");
            foreach (var a in actions)
            {
                Console.WriteLine(a.Name.ToString() + " p: " + a.Utility);
            }

            //this is how you can load the asset from a file
            Console.WriteLine("Loading From File: ");
            edm = EmotionalDecisionMakingAsset.LoadFromFile("../../../Examples/EDM-Tutorial/EDMTest.edm");
            edm.RegisterKnowledgeBase(kb);
            actions = edm.Decide(Name.UNIVERSAL_SYMBOL);

            foreach (var a in actions)
            {
              Console.WriteLine(a.Name.ToString() + " p: " + a.Utility);
            }
            Console.WriteLine("Decisions: ");
            foreach (var a in actions)
            {
                Console.WriteLine(a.Name.ToString() + " p: " + a.Utility);
            }
            Console.ReadKey();
        }
    }
}


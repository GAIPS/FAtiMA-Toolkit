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
		//	var edm = new EmotionalDecisionMakingAsset();

            //Second, we need to associate an existing EmotionalAppraisalAsset to the new instance
            //Since there is none existing we create a new EmotionalAppraisalAsset as well
            /*   var kb = new KB((Name)"John");
               edm.RegisterKnowledgeBase(kb);

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

               Console.ReadKey();*/

            var edm = EmotionalDecisionMakingAsset.LoadFromFile("../../../Examples/cifEDM.edm");

            foreach (var react in edm.GetAllReactions())
            {
                Console.WriteLine(react.Action + " " + react.Target + "\n ");
            }

            
            var kb = new KB((Name)"Mary");

            kb.Tell((Name)"Volition(Flirt,SELF,Peter)", (Name)"6");
            kb.Tell((Name)"Volition(Compliment,SELF,Peter)", (Name)"-6");

            Console.WriteLine("Mary's knowledge base: " + "Volition(Flirt, SELF, Peter) " + kb.AskProperty((Name)"Volition(Flirt, SELF, Peter)").ToString());
            Console.WriteLine("Mary's knowledge base: " + "Volition(Compliment, SELF, Peter) " + kb.AskProperty((Name)"Volition(Compliment, SELF, Peter)").ToString() + "\n");

            edm.RegisterKnowledgeBase(kb);


            var actions = edm.Decide();
            foreach (var move in actions)
            {
                Console.WriteLine(kb.Perspective + " Decisions: " + move.ToStartEventName((Name)"Mary"));
            }
          
            Console.WriteLine("\n");

            kb = new KB((Name)"Peter");

            kb.Tell((Name)"Volition(Flirt,SELF,Mary)", (Name)"6");
            kb.Tell((Name)"Volition(Compliment,SELF,Mary)", (Name)"6");

            Console.WriteLine("Peter's knowledge base: " + "Volition(Flirt, SELF, Mary) " + kb.AskProperty((Name)"Volition(Flirt, SELF, Mary)").ToString());
           Console.WriteLine("Peter's knowledge base: " + "Volition(Compliment, SELF, Mary) " + kb.AskProperty((Name)"Volition(Compliment, SELF, Mary)").ToString() + "\n");

            edm.RegisterKnowledgeBase(kb);


          
            actions = edm.Decide();
            foreach (var move in actions)
            {
                Console.WriteLine(kb.Perspective + " Decisions: " + move.ToStartEventName((Name)"Peter"));
               
            }
       
            

            Console.WriteLine("\n");

            

            Console.ReadKey();

           // edm.Save();



        }
    }
}


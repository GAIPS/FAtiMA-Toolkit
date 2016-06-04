using System;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;

namespace EmotionalAppraisalTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Emotional Appraisal Asset
        static void Main(string[] args)
        {
            var kickEvent = "Event(Action, Player, Kick, John)";

            // To create a new asset it is required to tell the name of the agent which will correpond to the perspective of the "SELF" 
            EmotionalAppraisalAsset ea = new EmotionalAppraisalAsset("John");

            //The mood value starts at 0 and it can vary from -10 to 10 in response to new emotions.
            Console.WriteLine("Default Mood: " + ea.Mood);
            Console.WriteLine("Active Emotions: " + string.Concat(ea.ActiveEmotions.Select(e => e.Type + ":" + e.Intensity + ", ")));

            //Emotions are generated in reponse to events that occur in the game world 
            ea.AppraiseEvents(new[] { kickEvent });

            //Given that the agent has no appraisal rules defined, the previous event will have no effect on the agent's mood
            Console.WriteLine("\nMood after appraising  '" + kickEvent + "' with no appraisal rules defined: " + ea.Mood);
            Console.WriteLine("Active Emotions: " + string.Concat(ea.ActiveEmotions.Select(e => e.Type + ":" + e.Intensity + ", ")));


            //The following lines add an appraisal rule that will make the kickEvent be perceived as undesirable
            var rule = new AppraisalRuleDTO { EventMatchingTemplate = "Event(Action, *, Kick, SELF)", Desirability = -5f, Praiseworthiness = -3f };
            ea.AddOrUpdateAppraisalRule(rule);

            //After adding the previous appraisal rule, the kick event will have a negative effect on the agent's mood
            ea.AppraiseEvents(new[] { kickEvent });
            Console.WriteLine("\nMood after appraising  '" + kickEvent + "' with the appraisal rule: '" + rule.EventMatchingTemplate + "':" + ea.Mood);
            Console.WriteLine("Active Emotions: " + string.Concat(ea.ActiveEmotions.Select(e => e.Type + "-" + e.Intensity + ", ")));

            //The update function will increase the current tick by 1. Each active emotion will decay to 0 and the mood will slowly return to 0
            for (int i = 0; i < 10; i++)
            {
                ea.Update();
                Console.WriteLine("\nMood on tick '" + ea.Tick + "':" + ea.Mood);
                Console.WriteLine("Active Emotions: " + string.Concat(ea.ActiveEmotions.Select(e => e.Type + "-" + e.Intensity + ", ")));
            }

            Console.ReadKey();
        }
    }
}


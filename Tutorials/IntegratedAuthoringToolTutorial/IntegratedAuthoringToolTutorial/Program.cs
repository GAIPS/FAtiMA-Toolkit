using AssetManagerPackage;
using RolePlayCharacter;
using IntegratedAuthoringTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ActionLibrary;
using WellFormedNames;

namespace IntegratedAuthoringToolTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            AssetManager.Instance.Bridge = new BasicIOBridge();

            //Loading the asset
            /*     var iat = IntegratedAuthoringToolAsset.LoadFromFile("C://Teste/SpaceModulesScenarioA.iat");
                 var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
                 rpc.Initialize();
                 iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

                 var eventStr = "Event(Action-Finished, Player, Kick, Client)";
                 Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
                 Console.WriteLine("Mood: " + rpc.Mood);
                 Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);

                 var action = rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") });

                 WriteAction(action);
                 Console.WriteLine();
                 WriteAction(rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") }));

                 rpc.ActionFinished(action);
                 WriteAction(rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") }));

                
             }
       */



            var iat = IntegratedAuthoringToolAsset.LoadFromFile("../../../../Examples/cifIAT.iat");
            List<RolePlayCharacterAsset> rpcList = new List<RolePlayCharacterAsset>();
            foreach (var source in iat.GetAllCharacterSources())
            {
                var rpc = RolePlayCharacterAsset.LoadFromFile(source.Source);
                rpc.Initialize();
                iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
                rpcList.Add(rpc);
            }


            while (true)
            {

                var position = new Random();


                var actor = rpcList.ElementAt(position.Next(2));

                var actioN = actor.PerceptionActionLoop(new[] {(Name) ("Event(Action-Start,Player,Start,-)")});

               if(actioN!=null) actor.ActionFinished(actioN);

                Console.WriteLine("Character" + actor.CharacterName + " does " + WriteAction(actioN) + "\n");

                Console.ReadKey();
            }

        }

        /*         Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
                 Console.WriteLine("Mood: " + rpc.Mood);
                 Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" +
                                   rpc.GetStrongestActiveEmotion()?.Intensity);
     
                 var action = rpc.PerceptionActionLoop(new[] {(Name) ("Event(Action-Start,Player,Start,-)")});
     
        //         WriteAction(action);
                 Console.WriteLine("\n");
     
     
                 rpc2.Initialize();
                 iat.BindToRegistry(rpc2.DynamicPropertiesRegistry);
     
                 /*Console.WriteLine("The name of the character loaded is: " + rpc2.CharacterName);
                 Console.WriteLine("Mood: " + rpc2.Mood);
                 Console.WriteLine("Strongest emotion: " + rpc2.GetStrongestActiveEmotion()?.EmotionType + "-" +
                                   rpc.GetStrongestActiveEmotion()?.Intensity);
     
                 action = rpc2.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") });
     
          //       WriteAction(action);
                 Console.WriteLine("\n");
     
                 rpc2.ActionFinished(action);
                 
     
     //            WriteAction(rpc2.PerceptionActionLoop(new[] {(Name) ("Event(Action-Start,Player,Start,-)")}));
     
      //           WriteAction(rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") }));
     
     
                 Console.ReadKey();
                 */
        


        static string WriteAction(IAction a)
        {
            string result = "";
            if (a == null)
            {
              
                return "Null action";
            }

            result += a.ActionName;
            result += " Parameters: ";
            foreach (var p in a.Parameters)
            {
                result += p + ", ";
            }
            return result;
        }
    }
}

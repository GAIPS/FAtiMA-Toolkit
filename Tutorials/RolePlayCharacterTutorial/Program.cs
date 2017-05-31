using System;
using AssetManagerPackage;
using RolePlayCharacter;
using System.Linq;

namespace RolePlayCharacterTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
			AssetManager.Instance.Bridge = new BasicIOBridge();
            //Loading the asset
	        var rpc = RolePlayCharacterAsset.LoadFromFile("../../../Examples/RPCTest.rpc");
            rpc.LoadAssociatedAssets();

            Console.WriteLine("Starting Mood: " + rpc.Mood);
           var action = rpc.Decide().FirstOrDefault();
            rpc.Update();

            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
          //  Console.WriteLine("The following event was perceived: " + event1);
            Console.WriteLine("Mood after event: " + rpc.Mood);
            Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);
            Console.WriteLine("First Response: " + action?.Name + ", Target:" + action?.Target.ToString());

            var event2 = EventHelper.ActionStart(rpc.CharacterName.ToString(), action?.Name.ToString(), "Player");

            rpc.Perceive(new[] { event2 });
            var busyAction = rpc.Decide().FirstOrDefault(); ;

            Console.WriteLine("Second Response: " + busyAction?.Name + ", Target:" + action?.Target.ToString());

            var event3 = EventHelper.ActionEnd(rpc.CharacterName.ToString(), action?.Name.ToString(), "Player");

            rpc.Perceive(new[] { event3 });
            action = rpc.Decide().FirstOrDefault(); 

            Console.WriteLine("Third Response: " + action?.Name +", Target:" + action?.Target.ToString());

        
            int x = 0;
            while (true)
            {
               
                Console.WriteLine("Mood after tick: " + rpc.Mood + " x: "  + x + " tick: " + rpc.Tick);
                Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);
                rpc.SaveToFile("../../../Examples/RPCTest-Output.rpc");
                rpc.Update();
               Console.ReadLine();


                if (x == 10)
                {
                    var event1 = EventHelper.ActionEnd("Player", "Kick", rpc.CharacterName.ToString());

                    rpc.Perceive(new[] { event1 });
                    action = rpc.Decide().FirstOrDefault();
                    rpc.SaveToFile("../../../Examples/RPCTest-OutputEvent.rpc");
                    rpc.Update();
                }


                if (x == 11)
                {
                    rpc.ResetEmotionalState();
                }
                if (x == 25)
                {
                    var event1 = EventHelper.ActionEnd("Player", "Kick", rpc.CharacterName.ToString());

                    rpc.Perceive(new[] { event1 });
                     action = rpc.Decide().FirstOrDefault();
                    rpc.SaveToFile("../../../Examples/RPCTest-OutputEvent.rpc");
                    rpc.Update();
                 }


                else if (x == 30)
                {
                   Console.WriteLine("Reloading " + rpc.GetStrongestActiveEmotion().Intensity + " " + rpc.GetStrongestActiveEmotion().EmotionType + " mood: " + rpc.Mood);
                    rpc.SaveToFile("RPCTestReload.rpc");

                    rpc = RolePlayCharacterAsset.LoadFromFile("RPCTestReload.rpc");
                    Console.WriteLine("Reloading result: " + rpc.GetStrongestActiveEmotion().Intensity + " " + rpc.GetStrongestActiveEmotion().EmotionType + " mood: " + rpc.Mood);

                }

                    x++;

            

            }
            Console.WriteLine("Mood after tick: " + rpc.Mood);
            Console.ReadKey();

        }
    }
}

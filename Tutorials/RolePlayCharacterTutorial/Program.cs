using System;
using AssetManagerPackage;
using RolePlayCharacter;
using System.Linq;
using WellFormedNames;

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

            var event1 = EventHelper.ActionEnd("Player","Kick", rpc.CharacterName.ToString());
                      
            var action = rpc.PerceptionActionLoop(new[] { event1 }).FirstOrDefault();;

            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
            Console.WriteLine("The following event was perceived: " + event1);
            Console.WriteLine("Mood after event: " + rpc.Mood);
            Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);
            Console.WriteLine("First Response: " + action?.Name + ", Target:" + action?.Target.ToString());

            var event2 = EventHelper.ActionStart(rpc.CharacterName.ToString(), action?.Name.ToString(), "Player");

            var busyAction = rpc.PerceptionActionLoop(new[] { event2 }).FirstOrDefault();

            Console.WriteLine("Second Response: " + busyAction?.Name + ", Target:" + action?.Target.ToString());

            var event3 = EventHelper.ActionEnd(rpc.CharacterName.ToString(), action?.Name.ToString(), "Player");

            action = rpc.PerceptionActionLoop(new[] { event3 }).FirstOrDefault();

            Console.WriteLine("Third Response: " + action?.Name +", Target:" + action?.Target.ToString());

            rpc.SaveToFile("../../../Examples/RPCTest-Output.rpc");
            Console.ReadKey();

        }
    }
}

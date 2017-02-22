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

            var event1 = EventHelper.ActionEnd("Player","Kick", rpc.CharacterName.ToString());

            rpc.Perceive(new[] { event1 });
            var action = rpc.Decide().FirstOrDefault();;
            rpc.Update();

            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
            Console.WriteLine("The following event was perceived: " + event1);
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

            rpc.SaveToFile("../../../Examples/RPCTest-Output.rpc");

            while (rpc.Mood != 0)
            {
                Console.WriteLine("Mood after tick: " + rpc.Mood);
                rpc.Update();
                Console.ReadKey();
            }
            Console.WriteLine("Mood after tick: " + rpc.Mood);
            Console.ReadKey();

        }
    }
}

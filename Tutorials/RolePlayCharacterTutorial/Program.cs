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
            rpc.Initialize();
            var event1 = EventHelper.ActionEnd("Player","Kick",rpc.CharacterName.ToString());
            var action = rpc.PerceptionActionLoop(new[] { event1 }).FirstOrDefault();;
            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
            Console.WriteLine("The following event was perceived: " + event1);
            Console.WriteLine("Mood after event: " + rpc.Mood);
            Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);
            Console.WriteLine("Response: " + action?.ToString());
            Console.ReadKey();
            rpc.SaveToFile("../../../Examples/RPCTest-Output.rpc");
        }
    }
}

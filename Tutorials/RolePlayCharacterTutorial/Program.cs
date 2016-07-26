using System;
using GAIPS.Rage;
using RolePlayCharacter;

namespace RolePlayCharacterTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loading the asset
            var rpc = RolePlayCharacterAsset.LoadFromFile(LocalStorageProvider.Instance, "../../../Examples/RPCTest.rpc");
            var eventStr = "Event(Action-Start, Player, Kick, Client)";

            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
            Console.WriteLine("Perspective: " + rpc.Perspective);
            Console.WriteLine("Mood: " + rpc.Mood);
            Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-"+ rpc.GetStrongestActiveEmotion()?.Intensity);
            Console.WriteLine("Selected Action: " + rpc.PerceptionActionLoop(new []{eventStr})?.ActionName);
            Console.ReadKey();
        }
    }
}

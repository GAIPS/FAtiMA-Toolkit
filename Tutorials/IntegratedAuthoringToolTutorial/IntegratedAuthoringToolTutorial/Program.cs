using AssetManagerPackage;
using RolePlayCharacter;
using IntegratedAuthoringTool;
using System;
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
            var iat = IntegratedAuthoringToolAsset.LoadFromFile("/../../../Examples/IATTest.iat");
	        var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

            var eventStr = "Event(Action-Finished, Player, Kick, Client)";
            Console.WriteLine("The name of the character loaded is: " + rpc.CharacterName);
            Console.WriteLine("Mood: " + rpc.Mood);
            Console.WriteLine("Strongest emotion: " + rpc.GetStrongestActiveEmotion()?.EmotionType + "-" + rpc.GetStrongestActiveEmotion()?.Intensity);
            var action = rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") }).FirstOrDefault();
            WriteAction(action);
            Console.WriteLine();
            WriteAction(rpc.PerceptionActionLoop(new[] { (Name)("Event(Action-Start,Player,Start,-)") }).FirstOrDefault());
			
            Console.ReadKey();
        }

        static void WriteAction(IAction a)
        {
            if(a == null)
            {
                Console.WriteLine("Null action");
                return;
            }

            Console.WriteLine("Selected Action: " + a.Key);
            Console.WriteLine("Parameters: ");
            foreach (var p in a.Parameters)
            {
                Console.Write(p + ", ");
            }
        }
    }
}

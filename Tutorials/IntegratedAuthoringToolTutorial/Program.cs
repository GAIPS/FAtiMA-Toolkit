using AssetManagerPackage;
using RolePlayCharacter;
using IntegratedAuthoringTool;
using System;
using System.Linq;

namespace IntegratedAuthoringToolTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            AssetManager.Instance.Bridge = new BasicIOBridge();
            var playerStr = IATConsts.PLAYER;
         
            //Loading the asset
            var iat = IntegratedAuthoringToolAsset.LoadFromFile("../../../Examples/IATTest.iat");
            var currentState = IATConsts.INITIAL_DIALOGUE_STATE;

            var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);


            while (currentState != IATConsts.TERMINAL_DIALOGUE_STATE)
            {
                var playerDialogs = iat.GetDialogueActionsByState(playerStr, IATConsts.INITIAL_DIALOGUE_STATE);

                Console.WriteLine("Current Dialogue State: " + currentState);
                Console.WriteLine("Available choices: ");

                for (int i = 0; i < playerDialogs.Count(); i++)
                {
                    Console.WriteLine(i + " - " + playerDialogs.ElementAt(i).Utterance);
                }
                int pos = -1;

                do
                {
                    Console.Write("Select Option: ");
                } while (!Int32.TryParse(Console.ReadLine(), out pos) || pos > playerDialogs.Count() || pos < 0);

                var actionName = iat.BuildSpeakActionName(playerStr, playerDialogs.ElementAt(pos).Id);
                var evt = EventHelper.ActionEnd(playerStr, actionName.ToString(), rpc.CharacterName.ToString());

                var characterAction = rpc.PerceptionActionLoop(new[] { evt }).FirstOrDefault();

                
                Console.WriteLine("\n" + rpc.CharacterName + ": " + characterAction.Name +"\n");
            }
            Console.ReadKey();
        }
    }
}

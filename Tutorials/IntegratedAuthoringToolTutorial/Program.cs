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
        //    var iat = IntegratedAuthoringToolAsset.LoadFromFile("D:/FA/inspector-toto.iat");
            //D:/FA/inspector-toto.iat
            var currentState = IATConsts.INITIAL_DIALOGUE_STATE;

            var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

            while (currentState != IATConsts.TERMINAL_DIALOGUE_STATE)
            {
                var playerDialogs = iat.GetDialogueActionsByState(playerStr, currentState);

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
                } while (!Int32.TryParse(Console.ReadLine(), out pos) || pos >= playerDialogs.Count() || pos < 0);

                var chosenDialog = playerDialogs.ElementAt(pos);

                var actionName = iat.BuildSpeakActionName(playerStr, chosenDialog.Id);
                var speakEvt = EventHelper.ActionEnd(playerStr, actionName.ToString(), rpc.CharacterName.ToString());

                currentState = chosenDialog.NextState;
                var dialogStateChangeEvt = EventHelper.PropertyChange(string.Format(IATConsts.DIALOGUE_STATE_PROPERTY,playerStr), chosenDialog.NextState, playerStr);

                rpc.Perceive(new[] { speakEvt, dialogStateChangeEvt });
                var characterActions = rpc.Decide();

                var characterAction = characterActions.FirstOrDefault();

                if (characterAction.Key.ToString() == IATConsts.DIALOG_ACTION_KEY)
                {
                    var dialog = iat.GetDialogueActions(
                        IATConsts.AGENT,
                        characterAction.Parameters[0],
                        characterAction.Parameters[1],
                        characterAction.Parameters[2],
                        characterAction.Parameters[3]).FirstOrDefault();
                    Console.WriteLine("\n" + rpc.CharacterName + ": " + dialog.Utterance + "\n");
                    currentState = characterAction.Parameters[1].ToString();
                    Console.WriteLine("\nMood: " + rpc.Mood);
                   // rpc.SaveToFile("../../../ Examples/IATTest.iat");
                }
                else
                {
                    Console.WriteLine("\n" + rpc.CharacterName + ": " + characterAction.Name + "\n");
                }
            }
            Console.WriteLine("Dialogue Reached a Terminal State");
            Console.ReadKey();
        }
    }
}

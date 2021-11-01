using RolePlayCharacter;
using IntegratedAuthoringTool;
using GAIPS.Rage;
using System;
using System.Linq;
using System.IO;
using WellFormedNames;

namespace IntegratedAuthoringToolTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerStr = IATConsts.PLAYER;

            var storage = AssetStorage.FromJson(File.ReadAllText("../../../../Examples/IAT-Tutorial/Scenarios/kbstorage.json"));
             
            //Loading the asset
            var iat = IntegratedAuthoringToolAsset.FromJson(File.ReadAllText("../../../../Examples/IAT-Tutorial/Scenarios/kbscenario.json"), storage);
            
            var currentState = IATConsts.INITIAL_DIALOGUE_STATE;
            //  var rpc = iat.Characters.ElementAt(0);

            /*iat.commonSense.Tell((Name)"is(Animal)", (Name)"Entity");

          
            foreach (var b in beliefs)
                Console.WriteLine("IAT Common Sense Belief:" + b.Name + "= " + b.Value);

            iat.SetCulture((Name)"Default", iat.Characters.ElementAt(0).CharacterName);*/

            var rpc = new RolePlayCharacterAsset();
            rpc.CharacterName = (Name)"Peter";
            rpc.m_kb.Tell((Name)"is(Orange, fruit)", (Name)"True", rpc.CharacterName);
            var beliefs = rpc.m_kb.GetAllBeliefs();
            foreach (var b in beliefs)
                Console.WriteLine("Character Beliefs:" + b.Name + "= " + b.Value);

            rpc.m_kb.Reason();
            Console.WriteLine("After reasoning");
            beliefs = rpc.m_kb.GetAllBeliefs();
            foreach (var b in beliefs)
                Console.WriteLine("Character Beliefs:" + b.Name + "= " + b.Value);

            while (currentState != IATConsts.TERMINAL_DIALOGUE_STATE)
            {
                var playerDialogs = iat.GetDialogueActionsByState(currentState);

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

                var actionName = iat.BuildSpeakActionName(chosenDialog.Id);
                var speakEvt = EventHelper.ActionEnd(playerStr, actionName.ToString(), rpc.CharacterName.ToString());

                currentState = chosenDialog.NextState;
                var dialogStateChangeEvt = EventHelper.PropertyChange(string.Format(IATConsts.DIALOGUE_STATE_PROPERTY, playerStr), chosenDialog.NextState, playerStr);

                rpc.Perceive(speakEvt);
                rpc.Perceive(dialogStateChangeEvt);
                var characterActions = rpc.Decide();

                var characterAction = characterActions.FirstOrDefault();

                if (characterAction.Key.ToString() == IATConsts.DIALOG_ACTION_KEY)
                {
                    var dialog = iat.GetDialogueActions(
                        characterAction.Parameters[0],
                        characterAction.Parameters[1],
                        characterAction.Parameters[2],
                        characterAction.Parameters[3]).FirstOrDefault();
                    Console.WriteLine("\n" + rpc.CharacterName + ": " + dialog.Utterance + "\n");
                    currentState = characterAction.Parameters[1].ToString();
                    Console.WriteLine("\nMood: " + rpc.Mood);

                }
                else
                {
                    Console.WriteLine("\n" + rpc.CharacterName + ": " + characterAction.Name + "\n");
                }
            }


            // The next step in this tutorrial is to start using the World Model to cause effects in the belief state of the characters

            Console.WriteLine("Dialogue Reached a Terminal State");
            Console.ReadKey();
        }
    }
}

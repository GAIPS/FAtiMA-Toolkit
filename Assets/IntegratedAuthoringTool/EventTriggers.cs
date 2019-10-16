using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RolePlayCharacter;
using Utilities;
using WellFormedNames;

namespace IntegratedAuthoringTool
{
    public class EventTriggers
    {
        public static Dictionary<Name, string> _EventTriggerVariables = new Dictionary<Name, string>()
        {

            {
                Name.BuildName("Has(Floor)"), "Has(Floor)"
            },

            {
                Name.BuildName("DialogueState"), "DialogueState"
            }

        };

        public List<Name> ComputeTriggersList(List<RolePlayCharacter.RolePlayCharacterAsset> rpcs)
        {
            var retList = new List<Name>();

            foreach (var npc in rpcs)
            {
                var ev = NoDialoguesLeft(npc);

                if (ev != null)
                    retList.AddRange(ev);
            }

            return retList;
        }



        private List<Name> NoDialoguesLeft(RolePlayCharacter.RolePlayCharacterAsset rpc)
        {
            var decisions = rpc.Decide();

            var hasFloor = _EventTriggerVariables[(Name)"Has(Floor)"];

            var dialogueStates = rpc.GetAllBeliefs().ToList().FindAll(x => x.Name.ToString().Contains(_EventTriggerVariables[(Name) "DialogueState"]));

            List<string> dialogueStateTargets = new List<string>();
            List<string> dialogueStateValues = new List<string>();

            List<Name> noDialogueEvents = new List<Name>();

            foreach (var d in dialogueStates)
            {
                var belief = d.Name.ToString().Split('(', ')');

                dialogueStateTargets.Add(belief[1]);
                dialogueStateValues.Add(d.Value);


            }

            
            if (rpc.GetBeliefValue(hasFloor) != rpc.CharacterName.ToString())
                return null;

            if (!decisions.Any())
            {

                int i = 0;
                foreach (var target in dialogueStateTargets)
                {
                    
                   noDialogueEvents.Add(EventHelper.ActionEnd(target, "NoDialoguesLeft(" + target + "," + dialogueStateValues[i] + ")", rpc.CharacterName.ToString()));
                    i++;
                }

                return noDialogueEvents;
            }
              

      /*      var speakDecisions = decisions.Select(x => x.Key.ToString() == "Speak");

            if (speakDecisions.IsEmpty())

                return noDialoguesEvent;
                */
            return null;

        }
    }

}
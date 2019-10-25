using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ActionLibrary;
using IntegratedAuthoringTool;
using RolePlayCharacter;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelTutorial
{
    class Program
    {
        static List<RolePlayCharacterAsset> rpcList;

        static void Main(string[] args)
        {

            var iat = IntegratedAuthoringToolAsset.FromJson(File.ReadAllText("../../../Examples/CiF-Tutorial/JobInterview.iat"), new GAIPS.Rage.AssetStorage());
            rpcList = new List<RolePlayCharacterAsset>();
            var wm = new WorldModelAsset();

            wm.AddActionEffect((Name)"Event(Action-End, *, Speak(*,*,*,*), [t])", new EffectDTO()
            {
                PropertyName = (Name)"Has(Floor)",
                NewValue = (Name)"[t]",
                ObserverAgent = (Name)"*"
            });

            wm.AddActionEffect((Name)"Event(Action-End, [i], Speak([cs],[ns],*,*), SELF)", new EffectDTO()
            {
                PropertyName = (Name)"DialogueState([i])",
                NewValue = (Name)"[ns]",
                ObserverAgent = (Name)"[t]"
            });


            wm.AddActionEffect((Name)"Event(Action-End, SELF, Speak([cs],[ns],*,*), [t])", new EffectDTO()
            {
                PropertyName = (Name)"DialogueState([t])",
                NewValue = (Name)"[ns]",
                ObserverAgent = (Name)"[i]"
            });

            foreach (var actor in rpcList)
            {


                foreach (var anotherActor in rpcList)
                {
                    if (actor != anotherActor)
                    {


                        var changed = new[] { EventHelper.ActionEnd(anotherActor.CharacterName.ToString(), "Enters", "Room") };
                        actor.Perceive(changed);
                    }
                    
                }
            }



            List<Name> _events = new List<Name>();
            List<IAction> _actions = new List<IAction>();

            IAction action = null;
            while (true)
            {
                _actions.Clear();
                foreach (var rpc in rpcList)
                {
                    rpc.Tick++;

                    Console.WriteLine("Character perceiving: " + rpc.CharacterName + " its mood: " + rpc.Mood);

                    rpc.Perceive(_events);
                    var decisions = rpc.Decide();
                    _actions.Add(decisions.FirstOrDefault());

                    foreach (var act in decisions)
                    {
                        Console.WriteLine(rpc.CharacterName + " has this action: " + act.Name);
                        
                    }
                }

                _events.Clear();
             
                    var randomGen = new Random(Guid.NewGuid().GetHashCode());

                    var pos = randomGen.Next(rpcList.Count);
                
                  
                    var initiator = rpcList[pos];
                
               
                    action = _actions.ElementAt(pos);
              
                Console.WriteLine();
                if(action == null)
                Console.WriteLine(initiator.CharacterName + " does not have any action to do ");

                if (action != null)
                {

                    var initiatorName = initiator.CharacterName.ToString();
                    var targetName = action.Target.ToString();
                    var nextState = action.Parameters[1].ToString();
                    var currentState = action.Parameters[0].ToString();

                    Console.WriteLine("Action: " + initiatorName + " does " + action.Name + " to " +
                                     targetName + "\n" + action.Parameters[1]);

                    _events.Add(EventHelper.ActionEnd(initiatorName, action.Name.ToString(),
                        action.Target.ToString()));

                 

                    Console.WriteLine();
                    Console.WriteLine("Dialogue:");
                    Console.WriteLine("Current State: " + currentState);
                    if (iat.GetDialogueActions(action.Parameters[0], action.Parameters[1], action.Parameters[2], action.Parameters[3]).FirstOrDefault() != null)
                        Console.WriteLine(initiator.CharacterName + " says: ''" +
                                         iat.GetDialogueActions(action.Parameters[0], action.Parameters[1], action.Parameters[2], action.Parameters[3]).FirstOrDefault().Utterance + "'' to " + targetName);
                    else Console.WriteLine(initiator.CharacterName + " says: " + "there is no dialogue suited for this action");


                    // WORLD MODEL
                    var effects = wm.Simulate(_events.ToArray());
                    foreach (var ef in effects)
                    {

                        if (ef.ObserverAgent == (Name) "*")
                        {
                            foreach (var rpc in rpcList)
                            {
                                var proChange =
                                    EventHelper.PropertyChange(ef.PropertyName.ToString(), ef.NewValue.ToString(), "World");
                                rpc.Perceive(proChange);
                                
                            }
                        }

                        else
                        {
                            var proChange =
                                EventHelper.PropertyChange(ef.PropertyName.ToString(), ef.NewValue.ToString(), "World");
                            rpcList.Find(x=>x.CharacterName == ef.ObserverAgent).Perceive(proChange);
                        }

                    }
                    
                  Console.WriteLine("Next State: " + nextState);

                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}

    






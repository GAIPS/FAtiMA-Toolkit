using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActionLibrary;
using AssetManagerPackage;
using CommeillFaut.DTOs;
using CommeillFaut;
using Conditions.DTOs;
using IntegratedAuthoringTool;
using KnowledgeBase;
using Microsoft.CSharp.RuntimeBinder;
using RolePlayCharacter;
using WellFormedNames;

namespace CommeillFautTutorial
{
    class Program
    {
        static List<RolePlayCharacterAsset> rpcList;

        static void Main(string[] args)
        {
            AssetManager.Instance.Bridge = new BasicIOBridge();



            var iat = IntegratedAuthoringToolAsset.LoadFromFile("../../../Examples/cifIAT.iat");
            rpcList = new List<RolePlayCharacterAsset>();
            foreach (var source in iat.GetAllCharacterSources())
            {
                var rpc = RolePlayCharacterAsset.LoadFromFile(source.Source);
               rpc.LoadAssociatedAssets();
                iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
                rpcList.Add(rpc);

              
            }


           
            List<Name> _events = new List<Name>();
            List<IAction> _actions = new List<IAction>();

            while (true)
            {
                _actions.Clear();
                foreach (var rpc in rpcList)
                {
                   
                    rpc.Perceive(_events);
                    _actions.Add(rpc.Decide().FirstOrDefault());
                }

                _events.Clear();

                var randomGen = new Random();

                var pos = randomGen.Next(3);
                var initiator = rpcList.ElementAt(pos);
                var action = _actions.ElementAt(pos);





                if (action != null)
                {
                    Console.WriteLine("Character: " + initiator.CharacterName + " does " + action.Name + "to " +
                                      action.Target + "\n");

                    _events.Add(EventHelper.ActionEnd(initiator.CharacterName.ToString(), action.Name.ToString(),
                        action.Target.ToString()));

                    var Initiator_Events = new List<Name>();

                   
                     Initiator_Events.Add(EventHelper.PropertyChanged("DialogueState(" + action.Target.ToString() + ")",
                            action.Parameters[1].ToString(), initiator.CharacterName.ToString()));

                    Initiator_Events.Add(EventHelper.PropertyChanged("HasFloor(" + initiator.CharacterName + ")",
                        "false",
                        action.Target.ToString()));

                    initiator.Perceive(Initiator_Events);
                    





                    Console.WriteLine("Current State: " + action.Parameters[0].ToString());
                    Console.WriteLine(initiator.CharacterName + " says: " +
                                      iat.GetDialogueAction(IATConsts.PLAYER, action.Parameters[0],action.Parameters[1], action.Parameters[2], action.Parameters[3]).Utterance + " to " + action.Target);
                    Console.WriteLine("Next State: " + action.Parameters[1].ToString());


                    var replier = rpcList.Find(x => x.CharacterName == action.Target);


                    if (action.Parameters[1].ToString() != "Start")
                    {

                        var targetEvents = new List<Name>();
                        targetEvents.Add(EventHelper.PropertyChanged("DialogueState(" + initiator.CharacterName.ToString() + ")",
                                action.Parameters[1].ToString(), action.Target.ToString()));

                        targetEvents.Add(EventHelper.PropertyChanged("HasFloor(" + action.Target.ToString() + ")",
                            "true",
                            action.Target.ToString()));

                        rpcList.Find(x => x.CharacterName == action.Target).Perceive(targetEvents);

                    }

                    else
                    {
                        var targetEvents = new List<Name>();

                       targetEvents.Add(EventHelper.PropertyChanged("DialogueState(" + initiator.CharacterName.ToString() + ")",
                                action.Parameters[1].ToString(), action.Target.ToString()));
                        targetEvents.Add(EventHelper.PropertyChanged("HasFloor(" + action.Target.ToString() + ")",
                            "false",
                            action.Target.ToString()));

                        rpcList.Find(x=>x.CharacterName == action.Target).Perceive(targetEvents);
                        var rand = randomGen.Next(3);
                        //     Console.WriteLine(rand + "");

                        var next = rpcList.ElementAt(rand);

                        Console.WriteLine("next: " + next.CharacterName + " index: " + rand);

                        var finalEvents = new List<Name>();

                        finalEvents.Add( EventHelper.PropertyChanged("HasFloor(" + next.CharacterName + ")", "true",
                            "Random"));
                   
                       
                         next.Perceive(finalEvents);

                    }







                    Console.WriteLine();



                    //    actor.PerceptionActionLoop(new[] { _event });

                    foreach (var rpc in rpcList)
                    {
                        rpc.SaveToFile("../../../Examples/" + rpc.CharacterName + "-output" + ".rpc");
                    }


                }
                else Console.WriteLine("Character" + initiator.CharacterName + " does null \n");

                Console.ReadKey();
            }
        }
    }
}





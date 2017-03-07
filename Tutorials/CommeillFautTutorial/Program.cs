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
using ActionLibrary.DTOs;
using AssetManagerPackage;
using CommeillFaut.DTOs;
using CommeillFaut;
using Conditions.DTOs;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
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
               
              
                //rpc.DynamicPropertiesRegistry.RegistDynamicProperty(Name.BuildName("Volition"),cif.VolitionPropertyCalculator);
                    rpc.LoadAssociatedAssets();
                iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

                rpcList.Add(rpc);


            }
            var cif = CommeillFautAsset.LoadFromFile(rpcList.First().CommeillFautAssetSource);

            foreach (var actor in rpcList)
            {

              
                foreach (var anotherActor in rpcList)
                {
                    if (actor != anotherActor)
                    {


                        var changed = new[] {EventHelper.ActionEnd(anotherActor.CharacterName.ToString(), "Enters", "Room")};
                        actor.Perceive(changed);
                    }

                       

                          
                    }

          //      actor.SaveToFile("../../../Examples/" + actor.CharacterName + "-output" + ".rpc");
            }

            foreach (var go in iat.GetDialogueActionsByState("Player", "Start"))
            {
                Console.WriteLine(go.Utterance);
            }
           
           
            List<Name> _events = new List<Name>();
            List<IAction> _actions = new List<IAction>();
            var currentSocialMoveAction = "";
            var currentSocialMoveResult = "";

            while (true)
            {
                _actions.Clear();
                foreach (var rpc in rpcList)
                {

                    Console.WriteLine("Character deciding: "+ rpc.CharacterName);

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

                    _events.Add(EventHelper.ActionStart(initiator.CharacterName.ToString(), action.Name.ToString(),
                     action.Target.ToString()));

                    _events.Add(EventHelper.ActionEnd(initiator.CharacterName.ToString(), action.Name.ToString(),
                        action.Target.ToString()));

                    var Initiator_Events = new List<Name>();

                   
                     Initiator_Events.Add(EventHelper.PropertyChange("DialogueState(" + action.Target.ToString() + ")",
                            action.Parameters[1].ToString(), initiator.CharacterName.ToString()));

                    Initiator_Events.Add(EventHelper.PropertyChange("HasFloor(" + initiator.CharacterName + ")",
                        "false",
                        action.Target.ToString()));

                    initiator.Perceive(Initiator_Events);
                 

                    // storing data to apply consequences

                    if (action.Parameters[0].ToString().Contains("Start"))
                    {
                        currentSocialMoveAction = action.Parameters[0].ToString().Replace("Start", "");

                        Console.WriteLine("Started " + currentSocialMoveAction);
                    }

                    if (action.Parameters[0].ToString().Contains("Respond"))
                    {
                        currentSocialMoveResult = action.Parameters[2].ToString();

                        Console.WriteLine("Result " + currentSocialMoveResult);
                    }



                    Console.WriteLine("Current State: " + action.Parameters[0].ToString());
                    //Console.WriteLine(initiator.CharacterName + " says: ''" +
                    //                  iat.GetDialogueAction(IATConsts.PLAYER, action.Parameters[0],action.Parameters[1], action.Parameters[2], action.Parameters[3]).Utterance + "'' to " + action.Target);
                    Console.WriteLine("Next State: " + action.Parameters[1].ToString());


                    var replier = rpcList.Find(x => x.CharacterName == action.Target);


                    if (action.Parameters[1].ToString() != "Start")
                    {

                        var targetEvents = new List<Name>();
                        targetEvents.Add(EventHelper.PropertyChange("DialogueState(" + initiator.CharacterName.ToString() + ")",
                                action.Parameters[1].ToString(), action.Target.ToString()));

                        targetEvents.Add(EventHelper.PropertyChange("HasFloor(" + action.Target.ToString() + ")",
                            "true",
                            action.Target.ToString()));

                        rpcList.Find(x => x.CharacterName == action.Target).Perceive(targetEvents);

                    }

                    else
                    {
                        var targetEvents = new List<Name>();

                       targetEvents.Add(EventHelper.PropertyChange("DialogueState(" + initiator.CharacterName.ToString() + ")",
                                action.Parameters[1].ToString(), action.Target.ToString()));
                        targetEvents.Add(EventHelper.PropertyChange("HasFloor(" + action.Target.ToString() + ")",
                            "false",
                            action.Target.ToString()));

                        rpcList.Find(x => x.CharacterName == action.Target).Perceive(targetEvents);



                        // Apply consequences

                        Console.WriteLine("Social Exchange Finished, applying effects");

                        var target = rpcList.Find(x => x.CharacterName == action.Target);

                        //   var initiatorCIF = CommeillFautAsset.LoadFromFile(initiator.CommeillFautAssetSource);
                        //   var initiatorEA = EmotionalAppraisalAsset.LoadFromFile(initiator.EmotionalAppraisalAssetSource);

                        var _socialExchange = cif.m_SocialExchanges.Find(x => x.ActionName.ToString() == currentSocialMoveAction);

                        //_socialExchange.ApplyConsequences(initiator.m_kb, target.CharacterName, currentSocialMoveResult,true);
                        //_socialExchange.ApplyConsequences(target.m_kb, initiator.CharacterName, currentSocialMoveResult, false);

                        currentSocialMoveAction = "";
                        currentSocialMoveResult = "";


                        var rand = randomGen.Next(3);
                        //     Console.WriteLine(rand + "");

                        var next = rpcList.ElementAt(rand);

                        Console.WriteLine("next: " + next.CharacterName + " index: " + rand);

                        var finalEvents = new List<Name>();

                        finalEvents.Add( EventHelper.PropertyChange("HasFloor(" + next.CharacterName + ")", "true",
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





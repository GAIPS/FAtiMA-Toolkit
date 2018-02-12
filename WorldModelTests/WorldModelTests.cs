using RolePlayCharacter;
using WellFormedNames;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Conditions;
using SerializationUtilities;
using ActionLibrary;
using ActionLibrary.DTOs;
using KnowledgeBase;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelTests
{
    [TestFixture]
    public class WorldModelTests
    {
        private static WorldModelAsset _wm = BuildWorldModelAsset();

        private static WorldModelAsset BuildWorldModelAsset()
        {
            var wm = new WorldModelAsset();

            return wm;
        }

        private void AddEffects(WorldModelAsset wm)
        {

            wm.AddEventEffect(EventHelper.ActionEnd("John", "Speak([x],[y],*,*)", "Sarah"), new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});
            wm.AddEventEffect(EventHelper.ActionEnd("John", "Shoot", "Sarah"), new EffectDTO(){ PropertyName = (Name)"DialogueState(Player)", NewValue = (Name)"Start"});
            wm.AddEventEffect((Name)"Event(Action-End , [s], *, John)", new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"False"});
            wm.AddEventEffect((Name)"Event(Action-End , [s], Surf, [t])",new EffectDTO(){ PropertyName = (Name)"DialogueState([t])", NewValue = (Name)"[s]"});
        }


        [Test]
        public void Test_WM_AddingEffects()
        {

            var wm = BuildWorldModelAsset();

            wm.AddEventEffect(EventHelper.ActionEnd("John", "Speak(*,*,*,*)", "Sarah"), new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});

            Assert.AreEqual(1, wm.GetAllEventEffects().Count);

        }

        [TestCase("Event(Action-End, John, Speak(Start, S1, - , -), Sarah) | Event(Action-End, John, Shoot, Sarah)", 2)]
        [TestCase("Event(Action-End, John, Shoot, Sarah)", 1)]
        [TestCase("Event(Action-End, Sarah, Speak(Start, S1, - , -), John)", 1)]
        public void Test_WM_Evaluate_Match(string happenedEvents, int effectsNumber)
        {

            var wm = BuildWorldModelAsset();
            AddEffects(wm);

            var pastEvents = happenedEvents.Split('|');
            List<Name> toSimulate = new List<Name>(); 
            foreach (var evt in pastEvents)
            {
                toSimulate.Add((Name)evt);
            }

           var effects = wm.Simulate(toSimulate);

            Assert.AreEqual(effects.Count(), effectsNumber);

        }

        [TestCase("Event(Action-End, John, Speak(Start, S1, - , -), Sarah)", "Event(Property-Change, World, Has(Floor), Player)")]
        [TestCase("Event(Action-End, John, Shoot, Sarah)", "Event(Property-Change, World, DialogueState(Player), Start)")]
        [TestCase("Event(Action-End, Sarah, Walk, John)", "Event(Property-Change, World, Has(Floor), False)")]
        [TestCase("Event(Action-End, Sarah, Surf, John)", "Event(Property-Change, World, DialogueState(John), Sarah)")]
        [TestCase("Event(Action-End, John, Surf, Sarah)", "Event(Property-Change, World, DialogueState(Sarah), John)")]
        public void Test_WM_Evaluate_Content_Match(string pastEvent, string propertyChangeEffect)
        {

            var wm = BuildWorldModelAsset();
            AddEffects(wm);

           
            List<Name> toSimulate = new List<Name>(); 
            toSimulate.Add((Name)pastEvent);
            
            var effects = wm.Simulate(toSimulate);

            Assert.AreEqual(effects.FirstOrDefault().ToString(), propertyChangeEffect);

        }
        
    }
}

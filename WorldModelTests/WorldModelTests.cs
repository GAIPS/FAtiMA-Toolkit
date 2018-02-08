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


        [Test]
        public void Test_WM_AddingEffects()
        {

            var wm = BuildWorldModelAsset();

            wm.AddEventEffect(EventHelper.ActionEnd("John", "Speak(*,*,*,*)", "Sarah"), new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});

            Assert.AreEqual(1, wm.GetAllEventEffects().Count);

        }

        [TestCase("Event(Action-End, John, Speak(Start, S1, - , -), Sarah) | Event(Action-End, John, Shoot, Sarah)")]
        public void Test_WM_Evaluate_Match(string happenedEvents)
        {

            var wm = BuildWorldModelAsset();

            var pastEvents = happenedEvents.Split('|');
            List<Name> toSimulate = new List<Name>(); 
            foreach (var evt in pastEvents)
            {
                toSimulate.Add((Name)evt);
            }

            wm.AddEventEffect(EventHelper.ActionEnd("John", "Speak([x],[y],*,*)", "Sarah"), new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});

           var effects = wm.Simulate(toSimulate);

            Assert.AreEqual(effects.Count(), 1);

        }
        
    }
}

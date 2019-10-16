using RolePlayCharacter;
using WellFormedNames;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WorldModel;
using WorldModel.DTOs;
using SerializationUtilities;

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
            var a1 = EventHelper.ActionEnd("John", "Speak([x],[y],*,*)", "Sarah");
            var a2 = EventHelper.ActionEnd("John", "Shoot", "Sarah");
            var a3 = (Name)"Event(Action-End , [s], *, John)";
            var a4 = (Name)"Event(Action-End , [s], Surf, [t])";

            wm.addActionTemplate(a1, 1);
            wm.addActionTemplate(a2, 1);
            wm.addActionTemplate(a3, 1);
            wm.addActionTemplate(a4, 1);

            wm.AddActionEffect(a1, new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});
            wm.AddActionEffect(a2, new EffectDTO(){ PropertyName = (Name)"DialogueState(Player)", NewValue = (Name)"Start"});
            wm.AddActionEffect(a3, new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"False"});
            wm.AddActionEffect(a4, new EffectDTO(){ PropertyName = (Name)"DialogueState([t])", NewValue = (Name)"[s]"});
        }


        [Test]
        public void Test_WM_AddingEffects()
        {
            var wm = BuildWorldModelAsset();
            var a1 = EventHelper.ActionEnd("John", "Speak(*,*,*,*)", "Sarah");
            wm.addActionTemplate(a1, 2);
            wm.AddActionEffect(a1, new EffectDTO(){ PropertyName = (Name)"Has(Floor)", NewValue = (Name)"Player"});
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

           var effects = wm.Simulate(toSimulate.ToArray());

            Assert.AreEqual(effects.Count(), effectsNumber);

        }

        [TestCase("Event(Action-End, John, Speak(Start, S1, - , -), Sarah)", "Event(Property-Change, John, Has(Floor), Player)")]
        [TestCase("Event(Action-End, John, Shoot, Sarah)", "Event(Property-Change, John, DialogueState(Player), Start)")]
        [TestCase("Event(Action-End, Sarah, Walk, John)", "Event(Property-Change, Sarah, Has(Floor), False)")]
        [TestCase("Event(Action-End, Sarah, Surf, John)", "Event(Property-Change, Sarah, DialogueState(John), Sarah)")]
        [TestCase("Event(Action-End, John, Surf, Sarah)", "Event(Property-Change, John, DialogueState(Sarah), John)")]
        public void Test_WM_Evaluate_Content_Match(string pastEvent, string propertyChangeEffect)
        {
            var wm = BuildWorldModelAsset();
            AddEffects(wm);

            List<Name> toSimulate = new List<Name>(); 
            toSimulate.Add((Name)pastEvent);
            
            var effect = wm.Simulate(toSimulate.ToArray()).FirstOrDefault();
            var pcEvent = EventHelper.PropertyChange(effect.PropertyName, effect.NewValue, ((Name)pastEvent).GetNTerm(2));
            Assert.AreEqual(pcEvent.ToString(), propertyChangeEffect);
        }

        [Test]
        public void Test_WM_Evaluate_MultipleEffects_Match()
        {
            var wm = BuildWorldModelAsset();
            
            var effectList = new List<EffectDTO>();

            effectList.Add(new EffectDTO()
            {
                NewValue = (Name)"[t]",
                PropertyName = (Name)"DialogueState([i])",
                ObserverAgent = (Name)"*"
            });

            effectList.Add(new EffectDTO()
            {
                NewValue = (Name)"[i]",
                PropertyName = (Name)"DialogueState([t])",
                ObserverAgent = (Name)"*"
            });

            var aTemplate = (Name) "Event(Action-End , [i], *, [t])";

            wm.addActionTemplate(aTemplate,1);
            wm.AddActionEffectsDTOs(aTemplate, effectList);

            var toSimulate = new List<Name> {(Name) "Event(Action-End, John, Speak(Start, S1, - , -), Sarah)"};

            var effectOne = wm.Simulate(toSimulate.ToArray()).ElementAt(1);
            var effectZero = wm.Simulate(toSimulate.ToArray()).ElementAt(0);

            Assert.AreEqual(EventHelper.PropertyChange(effectOne.PropertyName, effectOne.NewValue, (Name)"John").ToString(), 
               "Event(Property-Change, John, DialogueState(Sarah), John)");

            Assert.AreEqual(EventHelper.PropertyChange(effectZero.PropertyName, effectZero.NewValue, (Name)"John").ToString(),
                "Event(Property-Change, John, DialogueState(John), Sarah)");
           

        }


        
        [TestCase]
        public void WM_Serialization_Test()
        {
            var asset = BuildWorldModelAsset();

            using (var stream = new MemoryStream())
            {
                var formater = new JSONSerializer();
                formater.Serialize(stream, asset);
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(new StreamReader(stream).ReadToEnd());
            }
        }

        [TestCase]
        public void WM_Deserialization_Test()
        {
            var asset = BuildWorldModelAsset();

            using (var stream = new MemoryStream())
            {
                var formater = new JSONSerializer();
                formater.Serialize(stream, asset);
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(new StreamReader(stream).ReadToEnd());
                stream.Seek(0, SeekOrigin.Begin);
                var obj = formater.Deserialize(stream);
            }
        }

/*          [TestCase]
        public void WM_SaveToFile_Test()
        {
            var asset = BuildWorldModelAsset();
             
            this.AddEffects(asset);

            var dir = Directory.GetCurrentDirectory();
            asset.SaveToFile("C:/Users/Manue/Desktop/Test.wmo");

            var newwm = WorldModelAsset.LoadFromFile("C:/Users/Manue/Desktop/Test.wmo");

           Assert.IsNotNull(newwm);
        }*/
    }
}

using System;
using RolePlayCharacter;
using WellFormedNames;
using KnowledgeBase;
using NUnit.Framework;
using Conditions.DTOs;
using GAIPS.Rage;
using AutobiographicMemory.DTOs;
using AutobiographicMemory;

namespace Tests.AutobiographicMemory
{
    [TestFixture]
    public class AMTests
    {

        private static AM AutMemory = BuildAMAsset();

        private static AM BuildAMAsset()
        {
            var am = new AM()
            {
                Tick = 0,
            };
    
            return am;

        }

        private static RolePlayCharacterAsset RPC = BuildRPCAsset();

        private static RolePlayCharacterAsset BuildRPCAsset()
        {
            var kb = new KB((Name)"Matt");
    
         
            var rpc = new RolePlayCharacterAsset
            {
                BodyName = "Male",
                VoiceName = "Male",
                CharacterName = (Name)"Matt",
                m_kb = kb,
            };

            rpc.LoadAssociatedAssets();
            return rpc;

        }


        [Test]
        public void TestAMInitialTick()
        {
            
            Assert.AreEqual(0, AutMemory.Tick);
        }


        [TestCase(3, ExpectedResult = 3)]
        [TestCase(5, ExpectedResult = 5)]
        [TestCase(10, ExpectedResult = 10)]
        [Test]
        public ulong TestAMMultipleTicks(int eventNumber)
        {
          
            int i = 0;
            for(i = 0; i < eventNumber - 1; i++)
                AutMemory.RecordEvent(new EventDTO());

            return AutMemory.Tick;
        }


   /*     [Test]
        public void TestLastEventID()
        {

            KnowledgeBase.DTOs.DynamicPropertyDTO dynamicProperty = new KnowledgeBase.DTOs.DynamicPropertyDTO();
            
            foreach (var r in RPC.DynamicPropertiesRegistry.GetDynamicProperties())
                if (r.PropertyTemplate.ToString().Contains("LastEventID"))
                    dynamicProperty = r;

            AutMemory.
            AutMemory.RecordEvent(new EventDTO() { Event = EventHelper.ActionEnd("Matt", "talked", "John").ToString(), Id = 0, Subject = "Matt", Time = 0 });
            */
            

        }

    }
};
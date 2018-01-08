using System;
using RolePlayCharacter;
using WellFormedNames;
using KnowledgeBase;
using NUnit.Framework;
using Conditions.DTOs;
using GAIPS.Rage;
using AutobiographicMemory.DTOs;
using AutobiographicMemory;
using Conditions;

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


        [TestCase("Enter", "Sarah", "John", "LastEventId(Action-End, [x], Speak(*, *, SE([se], Initiate), Positive), SELF) !=null")]
        [TestCase("Speak(*, *, SE(Flirt, Initiate), Positive)", "Sarah", "John", "LastEventId(Action-End, [x], Speak(*, *, SE([se], Initiate), Positive), SELF) !=null")]
        [Test]
        public void Test_DP_LastEventID_NoMatch(string actionInMemory, string actionInMemorySubject, string actionInMemoryTarget, string lastEventMethod)
        {
            var rpc = BuildRPCAsset();


            rpc.Perceive(EventHelper.ActionEnd(actionInMemorySubject, actionInMemory, actionInMemoryTarget));


            var condSet = new ConditionSet();
            var cond = Condition.Parse(lastEventMethod);

            condSet = condSet.Add(cond);

           var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL,null);

           Assert.IsEmpty(result);

       }

        [TestCase("Enter", "Matt", "Matt", "LastEventId(Action-End, SELF , Enter, SELF) !=null")]
        [Test]
        public void Test_DP_LastEventID_Match(string actionInMemory, string actionInMemorySubject, string actionInMemoryTarget, string lastEventMethod)
        {
            var rpc = BuildRPCAsset();


            rpc.Perceive(EventHelper.ActionEnd(actionInMemorySubject, actionInMemory, actionInMemoryTarget));


            var condSet = new ConditionSet();
            var cond = Condition.Parse(lastEventMethod);

            condSet = condSet.Add(cond);

            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);

            Assert.IsNotEmpty(result);

        }

    }
};
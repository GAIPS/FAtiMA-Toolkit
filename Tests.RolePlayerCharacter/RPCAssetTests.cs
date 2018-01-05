using System;
using RolePlayCharacter;
using WellFormedNames;
using KnowledgeBase;
using NUnit.Framework;
using Conditions.DTOs;
using GAIPS.Rage;


namespace Tests.RolePlayCharacter
{
    [TestFixture]
    public class RPCAssetTests
    {

        private static RolePlayCharacterAsset ASSET_TO_TEST = BuildAsset();

        private static RolePlayCharacterAsset BuildAsset()
        {
            var kb = new KB((Name)"Matt");
            /*  #region Set KB
              kb.Tell((Name)"IsPerson(Matt)", (Name)"true", (Name)"*");
              kb.Tell((Name)"IsPerson(Mary)", (Name)"true", (Name)"*");
              kb.Tell((Name)"IsPerson(Robot)", (Name)"true", (Name)"Diego");
              kb.Tell((Name)"IsOutsider(Diego)", (Name)"true", (Name)"*");
              kb.Tell((Name)"IsOutsider(Diego)", (Name)"false", (Name)"Robot");
              kb.Tell((Name)"IsFriends(Matt,Mary)", (Name)"true", (Name)"SELF");
              kb.Tell((Name)"IsFriends(Matt,Diego)", (Name)"false", (Name)"SELF");
              */
            #region RPC especification
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

        #endregion

        [Test]
        public void TestCharacterName()
        {
            var r = BuildAsset();
            Assert.AreEqual("Matt", r.CharacterName.ToString());
        }

        [Test]
        public void TestBodyName()
        {
            var r = BuildAsset();
            Assert.AreEqual("Male", r.BodyName.ToString());
        }

        [Test]
        public void TestVoiceName()
        {
            var r = BuildAsset();
            Assert.AreEqual("Male", r.VoiceName.ToString());
        }

        [TestCase("Diego", "Speak(Start, S1, Positive, -)", "Matt", ExpectedResult = true)]
        [Test]
        public bool TestEventRecording(string subject, string evt, string target)
        {
            var rpc = BuildAsset();

            var eve = EventHelper.ActionEnd(subject, evt, target);

            rpc.Perceive(eve);

           var records = rpc.EventRecords;

            bool ret = false;
            foreach (var r in records)
                if (r.Event.ToString() == eve.ToString())
                    ret = true;

            return ret;
            

        }
    }
};


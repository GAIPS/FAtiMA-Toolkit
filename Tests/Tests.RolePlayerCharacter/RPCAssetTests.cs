using RolePlayCharacter;
using WellFormedNames;
using System.IO;
using System;
using KnowledgeBase;
using System.Collections.Generic;
using NUnit.Framework;
using Conditions;
using SerializationUtilities;
using EmotionalAppraisal;

namespace Tests.RolePlayCharacter
{
    [TestFixture]
    public class RPCAssetTests
    {
        private Dictionary<int, List<string>> eventSets;

        private void PopulateEventSet(int set)
        {
            eventSets = new Dictionary<int, List<string>>();
            var eventList = new List<string>();

            if (set == 1)
            {
                eventList = new List<string>()
                {
                EventHelper.ActionEnd("Matt", "EntersRoom", "Sarah").ToString(),
                EventHelper.ActionEnd("Sarah", "EntersRoom", "Matt").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, -, -)", "Sarah").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, -, Polite)", "Sarah").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, Silly, Polite)", "Sarah").ToString(),
                EventHelper.PropertyChange("Has(Floor)", "Sarah", "Matt").ToString(),
                //THIS SHOULD BE THE LAST EVENT
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, SE(Flirt, Initiate), Positive)", "Sarah").ToString()

            };

            }


            eventSets.Add(set, eventList);



        }

        private static RolePlayCharacterAsset RPC = BuildRPCAsset();

        private static RolePlayCharacterAsset BuildRPCAsset()
        {
            var kb = new KB((Name)"Matt");


            var ea = new EmotionalAppraisalAsset();

            var appraisalRule = new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                Desirability = Name.BuildName(2),
                Praiseworthiness = Name.BuildName(2)
            };

            ea.AddOrUpdateAppraisalRule(appraisalRule);
           

            var rpc = new RolePlayCharacterAsset
            {
                BodyName = "Male",
                VoiceName = "Male",
                CharacterName = (Name)"Matt",
                m_kb = kb,
                
            };

            ea.SaveToFile("Tests/UnitTestAuxEA");

            var path = ea.AssetFilePath;
            rpc.EmotionalAppraisalAssetSource = path;
            rpc.LoadAssociatedAssets();
            return rpc;

        }

        [Test]
        public void TestCharacterName()
        {
            var r = BuildRPCAsset();
            Assert.AreEqual("Matt", r.CharacterName.ToString());
        }

        [Test]
        public void TestBodyName()
        {
            var r = BuildRPCAsset();
            Assert.AreEqual("Male", r.BodyName.ToString());
        }

        [Test]
        public void TestVoiceName()
        {
            var r = BuildRPCAsset();
            Assert.AreEqual("Male", r.VoiceName.ToString());
        }

        [TestCase("Diego", "Speak(Start, S1, Positive, -)", "Matt", ExpectedResult = true)]
        [Test]
        public bool TestEventRecording(string subject, string evt, string target)
        {
            var rpc = BuildRPCAsset();

            var eve = EventHelper.ActionEnd(subject, evt, target);

            rpc.Perceive(eve);

            var records = rpc.EventRecords;

            bool ret = false;
            foreach (var r in records)
                if (r.Event.ToString() == eve.ToString())
                    ret = true;

            return ret;


        }
        #region Test RPC Dynamic Properties
        [TestCase(1, "[x] = Sarah", "isAgent([x])=False")]
        [TestCase(1, "[x] = Matt", "isAgent([x])=False")]
        [TestCase(1, "[x] = Sarah", "isAgent(EntersRoom)=False")]
        [TestCase(1, "", "isAgent(False)=True")]
        [Test]
        public void Test_DP_isAgent_NoMatch(int eventSet, string context, string lastEventMethodCall)
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');

                 
                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsEmpty(result);
        }

        [TestCase(1, "", "isAgent([x])=True")]
        [TestCase(1, "", "isAgent(Sarah)=True")]
        [TestCase(1, "", "isAgent(Matt)=True")]
        [TestCase(1, "[x] = Sarah", "isAgent([x])=True")]
        [Test]
        public void Test_DP_isAgent_Match(int eventSet, string context, string lastEventMethodCall)
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);
        }

        [TestCase(1, "isAgent([x])=True", "Mood(Matt) = [m]")]
        [TestCase(1, "", "Mood(SELF) = [m]")]
        [TestCase(1, "isAgent(Matt)=True", "Mood([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "Mood([x]) = 0")]
        [TestCase(1, "isAgent([x])=True", "Mood([x]) < 5")]
        [TestCase(1, "isAgent([x])=True, [m] < 5", "Mood(Matt) = [m]")]
        [TestCase(1, "isAgent([x])=True, [m] < 5", "Mood([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True, [m] < 5", "Mood(SELF) = [m]")]

        public void Test_DP_Mood_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }

        [TestCase(1, "isAgent([x])=True", "StrongestAttributionEmotion([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestAttributionEmotion(Matt) = [m]")]
        public void Test_DP_StrongestAttributionEmotion_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }

        [TestCase(1, "isAgent([x])=True", "StrongestEmotion([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestEmotion(Matt) = [m]")]
        public void Test_DP_StrongestEmotion_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }

        [TestCase(1, "isAgent([x])=True", "StrongestEmotionForEvent([x], *) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestEmotionForEvent([x], [y]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestEmotionForEvent(Matt, [y]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestEmotionForEvent([x], Event(Action-End, Matt, EntersRoom, Sarah)) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestEmotionForEvent(Matt, Event(Action-End, Matt, EntersRoom, Sarah)) = [m]")]
        public void Test_DP_StrongestEmotionForEvent_Match(int eventSet, string context, string lastEventMethodCall)   // TO DO
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }

        [TestCase(1, "isAgent([x])=True", "StrongestWellBeingEmotion([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestWellBeingEmotion(Matt) = [m]")]
        public void Test_DP_StrongestWellBeingEmotion_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }


        [TestCase(1, "isAgent([x])=True", "StrongestCompoundEmotion([x]) = [m]")]
        [TestCase(1, "isAgent([x])=True", "StrongestCompoundEmotion(Matt) = [m]")]
        public void Test_DP_StrongestCompoundEmotion_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }


        [TestCase(1, "isAgent([x])=True", "EmotionIntensity([x], Gratification) = [m]")]
        [TestCase(1, "isAgent([x])=True", "EmotionIntensity(Matt, Gratification) = [m]")]
        [TestCase(1, "isAgent([x])=True", "EmotionIntensity([x], [y]) = [m]")]
        public void Test_DP_EmotionIntensity_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Initializing
            var condSet = new ConditionSet();
            var cond = Condition.Parse("[x] = True");
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (context != "")
            {
                var conditions = context.Split(',');


                cond = Condition.Parse(conditions[0]);

                // Apply conditions to RPC
                foreach (var res in conditions)
                {
                    cond = Condition.Parse(res);
                    condSet = condSet.Add(cond);


                }
                resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);
            }

            condSet = new ConditionSet();
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }
        #endregion



        #region Serialization Tests



        [TestCase]
        public void RPC_Serialization_Test()
        {
            var asset = BuildRPCAsset();

            using (var stream = new MemoryStream())
            {
                var formater = new JSONSerializer();
                formater.Serialize(stream, asset);
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(new StreamReader(stream).ReadToEnd());
            }
        }

        [TestCase]
        public void RPC_Deserialization_Test()
        {
            var asset = BuildRPCAsset();

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

        #endregion
    }
    };


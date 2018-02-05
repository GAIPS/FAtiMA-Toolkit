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
using EmotionalDecisionMaking;
using CommeillFaut;
using SocialImportance;
using ActionLibrary;

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

        private static RolePlayCharacterAsset BuildEmotionalRPCAsset()
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

            rpc.m_emotionalAppraisalAsset = ea;

            rpc.BindToRegistry(rpc.m_kb);



            EmotionalDecisionMakingAsset edm =  new EmotionalDecisionMakingAsset();
            SocialImportanceAsset si =  new SocialImportanceAsset();
            CommeillFautAsset cfa = new CommeillFautAsset();

             rpc.m_emotionalAppraisalAsset = ea;
            rpc.m_emotionalDecisionMakingAsset = edm;
            rpc.m_socialImportanceAsset = si;
            rpc.m_commeillFautAsset = cfa;
            return rpc;

        }

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
        public void Test_rpc_CharacterName()
        {
            var r = BuildRPCAsset();
            Assert.AreEqual("Matt", r.CharacterName.ToString());
        }

        [Test]
        public void Test_RPC_BodyName()
        {
            var r = BuildRPCAsset();
            Assert.AreEqual("Male", r.BodyName.ToString());
        }

        [Test]
        public void Test_RPC_VoiceName()
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


        [TestCase]
        public void Test_RPC_AssetSources()
        {

            var kb = new KB((Name)"Matt");

            var rpc = new RolePlayCharacterAsset
            {
                BodyName = "Male",
                VoiceName = "Male",
                CharacterName = (Name)"Matt",
                m_kb = kb,

            };

            var edmPath = "Tests/EmptyEDM";

            var cifPath = "Tests/EmptyCIF";

            var siPath = "Tests/EmtpySI";

            var eaPath = "Tests/EmptyEA";

            rpc.CommeillFautAssetSource = cifPath;
            rpc.EmotionalDecisionMakingSource = edmPath;
            rpc.SocialImportanceAssetSource = siPath;
            rpc.EmotionalAppraisalAssetSource = eaPath;

            Assert.IsNotNull(rpc.DynamicPropertiesRegistry);
            Assert.IsNotEmpty(rpc.CommeillFautAssetSource);
            Assert.IsNotEmpty(rpc.EmotionalDecisionMakingSource);
            Assert.IsNotEmpty(rpc.SocialImportanceAssetSource);
            Assert.IsNotEmpty(rpc.EmotionalAppraisalAssetSource);
        }

        [TestCase]
        public void Test_RPC_Decide()
        {

            var kb = new KB((Name)"Matt");

            var rpc = BuildEmotionalRPCAsset();

            var edm = new EmotionalDecisionMakingAsset();

            edm.AddActionRule(new ActionLibrary.DTOs.ActionRuleDTO() { Action = (Name)"EnterRoom", Priority = Name.BuildName(3), Target = (Name)"[x]", Conditions = new Conditions.DTOs.ConditionSetDTO() { ConditionSet = new string[] { "[x]!=SELF" } } });

            rpc.m_emotionalDecisionMakingAsset = edm;

            edm.RegisterKnowledgeBase(rpc.m_kb);
            
            PopulateEventSet(1);

            foreach (var eve in eventSets[1])
            {
                rpc.Perceive((Name)eve);
                rpc.Update();
            }

            var actions = rpc.Decide();

            Assert.IsNotNull(actions);
        }


        [TestCase("Dialogue(State)", "Start")]
        [TestCase("Dialogue(Matt)", "0")]
        public void Test_RPC_BeliefValue(string belief, string value)
        {
            var rpc = BuildRPCAsset();

            rpc.m_kb.Tell((Name)belief, (Name)value);

            var beliefs = rpc.GetAllBeliefs();

            Assert.AreEqual(value, rpc.GetBeliefValue(belief));

            Assert.IsNotEmpty(rpc.GetAllBeliefs());

            rpc.RemoveBelief(belief, "SELF");


            Assert.IsEmpty(rpc.GetAllBeliefs());

        }

        [TestCase("Dialogue(State)", "Start")]
        [TestCase("Dialogue(Matt)", "0")]
        public void Test_RPC_UpdateBeliefValue(string belief, string value)
        {
            var rpc = BuildRPCAsset();

            rpc.m_kb.Tell((Name)belief, (Name)value);

            var beliefs = rpc.GetAllBeliefs();

            Assert.AreEqual(value, rpc.GetBeliefValue(belief));

            Assert.IsNotEmpty(rpc.GetAllBeliefs());

            rpc.Update();

            var originalValue = rpc.GetBeliefValue(belief);

            rpc.UpdateBelief(belief, "newvalue");

            Assert.AreEqual("newvalue", rpc.GetBeliefValue(belief));


        }


        [TestCase("Dialogue(State)", "Start", "0.4")]
        [TestCase("Dialogue(Matt)", "0", "1.0")]
        public void Test_RPC_BeliefValueAnCertainty(string belief, string value, string certainty)
        {
            var rpc = BuildRPCAsset();

            rpc.m_kb.Tell((Name)belief, (Name)value, (Name)"SELF", Convert.ToSingle(certainty));

            var beliefs = rpc.GetAllBeliefs();

            Assert.AreEqual(Convert.ToSingle(certainty), rpc.GetBeliefValueAndCertainty(belief).Certainty);
        }





        [TestCase]
        public void Test_RPC_GetAllBeliefs()
        {
            var rpc = BuildRPCAsset();

            var originalbeliefs = rpc.GetAllBeliefs();

            int count = 0;
            foreach (var b in originalbeliefs)
                count++;


            rpc.m_kb.Tell((Name)"Love(Sarah)", (Name)"True");

            var afterbeliefs = rpc.GetAllBeliefs();

            int countafter = 0;
            foreach (var b in originalbeliefs)
                countafter++;


            Assert.AreEqual((count + 1), countafter);
        }



        [TestCase]
        public void Test_RPC_ActiveEmotions()
        {

            var rpc = BuildEmotionalRPCAsset();

            PopulateEventSet(1);

            var events = new List<Name>(); 

            foreach (var eve in eventSets[1])
            {
                events.Add((Name)eve);
            }

            rpc.Perceive(events);


            Assert.IsNotEmpty(rpc.GetAllActiveEmotions());


            var intensity = -1.0f;
            EmotionalAppraisal.DTOs.EmotionDTO maxEmotion = new EmotionalAppraisal.DTOs.EmotionDTO();
            foreach (var e in rpc.GetAllActiveEmotions())
            {
                if (e.Intensity > intensity)
                {
                    intensity = e.Intensity;
                    maxEmotion = e;
                }
            }


                Assert.AreEqual(maxEmotion.Type, rpc.GetStrongestActiveEmotion().EmotionType);
        }


        [TestCase]
        public void Test_RPC_ForgetEvent()
        {

            var rpc = BuildEmotionalRPCAsset();

            PopulateEventSet(1);

            var events = new List<Name>();

            foreach (var eve in eventSets[1])
            {
                events.Add((Name)eve);
            }

            rpc.Perceive(events);


            Assert.IsNotEmpty(rpc.EventRecords);


            int counter = 0;

            foreach (var eve in events)
            {
                rpc.ForgetEvent((uint)counter);
                counter++;
            }

            Assert.IsEmpty(rpc.EventRecords);

        }





        [TestCase]
        public void Test_RPC_SetMood()
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(1);

            var events = new List<Name>();

            foreach (var eve in eventSets[1])
            {
                events.Add((Name)eve);
            }

            rpc.Perceive(events);

            var originalMood = rpc.Mood;

            rpc.Mood = 3.0f;

            Assert.AreNotEqual(originalMood, rpc.Mood);
        }

        [TestCase]
        public void Test_RPC_GetEventDetails()
        {

            var rpc = BuildRPCAsset();
            PopulateEventSet(1);

            foreach (var eve in eventSets[1])
            {
                rpc.Perceive((Name)eve);
                rpc.Update();
            }

            uint counter = 0;
            foreach (var eve in eventSets[1])
            {
                Assert.IsNotNull(rpc.GetEventDetails(counter));
                counter++;
            }
        }



        #region Test RPC Dynamic Properties
        [TestCase(1, "[x] = Sarah", "isAgent([x])=False")]
        [TestCase(1, "[x] = Matt", "isAgent([x])=False")]
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
        [TestCase(1, "[result] = True", "isAgent(Sarah)=[result]")]
        [TestCase(1, "[result] = True", "isAgent(Matt)=[result]")]
        [TestCase(1, "[result] = False", "isAgent(True)=[result]")]
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

            var rpc = BuildEmotionalRPCAsset();
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

            var rpc = BuildEmotionalRPCAsset();
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

            var rpc = BuildEmotionalRPCAsset();
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

            var rpc = BuildEmotionalRPCAsset();
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

            var rpc = BuildEmotionalRPCAsset();
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


        [TestCase(1, "isAgent([x])=True", "EmotionIntensity([x], Gratitude) = [m]")]
        [TestCase(1, "isAgent([x])=True", "EmotionIntensity(Matt, Gratitude) = [m]")]
        [TestCase(1, "isAgent([x])=True", "EmotionIntensity([x], [y]) = [m]")]
        public void Test_DP_EmotionIntensity_Match(int eventSet, string context, string lastEventMethodCall)
        {

            var rpc = BuildEmotionalRPCAsset();
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


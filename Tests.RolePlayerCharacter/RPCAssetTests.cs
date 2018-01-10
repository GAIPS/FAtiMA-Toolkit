using System;
using RolePlayCharacter;
using WellFormedNames;
using KnowledgeBase;
using Conditions.DTOs;
using GAIPS.Rage;
using System.Collections.Generic;
using NUnit.Framework;
using Conditions;

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

            /*EmotionalAppraisal.EmotionalAppraisalAsset ea = new EmotionalAppraisal.EmotionalAppraisalAsset();

            var conditionDto = new ConditionSetDTO() { ConditionSet = new string[] { "[x] != True" } };
            ea.AddOrUpdateAppraisalRule(new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = conditionDto,
                EventMatchingTemplate = (Name)"Event(Action-End, *, Speak(*, *, *, Positive), *)",
                Desirability = Name.BuildName(7),
                Praiseworthiness = Name.BuildName(7),
            });

    */
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
        }
    };


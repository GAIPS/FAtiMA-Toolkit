using RolePlayCharacter;
using WellFormedNames;
using System.IO;
using System;
using KnowledgeBase;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Conditions;
using SerializationUtilities;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using CommeillFaut;
using SocialImportance;
using ActionLibrary;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;

namespace Tests.RolePlayCharacter
{
    [TestFixture]
    public class RPCAssetTests
    {
        private Dictionary<int, List<string>> eventSets;

        private static Dictionary<int, AppraisalRuleDTO> appraisalRuleSet;

        private static void PopulateAppraisalRuleSet()
        {

            appraisalRuleSet = new Dictionary<int, AppraisalRuleDTO>();

            appraisalRuleSet.Add(1, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5",
                   }                   
               })
            });


              appraisalRuleSet.Add(2, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"5",
                   }                   
               })
            });


              appraisalRuleSet.Add(3, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5",
                   }                   
               })
            });

              appraisalRuleSet.Add(4, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"-5",
                   }                   
               })
            });

               appraisalRuleSet.Add(5, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.LIKE,
                       Value = (Name)"5",
                   }                   
               })
            });

               appraisalRuleSet.Add(6, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.LIKE,
                       Value = (Name)"-5",
                   }                   
               })
            });

              appraisalRuleSet.Add(7, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5"
                      
                   }                   
               })
            });

            appraisalRuleSet.Add(8, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5"
                   }                   
               })
            });

            appraisalRuleSet.Add(9, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5",
                       Target = (Name)"SELF"
                   }                   
               })
            });

              appraisalRuleSet.Add(10, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5",
                       Target = (Name)"SELF"
                   }                   
               })
            });

             appraisalRuleSet.Add(11, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"0.7",
                       Target = (Name)"Goal"
                   }                   
               })
            
               });

               appraisalRuleSet.Add(12, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"0.3",
                       Target = (Name)"Goal"
                   }                   
               })
            
               });

             appraisalRuleSet.Add(13, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"1",
                       Target = (Name)"Goal"
                   }                   
               })
            
               });


             appraisalRuleSet.Add(14, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"0",
                       Target = (Name)"Goal"
                   }                   
               })
            
               });

             appraisalRuleSet.Add(15, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"5",
                       Target = (Name)"Sarah"
                   }                   
               })
            
               });


             appraisalRuleSet.Add(16, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"-5",
                       Target = (Name)"Sarah"
                   }                   
               })
            
               });

             appraisalRuleSet.Add(17, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"1",
                       Target = (Name)"GoalPositive"
                   }                   
               })
            
               });

             appraisalRuleSet.Add(18, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               
                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
                       Value = (Name)"1",
                       Target = (Name)"GoalNegative"
                   }                   
               })
            
               });

            appraisalRuleSet.Add(19, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()

                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5"
                   },

                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
                       Value = (Name)"5",
                       Target = (Name)"Other"

                   }

                    
               })

            });

            appraisalRuleSet.Add(20, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()

                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5"
                   },

                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
                       Value = (Name)"5",
                       Target = (Name)"Other"

                   }


               })

            });

            appraisalRuleSet.Add(21, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()

                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5"
                   },

                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
                       Value = (Name)"-5",
                       Target = (Name)"Other"

                   }
                

               })

            });

            appraisalRuleSet.Add(22, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()

                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5"
                   },

                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
                       Value = (Name)"-5",
                       Target = (Name)"Other"

                   }


               })

            });

            appraisalRuleSet.Add(23, new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()

                {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-5"
                   },

                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
                       Value = (Name)"-5",
                       Target = (Name)"Other"

                   },

                     new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"5",
                       Target = (Name)"Other"

                   }


               })

            });

        }




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

        private static RolePlayCharacterAsset BuildEmotionalRPCAsset(int eaSet = -1)
        {
            var kb = new KB((Name)"Matt");


            var ea = new EmotionalAppraisalAsset();


            if(eaSet == -1){
            var appraisalRule = new EmotionalAppraisal.DTOs.AppraisalRuleDTO()
            {
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                EventMatchingTemplate = (Name)"Event(*, *,*, *)",
              AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"2"
                   },
                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"2"
                   }
               })
            };

            ea.AddOrUpdateAppraisalRule(appraisalRule);
            }

            else{ 
                
                PopulateAppraisalRuleSet();
                ea.AddOrUpdateAppraisalRule( appraisalRuleSet[eaSet]);
            }

           
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


            rpc.LoadAssociatedAssets(new GAIPS.Rage.AssetStorage());
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

        [TestCase("Has(Floor) = Player", "Has(Floor)", 0)]
        [TestCase("Has(Floor) = Player, DialogueState(P) = Start", "Has(Floor)", 1)]
        [TestCase("Has(Floor) = Player, DialogueState(P) = Start", "Has(Floor), DialogueState(P)", 0)]
        public void Test_RPC_ForgetBeliefEvent(string toAdd, string toRemove, int total)
        {
            var rpc = BuildEmotionalRPCAsset();

            var add = toAdd.Split(',');
            foreach (var b in add)
            {
                var value = b.Split('=');

                var ev = EventHelper.PropertyChange(value[0], value[1], "WTV");

                rpc.Perceive(ev);
            }

            var rem = toRemove.Split(',');
            foreach (var b in rem)
            {
                var ev = EventHelper.PropertyChange(b, "-" , "WTV");

                rpc.Perceive(ev);
            }


            Assert.AreEqual(total, rpc.GetAllBeliefs().Count());

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


      //  [TestCase(1, "isAgent([x])=True", "StrongestCompoundEmotion([x]) = [m]")]
      //  [TestCase(1, "isAgent([x])=True", "StrongestCompoundEmotion(Matt) = [m]")]
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


        [TestCase(1, "isAgent([x])=True", "EmotionIntensity([x], Joy) = [m]")]
        [TestCase(1, "isAgent([x])=True", "EmotionIntensity(Matt, Joy) = [m]")]
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




        
        [TestCase(1, "isAgent([x])=True")]
        [TestCase(1, "Mood([x]) = [reseult]")]
        [TestCase(1, "EmotionIntensity([x], Joy) = [m]")]
        // [TestCase(1, "isAgent(Sarah)=True")]
        public void Test_DP_NoConstraintSet_Match(int eventSet, string lastEventMethodCall)
        {

            var rpc = BuildEmotionalRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

        

            var condSet = new ConditionSet();
            var cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);

            Assert.IsNotEmpty(result);

        }


        [Test]
        public void Test_DP_DynamicPropertyChangeEvent_Match()
        {

            var rpc = BuildEmotionalRPCAsset();
         
            rpc.m_kb.Tell((Name)"High(Score)", (Name)"2");

            Assert.AreEqual(rpc.GetBeliefValue("High(Score)"), "2");

            var propertyChange = EventHelper.PropertyChange("High(Score)", "Mood(SELF)", "Test");

            rpc.Perceive(propertyChange);
            Assert.AreEqual(rpc.GetBeliefValue("High(Score)"), "0");

            PopulateEventSet(1);

            foreach (var eve in eventSets[1])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            var propertyChange2 = EventHelper.PropertyChange("High(Score)", "Mood(SELF)", "Test");

            rpc.Perceive(propertyChange2);

            Assert.AreNotEqual(rpc.GetBeliefValue("High(Score)"), "2");

            rpc.m_kb.Tell((Name)"Has(Money)", (Name)"5");

            var propertyChange3 = EventHelper.PropertyChange("Has(Money)", "Math(2, Plus, 5)", "Test");

            rpc.Perceive(propertyChange3);

            Assert.AreEqual(rpc.GetBeliefValue("Has(Money)"), "7");
        }
        #endregion



        #region Emotional Appraisal


        [TestCase(1, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Joy")]
        [TestCase(2, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Pride")]
        [TestCase(3, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Distress")]
        [TestCase(4, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Shame")]
        [TestCase(5, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Love")]
        [TestCase(6, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Hate")]
        [TestCase(7, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Joy")]
        [TestCase(8, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Distress")]
        [TestCase(9, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Joy")]
        [TestCase(10, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Distress")]
        [TestCase(11, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Hope")]
        [TestCase(12, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Fear")]
        [TestCase(13, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Relief")]
        [TestCase(14, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Disappointment")]
        [TestCase(15, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Admiration")]
        [TestCase(16, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Reproach")]
        [TestCase(17, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Satisfaction")]
        [TestCase(18, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)","Relief")]
        [TestCase(19, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)", "Resentment")]
        [TestCase(20, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)", "Happy-For")]
        [TestCase(21, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)", "Gloating")]
        [TestCase(22, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)", "Pitty")]
        [TestCase(23, "Event(Action-End , Matt, Speak(Start, S1, -, -), Sarah)", "Pitty")]
        public void Test_EA_EmotionForEvent(int appraisalRule, string ev, string emotionFelt)
        {

            var rpc = BuildEmotionalRPCAsset(appraisalRule);
          
            rpc.Perceive((Name)ev);
            rpc.Tick++;
           

            var strongestEmotion = rpc.GetStrongestActiveEmotion();
            
            Assert.AreEqual(strongestEmotion.EmotionType, emotionFelt);

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


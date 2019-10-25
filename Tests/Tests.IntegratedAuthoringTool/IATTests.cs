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
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using ActionLibrary.DTOs;
using KnowledgeBase;

namespace IATTests
{
    [TestFixture]
    public class IATTests
    {

        private static IntegratedAuthoringToolAsset _iat = Build_IAT_Asset();

        private static IntegratedAuthoringToolAsset Build_IAT_Asset()
        {
            var iat = new IntegratedAuthoringToolAsset()
            {
                ScenarioName = "Test",
                ScenarioDescription = "default"

            };

            return iat;


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
        public void Test_IAT_ScenarioName()
        {
            var iat = Build_IAT_Asset();
            Assert.AreEqual("Test", iat.ScenarioName);

            iat.ScenarioName = "different";

            Assert.AreEqual("different", iat.ScenarioName);
        }

        [Test]
        public void Test_IAT_ScenarioDescription()
        {
            var iat = Build_IAT_Asset();
            Assert.AreEqual("default", iat.ScenarioDescription);

            iat.ScenarioDescription = "something different";

            Assert.AreEqual("something different", iat.ScenarioDescription);

        }

        [Test]
        public void Test_IAT_GetDialogueActionById()
        {
            var iat = Build_IAT_Asset();

            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);

            var trueID = iat.GetAllDialogueActions().FirstOrDefault().Id;



            Assert.AreEqual(iat.GetDialogActionById(trueID).CurrentState, d.CurrentState);

        }

        [Test]
        public void Test_IAT_GetDialogueActionsByState()
        {
            var iat = Build_IAT_Asset();

            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);


            Assert.AreEqual(iat.GetDialogueActionsByState("Start").FirstOrDefault().Utterance, d.Utterance);

        }

        [Test]
        public void Test_IAT_GetAllDialogueAction()
        {
            var iat = Build_IAT_Asset();

            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            var id2 = new Guid();
            var d2 = new DialogueStateActionDTO()
            {
                CurrentState = "S1",
                Meaning = "-",
                NextState = "S2",
                Style = "-",
                Utterance = "sbahh",
                Id = id2,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);
            iat.AddDialogAction(d2);

            Assert.AreEqual(iat.GetAllDialogueActions().Count(), 2);

        }

        [Test]
        public void Test_IAT_EditDialogueAction()
        {
            var iat = Build_IAT_Asset();

            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            var id2 = new Guid();

            var d2 = new DialogueStateActionDTO()
            {
                CurrentState = "S1",
                Meaning = "-",
                NextState = "S2",
                Style = "-",
                Utterance = "aaaaaaaa",
                Id = id2,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);

            var newD = iat.GetAllDialogueActions().FirstOrDefault();

            iat.EditDialogAction(newD, d2);



            Assert.AreEqual(iat.GetAllDialogueActions().FirstOrDefault().CurrentState.ToString(), d2.CurrentState.ToString());

        }

        [Test]
        public void Test_IAT_RemoveDialogueAction()
        {
            var iat = Build_IAT_Asset();

            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            var id2 = new Guid();
            var d2 = new DialogueStateActionDTO()
            {
                CurrentState = "S1",
                Meaning = "-",
                NextState = "S2",
                Style = "-",
                Utterance = "sbahh",
                Id = id2,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);
            iat.AddDialogAction(d2);

            var r = iat.GetAllDialogueActions().FirstOrDefault();

            iat.RemoveDialogueActions(new List<DialogueStateActionDTO>() {r});

            Assert.AreEqual(iat.GetAllDialogueActions().Count(), 1);

        }

        [TestCase("IsAgent([x])=True", "ValidDialogue([cs],[ns],[m],[sty]) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(Start,S1,-,-) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(Start,*,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,S1,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,-,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,-) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,Rude) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,*) = True")]
        [TestCase("IsAgent([x])=True, [cs] = Start", "ValidDialogue([cs],*,*,*) = True")]
        [TestCase("IsAgent([x])=True, [cs] = S1", "ValidDialogue([cs],*,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,-) = True")]
        [TestCase("", "ValidDialogue(*,*,*,[sty]) = True")]
        [TestCase("", "ValidDialogue([cs],*,*,*) = True")]
        public void Test_IAT_DP_ValidDialogue_Match(string context, string lastEventMethodCall)
        {
            var iat = Build_IAT_Asset();
           
            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            var id2 = new Guid();
            var d2 = new DialogueStateActionDTO()
            {
                CurrentState = "S1",
                Meaning = "-",
                NextState = "S2",
                Style = "Rude",
                Utterance = "sbahh",
                Id = id2,
                UtteranceId = "1"
            };

            iat.AddDialogAction(d);
            iat.AddDialogAction(d2);

           // iat.AddNewCharacterSource(new CharacterSourceDTO(){});

            var rpc = BuildRPCAsset();

            // Associating IAT to RPC
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

            // Making sure the RPC is well Initialized
            rpc.Perceive(EventHelper.ActionEnd("Sarah", "EnterRoom", "Matt"));

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

        [TestCase("IsAgent([x])=True", "ValidDialogue([cs],[ns],[m],[sty]) = False")]
        [TestCase("IsAgent([x])=True, [r] = False", "ValidDialogue([cs],[ns],[m],[sty]) = [r]")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,Start,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue([x],*,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,[x],*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,S1,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(-,*,*,*) = True")]
        [TestCase("IsAgent([x])=True", "ValidDialogue(*,*,*,Polite) = True")]
        [TestCase("IsAgent([x])=True, [m] = Meaning", "ValidDialogue(*,*,[m],*) = True")]
        public void Test_IAT_DP_ValidDialogue_NoMatch(string context, string lastEventMethodCall)
        {
            var iat = Build_IAT_Asset();
           
            var id = new Guid();
            var d = new DialogueStateActionDTO()
            {
                CurrentState = "Start",
                Meaning = "-",
                NextState = "S1",
                Style = "-",
                Utterance = "sbahh",
                Id = id,
                UtteranceId = "1"
            };

            var id2 = new Guid();
            var d2 = new DialogueStateActionDTO()
            {
                CurrentState = "S1",
                Meaning = "-",
                NextState = "S2",
                Style = "Rude",
                Utterance = "ssadasdasdh",
                Id = id2,
                UtteranceId = "2"
            };

            iat.AddDialogAction(d);
            iat.AddDialogAction(d2);

           // iat.AddNewCharacterSource(new CharacterSourceDTO(){});

            var rpc = BuildRPCAsset();

            // Associating IAT to RPC
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

            // Making sure the RPC is well Initialized
            rpc.Perceive(EventHelper.ActionEnd("Sarah", "EnterRoom", "Matt"));
            rpc.Perceive(EventHelper.ActionEnd("Sarah", "Speak(S3,S4,Polite, Rude)", "Matt"));

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



        #region Serialization Tests



        [TestCase]
        public void IAT_Serialization_Test()
        {
            var asset = Build_IAT_Asset();

            using (var stream = new MemoryStream())
            {
                var formater = new JSONSerializer();
                formater.Serialize(stream, asset);
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(new StreamReader(stream).ReadToEnd());
            }
        }

        [TestCase]
        public void IAT_Deserialization_Test()
        {
            var asset = Build_IAT_Asset();

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
}


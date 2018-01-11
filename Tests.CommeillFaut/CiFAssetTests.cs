using System;
using NUnit.Framework;
using CommeillFaut;
using CommeillFaut.DTOs;
using System.Collections.Generic;
using Conditions;
using RolePlayCharacter;
using KnowledgeBase;
using WellFormedNames;
using SerializationUtilities;
using System.IO;

namespace Tests.CommeillFaut
{
    [TestFixture]
    public class CiFAssetTests
    {

        private static CommeillFautAsset CIF = BuildCIFAsset();

        private static CommeillFautAsset BuildCIFAsset()
        {

            var seDTO = new SocialExchangeDTO()
            {
                Action = "Flirt",
                Intent = "When I'm atracted to...",
                InfluenceRule = new InfluenceRuleDTO()
                {
                    RuleConditions = new Conditions.DTOs.ConditionSetDTO()
                    {
                        ConditionSet = new string[] { "[x] != True" }

                    },
                    Target = "[x]",
                    RuleName = "Attraction"
                }


            };

            var cif1 = new CommeillFautAsset();
            cif1.AddExchange(seDTO);
  
            return cif1;

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

       //     rpc.LoadAssociatedAssets();
            return rpc;

        }

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




        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, [x]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, SELF, [x]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition([se], [y], [x]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition([se], [y], Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition([se], SELF, Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition([se], Matt, Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, [x]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, [x]) = Negative")]
        public void Test_DP_Volition_Match(int eventSet, string context, string MethodCall)
        {
            var rpc = BuildRPCAsset();
            var cif = BuildCIFAsset();
            cif.RegisterKnowledgeBase(rpc.m_kb);
             rpc.LoadAssociatedAssets();



            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

           

            // conditions
            var conditions = context.Split(',');

            IEnumerable<SubstitutionSet> resultingConstraints;

            var condSet = new ConditionSet();

            var cond = Condition.Parse(conditions[0]);

          
            // Apply conditions to RPC
            foreach (var res in conditions)
            {
                cond = Condition.Parse(res);
                condSet = condSet.Add(cond);


            }

            resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);

            condSet = new ConditionSet();
            cond = Condition.Parse(MethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsNotEmpty(result);

        }


        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, Matt) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Sarah, Matt) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Compliment, Matt, Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Compliment, [x], [y]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, Sarah) = Positive")]
        public void Test_DP_Volition_NoMatch(int eventSet, string context, string MethodCall)
        {
            var rpc = BuildRPCAsset();
            var cif = BuildCIFAsset();
            cif.RegisterKnowledgeBase(rpc.m_kb);
            rpc.LoadAssociatedAssets();



            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }



            // conditions
            var conditions = context.Split(',');

            IEnumerable<SubstitutionSet> resultingConstraints;

            var condSet = new ConditionSet();

            var cond = Condition.Parse(conditions[0]);


            // Apply conditions to RPC
            foreach (var res in conditions)
            {
                cond = Condition.Parse(res);
                condSet = condSet.Add(cond);


            }

            resultingConstraints = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, null);

            condSet = new ConditionSet();
            cond = Condition.Parse(MethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

            Assert.IsEmpty(result);

        }


        [TestCase]
        public void CommeillFaut_Serialization_Test()
        {
            var asset = BuildCIFAsset();

            using (var stream = new MemoryStream())
            {
                var formater = new JSONSerializer();
                formater.Serialize(stream, asset);
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(new StreamReader(stream).ReadToEnd());
            }
        }

        [TestCase]
        public void CommeillFaut_DeSerialization_Test()
        {
            var asset = BuildCIFAsset();

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

    }

};
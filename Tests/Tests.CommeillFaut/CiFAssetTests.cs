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
using System.Linq;

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
               Name = Name.BuildName("Flirt"),
               Description = "When I'm atracted to...",
               Conditions = new Conditions.DTOs.ConditionSetDTO()
               {
                    ConditionSet = new string[] { "Has(Floor) != Start" }
               },
               Initiator = Name.BuildName("[i]"),
               Target = Name.BuildName("[x]")
            };

            var cif1 = new CommeillFautAsset();
            cif1.AddOrUpdateExchange(seDTO);
  
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
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, [x]) = Positive")]
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
        [TestCase(1, "IsAgent([x]) = True", "Volition(Compliment, Matt, Sarah) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Compliment, [x], [y]) = [s]")]
        [TestCase(1, "IsAgent([x]) = True", "Volition(Flirt, Matt, Sarah) = Negative")]
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

#region Serialization Tests

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
#endregion

        [TestCase]
        public void Test_CIF_UpdateSocialExchange()
        {
            var cif = BuildCIFAsset();

            var se = new SocialExchange(cif.GetSocialExchanges().FirstOrDefault(x => x.Name == (Name)"Flirt"));

            var initialValue = se.Conditions.Count();

            var c = Condition.Parse("[x] != False");

            se. AddCondition(c);

            cif.UpdateSocialExchange(se.ToDTO());

            var aux = cif.GetSocialExchanges().First(x => x.Name == (Name)"Flirt").Conditions.ConditionSet.Count();

            Assert.AreEqual(aux, (initialValue + 1) );

        }


        [TestCase]
        public void Test_CIF_RemoveSocialExchange()
        {
            var cif = BuildCIFAsset();

            var se = cif.GetSocialExchanges().First(x => x.Name == (Name)"Flirt");

            cif.RemoveSocialExchange(se.Id);

            Assert.IsEmpty(cif.GetSocialExchanges());
        }


        [TestCase]
        public void Test_CIF_AddSocialExchange()
        {
            var cif = BuildCIFAsset();

            var originalCount = cif.GetSocialExchanges().Count();

            var seDTO = new SocialExchangeDTO()
            {
                Name = Name.BuildName("Compliment"),
                Description = "When I'm atracted to...",
                Conditions = new Conditions.DTOs.ConditionSetDTO()
                {
                    ConditionSet = new string[] { "[x] != Start" }
                },
                Initiator = Name.BuildName("[i]"),
                Target = Name.BuildName("[x]")
            };

            cif.AddOrUpdateExchange(seDTO);
            Assert.AreEqual(cif.GetSocialExchanges().Count(), (originalCount + 1));

        }

        [TestCase]
        public void Test_CIF_GetDTO()
        {
            var cif = BuildCIFAsset();

            var dto = cif.GetDTO();
       
            Assert.AreEqual(dto._SocialExchangesDtos.Length, cif.GetSocialExchanges().Count());

        }


        [TestCase(0.5f, "Neutral")]
        [TestCase(0.75f, "Positive")]
        [TestCase(0.3f, "Negative")]
        public void Test_CIF_StyleCalculator(float value, string output)
        {
            var cif = BuildCIFAsset();

            var result = cif.CalculateStyle(value);

            Assert.AreEqual(output, result);

        }

        [TestCase]
        public void Test_CIF_LoadFromDTO()
        {
            var cif = BuildCIFAsset();

            var seDTO = new SocialExchangeDTO()
            {
                Name = Name.BuildName("Compliment"),
                Description = "When we are friends..",
                Conditions = new Conditions.DTOs.ConditionSetDTO()
                {
                    ConditionSet = new string[] { "[x] != Start" }
                },
                Initiator = Name.BuildName("[i]"),
                Target = Name.BuildName("[x]")
            };

            var cifDTO = new CommeillFautDTO()
            {
                _SocialExchangesDtos = new[] { seDTO }
            };


            cif.LoadFromDTO(cifDTO);
            Assert.AreEqual(cif.GetSocialExchanges().First().Name.ToString(), "Compliment");
        }


        [TestCase]
        public void Test_CIF_SE_AddCondition()
        {
            var cif = BuildCIFAsset();

            var seDTO = cif.GetSocialExchanges().First(x=>x.Name == (Name)"Flirt");
            var se = new SocialExchange(seDTO);
            var totalCondsBefore = se.Conditions.Count;
            se.AddCondition(Condition.Parse("[x] != Start"));

            Assert.AreEqual((totalCondsBefore + 1), se.Conditions.Count);

        }

        [TestCase]
        public void Test_CIF_SE_RemoveCondition()
        {
            var cif = BuildCIFAsset();

            var seDTO = cif.GetSocialExchanges().First(x => x.Name == (Name)"Flirt");
            var se = new SocialExchange(seDTO);

            se.RemoveCondition(Condition.Parse("Has(Floor) != Start"));

            Assert.IsEmpty(se.Conditions);

        }

        [TestCase]
        public void Test_CIF_SE_ToString()
        {
            var cif = BuildCIFAsset();
            var se = cif.GetSocialExchanges().First(x => x.Name == (Name)"Flirt");
            Assert.IsTrue(se.ToString().Contains("Flirt"));
        }
    }

};
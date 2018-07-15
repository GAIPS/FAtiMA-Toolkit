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
             

               Target = Name.BuildName("[t]"),
               StartingConditions = new Conditions.DTOs.ConditionSetDTO()
               {
                    ConditionSet = new string[] { "Has(Floor) = SELF"}
               },
               Steps = new List<Name>() {(Name)"Start"},
               InfluenceRules = new List<InfluenceRuleDTO>()
               {

                  new InfluenceRuleDTO()
                   {
                       Value = 5,
                       Mode = (Name)"General",
                       Rule= new Conditions.DTOs.ConditionSetDTO()
                       
                       {
                           
                         ConditionSet = new string[] { "Likes([t]) = True"}                   
                           
                           }
                 
               },

                   new InfluenceRuleDTO()
                   {
                       Value = 10,
                       Mode = (Name)"Love",
                       Rule= new Conditions.DTOs.ConditionSetDTO()
                     
                       {
                           
                         ConditionSet = new string[] { "Likes([t]) = True", "EventId(Action-End, [i], Speak(*, *, *, *), [t])>0"}                   
                           
                           }
                 
               }

               }

            };

            var cif1 = new CommeillFautAsset();
            cif1.AddOrUpdateExchange(seDTO);
  


             var seDTO2 = new SocialExchangeDTO()
            {
               Name = Name.BuildName("Insult"),
               Description = "I hate you ",
             

               Target = Name.BuildName("[t]"),
               StartingConditions = new Conditions.DTOs.ConditionSetDTO()
               {
                    ConditionSet = new string[] { "IsAgent(SELF) = True"}
               },
               Steps = new List<Name>() {(Name)"Start"},
               InfluenceRules = new List<InfluenceRuleDTO>()
               {

               new InfluenceRuleDTO()
                   {
                       Value = 4,
                       Rule= new Conditions.DTOs.ConditionSetDTO()
                       
                       {
                           
                         ConditionSet = new string[] { "Likes([t]) = False"}                   
                           
                           }
                 
               }

               }

            };

             cif1.AddOrUpdateExchange(seDTO2);

            return cif1;

        }


        private static RolePlayCharacterAsset Matt = BuildRPCAsset();

        private static RolePlayCharacterAsset Sarah = BuildRPCAsset2();

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

        private static RolePlayCharacterAsset BuildRPCAsset2()
        {
            var kb = new KB((Name)"Sarah");


            var rpc = new RolePlayCharacterAsset
            {
                BodyName = "Female",
                VoiceName = "Female",
                CharacterName = (Name)"Sarah",
                m_kb = kb,

            };

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
                EventHelper.PropertyChange("Has(Floor)", "Matt", "Matt").ToString(),
                EventHelper.PropertyChange("Likes(Sarah)", "True", "Matt").ToString(),
                //THIS SHOULD BE THE LAST EVENT
               EventHelper.ActionEnd("Matt", "Speak(Start, S1, Hello, Positive)", "Sarah").ToString()

            };

            } else if(set == 2)
            {

                 eventList = new List<string>()
                {
                EventHelper.ActionEnd("Matt", "EntersRoom", "Sarah").ToString(),
                EventHelper.ActionEnd("Sarah", "EntersRoom", "Matt").ToString(),
                EventHelper.PropertyChange("Likes(Matt)", "False", "Sarah").ToString()
                //THIS SHOULD BE THE LAST EVENT

            };
            }

            eventSets.Add(set, eventList);
        
        }


       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, *) = [value]")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, *) = 15")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, Love) = 10")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, General) = 5")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, [x], Love) = 10")]

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

       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, *) = [value]","[value]", "15")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, *) = [value]","[value]", "15")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, Love) = [value]","[value]", "10")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, General) = [value]","[value]", "5")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, [x], Love) = [value]","[value]", "10")]
        public void Test_DP_Volition_Match_Value_Matt(int eventSet, string context, string MethodCall, string variable, string value)
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


           var sub = result.FirstOrDefault().Where(x=>x.Variable == (Name)variable);

          var ret = sub.FirstOrDefault().SubValue;

                Assert.AreEqual(ret.Value.ToString(), value);

        }

          [TestCase(2, "IsAgent([x]) = True", "Volition([se], *, Matt, *) = [value]","[se]", "Insult")]
          [TestCase(2, "IsAgent([y]) = True", "Volition(Insult, *, [y], *) = [value]","[y]", "Matt")]
        public void Test_DP_Volition_Match_Value_Sarah(int eventSet, string context, string MethodCall, string variable, string value)
        {
            var rpc = BuildRPCAsset2();
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


           var sub = result.FirstOrDefault().Where(x=>x.Variable == (Name)variable);

          var ret = sub.FirstOrDefault().SubValue;

                Assert.AreEqual(ret.Value.ToString(), value);

        }




       [TestCase(1, "IsAgent([x]) = True", "Volition([x], *, Matt, Sarah) = [value]")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Sarah, Matt) = 5")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Matt, [x]) = 0")]
       [TestCase(1, "IsAgent([x]) = True", "Volition([se], *, Matt, Sarah) = [x]")]
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

            var se = new SocialExchange(cif.GetAllSocialExchanges().FirstOrDefault(x => x.Name == (Name)"Flirt"));

            var initialValue = se.StartingConditions.Count();

            var c = Condition.Parse("[x] != False");

            se. AddStartingCondition(c);

            cif.UpdateSocialExchange(se.ToDTO);

            var aux = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Flirt").StartingConditions.ConditionSet.Count();

            Assert.AreEqual(aux, (initialValue + 1) );

        }


        [TestCase]
        public void Test_CIF_RemoveSocialExchange()
        {
            var cif = BuildCIFAsset();

            var se = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Flirt");

            cif.RemoveSocialExchange(se.Id);

               var se2 = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Insult");

              cif.RemoveSocialExchange(se2.Id);

            Assert.IsEmpty(cif.GetAllSocialExchanges());
        }


        [TestCase]
        public void Test_CIF_AddSocialExchange()
        {
            var cif = BuildCIFAsset();

            var originalCount = cif.GetAllSocialExchanges().Count();

            var seDTO = new SocialExchangeDTO()
            {
                Name = Name.BuildName("Compliment"),
                Description = "When I'm atracted to...",
                StartingConditions = new Conditions.DTOs.ConditionSetDTO(),
                InfluenceRules = new List<InfluenceRuleDTO>(),
                Target = Name.BuildName("[x]")
            };

            cif.AddOrUpdateExchange(seDTO);
            Assert.AreEqual(cif.GetAllSocialExchanges().Count(), (originalCount + 1));

        }

        [TestCase]
        public void Test_CIF_GetDTO()
        {
            var cif = BuildCIFAsset();

            var dto = cif.GetDTO();
       
            Assert.AreEqual(dto._SocialExchangesDtos.Length, cif.GetAllSocialExchanges().Count());

        }


        [TestCase]
        public void Test_CIF_LoadFromDTO()
        {
            var cif = BuildCIFAsset();

            var seDTO = new SocialExchangeDTO()
            {
                Name = Name.BuildName("Compliment"),
                Description = "When we are friends..",
                StartingConditions = new Conditions.DTOs.ConditionSetDTO(),
                Steps = new List<Name>(),
                InfluenceRules = new List<InfluenceRuleDTO>(),
                Target = Name.BuildName("[x]")
            };

            var cifDTO = new CommeillFautDTO()
            {
                _SocialExchangesDtos = new[] { seDTO }
            };


            cif.LoadFromDTO(cifDTO);
            Assert.AreEqual(cif.GetAllSocialExchanges().FirstOrDefault().Name.ToString(), "Compliment");
        }


        [TestCase]
        public void Test_CIF_SE_AddStartingCondition()
        {
            var cif = BuildCIFAsset();

            var seDTO = cif.GetAllSocialExchanges().First(x=>x.Name == (Name)"Flirt");
            var se = new SocialExchange(seDTO);
            var totalCondsBefore = se.StartingConditions.Count;
            se.AddStartingCondition(Condition.Parse("[x] != Start"));

            Assert.AreEqual((totalCondsBefore + 1), se.StartingConditions.Count);

        }


        [TestCase]
        public void Test_CIF_SE_RemoveStartingCondition()
        {
            var cif = BuildCIFAsset();

        
            var seDTO = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Flirt");
            var se = new SocialExchange(seDTO);

            var originalCount = se.StartingConditions.Count();

            se.RemoveStartingCondition(Condition.Parse("Has(Floor) = SELF"));

            Assert.AreEqual(se.StartingConditions.Count, originalCount - 1);

        }

     
        

            [TestCase]
        public void Test_CIF_SE_AddInfluenceRule()
        {
            var cif = BuildCIFAsset();

            var seDTO = cif.GetAllSocialExchanges().First(x=>x.Name == (Name)"Flirt");
            var se = new SocialExchange(seDTO);
            var totalCondsBefore = se.InfluenceRules.Count;

            se.InfluenceRules.Add(new InfluenceRule( new InfluenceRuleDTO(){
            Value = 2,
            Rule = new Conditions.DTOs.ConditionSetDTO()
                
                })
                );

            Assert.AreEqual((totalCondsBefore + 1), se.InfluenceRules.Count);

        }

           [TestCase]
        public void Test_CIF_SE_RemoveInfluenceRule()
        {
            var cif = BuildCIFAsset();

        
            var seDTO = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Flirt");

              var se = new SocialExchange(seDTO);
            var totalCondsBefore = se.InfluenceRules.Count;

            var inf = new InfluenceRule( new InfluenceRuleDTO(){
            Value = 2,
            Rule = new Conditions.DTOs.ConditionSetDTO()
                
                });

            se.InfluenceRules.Add(inf);

            Assert.AreEqual((totalCondsBefore + 1), se.InfluenceRules.Count);

            se.InfluenceRules.Remove(inf);

            Assert.AreEqual(se.InfluenceRules.Count, totalCondsBefore);

        }

        [TestCase]
        public void Test_CIF_SE_ToString()
        {
            var cif = BuildCIFAsset();
            var se = cif.GetAllSocialExchanges().First(x => x.Name == (Name)"Flirt");
            var toString = new SocialExchange(se).ToString();
            Assert.IsTrue(toString.Contains("Flirt"));
        }
    }

};
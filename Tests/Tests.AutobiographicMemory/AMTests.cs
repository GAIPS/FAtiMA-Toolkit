using RolePlayCharacter;
using WellFormedNames;
using KnowledgeBase;
using AutobiographicMemory;
using Conditions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.AutobiographicMemory
{
    [TestFixture]
    public class AMTests
    {

        private static AM AutMemory = BuildAMAsset();


        private Dictionary<int, List<string>> eventSets;

        private static AM BuildAMAsset()
        {
            var am = new AM()
            {
                Tick = 0,
            };
    
            return am;

        }



        private void PopulateEventSet(int set)
        {
            eventSets = new Dictionary<int, List<string>>();
            var eventList = new List<string>();

            if (set == 1)
            {
                eventList= new List<string>()
                {
                EventHelper.ActionEnd("Matt", "EntersRoom", "Sarah").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, -, -)", "Sarah").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, -, Polite)", "Sarah").ToString(),
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, Silly, Polite)", "Sarah").ToString(),
                EventHelper.PropertyChange("Has(Floor)", "Sarah", "Matt").ToString(),
                //THIS SHOULD BE THE LAST EVENT
                EventHelper.ActionEnd("Matt", "Speak(Start, S1, SE(Flirt, Initiate), Positive)", "Sarah").ToString(),
                    EventHelper.ActionEnd("Matt", "Speak(Start, S3, -, -)", "Sarah").ToString()
    
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

            rpc.LoadAssociatedAssets(new GAIPS.Rage.AssetStorage());
            return rpc;

        }


        [Test]
        public void TestAMInitialTick()
        {
            
            Assert.AreEqual(0, AutMemory.Tick);
        }


        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, [x], Speak(*, *, SE([se], Initiate), Positive), SELF)=2")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S1, SE(Flirt, Initiate), Positive), Sarah)=2")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Sarah, *, Matt)=4")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-Start, *, *, *)=0")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, *, *, *)=4")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, *, Speak(*, *, SE([se], Initiate), Positive), *)=4")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Property-Change, *, *, *)=0")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(*, *, *, *)=1")]
        [Test]
        public void Test_DP_LastEventID_NoMatch(int eventSet, string context, string lastEventMethodCall)
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }
            // Build the context, parsin the conditions:

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
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);

           
           var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

           Assert.IsEmpty(result);

       }

        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S1, SE(Flirt, Initiate), Positive), Sarah)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S1, SE(*, Initiate), Positive), Sarah)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S3, -, -), Sarah)=6")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, *, Sarah)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, *, *, *)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, *, *)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(*, *, *, *)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S1, *, Positive), Sarah)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Matt, Speak(Start, S1, *, *), *)=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, Sarah, Speak(Start, S1, *, *), *)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-Start, *, *, *)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, [x], Speak(*, *, SE([se], Initiate), Negative), SELF)=-1")]

        [Test]
        public void Test_DP_LastEventID_Match(int eventSet, string context, string lastEventMethodCall) {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Build the context, parsin the conditions:

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
            cond = Condition.Parse(lastEventMethodCall);
            condSet = condSet.Add(cond);


            var result = condSet.Unify(rpc.m_kb, Name.SELF_SYMBOL, resultingConstraints);

           Assert.IsNotEmpty(result);

        }

        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, [x], Speak(*, *, SE([se], Initiate), Negative), SELF)=[id]", "-1")]
        [TestCase(1, "IsAgent([x]) = True", "LastEventId(Action-End, *, Speak(*, *, SE([se], Initiate), Negative), SELF)=[id]", "-1")]
        [TestCase(1, "", "LastEventId(Action-End, *, Speak(*, *, SE([se], Initiate), Negative), SELF)=[id]", "-1")]
        [Test]
        public void Test_DP_LastEventID_ValueMatch(int eventSet, string context, string lastEventMethodCall, string expectedValue) {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
            {
                rpc.Perceive((Name)eve);
                rpc.Tick++;
            }

            // Build the context, parsin the conditions:

            var resultVariable = lastEventMethodCall.Split('=')[1];

            var conditions = context.Split(',');
            var cond = Condition.Parse("[x] != True");
            var  condSet = new ConditionSet();
            IEnumerable<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();

            if (conditions[0] != "")
            {


               

                condSet = new ConditionSet();

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

            var actualResult = "";
            foreach (var sub in result)
            {
                if (sub.Contains((Name) resultVariable))
                    actualResult = sub[(Name)resultVariable].ToString();
            }
            Assert.AreEqual(expectedValue, actualResult);

        }

        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, EntersRoom, Sarah)=0")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, [x], [y], [z])=0")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, [x], EntersRoom, [z])=0")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, -, -), Sarah)=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, -, Polite), Sarah)=2")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, Silly, Polite), Sarah)=3")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Property-Change, Matt, Has(Floor), Sarah)=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, SE(Flirt, Initiate), Positive), Sarah)=5")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, Initiate), *), Sarah)=5")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, *), *), Sarah)=5")]
        [TestCase(1, "IsAgent([x]) = True", "EventId([action], [x], [y], [z])=[id]")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-Start, [x], [y], [z])= -1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Sarah, Speak(*, *, SE(Flirt, Initiate), *), Matt)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Compliment, *), *), Sarah)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Talk(*, *, SE(Compliment, *), *), Sarah)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(S1, Start, -, -), Sarah)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S2, -, -), Sarah)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, S2, -, -), Sarah)=-1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak([cs], S2, *, *), Sarah)=-1")]
       
        [Test]
        public void Test_DP_EventID_Match(int eventSet, string context, string MethodCall)
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
                rpc.Perceive((Name)eve);

            // Build the context, parsin the conditions:

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

         /*   // what is the id I'm looking for
            var wantedSub = cond.ToString().Split('=');

            foreach (var sub in result)
            {
                if(sub)
            }*/ 

            Assert.IsNotEmpty(result);

        }

        // Wrong ID
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, EntersRoom, Sarah)=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, [x], [y], [z])=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, [x], EntersRoom, [z])=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, -, -), Sarah)=0")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, -, Polite), Sarah)=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, Silly, Polite), Sarah)=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Property-Change, Matt, Has(Floor), Sarah)=3")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(Start, S1, SE(Flirt, Initiate), Positive), Sarah)=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, Initiate), *), Sarah)=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, *), *), Sarah)=1")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(*, *,*, *)=7")]
        // No action like this happened
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, Answer), *), Sarah)=5")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Compliment, *), *), Sarah)=5")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Flirt, Answer), *), Sarah)=0")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Action-End, Matt, Speak(*, *, SE(Compliment, *), *), Sarah)=-2")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(Property-Change, Sarah, Has(Floor), Matt)=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(*, Sarah, Has(Floor), Matt)=4")]
        [TestCase(1, "IsAgent([x]) = True", "EventId(*, [x], [y], [z])=-1")]
        [Test]
        public void Test_DP_EventID_NoMatch(int eventSet, string context, string MethodCall)
        {
            var rpc = BuildRPCAsset();
            PopulateEventSet(eventSet);

            foreach (var eve in eventSets[eventSet])
                rpc.Perceive((Name)eve);

            // Build the context, parsin the conditions:

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

            /*   // what is the id I'm looking for
               var wantedSub = cond.ToString().Split('=');

               foreach (var sub in result)
               {
                   if(sub)
               }*/

            Assert.IsEmpty(result);

        }



    }
};
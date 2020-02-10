using System;
using EmotionalAppraisal;
using EmotionalAppraisal.OCCModel;
using NUnit.Framework;
using System.IO;
using EmotionalAppraisal.DTOs;
using WellFormedNames;
using System.Collections.Generic;
using AutobiographicMemory;
using KnowledgeBase;
using RolePlayCharacter;

namespace Tests.EmotionalAppraisal
{
    [TestFixture]
    public class EAAssetTests
    {

        private Dictionary<int, List<Name>> events;


        private void BuildDataSet()
        {

            events = new Dictionary<int, List<Name>>();

            events[0] = new List<Name>
            {

                (Name) "Event(Action-End,Sarah,Feed,self)"
            };


        }

        private static EmotionalAppraisalAsset BuildTestAsset()
        {

            var m_emotionalAppraisalAsset = new EmotionalAppraisalAsset();


            var joyDisposition = new EmotionDisposition(OCCEmotionType.Joy.Name, 2, 3);
            m_emotionalAppraisalAsset.AddEmotionDisposition(joyDisposition.ToDto());

            var distressDisposition = new EmotionDisposition(OCCEmotionType.Distress.Name, 2, 1);
            m_emotionalAppraisalAsset.AddEmotionDisposition(distressDisposition.ToDto());

            var prideDisposition = new EmotionDisposition(OCCEmotionType.Pride.Name, 5, 5);
            m_emotionalAppraisalAsset.AddEmotionDisposition(prideDisposition.ToDto());

            var shameDisposition = new EmotionDisposition(OCCEmotionType.Shame.Name, 2, 1);
            m_emotionalAppraisalAsset.AddEmotionDisposition(shameDisposition.ToDto());

            var gratificationDisposition = new EmotionDisposition(OCCEmotionType.Gratification.Name, 8, 5);
            m_emotionalAppraisalAsset.AddEmotionDisposition(gratificationDisposition.ToDto());

            var remorseDisposition = new EmotionDisposition(OCCEmotionType.Remorse.Name, 2, 1);
            m_emotionalAppraisalAsset.AddEmotionDisposition(remorseDisposition.ToDto());

            var admirationDisposition = new EmotionDisposition(OCCEmotionType.Admiration.Name, 5, 3);
            m_emotionalAppraisalAsset.AddEmotionDisposition(admirationDisposition.ToDto());

            var reproachDisposition = new EmotionDisposition(OCCEmotionType.Reproach.Name, 8, 2);
            m_emotionalAppraisalAsset.AddEmotionDisposition(reproachDisposition.ToDto());

            var gratitudeDisposition = new EmotionDisposition(OCCEmotionType.Gratitude.Name, 5, 3);
            m_emotionalAppraisalAsset.AddEmotionDisposition(gratitudeDisposition.ToDto());

            var angerDisposition = new EmotionDisposition(OCCEmotionType.Anger.Name, 5, 3);
            m_emotionalAppraisalAsset.AddEmotionDisposition(angerDisposition.ToDto());

            //Setup appraisal rules

            m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Pet,SELF)",
               AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"10"
                   },
               })
                   
            });

            m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Slap,SELF)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"10"
                   }
               })
            });

            m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Feed,SELF)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5"
                   },
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"10"
                   }
               })

            });

            m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Talk(High,Mad),SELF)",
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"-7"
                   },
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"15"
                   }
               })
            });

            m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Talk(Low,Happy),SELF)",
                 AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"5"
                   }
               })
            });

            //Generate emotion

            //m_emotionalAppraisalAsset.AppraiseEvents(new []{ "Event(Action-Finished,Player,Slap,self)" });

            //Add knowledge


            return m_emotionalAppraisalAsset;
        }

        [TestCase]
        public void Test_EA_GetAllAppraisalRules()
        {
            var asset = BuildTestAsset();


            var count = 0;

            foreach (var a in asset.GetAllAppraisalRules())
                count++;

            Assert.AreEqual(count, 5);
        }

        [TestCase]
        public void Test_EA_RemoveAppraisalRules()
        {
            var asset = BuildTestAsset();

            List<AppraisalRuleDTO> rules = new List<AppraisalRuleDTO>();

            foreach (var r in asset.GetAllAppraisalRules())
                rules.Add(r);

            asset.RemoveAppraisalRules(rules);


            Assert.IsEmpty(asset.GetAllAppraisalRules());
        }


        [TestCase]
        public void Test_EA_EmotionDisposition()
        {
            var asset = BuildTestAsset();

            var dispositions = asset.EmotionDispositions;

            List<string> types = new List<string>();

            foreach (var e in dispositions)
                types.Add(e.Emotion);

            foreach (var t in types)
                asset.RemoveEmotionDisposition(t);

            Assert.IsEmpty(asset.EmotionDispositions);
        }

        [TestCase]
        public void Test_EA_SetDefaultEmotionDisposition()
        {
            var asset = BuildTestAsset();

            asset.DefaultEmotionDisposition = new EmotionDisposition(OCCEmotionType.Admiration.Name, 5, 3).ToDto();

            Assert.IsNotNull(asset.DefaultEmotionDisposition);

        }

        [TestCase]
        public void Test_EA_GetAllAppraisalRuleCondition()
        {
            var asset = BuildTestAsset();

            foreach (var c in asset.GetAllAppraisalRules())
                Assert.IsEmpty(c.Conditions.ConditionSet);

            System.Guid id = new Guid();

            foreach (var c in asset.GetAllAppraisalRules())
            {
                if (c.EventMatchingTemplate == (Name) "Event(Action-End,*,Talk(Low,Happy),SELF)")
                {
                    id = c.Id;
                    break;
                }
            }


            asset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name) "Event(Action-End,*,Talk(Low,Happy),SELF)",
                 AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.DESIRABILITY,
                       Value = (Name)"10"
                   }
               }),
                Conditions = new Conditions.DTOs.ConditionSetDTO() {ConditionSet = new string[] {"[x] != SELF"}},
                Id = id

            });



            Assert.IsNotNull(asset.GetAllAppraisalRuleConditions(id).ConditionSet);
        }


        [TestCase]
        public void Test_EA_AppraiseEvent()
        {
            var asset = BuildTestAsset();

            asset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
            {
                EventMatchingTemplate = (Name)"Event(Action-End,*,*,*)",
                Conditions = new Conditions.DTOs.ConditionSetDTO(),
                AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                   new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"5"
                   }
               })
            }) ;

            var m_kb = new KB((Name)"Matt");

            asset.AppraiseEvents(new List<Name>() {EventHelper.ActionEnd("Matt", "Speak(Start,S1,-,-)", "Sarah")},
                new ConcreteEmotionalState(),
                new AM(), m_kb, null);


            Assert.IsNotNull(asset.DefaultEmotionDisposition);
            
        }
    }
}
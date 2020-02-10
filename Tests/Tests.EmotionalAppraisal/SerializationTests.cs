using System;
using EmotionalAppraisal;
using EmotionalAppraisal.OCCModel;
using NUnit.Framework;
using System.IO;
using EmotionalAppraisal.DTOs;
using WellFormedNames;
using System.Collections.Generic;

namespace Tests.EmotionalAppraisal
{
	[TestFixture]
	public class SerializationTests
	{
		private static EmotionalAppraisalAsset BuildTestAsset()
		{//Emotional System Setup
			var m_emotionalAppraisalAsset = new EmotionalAppraisalAsset();
            //	m_emotionalAppraisalAsset.SetPerspective("Test");

			//Setup Emotional Disposition

			//var loveDisposition = new EmotionDisposition(OCCEmotionType.Love.Name, 5, 3);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(loveDisposition);

			//var hateDisposition = new EmotionDisposition(OCCEmotionType.Hate.Name, 5, 3);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(hateDisposition);

			//var hopeDisposition = new EmotionDisposition(OCCEmotionType.Hope.Name, 8, 4);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(hopeDisposition);

			//var fearDisposition = new EmotionDisposition(OCCEmotionType.Fear.Name, 2, 1);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(fearDisposition);

			//var satisfactionDisposition = new EmotionDisposition(OCCEmotionType.Satisfaction.Name, 8, 5);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(satisfactionDisposition);

			//var reliefDisposition = new EmotionDisposition(OCCEmotionType.Relief.Name, 5, 3);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(reliefDisposition);

			//var fearsConfirmedDisposition = new EmotionDisposition(OCCEmotionType.FearsConfirmed.Name, 2, 1);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(fearsConfirmedDisposition);

			//var disapointmentDisposition = new EmotionDisposition(OCCEmotionType.Disappointment.Name, 5, 2);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(disapointmentDisposition);

			var joyDisposition = new EmotionDisposition(OCCEmotionType.Joy.Name, 2, 3);
			m_emotionalAppraisalAsset.AddEmotionDisposition(joyDisposition.ToDto());

			var distressDisposition = new EmotionDisposition(OCCEmotionType.Distress.Name, 2, 1);
			m_emotionalAppraisalAsset.AddEmotionDisposition(distressDisposition.ToDto());

			//var happyForDisposition = new EmotionDisposition(OCCEmotionType.HappyFor.Name, 5, 2);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(happyForDisposition);

			//var pittyDisposition = new EmotionDisposition(OCCEmotionType.Pitty.Name, 2, 2);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(pittyDisposition);

			//var resentmentDisposition = new EmotionDisposition(OCCEmotionType.Resentment.Name, 2, 3);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(resentmentDisposition);

			//var gloatingDisposition = new EmotionDisposition(OCCEmotionType.Gloating.Name, 8, 5);
			//m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(gloatingDisposition);

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
                EventMatchingTemplate = (Name)"Event(Action-End,*,Pet,self)",
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
                EventMatchingTemplate = (Name)"Event(Action-End,*,Slap,self)",
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
                EventMatchingTemplate = (Name)"Event(Action-End,*,Feed,self)",
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
                EventMatchingTemplate = (Name)"Event(Action-End,*,Talk(High,Mad),self)",
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
                       Value = (Name)"-15"
                   }
               })
            });

			m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
			{
                EventMatchingTemplate = (Name)"Event(Action-End,*,Talk(Low,Happy),self)",
               AppraisalVariables = new AppraisalVariables(new List<AppraisalVariableDTO>()
               {
                    new AppraisalVariableDTO()
                   {
                       Name = OCCAppraisalVariables.PRAISEWORTHINESS,
                       Value = (Name)"5"
                   }
               })
			});
			
			//Generate emotion

			//m_emotionalAppraisalAsset.AppraiseEvents(new []{ "Event(Action-Finished,Player,Slap,self)" });

			//Add knowledge
		
	
			return m_emotionalAppraisalAsset;
		}
/*
		[TestCase]
		public void EmotionalAppraisal_Serialization_Test()
		{
			var asset = BuildTestAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new SerializationUtilities.JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
			}
		}

		[TestCase]
		public void EmotionalAppraisal_Deserialization_Test()
		{
			var asset = BuildTestAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new SerializationUtilities.JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formater.Deserialize(stream);
			}
		}*/
	}
}

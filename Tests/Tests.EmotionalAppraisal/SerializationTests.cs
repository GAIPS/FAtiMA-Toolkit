using System;
using EmotionalAppraisal;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using NUnit.Framework;
using System.IO;
using EmotionalAppraisal.DTOs;
using GAIPS.Serialization;
using WellFormedNames;

namespace Tests.EmotionalAppraisal
{
	[TestFixture]
	public class SerializationTests
	{
		private static EmotionalAppraisalAsset BuildTestAsset()
		{//Emotional System Setup
			var m_emotionalAppraisalAsset = new EmotionalAppraisalAsset("Agent");
			m_emotionalAppraisalAsset.SetPerspective("Test");

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
				EventMatchingTemplate = "Event(Action-Finished,*,Pet,self)",
				Desirability = 10
			});

			m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
			{
				EventMatchingTemplate = "Event(Action-Finished,*,Slap,self)",
				Desirability = -10
			});

			m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
			{
				EventMatchingTemplate = "Event(Action-Finished, *, Feed, self)",
				Desirability = 5,
				Praiseworthiness = 10
			});

			m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
			{
				EventMatchingTemplate = "Event(Action-Finished,*,Talk(High,Mad),self)",
				Desirability = -7,
				Praiseworthiness = -15
			});

			m_emotionalAppraisalAsset.AddOrUpdateAppraisalRule(new AppraisalRuleDTO()
			{
				EventMatchingTemplate = "Event(Action-Finished,*,Talk(Low,Happy),self)",
				Praiseworthiness = 5
			});
			
			//Generate emotion

			m_emotionalAppraisalAsset.AppraiseEvents(new []{ "Event(Action-Finished,Player,Slap,self)" });

			//Add knowledge
			var kb = m_emotionalAppraisalAsset.Kb;
			kb.Tell((Name)"Strength(John)", (byte)5);
			kb.Tell((Name)"Strength(Mary)", (sbyte)3);
			kb.Tell((Name)"Strength(Leonidas)", (short)500);
			kb.Tell((Name)"Strength(Goku)", (uint)9001f);
			kb.Tell((Name)"Strength(SuperMan)", ulong.MaxValue);
			kb.Tell((Name)"Strength(Saitama)", float.MaxValue);
			kb.Tell((Name)"Race(Saitama)", "human");
			kb.Tell((Name)"Race(Superman)", "kriptonian");
			kb.Tell((Name)"Race(Goku)", "sayian");
			kb.Tell((Name)"Race(Leonidas)", "human");
			kb.Tell((Name)"Race(Mary)", "human");
			kb.Tell((Name)"Race(John)", "human");
			kb.Tell((Name)"Job(Saitama)", "super-hero");
			kb.Tell((Name)"Job(Superman)", "super-hero");
			kb.Tell((Name)"Job(Leonidas)", "Spartan");
			kb.Tell((Name)"AKA(Saitama)", "One-Punch_Man");
			kb.Tell((Name)"AKA(Superman)", "Clark_Kent");
			kb.Tell((Name)"AKA(Goku)", "Kakarot");
			kb.Tell((Name)"Hobby(Saitama)", "super-hero");
			kb.Tell((Name)"Hobby(Goku)", "training");

			return m_emotionalAppraisalAsset;
		}

		[TestCase]
		public void EmotionalAppraisal_Serialization_Test()
		{
			var asset = BuildTestAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
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
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formater.Deserialize(stream);
			}
		}
	}
}

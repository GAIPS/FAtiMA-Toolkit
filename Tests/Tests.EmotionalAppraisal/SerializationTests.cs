using System;
using EmotionalAppraisal;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using NUnit.Framework;
using KnowledgeBase.WellFormedNames;
using System.IO;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;

namespace Tests.EmotionalAppraisal
{
	[TestFixture]
	public class SerializationTests
	{
		private static EmotionalAppraisalAsset BuildTestAsset()
		{//Emotional System Setup
			var m_emotionalAppraisalAsset = new EmotionalAppraisalAsset("Agent");
			m_emotionalAppraisalAsset.Perspective = "Test";

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

			AppraisalRule petAppraisalRule = new AppraisalRule((Name)"Event(EventObject,*,Pet,self)");
			petAppraisalRule.Desirability = 10;
			//petAppraisalRule.Like = 7;
			//m_emotionalAppraisalAsset.AddAppraisalRule(petAppraisalRule);

			AppraisalRule slapAppraisalRule = new AppraisalRule((Name)"Event(EventObject,*,Slap,self)");
			slapAppraisalRule.Desirability = -10;
			//slapAppraisalRule.Like = -15;
			//m_emotionalAppraisalAsset.AddAppraisalRule(slapAppraisalRule);

			AppraisalRule feedAppraisalRule = new AppraisalRule((Name)"Event(EventObject,*,Feed,self)");
			feedAppraisalRule.Desirability = 5;
			feedAppraisalRule.Praiseworthiness = 10;
			//m_emotionalAppraisalAsset.AddAppraisalRule(feedAppraisalRule);

			AppraisalRule screamMad = new AppraisalRule((Name)"Event(EventObject,*,Talk(High,Mad),self)");
			screamMad.Desirability = -7;
			screamMad.Praiseworthiness = -15;
			//screamMad.Like = -4;
			//m_emotionalAppraisalAsset.AddAppraisalRule(screamMad);

			AppraisalRule talkSoftAppraisalRule = new AppraisalRule((Name)"Event(EventObject,*,Talk(Low,Happy),self)");
			talkSoftAppraisalRule.Praiseworthiness = 5;
			//talkSoftAppraisalRule.Like = 5;
			//m_emotionalAppraisalAsset.AddAppraisalRule(talkSoftAppraisalRule);

			//Generate emotion

			m_emotionalAppraisalAsset.AppraiseEvents(new []{ (Name)"Event(EventObject,*,Slap(Hard),self)" });

			//Add knowledge
			var kb = m_emotionalAppraisalAsset.Kb;
			kb.Tell((Name)"Strength(John)", (byte)5,true,KnowledgeVisibility.Self);
			kb.Tell((Name)"Strength(Mary)", (sbyte)3, true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Strength(Leonidas)", (short)500, true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Strength(Goku)", (uint)9001f, true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Strength(SuperMan)", ulong.MaxValue, true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Strength(Saitama)", float.MaxValue, true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Race(Saitama)", "human", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Race(Superman)", "kriptonian", true, KnowledgeVisibility.Universal);
			kb.Tell((Name)"Race(Goku)", "sayian",true,KnowledgeVisibility.Self);
			kb.Tell((Name)"Race(Leonidas)", "human", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Race(Mary)", "human", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Race(John)", "human", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Job(Saitama)", "super-hero",false,KnowledgeVisibility.Self);
			kb.Tell((Name)"Job(Superman)", "super-hero", true, KnowledgeVisibility.Universal);
			kb.Tell((Name)"Job(Leonidas)", "Spartan", false, KnowledgeVisibility.Self);
			kb.Tell((Name)"AKA(Saitama)", "One-Punch_Man", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"AKA(Superman)", "Clark_Kent", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"AKA(Goku)", "Kakarot", true, KnowledgeVisibility.Self);
			kb.Tell((Name)"Hobby(Saitama)", "super-hero", false, KnowledgeVisibility.Self);
			kb.Tell((Name)"Hobby(Goku)", "training", true, KnowledgeVisibility.Universal);

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

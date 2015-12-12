using System;
using System.Collections.Generic;
using System.Diagnostics;
using EmotionalAppraisal;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.Interfaces;
using EmotionalAppraisal.OCCModel;
using NUnit.Framework;
using KnowledgeBase.WellFormedNames;
using System.IO;
using GAIPS.Serialization;
using KnowledgeBase;

namespace Tests.EmotionalAppraisal
{
	[TestFixture]
	public class SerializationTests
	{
		private static EmotionalAppraisalAsset BuildTestAsset()
		{//Emotional System Setup
			var m_emotionalAppraisalAsset = new EmotionalAppraisalAsset();
			m_emotionalAppraisalAsset.Perspective = "Test";

			//Setup Emotional Disposition

			var loveDisposition = new EmotionDisposition(OCCEmotionType.Love.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(loveDisposition);

			var hateDisposition = new EmotionDisposition(OCCEmotionType.Hate.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(hateDisposition);

			var hopeDisposition = new EmotionDisposition(OCCEmotionType.Hope.Name, 8, 4);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(hopeDisposition);

			var fearDisposition = new EmotionDisposition(OCCEmotionType.Fear.Name, 2, 1);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(fearDisposition);

			var satisfactionDisposition = new EmotionDisposition(OCCEmotionType.Satisfaction.Name, 8, 5);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(satisfactionDisposition);

			var reliefDisposition = new EmotionDisposition(OCCEmotionType.Relief.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(reliefDisposition);

			var fearsConfirmedDisposition = new EmotionDisposition(OCCEmotionType.FearsConfirmed.Name, 2, 1);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(fearsConfirmedDisposition);

			var disapointmentDisposition = new EmotionDisposition(OCCEmotionType.Disappointment.Name, 5, 2);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(disapointmentDisposition);

			var joyDisposition = new EmotionDisposition(OCCEmotionType.Joy.Name, 2, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(joyDisposition);

			var distressDisposition = new EmotionDisposition(OCCEmotionType.Distress.Name, 2, 1);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(distressDisposition);

			var happyForDisposition = new EmotionDisposition(OCCEmotionType.HappyFor.Name, 5, 2);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(happyForDisposition);

			var pittyDisposition = new EmotionDisposition(OCCEmotionType.Pitty.Name, 2, 2);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(pittyDisposition);

			var resentmentDisposition = new EmotionDisposition(OCCEmotionType.Resentment.Name, 2, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(resentmentDisposition);

			var gloatingDisposition = new EmotionDisposition(OCCEmotionType.Gloating.Name, 8, 5);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(gloatingDisposition);

			var prideDisposition = new EmotionDisposition(OCCEmotionType.Pride.Name, 5, 5);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(prideDisposition);

			var shameDisposition = new EmotionDisposition(OCCEmotionType.Shame.Name, 2, 1);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(shameDisposition);

			var gratificationDisposition = new EmotionDisposition(OCCEmotionType.Gratification.Name, 8, 5);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(gratificationDisposition);

			var remorseDisposition = new EmotionDisposition(OCCEmotionType.Remorse.Name, 2, 1);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(remorseDisposition);

			var admirationDisposition = new EmotionDisposition(OCCEmotionType.Admiration.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(admirationDisposition);

			var reproachDisposition = new EmotionDisposition(OCCEmotionType.Reproach.Name, 8, 2);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(reproachDisposition);

			var gratitudeDisposition = new EmotionDisposition(OCCEmotionType.Gratitude.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(gratitudeDisposition);

			var angerDisposition = new EmotionDisposition(OCCEmotionType.Anger.Name, 5, 3);
			m_emotionalAppraisalAsset.EmotionalState.AddEmotionDisposition(angerDisposition);

			//Setup appraisal rules

			Reaction petReaction = new Reaction();
			petReaction.Desirability = 10;
			petReaction.Like = 7;
			m_emotionalAppraisalAsset.AddEmotionalReaction(new TestEvent(Name.UNIVERSAL_STRING, "Pet", Name.SELF_STRING), petReaction);

			Reaction slapReaction = new Reaction();
			slapReaction.Desirability = -10;
			slapReaction.Like = -15;
			m_emotionalAppraisalAsset.AddEmotionalReaction(new TestEvent(Name.UNIVERSAL_STRING, "Slap", Name.SELF_STRING), slapReaction);

			Reaction feedReaction = new Reaction();
			feedReaction.Desirability = 5;
			feedReaction.Praiseworthiness = 10;
			m_emotionalAppraisalAsset.AddEmotionalReaction(new TestEvent(Name.UNIVERSAL_STRING, "Feed", Name.SELF_STRING), feedReaction);

			var madScreamEvent = new TestEvent(Name.UNIVERSAL_STRING, "Talk", null);
			var parameters1 = new List<IEventParameter>();
			parameters1.Add(new EventParameter() { ParameterName = "Volume", Value = "High" });
			parameters1.Add(new EventParameter() { ParameterName = "Tone", Value = "Mad" });
			madScreamEvent.Parameters = parameters1;
			Reaction screamMad = new Reaction();
			screamMad.Desirability = -7;
			screamMad.Praiseworthiness = -15;
			screamMad.Like = -4;
			m_emotionalAppraisalAsset.AddEmotionalReaction(madScreamEvent, screamMad);

			var talkSoftEvent = new TestEvent(Name.UNIVERSAL_STRING, "Talk", null);
			var parameters2 = new List<IEventParameter>();
			parameters2.Add(new EventParameter() { ParameterName = "Volume", Value = "Low" });
			parameters2.Add(new EventParameter() { ParameterName = "Tone", Value = "Happy" });
			talkSoftEvent.Parameters = parameters2;
			Reaction talkSoftReaction = new Reaction();
			talkSoftReaction.Praiseworthiness = 5;
			talkSoftReaction.Like = 5;
			m_emotionalAppraisalAsset.AddEmotionalReaction(talkSoftEvent, talkSoftReaction);

			//Generate emotion
			var eventToAppraise=new TestEvent("Player","Slap","Test");
			m_emotionalAppraisalAsset.AppraiseEvents(new []{eventToAppraise});

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

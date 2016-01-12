using System;
using AssetPackage;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal
{
	[Serializable]
	public sealed partial class EmotionalAppraisalAsset : BaseAsset, ICustomSerialization
	{

        public static string[] GetKnowledgeVisibilities()
        {
            return Enum.GetNames(typeof(KnowledgeVisibility));
        }

        private static readonly InternalAppraisalFrame APPRAISAL_FRAME = new InternalAppraisalFrame();
		[NonSerialized]
		private long _lastFrameAppraisal = 0;
		[NonSerialized]
		private OCCAffectDerivationComponent m_occAffectDerivator;

		public string Perspective { get; set; }

        private float m_emotionalHalfLifeDecayTime = 15;
		/// <summary>
		/// Defines how fast a emotion decay over time.
		/// This value is the actual time it takes a decay:1 emotion to reach half of its initial intensity
		/// </summary>
		public float EmotionalHalfLifeDecayTime
		{
			get { return m_emotionalHalfLifeDecayTime; }
			set { m_emotionalHalfLifeDecayTime = value < Constants.MinimumDecayTime ? Constants.MinimumDecayTime : value; }
		}

		private float m_moodDecay = 60;
		/// <summary>
		/// Defines how fast mood decay over time.
		/// This value is the actual time it takes the mood to reach half of its initial intensity
		/// </summary>
		public float MoodHalfLifeDecayTime {
			get { return m_moodDecay; }
			set { m_moodDecay = value < Constants.MinimumDecayTime ? Constants.MinimumDecayTime : value; }
		}

        private KB m_kb;
		private AM m_am;
		private ConcreteEmotionalState m_emotionalState;
		private ReactiveAppraisalDerivator m_appraisalDerivator;
		#region Component Manager

		//private HashSet<IAppraisalDerivator> m_appraisalDerivators = new HashSet<IAppraisalDerivator>();
		//private HashSet<IAffectDerivator> m_affectDerivators = new HashSet<IAffectDerivator>();
		//private HashSet<IEmotionProcessor> m_emotionalProcessors = new HashSet<IEmotionProcessor>();
		/*
		public bool AddComponent(IAppraisalDerivator component)
		{
			return m_appraisalDerivators.Add(component);
		}

		public bool RemoveComponent(IAppraisalDerivator component)
		{
			return m_appraisalDerivators.Remove(component);
		}
		
		public bool AddComponent(IAffectDerivator component)
		{
			return m_affectDerivators.Add(component);
		}

		public bool RemoveComponent(IAffectDerivator component)
		{
			return m_affectDerivators.Remove(component);
		}
		
		public bool AddComponent(IEmotionProcessor component)
		{
			return m_emotionalProcessors.Add(component);
		}

		public bool RemoveComponent(IEmotionProcessor component)
		{
			return m_emotionalProcessors.Remove(component);
		}
		*/
		#endregion

		/// <summary>
		/// Returns the agent's emotional state.
		/// </summary>
		public IEmotionalState EmotionalState
		{
			get
			{
				return m_emotionalState;
			}
		}

		//public ReactiveAppraisalDerivator AppraisalRules
		//{
		//	get
		//	{
		//		return m_appraisalDerivator;
		//	}
		//}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalReaction">the Reaction to add</param>
		public void AddEmotionalReaction(IEvent evt, Reaction emotionalReaction)
		{
			AddEmotionalReaction(evt,null,emotionalReaction);
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalReaction">the Reaction to add</param>
		public void AddEmotionalReaction(IEvent evt, ConditionEvaluatorSet conditionsEvaluator, Reaction emotionalReaction)
		{
			m_appraisalDerivator.AddEmotionalReaction(evt,Perspective, conditionsEvaluator, emotionalReaction);
		}

		public KB Kb
		{
			get { return m_kb; }
		}

		public EmotionalAppraisalAsset(string perspective)
		{
            Perspective = perspective;
			m_kb = new KB();
			m_am = new AM();
			m_am.BindCalls(m_kb);

			m_emotionalState = new ConcreteEmotionalState(this);
			m_occAffectDerivator = new OCCAffectDerivationComponent();
			m_appraisalDerivator = new ReactiveAppraisalDerivator();
		}

		public void AppraiseEvents(IEnumerable<IEvent> events)
		{
			var APPRAISAL_FRAME = new InternalAppraisalFrame();
			using (var it = events.GetEnumerator())
			{
				while (it.MoveNext())
				{
					var evt = m_am.RecordEvent(it.Current,Perspective);
					APPRAISAL_FRAME.Reset(evt);
					var componentFrame = APPRAISAL_FRAME.RequestComponentFrame(m_appraisalDerivator, m_appraisalDerivator.AppraisalWeight);
					m_appraisalDerivator.Appraisal(this, it.Current, componentFrame);
					UpdateEmotions(APPRAISAL_FRAME);
				}
			}
		}

		public void UpdateEmotions(IAppraisalFrame frame)
		{
			if (_lastFrameAppraisal >= frame.LastChange)
				return;

			var emotions = m_occAffectDerivator.AffectDerivation(this, frame);
			foreach (var emotion in emotions)
			{
				var activeEmotion = this.EmotionalState.AddEmotion(emotion);
				if (activeEmotion == null)
					continue;

				//foreach (var processor in m_emotionalProcessors)
				//{
				//	processor.EmotionActivation(this, activeEmotion);
				//}
			}

			_lastFrameAppraisal = frame.LastChange;
		}

		public void Update(float deltaTime)
		{
			if (deltaTime <= 0)
				return;

			m_emotionalState.Decay(deltaTime, this);
		}

		public void Reappraise()
		{
			var frame = m_appraisalDerivator.Reappraisal(this);
			if (frame != null)
				UpdateEmotions(frame);
		}

        public void AddBelief(string name, string value, string visibility)
	    {
	        var visibilityEnum = (KnowledgeVisibility) Enum.Parse(typeof(KnowledgeVisibility),visibility);
	        this.Kb.Tell(Name.BuildName(name), PrimitiveValue.Parse(value), false, visibilityEnum);
	    }

	    

        #region ICustomSerialization

        public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("Perspective",Perspective);
			dataHolder.SetValue("EmotionalHalfLifeDecayTime",m_emotionalHalfLifeDecayTime);
			dataHolder.SetValue("MoodHalfLifeDecayTime", m_moodDecay);
			dataHolder.SetValue("KnowledgeBase",m_kb);
			dataHolder.SetValue("AutobiographicMemory",m_am);
			dataHolder.SetValue("EmotionalState", m_emotionalState);
			dataHolder.SetValue("AppraisalRules", m_appraisalDerivator);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			Perspective = dataHolder.GetValue<string>("Perspective");
			m_emotionalHalfLifeDecayTime = dataHolder.GetValue<float>("EmotionalHalfLifeDecayTime");
			m_moodDecay = dataHolder.GetValue<float>("MoodHalfLifeDecayTime");
			m_kb = dataHolder.GetValue<KB>("KnowledgeBase");
			m_am = dataHolder.GetValue<AM>("AutobiographicMemory");
			m_am.BindCalls(m_kb);

			m_emotionalState = dataHolder.GetValue<ConcreteEmotionalState>("EmotionalState");
			m_appraisalDerivator = dataHolder.GetValue<ReactiveAppraisalDerivator>("AppraisalRules");

			m_occAffectDerivator = new OCCAffectDerivationComponent();
		}

		#endregion
	}
}

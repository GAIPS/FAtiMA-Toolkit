using System;
using AssetPackage;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.Interfaces;
using EmotionalAppraisal.OCCModel;
using System.Collections.Generic;
using GAIPS.Serialization;

namespace EmotionalAppraisal
{
	public sealed partial class EmotionalAppraisalAsset : BaseAsset
	{
		private static readonly InternalAppraisalFrame APPRAISAL_FRAME = new InternalAppraisalFrame();
		[NonSerialized]
		private long _lastFrameAppraisal = 0;
		[NonSerialized]
		private OCCAffectDerivationComponent m_occAffectDerivator;

		public string Perspective { get; set; }

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

		public ReactiveAppraisalDerivator AppraisalRules
		{
			get
			{
				return m_appraisalDerivator;
			}
		}

		public EmotionalAppraisalAsset() : this(string.Empty){}

		public EmotionalAppraisalAsset(string perspective)
		{
			Perspective = perspective;
			m_emotionalState = new ConcreteEmotionalState();
			m_occAffectDerivator = new OCCAffectDerivationComponent();
			m_appraisalDerivator = new ReactiveAppraisalDerivator();
		}

		public void AppraiseEvents(IEnumerable<IEvent> events)
		{
			using (var it = events.GetEnumerator())
			{
				while (it.MoveNext())
				{
					APPRAISAL_FRAME.Reset(it.Current);
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

			m_emotionalState.Decay(deltaTime);
		}

		public void Reappraise()
		{
			var frame = m_appraisalDerivator.Reappraisal(this);
			if (frame != null)
				UpdateEmotions(frame);
		}
	}
}

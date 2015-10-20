using AssetPackage;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.Interfaces;
using System.Collections.Generic;

namespace EmotionalAppraisal
{
	public sealed partial class EmotionalAppraisalAsset : BaseAsset
	{
		private static readonly InternalAppraisalFrame APPRAISAL_FRAME = new InternalAppraisalFrame();
		private long lastFrameAppraisal = 0;

		private ITime m_timeKeeper;

		#region Component Manager

		private HashSet<IAppraisalDerivator> m_appraisalDerivators = new HashSet<IAppraisalDerivator>();
		private HashSet<IAffectDerivator> m_affectDerivators = new HashSet<IAffectDerivator>();
		private HashSet<IEmotionProcessor> m_emotionalProcessors = new HashSet<IEmotionProcessor>();

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

		#endregion

		/// <summary>
		/// Returns the agent's emotional state.
		/// </summary>
		public IEmotionalState EmotionalState
		{
			get;
			private set;
		}

		public EmotionalAppraisalAsset(ITime timeKeeper)
		{
			m_timeKeeper = timeKeeper;
			this.EmotionalState = new ConcreteEmotionalState(timeKeeper);
		}

		public void AppraiseEvents(IEnumerable<IEvent> events)
		{
			using (IEnumerator<IEvent> it = events.GetEnumerator())
			{
				while (it.MoveNext())
				{
					APPRAISAL_FRAME.Reset(it.Current);
					foreach (var c in m_appraisalDerivators)
					{
						var componentFrame = APPRAISAL_FRAME.RequestComponentFrame(c, c.AppraisalWeight);
						c.Appraisal(this, it.Current, componentFrame);
						UpdateEmotions(APPRAISAL_FRAME);
					}
				}
			}
		}

		public void UpdateEmotions(IAppraisalFrame frame)
		{
			if (lastFrameAppraisal < frame.LastChange)
			{
				foreach (var affectDerivator in m_affectDerivators)
				{
					var emotions = affectDerivator.AffectDerivation(this, frame);
					foreach (var emotion in emotions)
					{
						var activeEmotion = this.EmotionalState.AddEmotion(emotion, m_timeKeeper);
						if (activeEmotion != null)
						{
							foreach (var processor in m_emotionalProcessors)
							{
								processor.EmotionActivation(this, activeEmotion);
							}
						}
					}
				}

				lastFrameAppraisal = frame.LastChange;
			}
		}

		public void Reappraise()
		{
			foreach (var c in m_appraisalDerivators)
			{
				IAppraisalFrame frame = c.Reappraisal(this);
				if (frame != null)
					UpdateEmotions(frame);
			}
		}
	}
}

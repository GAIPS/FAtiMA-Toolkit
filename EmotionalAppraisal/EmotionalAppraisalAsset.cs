using System;
using AssetPackage;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal.DTOs;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalAppraisal
{
	[Serializable]
	public sealed partial class EmotionalAppraisalAsset : BaseAsset, ICustomSerialization
	{
        public static EmotionalAppraisalAsset LoadFromFile(string filename)
        {
            EmotionalAppraisalAsset ea;
            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                ea = serializer.Deserialize<EmotionalAppraisalAsset>(f);
            }
            return ea;
        }

        private static readonly InternalAppraisalFrame APPRAISAL_FRAME = new InternalAppraisalFrame();

		[NonSerialized]
		private long _lastFrameAppraisal = 0;
		[NonSerialized]
		private OCCAffectDerivationComponent m_occAffectDerivator;

		public string Perspective { get; set; }


	    /// <summary>
	    /// The half-life base decay for the exponential decay lambda calculation.
	    /// To calculate the lambda, divide this constant by the required half-life time.
	    /// </summary>
	    private double m_halfLifeDecayConstant = 0.5;
        public double HalfLifeDecayConstant
	    {
            get { return m_halfLifeDecayConstant; }
            set { m_halfLifeDecayConstant = value;}
        }
        
        /// <summary>
        /// Defines how fast a emotion decay over time.
        /// This value is the actual time it takes a decay:1 emotion to reach half of its initial intensity
        /// </summary>
        private float m_emotionalHalfLifeDecayTime = 15;
        public float EmotionalHalfLifeDecayTime
		{
			get { return m_emotionalHalfLifeDecayTime; }
			set { m_emotionalHalfLifeDecayTime = value < 1 ? 1 : value; }
		}

        /// <summary>
        /// Defines how strong is the influence of the emotion's intensity
        /// on the character's mood. Since we don't want the mood to be very
        /// volatile, we only take into account 30% of the emotion's intensity
        /// </summary>
        private float _mEmotionInfluenceOnMoodFactor = 0.3f;
        public float EmotionInfluenceOnMoodFactor
	    {
	        get { return _mEmotionInfluenceOnMoodFactor;}
            set { _mEmotionInfluenceOnMoodFactor = value;}    
	    }


	    /// <summary>
	    /// Defines how strong is the influence of the current mood 
	    /// in the intensity of the emotion. We don't want the influence
	    /// of mood to be that great, so we only take into account 30% of 
	    /// the mood's value
	    /// </summary>
	    private float _mMoodInfluenceOnEmotionFactor = 0.3f;
        public float MoodInfluenceOnEmotionFactor
	    {
            get { return _mMoodInfluenceOnEmotionFactor; }
            set { _mMoodInfluenceOnEmotionFactor = value; }
        }
        
        /// <summary>
        /// Defines the minimum absolute value that mood must have,
        /// in order to be considered for influencing emotions. At the 
        /// moment, values of mood ranged in ]-0.5;0.5[ are considered
        /// to be neutral moods that do not infuence emotions
        /// </summary>
        public float MMinimumMoodValueForInfluencingEmotions = 0.5f;
        public float MinimumMoodValueForInfluencingEmotions
        {
            get { return MMinimumMoodValueForInfluencingEmotions; }
            set { MMinimumMoodValueForInfluencingEmotions = value; }
        }

        /// <summary>
        /// Defines how fast mood decay over time.
        /// This value is the actual time it takes the mood to reach half of its initial intensity
        /// </summary>
        private float m_moodDecay = 60;
        public float MoodHalfLifeDecayTime {
			get { return m_moodDecay; }
			set { m_moodDecay = value < 1 ? 1 : value; }
		}

        private KB m_kb;
		private AM m_am;
		private ConcreteEmotionalState m_emotionalState;
		private ReactiveAppraisalDerivator m_appraisalDerivator;
	
		/// <summary>
		/// Returns the agent's emotional state. TODO:remove (cannot return smart objects)
		/// </summary>
		public IEmotionalState EmotionalState
		{
			get
			{
				return m_emotionalState;
			}
		}
        
        /// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
		public void AddEmotionalReaction(AppraisalRule emotionalAppraisalRule)
		{
			m_appraisalDerivator.AddEmotionalReaction(emotionalAppraisalRule);
		}

	    public IEnumerable<EmotionDispositionDTO> GetEmotionDispositions()
	    {
	        return this.EmotionalState.GetEmotionDispositions().Select(disp => new EmotionDispositionDTO
	        {
	            Emotion = disp.Emotion,
	            Decay = disp.Decay,
	            Threshold = disp.Threshold
	        });
	    } 

        public IEnumerable<AppraisalRuleDTO> GetAllAppraisalRules()
        {
            var appraisalRules = this.m_appraisalDerivator.GetAppraisalRules().Select(r => new AppraisalRuleDTO
            {
                Id = r.Id,
                EventName = r.EventName.ToString(),
                Desirability = r.Desirability,
                Praiseworthiness = r.Praiseworthiness,
                Conditions = r.Conditions.Select(c => new ConditionDTO {Condition = c.ToString()})
            });
            return appraisalRules;
        }

	    public IEnumerable<IEventRecord> GetAllEventRecords()
	    {
	        return this.m_am.RecallAllEvents();
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
			BindCalls(m_kb);
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

        public void AddOrUpdateBelief(BeliefDTO belief)
	    {
	        this.Kb.Tell(Name.BuildName(belief.Name), PrimitiveValue.Parse(belief.Value), true, belief.Visibility);
	    }

	    public string GetBeliefValue(string beliefName)
	    {
            var result = this.Kb.AskProperty(Name.BuildName(beliefName)).ToString();
	        return result;
	    }

        public bool BeliefExists(string name)
        {
            return this.Kb.BeliefExists(Name.BuildName(name));
        }
		private void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(MOOD_TEMPLATE, MoodPropertyCalculator, new[] { "x" });
			kb.RegistDynamicProperty(STRONGEST_EMOTION_TEMPLATE, StrongestEmotionCalculator, new[] { "x" });
			kb.RegistDynamicProperty(EMOTION_INTENSITY_TEMPLATE, EmotionIntensityPropertyCalculator, new[] { "x","y" });
		}

        public void RemoveBelief(string name)
        {
            this.Kb.Retract(Name.BuildName(name));
        }

	    public void SetMood(float value)
	    {
	       this.m_emotionalState.SetMood(value);
	    }
        
        #region Dynamic Properties
        private static readonly Name MOOD_TEMPLATE = (Name)"Mood([x])";
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> MoodPropertyCalculator(KB kb, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name arg = args["x"];
			if (arg.IsVariable)
			{
				var sub = new Substitution(arg, Name.SELF_SYMBOL);
				foreach (var c in constraints)
				{
					if (c.AddSubstitution(sub))
						yield return Tuples.Create((PrimitiveValue)EmotionalState.Mood, c);	
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(arg, constraints))
				{
					var name = Name.BuildName(resultPair.Item1);
					if (name == Name.SELF_SYMBOL)
					{
						var v = (PrimitiveValue) EmotionalState.Mood;
						foreach (var c in resultPair.Item2)
						{
							yield return Tuples.Create(v, c);
						}
					}
				}
			}
		}

		private static readonly Name STRONGEST_EMOTION_TEMPLATE = (Name)"StrongestEmotion([x])";
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> StrongestEmotionCalculator(KB kb, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name arg = args["x"];

			var emo = EmotionalState.GetStrongestEmotion();
			if(emo==null)
				yield break;

			var emoValue = (PrimitiveValue) emo.EmotionType;

			if (arg.IsVariable)
			{
				var sub = new Substitution(arg, Name.SELF_SYMBOL);
				foreach (var c in constraints)
				{
					if (c.AddSubstitution(sub))
						yield return Tuples.Create(emoValue, c);
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(arg, constraints))
				{
					var name = Name.BuildName(resultPair.Item1);
					if (name != Name.SELF_SYMBOL)
						continue;

					foreach (var c in resultPair.Item2)
						yield return Tuples.Create(emoValue, c);
				}
			}
		}

		private static readonly Name EMOTION_INTENSITY_TEMPLATE = (Name) "EmotionIntensity([x],[y])";
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EmotionIntensityPropertyCalculator(KB kb, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name entity = args["x"];
			Name emotionName = args["y"];

			List<Pair<PrimitiveValue, SubstitutionSet>> result = new List<Pair<PrimitiveValue, SubstitutionSet>>();
			if (entity.IsVariable)
			{
				var newSub = new Substitution(entity,Name.SELF_SYMBOL);
				var newC = constraints.Where(c => c.AddSubstitution(newSub));
				if(newC.Any())
					result.AddRange(GetEmotionsForEntity(EmotionalState, emotionName, kb, newC));
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(entity,constraints))
				{
					Name entityName = Name.BuildName(resultPair.Item1);
					if(entityName!=Name.SELF_SYMBOL)
						continue;
					result.AddRange(GetEmotionsForEntity(EmotionalState, emotionName, kb, resultPair.Item2));
				}
			}
			return result;
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> GetEmotionsForEntity(IEmotionalState state,
			Name emotionName, KB kb, IEnumerable<SubstitutionSet> constraints)
		{
			if (emotionName.IsVariable)
			{
				foreach (var emotion in state.GetAllEmotions())
				{
					var sub = new Substitution(emotionName,(Name)emotion.EmotionType);
					foreach (var c in constraints)
					{
						if(c.Conflicts(sub))
							continue;

						var newConstraints = new SubstitutionSet(c);
						newConstraints.AddSubstitution(sub);
						yield return Tuples.Create((PrimitiveValue)emotion.Intensity, newConstraints);
					}
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(emotionName,constraints))
				{
					string emotionKey = resultPair.Item1.ToString();
					var emotion = state.GetEmotionsByType(emotionKey).OrderByDescending(e => e.Intensity).FirstOrDefault();
					PrimitiveValue value = emotion == null ? 0 : emotion.Intensity;
					foreach (var c in resultPair.Item2)
						yield return Tuples.Create(value, c);
				}
			}
		}

		#endregion

		public void SaveToFile(Stream file)
	    {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
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
			BindCalls(m_kb);
		}

		#endregion
	}
}

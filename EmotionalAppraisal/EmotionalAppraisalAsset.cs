using System;
using AssetPackage;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.DTOs.Conditions;
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
        private KB m_kb;
        private AM m_am;
        private ConcreteEmotionalState m_emotionalState;
        private ReactiveAppraisalDerivator m_appraisalDerivator;

        #region Constants
        /// <summary>
        /// The half-life base decay for the exponential decay lambda calculation.
        /// To calculate the lambda, divide this constant by the required half-life time.
        /// </summary>
        public double HalfLifeDecayConstant { get; private set; } = 0.5;
        
	    /// <summary>
	    /// Defines how strong is the influence of the emotion's intensity
	    /// on the character's mood. Since we don't want the mood to be very
	    /// volatile, we only take into account 30% of the emotion's intensity
	    /// </summary>
	    public float EmotionInfluenceOnMoodFactor { get; private set; } = 0.3f;

	    /// <summary>
	    /// Defines how strong is the influence of the current mood 
	    /// in the intensity of the emotion. We don't want the influence
	    /// of mood to be that great, so we only take into account 30% of 
	    /// the mood's value
	    /// </summary>
	    public float MoodInfluenceOnEmotionFactor { get; private set; } = 0.3f;

        /// <summary>
        /// Defines the minimum absolute value that mood must have,
        /// in order to be considered for influencing emotions. At the 
        /// moment, values of mood ranged in ]-0.5;0.5[ are considered
        /// to be neutral moods that do not infuence emotions
        /// </summary>
        public double MinimumMoodValueForInfluencingEmotions { get; private set; } = 0.5;

        /// <summary>
        /// Defines how fast a emotion decay over time.
        /// This value is the actual time it takes a decay:1 emotion to reach half of its initial intensity
        /// </summary>
        public float EmotionalHalfLifeDecayTime { get; private set; } = 15;

        /// <summary>
        /// Defines how fast mood decay over time.
        /// This value is the actual time it takes the mood to reach half of its initial intensity
        /// </summary>
        public float MoodHalfLifeDecayTime { get; private set; } = 60;

        #endregion
        
        [NonSerialized]
		private long _lastFrameAppraisal = 0;
		[NonSerialized]
		private OCCAffectDerivationComponent m_occAffectDerivator;

        /// <summary>
        /// Indicates the name of the agent that corresponds to "SELF"
        /// </summary>
		public string Perspective {
	        get { return m_kb.Perspective.ToString(); }
	        set { m_kb.Perspective = Name.BuildName(value); }
		}

	    /// <summary>
	    /// Indicates the current time tick
	    /// </summary>
	    public ulong Tick {
		    get { return m_am.Tick; }
		    set { m_am.Tick = value; }
	    }

        /// <summary>
	    /// The emotional mood of the agent, which can vary from -10 to 10
	    /// </summary>
	    public float Mood
	    {
	        get { return m_emotionalState.Mood; }
            set { m_emotionalState.Mood = value; }
	    }


	    public string[] KnowledgeVisibilities => new[] {Name.SELF_STRING, Name.UNIVERSAL_STRING};
        public string[] EventTypes => Enum.GetNames(typeof (EventType));
        

	    public EmotionDispositionDTO DefaultEmotionDisposition
	    {
	        get{ return m_emotionalState.DefaultEmotionDisposition.ToDto();}
	        set { m_emotionalState.DefaultEmotionDisposition = new EmotionDisposition(value); }
	    }

	    public IEnumerable<EmotionDTO> ActiveEmotions
	    {
	        get { return m_emotionalState.GetAllEmotions().Select(e => e.ToDto(m_am));}
	    }

	    public EmotionDTO AddActiveEmotion(EmotionDTO emotion)
	    {
            return m_emotionalState.AddActiveEmotion(emotion);
	    }

        public void RemoveEmotion(EmotionDTO emotion)
        {
            m_emotionalState.RemoveEmotion(emotion);
        }

        public IEnumerable<EmotionDispositionDTO> EmotionDispositions
        {
            get { return m_emotionalState.GetEmotionDispositions().Select(disp => disp.ToDto()); }
        }

        public IEnumerable<EventDTO> EventRecords
        {
            get
            {
                return this.m_am.RecallAllEvents().Select(e => new EventDTO
                {
                    Id = e.Id,
                    Event = e.EventName.ToString(),
                    Time = e.Timestamp
                });
            }
        }

        public IEnumerable<string> EmotionTypes
	    {
	        get { return OCCEmotionType.Types; }
	    } 

        /// <summary>
        /// Adds an emotional reaction to an event
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
        public void AddOrUpdateAppraisalRule(AppraisalRuleDTO emotionalAppraisalRule)
		{
			m_appraisalDerivator.AddOrUpdateAppraisalRule(emotionalAppraisalRule);
		}

        public void AddAppraisalRuleCondition(Guid appraisalRuleId, ConditionDTO conditionDTO)
        {
            m_appraisalDerivator.AddAppraisalRuleCondition(appraisalRuleId, conditionDTO);
        }
        
        public uint AddEventRecord(EventDTO eventDTO)
	    {
	        return this.m_am.RecordEvent(this.BuildEventName(eventDTO), eventDTO.Time).Id;
	    }

        public void AddEmotionDisposition(EmotionDispositionDTO emotionDispositionDto)
	    {
	        m_emotionalState.AddEmotionDisposition(new EmotionDisposition(emotionDispositionDto));
	    } 

		public IEnumerable<IActiveEmotion> GetAllActiveEmotions()
		{
			return m_emotionalState.GetAllEmotions();
		}

	    public EmotionDispositionDTO GetEmotionDisposition(string emotionType)
	    {
	        return this.m_emotionalState.GetEmotionDisposition(emotionType).ToDto();
	    }

	    public void RemoveEmotionDisposition(string emotionType)
	    {
	        m_emotionalState.RemoveEmotionDisposition(emotionType);
	    }

        public IEnumerable<AppraisalRuleDTO> GetAllAppraisalRules()
        {
            var appraisalRules = this.m_appraisalDerivator.GetAppraisalRules().Select(r => new AppraisalRuleDTO
            {
                Id = r.Id,
                EventMatchingTemplate = r.EventName.ToString(),
                Desirability = r.Desirability,
                Praiseworthiness = r.Praiseworthiness,
                Conditions = r.Conditions.Select(c => new ConditionDTO {Condition = c.ToString()}).ToList()
            });
            return appraisalRules;
        }

	    private Name BuildEventName(EventDTO evt)
	    {
	        var actionEvent = evt as ActionEventDTO;
	        if (actionEvent != null)
	        {
                return Name.BuildName(
	                (Name) "Event",
	                (Name) "Action",
	                (Name) actionEvent.Subject,
	                (Name) actionEvent.Action,
	                (Name) actionEvent.Target);
	        }
	        else
	        {
                var pcEvent = evt as PropertyChangeEventDTO;
                return Name.BuildName(
                    (Name)"Event",
                    (Name)"Property-Change",
                    (Name)pcEvent.Subject,
                    (Name)pcEvent.Property,
                    (Name)pcEvent.NewValue);
            }
        }

	    public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
	    {
	        foreach (var appraisalRuleDto in appraisalRules)
	        {
	            m_appraisalDerivator.RemoveAppraisalRule(new AppraisalRule(appraisalRuleDto));
	        }
	    } 

        public KB Kb
		{
			get { return m_kb; }
		}

		public EmotionalAppraisalAsset(string perspective)
		{
			m_kb = new KB((Name)perspective);
			m_am = new AM();
			m_am.BindCalls(m_kb);

			m_emotionalState = new ConcreteEmotionalState(this);
			m_occAffectDerivator = new OCCAffectDerivationComponent();
			m_appraisalDerivator = new ReactiveAppraisalDerivator();
			BindCalls(m_kb);
		}

		public void AppraiseEvents(IEnumerable<Name> eventNames)
		{
			var APPRAISAL_FRAME = new InternalAppraisalFrame();
			foreach (var n in eventNames)
			{
				var evtN = n.RemovePerspective(Perspective);
				var evt = m_am.RecordEvent(evtN,Tick);
                
				/*if (evt.EventType.Equals("property-change", StringComparison.InvariantCultureIgnoreCase))
				{
					var fact = evt.EventObject;
					var value = (Name)evt.Target;
					m_kb.Tell(fact, value.GetPrimitiveValue(), Name.SELF_SYMBOL);
				}*/

				APPRAISAL_FRAME.Reset(evt);
				var componentFrame = APPRAISAL_FRAME.RequestComponentFrame(m_appraisalDerivator, m_appraisalDerivator.AppraisalWeight);
				m_appraisalDerivator.Appraisal(this, evt, componentFrame);
				UpdateEmotions(APPRAISAL_FRAME);
			}
		}

		private void UpdateEmotions(IAppraisalFrame frame)
		{
            if (_lastFrameAppraisal >= frame.LastChange)
				return;

			var emotions = m_occAffectDerivator.AffectDerivation(this, frame);
			foreach (var emotion in emotions)
			{
				var activeEmotion = m_emotionalState.AddEmotion(emotion);
				if (activeEmotion == null)
					continue;

				//foreach (var processor in m_emotionalProcessors)
				//{
				//	processor.EmotionActivation(this, activeEmotion);
				//}
			}

			_lastFrameAppraisal = frame.LastChange;
		}

		public void Update()
		{
		    this.Tick++;
			m_emotionalState.Decay();
		}

		public void Reappraise()
		{
			var frame = m_appraisalDerivator.Reappraisal(this);
			if (frame != null)
				UpdateEmotions(frame);
		}

        public void AddOrUpdateBelief(BeliefDTO belief)
	    {
	        this.Kb.Tell(Name.BuildName(belief.Name), PrimitiveValue.Parse(belief.Value));
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
            this.Kb.Tell(Name.BuildName(name),null,Name.SELF_SYMBOL);
        }
        
        
        #region Dynamic Properties

        private static readonly Name MOOD_TEMPLATE = (Name)"Mood([x])";
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> MoodPropertyCalculator(KB kb, Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			if(perspective != Name.SELF_SYMBOL)
				yield break;

			Name arg = args["x"];
			if (arg.IsVariable)
			{
				var sub = new Substitution(arg, Name.SELF_SYMBOL);
				foreach (var c in constraints)
				{
					if (c.AddSubstitution(sub))
						yield return Tuples.Create((PrimitiveValue)m_emotionalState.Mood, c);	
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(arg,Name.SELF_SYMBOL, constraints))
				{
					var name = Name.BuildName(resultPair.Item1);
					if (name == Name.SELF_SYMBOL)
					{
						var v = (PrimitiveValue)m_emotionalState.Mood;
						foreach (var c in resultPair.Item2)
						{
							yield return Tuples.Create(v, c);
						}
					}
				}
			}
		}

		private static readonly Name STRONGEST_EMOTION_TEMPLATE = (Name)"StrongestEmotion([x])";
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> StrongestEmotionCalculator(KB kb,Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			if(perspective != Name.SELF_SYMBOL)
				yield break;

			Name arg = args["x"];
			var emo = m_emotionalState.GetStrongestEmotion();
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
				foreach (var resultPair in kb.AskPossibleProperties(arg,Name.SELF_SYMBOL, constraints))
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
		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> EmotionIntensityPropertyCalculator(KB kb,Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			List<Pair<PrimitiveValue, SubstitutionSet>> result = new List<Pair<PrimitiveValue, SubstitutionSet>>();
			if (perspective != Name.SELF_SYMBOL)
				return result;

			Name entity = args["x"];
			Name emotionName = args["y"];
			
			if (entity.IsVariable)
			{
				var newSub = new Substitution(entity,Name.SELF_SYMBOL);
				var newC = constraints.Where(c => c.AddSubstitution(newSub));
				if(newC.Any())
					result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, kb, newC));
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(entity,Name.SELF_SYMBOL, constraints))
				{
					Name entityName = Name.BuildName(resultPair.Item1);
					if(entityName!=Name.SELF_SYMBOL)
						continue;
					result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, kb, resultPair.Item2));
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
				foreach (var resultPair in kb.AskPossibleProperties(emotionName,Name.SELF_SYMBOL,constraints))
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
            dataHolder.SetValue("EmotionalHalfLifeDecayTime", EmotionalHalfLifeDecayTime);
			dataHolder.SetValue("MoodHalfLifeDecayTime", MoodHalfLifeDecayTime);
            dataHolder.SetValue("HalfLifeDecayConstant", HalfLifeDecayConstant);
            dataHolder.SetValue("EmotionInfluenceOnMoodFactor", EmotionInfluenceOnMoodFactor);
            dataHolder.SetValue("MoodInfluenceOnEmotionFactor", MoodInfluenceOnEmotionFactor);
            dataHolder.SetValue("MinimumMoodValueForInfluencingEmotions", MinimumMoodValueForInfluencingEmotions);

            dataHolder.SetValue("KnowledgeBase",m_kb);
			dataHolder.SetValue("AutobiographicMemory",m_am);
			dataHolder.SetValue("EmotionalState", m_emotionalState);
			dataHolder.SetValue("AppraisalRules", m_appraisalDerivator);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
            EmotionalHalfLifeDecayTime = dataHolder.GetValue<float>("EmotionalHalfLifeDecayTime");
            MoodHalfLifeDecayTime = dataHolder.GetValue<float>("MoodHalfLifeDecayTime");
		    HalfLifeDecayConstant = dataHolder.GetValue<double>("HalfLifeDecayConstant");
            EmotionInfluenceOnMoodFactor = dataHolder.GetValue<float>("EmotionInfluenceOnMoodFactor");
            MoodInfluenceOnEmotionFactor = dataHolder.GetValue<float>("MoodInfluenceOnEmotionFactor");
            MinimumMoodValueForInfluencingEmotions = dataHolder.GetValue<double>("MinimumMoodValueForInfluencingEmotions");

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

using System;
using AssetPackage;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.OCCModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal.DTOs;
using GAIPS.Rage;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.DTOs.Conditions;
using Utilities;
using WellFormedNames;

namespace EmotionalAppraisal
{
	/// <summary>
	/// Main class of the Emotional Appraisal Asset.
	/// </summary>
	[Serializable]
	public sealed partial class EmotionalAppraisalAsset : LoadableAsset<EmotionalAppraisalAsset>, ICustomSerialization
	{
        private KB m_kb;
        private AM m_am;
        private ConcreteEmotionalState m_emotionalState;
        private ReactiveAppraisalDerivator m_appraisalDerivator;

		#region Constants
		/// <summary>
		/// The half-life base decay for the exponential decay lambda calculation.
		/// To calculate the lambda, divide this constant by the required half-life time.
		/// </summary>
		/// @hideinitializer
		public double HalfLifeDecayConstant { get; private set; } = 0.5;

		/// <summary>
		/// Defines how strong is the influence of the emotion's intensity
		/// on the character's mood. Since we don't want the mood to be very
		/// volatile, we only take into account 30% of the emotion's intensity
		/// </summary>
		/// @hideinitializer
		public float EmotionInfluenceOnMoodFactor { get; private set; } = 0.3f;

		/// <summary>
		/// Defines how strong is the influence of the current mood 
		/// in the intensity of the emotion. We don't want the influence
		/// of mood to be that great, so we only take into account 30% of 
		/// the mood's value
		/// </summary>
		/// @hideinitializer
		public float MoodInfluenceOnEmotionFactor { get; private set; } = 0.3f;

		/// <summary>
		/// Defines the minimum absolute value that mood must have,
		/// in order to be considered for influencing emotions. At the 
		/// moment, values of mood ranged in ]-0.5;0.5[ are considered
		/// to be neutral moods that do not infuence emotions
		/// </summary>
		/// @hideinitializer
		public double MinimumMoodValueForInfluencingEmotions { get; private set; } = 0.5;

		/// <summary>
		/// Defines how fast a emotion decay over time.
		/// This value is the actual time it takes for an emotion to reach half of its initial intensity
		/// </summary>
		/// @hideinitializer
		public float EmotionalHalfLifeDecayTime { get; private set; } = 15;

		/// <summary>
		/// Defines how fast mood decay over time.
		/// This value is the actual time it takes the mood to reach half of its initial intensity
		/// </summary>
		/// @hideinitializer
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
		}

		/// <summary>
		/// The amount of update ticks this asset as experienced since its initialization
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

        /// <summary>
	    /// A short description of the asset's configuration
	    /// </summary>
	    public string Description { get; set; }

		/// <summary>
		/// Gets/Sets the default emotion disposition parameters.
		/// </summary>
        public EmotionDispositionDTO DefaultEmotionDisposition
	    {
	        get{ return m_emotionalState.DefaultEmotionDisposition.ToDto();}
	        set { m_emotionalState.DefaultEmotionDisposition = new EmotionDisposition(value); }
	    }

		protected override string OnAssetLoaded()
		{
			return null;
		}

		/// <summary>
		/// Returns the current set of active emotions
		/// <returns>An enumerable containing the emotion DTOs of the currently active emotions being expressed by the asset.</returns>
		/// </summary>
	    public IEnumerable<EmotionDTO> ActiveEmotions
	    {
	        get { return m_emotionalState.GetAllEmotions().Select(e => e.ToDto(m_am));}
	    }

		/// <summary>
		/// Creates a new <b>Active Emotion</b> and adds it to the asset's currently experiencing emotions set.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// Thrown if the given emotion is already being experienced by the asset.
		/// This can happend if in the given EmotionDTO the pair of parameters <b>Type</b> and <b>CauseEventId</b>
		/// are equal to an already existent ActiveEmotion in the asset.
		/// </exception>
		/// <param name="emotion">The DTO containing the emotion parameters to be used in the active emotion creation process</param>
		/// <returns>The DTO representing the actual emotion added to the active emotion set.</returns>
		public EmotionDTO AddActiveEmotion(EmotionDTO emotion)
	    {
            return m_emotionalState.AddActiveEmotion(emotion);
	    }

		/// <summary>
		/// Removes the given emotion from the asset's active emotions set.
		/// </summary>
		/// <param name="emotion">The DTO containing the emotion parameters to be used to select and remove the requested emotion from the active emotion set.</param>
		/// <remarks>Note that only the <b>Type</b> and <b>CauseEventId</b> fields are required to select an emotion to be removed.</remarks>
		public void RemoveEmotion(EmotionDTO emotion)
        {
            m_emotionalState.RemoveEmotion(emotion);
        }

		/// <summary>
		/// The asset's currently defined Emotion Dispositions.
		/// </summary>
        public IEnumerable<EmotionDispositionDTO> EmotionDispositions
        {
            get { return m_emotionalState.GetEmotionDispositions().Select(disp => disp.ToDto()); }
        }

		/// <summary>
		/// Gets all the recorded events experienced by the asset.
		/// </summary>
        public IEnumerable<EventDTO> EventRecords
        {
            get
            {
                return this.m_am.RecallAllEvents().Select(e => e.ToDTO());
            }
        }

		/// <summary>
		/// The currently supported emotional type keys
		/// </summary>
	    public IEnumerable<string> EmotionTypes
	    {
	        get { return OCCEmotionType.Types; }
	    } 

        /// <summary>
        /// Adds an emotional reaction to an event
        /// </summary>
        /// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
        public void AddOrUpdateAppraisalRule(AppraisalRuleDTO emotionalAppraisalRule)
		{
			m_appraisalDerivator.AddOrUpdateAppraisalRule(emotionalAppraisalRule);
		}

		/// <summary>
		/// Add an Event Record to the asset's autobiographical memory
		/// </summary>
		/// <param name="eventDTO">The dto containing the information regarding the event to add</param>
		/// <returns>The unique identifier associated to the event</returns>
        public uint AddEventRecord(EventDTO eventDTO)
	    {
	        return this.m_am.RecordEvent(eventDTO).Id;
	    }

		/// <summary>
		/// Updates the associated data regarding a recorded event.
		/// </summary>
		/// <param name="eventDTO">The dto containing the information regarding the event to update. The Id field of the dto must match the id of the event we want to update.</param>
		public void UpdateEventRecord(EventDTO eventDTO)
	    {
	        this.m_am.UpdateEvent(eventDTO);
	    }

		/// <summary>
		/// Returns all the associated information regarding an event
		/// </summary>
		/// <param name="eventId">The id of the event to retrieve</param>
		/// <returns>The dto containing the information of the retrieved event</returns>
		public EventDTO GetEventDetails(uint eventId)
		{
			IBaseEvent evt = m_am.RecallEvent(eventId);
			return evt.ToDTO();
		}

		/// <summary>
		/// Removes and forgets an event
		/// </summary>
		/// <param name="eventId">The id of the event to forget.</param>
		public void ForgetEvent(uint eventId)
        {
            this.m_am.ForgetEvent(eventId);
        }

		/// <summary>
		/// Creates and adds an emotional disposition to the asset.
		/// </summary>
		/// <param name="emotionDispositionDto">The dto containing the parameters to create a new emotional disposition on the asset</param>
        public void AddEmotionDisposition(EmotionDispositionDTO emotionDispositionDto)
	    {
	        m_emotionalState.AddEmotionDisposition(new EmotionDisposition(emotionDispositionDto));
	    } 

		/// <summary>
		/// Returns the emotional dispotion associated to a given emotion type.
		/// </summary>
		/// <param name="emotionType">The emotion type key of the emotional disposition to retrieve</param>
		/// <returns>The dto containing the retrieved emotional disposition information</returns>
	    public EmotionDispositionDTO GetEmotionDisposition(string emotionType)
	    {
	        return this.m_emotionalState.GetEmotionDisposition(emotionType).ToDto();
	    }

		/// <summary>
		/// Removes an emotional disposition from the asset.
		/// </summary>
		/// <param name="emotionType">The emotion type key of the emotional disposition to remove</param>
		public void RemoveEmotionDisposition(string emotionType)
	    {
	        m_emotionalState.RemoveEmotionDisposition(emotionType);
	    }

		/// <summary>
		/// Returns all the appraisal rules
		/// </summary>
		/// <returns>The set of dtos containing the information for all the appraisal rules</returns>
        public IEnumerable<AppraisalRuleDTO> GetAllAppraisalRules()
        {
            var appraisalRules = this.m_appraisalDerivator.GetAppraisalRules().Select(r => new AppraisalRuleDTO
            {
                Id = r.Id,
                EventMatchingTemplate = r.EventName.ToString(),
                Desirability = r.Desirability,
                Praiseworthiness = r.Praiseworthiness,
				Conditions = r.Conditions.ToDTO()
            });
            return appraisalRules;
        }

		/// <summary>
		/// Returns the condition set used for evaluating a particular appraisal rule set.
		/// </summary>
		/// <param name="ruleId">The unique identifier of the appraisal rule.</param>
		/// <returns>The dto of the condition set associated to the requested appraisal rule.</returns>
		/// <exception cref="ArgumentException">Thrown if the requested appraisal rule could not be found.</exception>
	    public ConditionSetDTO GetAllAppraisalRuleConditions(Guid ruleId)
		{
			var rule = m_appraisalDerivator.GetAppraisalRule(ruleId);
			if (rule == null)
				throw new ArgumentException($"Could not found requested appraisal rule for the id \"{ruleId}\"",nameof(ruleId));

		    return rule.Conditions.ToDTO();
	    }

		/// <summary>
		/// Removes appraisal rules from the asset.
		/// </summary>
		/// <param name="appraisalRules">A dto set of the appraisal rules to remove</param>
	    public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
	    {
	        foreach (var appraisalRuleDto in appraisalRules)
	        {
	            m_appraisalDerivator.RemoveAppraisalRule(new AppraisalRule(appraisalRuleDto));
	        }
	    }

		/// @cond DEV

		public IEnumerable<IActiveEmotion> GetAllActiveEmotions()
		{
			return m_emotionalState.GetAllEmotions();
		}

		public KB Kb
		{
			get { return m_kb; }
		}

		/// @endcond

		/// <summary>
		/// Change the perspective of the memories of the asset.
		/// Use this to change "name" which the asset identifies as itself.
		/// </summary>
		/// <param name="newPerspective">The string containing the new perspective of the asset.</param>
		public void SetPerspective(string newPerspective)
		{
			var p = (Name) newPerspective;
			if(p==m_kb.Perspective)
				return;

			m_am.SwapPerspective(m_kb.Perspective,p);
			m_kb.SetPerspective(p);
		}

		/// <summary>
		/// Asset constructor.
		/// Creates a new empty Emotional Appraisal Asset.
		/// </summary>
		/// <param name="perspective">The initial perspective of the asset.</param>
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

		/// <summary>
		/// Appraises a set of event strings.
		/// 
		/// Durring appraisal, the events will be recorded in the asset's autobiographical memory,
		/// and Property Change Events will update the asset's knowledge about the world, allowing
		/// the asset to use the new information derived from the events to appraise the correspondent
		/// emotions.
		/// </summary>
		/// <param name="eventNames">A set of string representation of the events to appraise</param>
		public void AppraiseEvents(IEnumerable<string> eventNames)
		{
			AppraiseEvents(eventNames.Select(Name.BuildName));
		}

		public void AppraiseEvents(IEnumerable<Name> eventNames)
		{
			var APPRAISAL_FRAME = new InternalAppraisalFrame();
			foreach (var n in eventNames)
			{
				var evtN = n.RemovePerspective((Name)Perspective);
				var evt = m_am.RecordEvent(evtN, Tick);

				var propEvt = evt as IPropertyChangedEvent;
				if (propEvt != null)
				{
					var fact = propEvt.Property;
					var value = (Name)propEvt.NewValue;
					m_kb.Tell(fact, value.GetPrimitiveValue(), Name.SELF_SYMBOL);
				}

				APPRAISAL_FRAME.Reset(evt);
				var componentFrame = APPRAISAL_FRAME.RequestComponentFrame(m_appraisalDerivator, m_appraisalDerivator.AppraisalWeight);
				m_appraisalDerivator.Appraisal(this, evt, componentFrame);
				UpdateEmotions(APPRAISAL_FRAME);
			}
		}

		/// <summary>
		/// Updates the assets internal clock of the asset and updates emotional decay
		/// </summary>
		public void Update()
		{
		    this.Tick++;
			m_emotionalState.Decay();
		}

		/// <summary>
		/// Reappraise the assets current emotional status
		/// </summary>
		/// <remarks>Currently this method is not fully developed. As such it will not create any changes to the asset's emotional state</remarks>
		public void Reappraise()
		{
			var frame = m_appraisalDerivator.Reappraisal(this);
			if (frame != null)
				UpdateEmotions(frame);
		}

		/// <summary>
		/// Adds a new belief to the asset's knowledge base.
		/// If the belief already exists, its value is updated.
		/// </summary>
		/// <param name="belief">The dto containing the parameters for the belief to add or update.</param>
        public void AddOrUpdateBelief(BeliefDTO belief)
	    {
	        Kb.Tell(Name.BuildName(belief.Name), PrimitiveValue.Parse(belief.Value),Name.BuildName(belief.Perspective));
	    }

		/// <summary>
		/// Return the value associated to a belief.
		/// Only returns believes regarding a SELF perspective.
		/// </summary>
		/// <param name="beliefName">The name of the belief to return</param>
		/// <returns>The string value of the belief, or null if no belief exists.</returns>
	    public string GetBeliefValue(string beliefName)
	    {
            var result = this.Kb.AskProperty(Name.BuildName(beliefName))?.ToString();
	        return result;
	    }

		/// <summary>
		/// Asks if the asset has a specific belief.
		/// </summary>
		/// <param name="name">The belief name to determine if any value is associated to it.</param>
		/// <returns>True if the requested belief has a value. False otherwise.</returns>
        public bool BeliefExists(string name)
        {
            return this.Kb.BeliefExists(Name.BuildName(name));
        }

		/// <summary>
		/// Removes a belief from the asset's knowledge base.
		/// </summary>
		/// <param name="name">The name of the belief to remove.</param>
		/// <param name="perspective">The perspective of the belief to remove</param>
		public void RemoveBelief(string name, string perspective)
		{
			var p = (Name) perspective;
			this.Kb.Tell(Name.BuildName(name), null, p);
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

		private void BindCalls(KB kb)
		{
			kb.RegistDynamicProperty(MOOD_TEMPLATE, MoodPropertyCalculator, new[] { "x" });
			kb.RegistDynamicProperty(STRONGEST_EMOTION_TEMPLATE, StrongestEmotionCalculator);
			kb.RegistDynamicProperty(EMOTION_INTENSITY_TEMPLATE, EmotionIntensityPropertyCalculator);
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
				var sub = new Substitution(arg, kb.Perspective);
				foreach (var c in constraints)
				{
					if (c.AddSubstitution(sub))
						yield return Tuples.Create((PrimitiveValue)m_emotionalState.Mood, c);	
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(arg,perspective, constraints))
				{
					var v = (PrimitiveValue)m_emotionalState.Mood;
					foreach (var c in resultPair.Item2)
					{
						yield return Tuples.Create(v, c);
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
				var sub = new Substitution(arg, kb.Perspective);
				foreach (var c in constraints)
				{
					if (c.AddSubstitution(sub))
						yield return Tuples.Create(emoValue, c);
				}
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(arg,perspective, constraints))
				{
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
				var newSub = new Substitution(entity,kb.Perspective);
				var newC = constraints.Where(c => c.AddSubstitution(newSub));
				if(newC.Any())
					result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, kb,perspective, newC));
			}
			else
			{
				foreach (var resultPair in kb.AskPossibleProperties(entity,perspective, constraints))
				{
					result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, kb,perspective, resultPair.Item2));
				}
			}
			return result;
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> GetEmotionsForEntity(IEmotionalState state,
			Name emotionName, KB kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
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
				foreach (var resultPair in kb.AskPossibleProperties(emotionName,perspective,constraints))
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

		/// @cond DEV
		#region ICustomSerialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
            dataHolder.SetValue("Description", Description);
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

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
            Description = dataHolder.GetValue<string>("Description");
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
		/// @endcond
	}
}
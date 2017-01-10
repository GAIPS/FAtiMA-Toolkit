using System;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using AutobiographicMemory;
using EmotionalAppraisal.DTOs;
using KnowledgeBase;
using SocialImportance;
using Utilities;
using WellFormedNames;
using CommeillFaut;
using AutobiographicMemory.DTOs;
using SerializationUtilities;
using GAIPS.Rage;
using AssetPackage;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>, IDynamicPropertiesRegister, ICustomSerialization
    {
        private static readonly Name DefaultCharacterName = (Name)"Nameless";

        private string _emotionalAppraisalAssetSource = null;
        private string _emotionalDecisionMakingAssetSource = null;
        private string _socialImportanceAssetSource = null;
        private string _commeillFautAssetSource = null;


        /// <summary>
        /// The name of the character
        /// </summary>
        public Name CharacterName
        {
            get { return m_kb.Perspective; }
            set { m_kb.Perspective = value; }
        }


        /// <summary>
        /// An identifier for the embodiment that is used by the character
        /// </summary>
        public string BodyName { get; set; }

        /// <summary>
        /// An identifier for the voice that is used by the character
        /// </summary>
        public string VoiceName { get; set; }

        /// <summary>
		/// The amount of update ticks this asset as experienced since its initialization
		/// </summary>
		public ulong Tick
        {
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
            return m_emotionalState.AddActiveEmotion(emotion, m_am);
        }

        public void RemoveEmotion(EmotionDTO emotion)
        {
            m_emotionalState.RemoveEmotion(emotion, m_am);
        }

        public IEnumerable<EmotionDTO> GetAllActiveEmotions()
        {
            return m_emotionalState.GetAllEmotions().Select(e => e.ToDto(m_am));
        }
        
        /// <summary>
        /// The source being used for the Emotional Appraisal Asset
        /// </summary>
        public string EmotionalAppraisalAssetSource
        {
            get { return ToAbsolutePath(_emotionalAppraisalAssetSource); }
            set { _emotionalAppraisalAssetSource = ToRelativePath(value); }
        }

        /// <summary>
        /// The source being used for the Emotional Decision Making Asset
        /// </summary>
        public string EmotionalDecisionMakingSource
        {
            get { return ToAbsolutePath(_emotionalDecisionMakingAssetSource); }
            set { _emotionalDecisionMakingAssetSource = ToRelativePath(value); }
        }

        /// <summary>
        /// The source being used for the Social Importance Asset
        /// </summary>
        public string SocialImportanceAssetSource
        {
            get { return ToAbsolutePath(_socialImportanceAssetSource); }
            set { _socialImportanceAssetSource = ToRelativePath(value); }
        }

        public string CommeillFautAssetSource
        {
            get { return ToAbsolutePath(_commeillFautAssetSource); }
            set { _commeillFautAssetSource = ToRelativePath(value); }
        }

        protected override string OnAssetLoaded()
        {
            return null;
        }

        protected override void OnAssetPathChanged(string oldpath)
        {
			if(!string.IsNullOrEmpty(_emotionalAppraisalAssetSource))
            _emotionalAppraisalAssetSource = ToRelativePath(AssetFilePath,
                ToAbsolutePath(oldpath, _emotionalAppraisalAssetSource));

			if (!string.IsNullOrEmpty(_emotionalDecisionMakingAssetSource))
				_emotionalDecisionMakingAssetSource = ToRelativePath(AssetFilePath,
                ToAbsolutePath(oldpath, _emotionalDecisionMakingAssetSource));

			if (!string.IsNullOrEmpty(_socialImportanceAssetSource))
				_socialImportanceAssetSource = ToRelativePath(AssetFilePath,
                ToAbsolutePath(oldpath, _socialImportanceAssetSource));

			if (!string.IsNullOrEmpty(_commeillFautAssetSource))
				_commeillFautAssetSource = ToRelativePath(AssetFilePath,
                ToAbsolutePath(oldpath, _commeillFautAssetSource));
        }


        public void Initialize()
        {
            //Reset state
            m_kb = new KB(m_kb.Perspective);
            m_am = new AM();
            m_emotionalState = new ConcreteEmotionalState();

            EmotionalAppraisalAsset ea = Loader(_emotionalAppraisalAssetSource, () => new EmotionalAppraisalAsset(this.CharacterName.ToString()));
            ea.SetPerspective(CharacterName.ToString());
            EmotionalDecisionMakingAsset edm = Loader(_emotionalDecisionMakingAssetSource, () => new EmotionalDecisionMakingAsset());
            SocialImportanceAsset si = Loader(_socialImportanceAssetSource, () => new SocialImportanceAsset());
            CommeillFautAsset cfa = Loader(_commeillFautAssetSource, () => new CommeillFautAsset());

            if (ea != null)
            {
                foreach (var bel in ea.GetAllBeliefs())
                {
                    var name = Name.BuildName(bel.Name).SwapTerms(ea.Perspective, CharacterName);
                    var value = Name.BuildName(bel.Value).SwapTerms(ea.Perspective, CharacterName);
                    m_kb.Tell(name, value, (Name)bel.Perspective);
                }
            }

            _emotionalAppraisalAsset = ea;
            _emotionalDecisionMakingAsset = edm;
            _socialImportanceAsset = si;
            _commeillFautAsset = cfa;

            //Dynamic properties
            BindToRegistry(m_kb);
            edm.RegisterKnowledgeBase(m_kb);
            si.RegisterKnowledgeBase(m_kb);
        }

        private T Loader<T>(string path, Func<T> generateDefault) where T : LoadableAsset<T>
        {
            if (string.IsNullOrEmpty(path))
                return generateDefault();

            return LoadableAsset<T>.LoadFromFile(ToAbsolutePath(path));
        }

        #region RolePlayCharater Fields

        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;
        private SocialImportanceAsset _socialImportanceAsset;
        private CommeillFautAsset _commeillFautAsset;

        private KB m_kb;
        private AM m_am;
        private ConcreteEmotionalState m_emotionalState;

        private IAction _currentAction = null;

        #endregion

        
        public RolePlayCharacterAsset()
        {
            m_kb = new KB(DefaultCharacterName);
            m_am = new AM();
            m_emotionalState = new ConcreteEmotionalState();
            BindToRegistry(m_kb);
        }
       
        /// <summary>
        /// Adds or updates a logical belief to the character that consists of a property-value pair
        /// </summary>
        /// <param name="propertyName">A wellformed name representing a logical property (e.g. IsPerson(John))</param>
        /// <param name="value">The value of the property</param>
        [Obsolete]
        public void AddBelief(string propertyName, string value)
        {
            m_kb.Tell(Name.BuildName(propertyName), Name.BuildName(value), Name.SELF_SYMBOL);
        }

        /// <summary>
        /// Retrieves the character's strongest emotion if any.
        /// </summary>
        public IActiveEmotion GetStrongestActiveEmotion()
        {
            IEnumerable<IActiveEmotion> currentActiveEmotions = m_emotionalState.GetAllEmotions();
            return currentActiveEmotions.MaxValue(a => a.Intensity);
        }


        /// <summary>
        /// Returns all the associated information regarding an event
        /// </summary>
        /// <param name="eventId">The id of the event to retrieve</param>
        /// <returns>The dto containing the information of the retrieved event</returns>
        public EventDTO GetEventDetails(uint eventId)
        {
            return this.m_am.RecallEvent(eventId).ToDTO();
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
        /// Removes and forgets an event
        /// </summary>
        /// <param name="eventId">The id of the event to forget.</param>
        public void ForgetEvent(uint eventId)
        {
            this.m_am.ForgetEvent(eventId);
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

        public IEnumerable<BeliefDTO> GetAllBeliefs()
        {
            return m_kb.GetAllBeliefs().Select(b => new BeliefDTO
            {
                Name = b.Name.ToString(),
                Perspective = b.Perspective.ToString(),
                Value = b.Value.ToString()
            });
        }

        /// <summary>
        /// Return the value associated to a belief.
        /// </summary>
        /// <param name="beliefName">The name of the belief to return</param>
        /// <returns>The string value of the belief, or null if no belief exists.</returns>
        public string GetBeliefValue(string beliefName, string perspective = Name.SELF_STRING)
        {
            var result = m_kb.AskProperty((Name)beliefName, (Name)perspective)?.ToString();
            return result;
        }

        /// <summary>
        /// Executes an iteration of the character's decision cycle.
        /// </summary>
        /// <param name="events">A list of new events that occurred since the last call to this method. Each event must be represented by a well formed name with the following format "EVENT([type], [subject], [param1], [param2])". 
        /// For illustration purposes here are some examples: EVENT(Property-Change, John, CurrentRole(Customer), False) ; EVENT(Action-Finished, John, Open, Box)</param>
        /// <returns>The action selected for execution or "null" otherwise</returns>
        public IAction PerceptionActionLoop(IEnumerable<Name> events)
        {
            _socialImportanceAsset.InvalidateCachedSI();

            _emotionalAppraisalAsset.AppraiseEvents(events, m_emotionalState, m_am, m_kb);
       
            if (_currentAction != null)
                return null;

            var possibleActions = _emotionalDecisionMakingAsset.Decide();
            var sociallyAcceptedActions = _socialImportanceAsset.FilterActions(Name.SELF_STRING, possibleActions);
            var conferralAction = _socialImportanceAsset.DecideConferral(Name.SELF_STRING);
            if (conferralAction != null)
                sociallyAcceptedActions = sociallyAcceptedActions.Append(conferralAction);

            _currentAction = TakeBestActions(sociallyAcceptedActions).Shuffle().FirstOrDefault();
            if (_currentAction != null)
            {
                var e = _currentAction.ToStartEventName(Name.SELF_SYMBOL);
                _emotionalAppraisalAsset.AppraiseEvents(new[] { e }, m_emotionalState, m_am, m_kb);
            }

            return _currentAction;
        }

        /// <summary>
        /// Updates the character's internal state. Should be called once every game tick.
        /// </summary>
        public void Update()
        {
            Tick++;
            m_emotionalState.Decay(Tick);
        }

        /// <summary>
        /// Method used to inform the character that its current action is finished and a new action may be selected. It can also generate an emotion associated to finishing an action successfully.
        /// </summary>
	    public void ActionFinished(IAction action)
        {
            if (_currentAction == null)
                throw new Exception("The RPC asset is not currently executing an action");

            if (!_currentAction.Equals(action))
                throw new ArgumentException("The given action mismatches the currently executing action.", nameof(action));

            var e = _currentAction.ToFinishedEventName(Name.SELF_SYMBOL);
            _emotionalAppraisalAsset.AppraiseEvents(new[] { e }, m_emotionalState, m_am);
            _currentAction = null;
        }

        public IDynamicPropertiesRegistry DynamicPropertiesRegistry => m_kb;


        public void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(MOOD_PROPERTY_NAME, MoodPropertyCalculator, "The current mood value for agent [x]");
            registry.RegistDynamicProperty(STRONGEST_EMOTION_PROPERTY_NAME, StrongestEmotionCalculator, "The type of the current strongest emotion that agent [x] is feeling.");
            registry.RegistDynamicProperty(EMOTION_INTENSITY_TEMPLATE, EmotionIntensityPropertyCalculator, "The intensity value for the emotion felt by agent [x] of type [y].");
            m_kb.BindToRegistry(registry);
            m_am.BindToRegistry(registry);
            if(_socialImportanceAsset != null) 
                _socialImportanceAsset.BindToRegistry(registry);
        }

        public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
        {
            m_kb.UnbindToRegistry(registry);
            _socialImportanceAsset.UnbindToRegistry(registry);
        }

        private static IEnumerable<IAction> TakeBestActions(IEnumerable<IAction> enumerable)
        {
            float best = float.NegativeInfinity;
            foreach (var a in enumerable.OrderByDescending(a => a.Utility))
            {
                if (a.Utility < best)
                    break;

                yield return a;
                best = a.Utility;
            }
        }


        #region Dynamic Properties

        private static readonly Name MOOD_PROPERTY_NAME = (Name)"Mood";
        private IEnumerable<DynamicPropertyResult> MoodPropertyCalculator(IQueryContext context, Name x)
        {
            if (context.Perspective != Name.SELF_SYMBOL)
                yield break;

            if (x.IsVariable)
            {
                var sub = new Substitution(x, context.Perspective);
                foreach (var c in context.Constraints)
                {
                    if (c.AddSubstitution(sub))
                        yield return new DynamicPropertyResult(Name.BuildName(m_emotionalState.Mood), c);
                }
            }
            else
            {
                foreach (var resultPair in context.AskPossibleProperties(x))
                {
                    var v = m_emotionalState.Mood;
                    foreach (var c in resultPair.Item2)
                    {
                        yield return new DynamicPropertyResult(Name.BuildName(v), c);
                    }
                }
            }
        }

        private static readonly Name STRONGEST_EMOTION_PROPERTY_NAME = (Name)"StrongestEmotion";
        private IEnumerable<DynamicPropertyResult> StrongestEmotionCalculator(IQueryContext context, Name x)
        {
            if (context.Perspective != Name.SELF_SYMBOL)
                yield break;

            var emo = m_emotionalState.GetStrongestEmotion();
            if (emo == null)
                yield break;

            var emoValue = emo.EmotionType;

            if (x.IsVariable)
            {
                var sub = new Substitution(x, context.Perspective);
                foreach (var c in context.Constraints)
                {
                    if (c.AddSubstitution(sub))
                        yield return new DynamicPropertyResult((Name)emoValue, c);
                }
            }
            else
            {
                foreach (var resultPair in context.AskPossibleProperties(x))
                {
                    foreach (var c in resultPair.Item2)
                        yield return new DynamicPropertyResult((Name)emoValue, c);
                }
            }
        }

        private static readonly Name EMOTION_INTENSITY_TEMPLATE = (Name)"EmotionIntensity";
        private IEnumerable<DynamicPropertyResult> EmotionIntensityPropertyCalculator(IQueryContext context, Name x, Name y)
        {
            List<DynamicPropertyResult> result = new List<DynamicPropertyResult>();
            if (context.Perspective != Name.SELF_SYMBOL)
                return result;

            Name entity = x;
            Name emotionName = y;

            if (entity.IsVariable)
            {
                var newSub = new Substitution(entity, context.Perspective);
                var newC = context.Constraints.Where(c => c.AddSubstitution(newSub));
                if (newC.Any())
                    result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, context.Queryable, context.Perspective, newC));
            }
            else
            {
                foreach (var resultPair in context.AskPossibleProperties(entity))
                {
                    result.AddRange(GetEmotionsForEntity(m_emotionalState, emotionName, context.Queryable, context.Perspective, resultPair.Item2));
                }
            }
            return result;
        }

        private IEnumerable<DynamicPropertyResult> GetEmotionsForEntity(IEmotionalState state,
            Name emotionName, WellFormedNames.IQueryable kb, Name perspective, IEnumerable<SubstitutionSet> constraints)
        {
            if (emotionName.IsVariable)
            {
                foreach (var emotion in state.GetAllEmotions())
                {
                    var sub = new Substitution(emotionName, (Name)emotion.EmotionType);
                    foreach (var c in constraints)
                    {
                        if (c.Conflicts(sub))
                            continue;

                        var newConstraints = new SubstitutionSet(c);
                        newConstraints.AddSubstitution(sub);
                        yield return new DynamicPropertyResult(Name.BuildName(emotion.Intensity), newConstraints);
                    }
                }
            }
            else
            {
                foreach (var resultPair in kb.AskPossibleProperties(emotionName, perspective, constraints))
                {
                    string emotionKey = resultPair.Item1.ToString();
                    var emotion = state.GetEmotionsByType(emotionKey).OrderByDescending(e => e.Intensity).FirstOrDefault();
                    float value = emotion?.Intensity ?? 0;
                    foreach (var c in resultPair.Item2)
                        yield return new DynamicPropertyResult(Name.BuildName(value), c);
                }
            }
        }



        #endregion

        public void SaveStateToFile(string filepath)
        {
            var storage = GetInterface<IDataStorage>();
            if (storage == null)
                throw new Exception($"No {nameof(IDataStorage)} defined in the AssetManager bridge.");
            var json = SERIALIZER.SerializeToJson(this);
            storage.Save(filepath, json.ToString(true));
        }

        /// @cond DEV
        #region ICustomSerialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("KnowledgeBase", m_kb);
            dataHolder.SetValue("BodyName", this.BodyName);
            dataHolder.SetValue("VoiceName", this.VoiceName);
            dataHolder.SetValue("EmotionalAppraisalAssetSource", this._emotionalAppraisalAssetSource);
            dataHolder.SetValue("EmotionalDecisionMakingSource", this._emotionalDecisionMakingAssetSource);
            dataHolder.SetValue("SocialImportanceAssetSource", this._socialImportanceAssetSource);
            dataHolder.SetValue("CommeillFautAssetSource", this._commeillFautAssetSource);
            dataHolder.SetValue("EmotionalState", m_emotionalState);
            dataHolder.SetValue("AutobiographicMemory", m_am);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            m_kb = dataHolder.GetValue<KB>("KnowledgeBase");
            this.BodyName = dataHolder.GetValue<string>("BodyName");
            this.VoiceName = dataHolder.GetValue<string>("VoiceName");
            this._emotionalAppraisalAssetSource = dataHolder.GetValue<string>("EmotionalAppraisalAssetSource");
            this._emotionalDecisionMakingAssetSource = dataHolder.GetValue<string>("EmotionalDecisionMakingSource");
            this._socialImportanceAssetSource = dataHolder.GetValue<string>("SocialImportanceAssetSource");
            this._commeillFautAssetSource = dataHolder.GetValue<string>("CommeillFautAssetSource");
            m_emotionalState = dataHolder.GetValue<ConcreteEmotionalState>("EmotionalState");
            m_am = dataHolder.GetValue<AM>("AutobiographicMemory");
            BindToRegistry(m_kb);
        }

        /// @endcond
        #endregion
    }
}
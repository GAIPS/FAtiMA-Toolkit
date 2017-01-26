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
using RolePlayCharacter;
using EmotionalDecisionMaking.DTOs;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed class RolePlayCharacterAsset : LoadableAsset<RolePlayCharacterAsset>, ICustomSerialization
    {


        private string m_emotionalAppraisalAssetSource = null;
        private string m_emotionalDecisionMakingAssetSource = null;
        private string m_socialImportanceAssetSource = null;
        private string m_commeillFautAssetSource = null;

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
		/// The name of the action that the character is currently executing
		/// </summary>
		public Name CurrentActionName { get; set; }

        /// <summary>
        /// The target of the action that the character is currently executing
        /// </summary>
        public Name CurrentActionTarget { get; set; }

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
            get { return ToAbsolutePath(m_emotionalAppraisalAssetSource); }
            set { m_emotionalAppraisalAssetSource = ToRelativePath(value); }
        }

        /// <summary>
        /// The source being used for the Emotional Decision Making Asset
        /// </summary>
        public string EmotionalDecisionMakingSource
        {
            get { return ToAbsolutePath(m_emotionalDecisionMakingAssetSource); }
            set { m_emotionalDecisionMakingAssetSource = ToRelativePath(value); }
        }

        /// <summary>
        /// The source being used for the Social Importance Asset
        /// </summary>
        public string SocialImportanceAssetSource
        {
            get { return ToAbsolutePath(m_socialImportanceAssetSource); }
            set { m_socialImportanceAssetSource = ToRelativePath(value); }
        }

        public string CommeillFautAssetSource
        {
            get { return ToAbsolutePath(m_commeillFautAssetSource); }
            set { m_commeillFautAssetSource = ToRelativePath(value); }
        }

        protected override string OnAssetLoaded()
        {
            return null;
        }

        protected override void OnAssetPathChanged(string oldpath)
        {
			if(!string.IsNullOrEmpty(m_emotionalAppraisalAssetSource))
				m_emotionalAppraisalAssetSource = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, m_emotionalAppraisalAssetSource));

			if (!string.IsNullOrEmpty(m_emotionalDecisionMakingAssetSource))
				m_emotionalDecisionMakingAssetSource = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, m_emotionalDecisionMakingAssetSource));

			if (!string.IsNullOrEmpty(m_socialImportanceAssetSource))
				m_socialImportanceAssetSource = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, m_socialImportanceAssetSource));

			if (!string.IsNullOrEmpty(m_commeillFautAssetSource))
				m_commeillFautAssetSource = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, m_commeillFautAssetSource));
        }


        public void Initialize()
        {
            //Reset state
            m_kb = new KB(m_kb.Perspective);
            m_am = new AM();
            m_emotionalState = new ConcreteEmotionalState();

            EmotionalAppraisalAsset ea = Loader(m_emotionalAppraisalAssetSource, () => new EmotionalAppraisalAsset(this.CharacterName.ToString()));
            ea.SetPerspective(CharacterName.ToString());
            EmotionalDecisionMakingAsset edm = Loader(m_emotionalDecisionMakingAssetSource, () => new EmotionalDecisionMakingAsset());
            SocialImportanceAsset si = Loader(m_socialImportanceAssetSource, () => new SocialImportanceAsset());
            CommeillFautAsset cfa = Loader(m_commeillFautAssetSource, () => new CommeillFautAsset());

            if (ea != null)
            {
                foreach (var bel in ea.GetAllBeliefs())
                {
                    var name = Name.BuildName(bel.Name).SwapTerms(ea.Perspective, CharacterName);
                    var value = Name.BuildName(bel.Value).SwapTerms(ea.Perspective, CharacterName);
                    m_kb.Tell(name, value, (Name)bel.Perspective);
                }
            }

            m_emotionalAppraisalAsset = ea;
            m_emotionalDecisionMakingAsset = edm;
            m_socialImportanceAsset = si;
            m_commeillFautAsset = cfa;

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

        private EmotionalAppraisalAsset m_emotionalAppraisalAsset;
        private EmotionalDecisionMakingAsset m_emotionalDecisionMakingAsset;
        private SocialImportanceAsset m_socialImportanceAsset;
        private CommeillFautAsset m_commeillFautAsset;

        private KB m_kb;
        private AM m_am;
        private ConcreteEmotionalState m_emotionalState;

	    private Dictionary<Name, AgentEntry> _knownAgents;

        
        #endregion

        public RolePlayCharacterAsset()
        {
            m_kb = new KB(Consts.DEFAULT_CHARACTER_NAME);
            m_am = new AM();
            m_emotionalState = new ConcreteEmotionalState();
			_knownAgents = new Dictionary<Name, AgentEntry>();
			BindToRegistry(m_kb);
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
        public IEnumerable<IAction> PerceptionActionLoop(IEnumerable<Name> events)
        {
            InternalEventAppraisal(events);
            
            if (CurrentActionName != null)
                return new IAction[] { new ActionLibrary.Action(new Name[] { (Name)"Busy", CurrentActionName }, CurrentActionTarget) };

            var possibleActions = m_emotionalDecisionMakingAsset.Decide();
            var sociallyAcceptedActions = m_socialImportanceAsset.FilterActions(Name.SELF_STRING, possibleActions);
            var conferralAction = m_socialImportanceAsset.DecideConferral(Name.SELF_STRING);
            if (conferralAction != null)
                sociallyAcceptedActions = sociallyAcceptedActions.Append(conferralAction);
            
            return TakeBestActions(sociallyAcceptedActions).Shuffle();
        }

        private void InternalEventAppraisal(IEnumerable<Name> events)
        {
            m_socialImportanceAsset.InvalidateCachedSI();

            foreach (var e in events.Select(e => e.RemoveSelfPerspective(m_kb.Perspective)))
            {
                if(Consts.ACTION_START_EVENT_PROTOTYPE.Match(e))
                {
                    var subject = e.GetNTerm(2);
                    if (subject == this.CharacterName)
                    {
                        CurrentActionName = e.GetNTerm(3);
                        CurrentActionTarget = e.GetNTerm(4);
                    }
                }
                if (Consts.ACTION_FINISHED_EVENT_PROTOTYPE.Match(e))
                {
                    var evt = EventHelper.ActionEnd(this.CharacterName.ToString(), CurrentActionName?.ToString(), CurrentActionTarget?.ToString());
                    if (evt.Match(e))
                        CurrentActionName = null;
                        CurrentActionTarget = null; 
                }
                else if (Consts.EVENT_MATCHING_AGENT_ADDED.Match(e))
                {
                    var n = e.GetNTerm(3);
                    var n2 = m_kb.AskProperty(n);
                    if (!_knownAgents.ContainsKey(n2))
                        _knownAgents.Add(n2, new AgentEntry(n2));
                }
                else if (Consts.EVENT_MATCHING_AGENT_REMOVED.Match(e))
                {
                    var n = e.GetNTerm(3);
                    var n2 = m_kb.AskProperty(n);
                    _knownAgents.Remove(n2);
                }
            }
            m_emotionalAppraisalAsset.AppraiseEvents(events, m_emotionalState, m_am, m_kb);
        }

        /// <summary>
        /// Updates the character's internal state. Should be called once every game tick.
        /// </summary>
        public void Update()
        {
            Tick++;
            m_emotionalState.Decay(Tick);
        }

        public IDynamicPropertiesRegistry DynamicPropertiesRegistry => m_kb;

		private void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(Consts.MOOD_PROPERTY_NAME, MoodPropertyCalculator);
            registry.RegistDynamicProperty(Consts.STRONGEST_EMOTION_PROPERTY_NAME, StrongestEmotionCalculator);
            registry.RegistDynamicProperty(EMOTION_INTENSITY_TEMPLATE, EmotionIntensityPropertyCalculator);
			registry.RegistDynamicProperty(IS_AGENT_TEMPLATE,IsAgentPropertyCalculator);
            m_am.BindToRegistry(registry);
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

		private static readonly Name IS_AGENT_TEMPLATE = (Name) "IsAgent";
		private IEnumerable<DynamicPropertyResult> IsAgentPropertyCalculator(IQueryContext context, Name x)
		{
			if (context.Perspective != Name.SELF_SYMBOL)
				yield break;

			if (x.IsVariable)
			{
				foreach (var s in _knownAgents.Keys.Select(n => new Substitution(x, n)))
				{
					foreach (var set in context.Constraints)
					{
						if(!set.AddSubstitution(s))
							continue;

						yield return new DynamicPropertyResult(Name.BuildName(true),set);
					}
				}
				yield break;
			}

			foreach (var prop in context.AskPossibleProperties(x))
			{
				if (_knownAgents.ContainsKey(prop.Item1))
				{
					foreach (var p in prop.Item2)
					{
						yield return new DynamicPropertyResult(prop.Item1, p);
					}
				}
			}
		}

		#endregion

        /// @cond DEV
        #region ICustomSerialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("KnowledgeBase", m_kb);
            dataHolder.SetValue("BodyName", this.BodyName);
            dataHolder.SetValue("VoiceName", this.VoiceName);
            dataHolder.SetValue("EmotionalAppraisalAssetSource", this.m_emotionalAppraisalAssetSource);
            dataHolder.SetValue("EmotionalDecisionMakingSource", this.m_emotionalDecisionMakingAssetSource);
            dataHolder.SetValue("SocialImportanceAssetSource", this.m_socialImportanceAssetSource);
            dataHolder.SetValue("CommeillFautAssetSource", this.m_commeillFautAssetSource);
            dataHolder.SetValue("EmotionalState", m_emotionalState);
            dataHolder.SetValue("AutobiographicMemory", m_am);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            m_kb = dataHolder.GetValue<KB>("KnowledgeBase");
            this.BodyName = dataHolder.GetValue<string>("BodyName");
            this.VoiceName = dataHolder.GetValue<string>("VoiceName");
            this.m_emotionalAppraisalAssetSource = dataHolder.GetValue<string>("EmotionalAppraisalAssetSource");
            this.m_emotionalDecisionMakingAssetSource = dataHolder.GetValue<string>("EmotionalDecisionMakingSource");
            this.m_socialImportanceAssetSource = dataHolder.GetValue<string>("SocialImportanceAssetSource");
            this.m_commeillFautAssetSource = dataHolder.GetValue<string>("CommeillFautAssetSource");
            m_emotionalState = dataHolder.GetValue<ConcreteEmotionalState>("EmotionalState");
            m_am = dataHolder.GetValue<AM>("AutobiographicMemory");
            BindToRegistry(m_kb);
        }

        /// @endcond
        #endregion
    }
}
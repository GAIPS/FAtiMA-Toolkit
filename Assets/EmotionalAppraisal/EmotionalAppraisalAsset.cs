using AutobiographicMemory;
using Conditions.DTOs;
using EmotionalAppraisal.AppraisalRules;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using GAIPS.Rage;
using KnowledgeBase;
using SerializationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using WellFormedNames;

namespace EmotionalAppraisal
{
    /// <summary>
    /// Main class of the Emotional Appraisal Asset.
    /// </summary>
    [Serializable]
    public sealed partial class EmotionalAppraisalAsset : Asset<EmotionalAppraisalAsset>, ICustomSerialization
    {
        private ReactiveAppraisalDerivator m_appraisalDerivator;
        private EmotionDisposition m_defaultEmotionalDisposition;
        private Dictionary<string, EmotionDisposition> m_emotionDispositions;

        [NonSerialized]
        private OCCAffectDerivationComponent m_occAffectDerivator;

        /// <summary>
        /// Asset constructor.
        /// Creates a new empty Emotional Appraisal Asset.
        /// </summary>
        /// <param name="perspective">The initial perspective of the asset.</param>
        public EmotionalAppraisalAsset()
        {
            m_emotionDispositions = new Dictionary<string, EmotionDisposition>();
            m_defaultEmotionalDisposition = new EmotionDisposition("*", 1, 0);
            m_occAffectDerivator = new OCCAffectDerivationComponent();
            m_appraisalDerivator = new ReactiveAppraisalDerivator();
        }

        public EmotionDispositionDTO DefaultEmotionDisposition
        {
            get { return m_defaultEmotionalDisposition.ToDto(); }
            set { m_defaultEmotionalDisposition = new EmotionDisposition(value); }
        }

        /// <summary>
	    /// A short description of the asset's configuration
	    /// </summary>
	    public string Description { get; set; }

        /// <summary>
        /// The asset's currently defined Emotion Dispositions.
        /// </summary>
        public IEnumerable<EmotionDispositionDTO> EmotionDispositions
        {
            get { return m_emotionDispositions.Values.Select(disp => disp.ToDto()); }
        }

        /// <summary>
        /// The currently supported emotional type keys
        /// </summary>
        public static IEnumerable<string> EmotionTypes => OCCEmotionType.Types;

        /// <summary>
        /// Creates and adds an emotional disposition to the asset.
        /// </summary>
        /// <param name="emotionDispositionDto">The dto containing the parameters to create a new emotional disposition on the asset</param>
        public void AddEmotionDisposition(EmotionDispositionDTO emotionDispositionDto)
        {
            var disp = new EmotionDisposition(emotionDispositionDto);
            this.m_emotionDispositions.Add(disp.Emotion, disp);
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
        /// Appraises a set of event strings.
        ///
        /// Durring appraisal, the events will be recorded in the asset's autobiographical memory,
        /// and Property Change Events will update the asset's knowledge about the world, allowing
        /// the asset to use the new information derived from the events to appraise the correspondent
        /// emotions.
        /// </summary>
        /// <param name="eventNames">A set of string representation of the events to appraise</param>
        public void AppraiseEvents(IEnumerable<Name> eventNames, Name perspective, IEmotionalState emotionalState, AM am, KB kb, Dictionary<string, Goal> goals)
        {
            foreach (var n in eventNames)
            {
                var evt = am.RecordEvent(n, am.Tick);
                var propEvt = evt as IPropertyChangedEvent;
                if (propEvt != null)
                {
                    var fact = propEvt.Property;
                    var value = Name.BuildName("-");
                    if (propEvt.NewValue.IsPrimitive)
                    {
                        value = propEvt.NewValue;
                        if (value.ToString() == "-")
                        {
                            var remove = kb.GetAllBeliefs().Where(x => x.Name == fact);
                            kb.removeBelief(fact);
                        }
                        else
                        {
                            kb.Tell(fact, value, perspective);
                        }
                    }
                    else // new value is not grounded
                    {
                        var values = kb.AskPossibleProperties(propEvt.NewValue, perspective, new List<SubstitutionSet>());
                        if (values.Count() == 1)
                        {
                            kb.Tell(fact, values.FirstOrDefault().Item1.Value, perspective);
                        }
                        else if (values.Count() == 0) throw new Exception("No value was found for the property:" + propEvt.Property); 
                        else throw new Exception("Multiple possible values for:" + propEvt.NewValue); ;
                    }
                }
                var appraisalFrame = new InternalAppraisalFrame();
                appraisalFrame.Perspective = kb.Perspective;
                appraisalFrame.AppraisedEvent = evt;
                m_appraisalDerivator.Appraisal(kb, evt, appraisalFrame);
                UpdateEmotions(appraisalFrame, goals, emotionalState, am);
            }
        }

        public void AppraiseEvents(IEnumerable<Name> eventNames, IEmotionalState emotionalState, AM am, KB kb, Dictionary<string, Goal> goals)
        {
            AppraiseEvents(eventNames, Name.SELF_SYMBOL, emotionalState, am, kb, goals);
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
                throw new ArgumentException($"Could not found requested appraisal rule for the id \"{ruleId}\"", nameof(ruleId));

            return rule.Conditions.ToDTO();
        }

        /// <summary>
        /// Returns all the appraisal rules
        /// </summary>
        /// <returns>The set of dtos containing the information for all the appraisal rules</returns>
        public IEnumerable<AppraisalRuleDTO> GetAllAppraisalRules()
        {
            return this.m_appraisalDerivator.GetAppraisalRules().Select(r => new AppraisalRuleDTO
            {
                Id = r.Id,
                EventMatchingTemplate = r.EventName,
                AppraisalVariables = r.AppraisalVariables,
                Conditions = r.Conditions.ToDTO()
            });
        }
           

        /// <summary>
        /// Returns the emotional dispotion associated to a given emotion type.
        /// </summary>
        /// <param name="emotionType">The emotion type key of the emotional disposition to retrieve</param>
        /// <returns>The dto containing the retrieved emotional disposition information</returns>
        public EmotionDispositionDTO GetEmotionDisposition(String emotionName)
        {
            EmotionDisposition disp;
            if (this.m_emotionDispositions.TryGetValue(emotionName, out disp))
                return disp.ToDto();

            return m_defaultEmotionalDisposition.ToDto();
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("Description", Description);
            dataHolder.SetValue("AppraisalRules", m_appraisalDerivator);
        }

        /// <summary>
        /// Removes appraisal rules from the asset.
        /// </summary>
        /// <param name="appraisalRules">A dto set of the appraisal rules to remove</param>
        public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
        {
            foreach (var appraisalRuleDto in appraisalRules)
            {
                m_appraisalDerivator.RemoveAppraisalRule(appraisalRuleDto.Id);
            }
        }

        /// <summary>
        /// Removes an emotional disposition from the asset.
        /// </summary>
        /// <param name="emotionType">The emotion type key of the emotional disposition to remove</param>
        public void RemoveEmotionDisposition(string emotionType)
        {
            this.m_emotionDispositions.Remove(emotionType);
        }

        /// @cond DEV
        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            Description = dataHolder.GetValue<string>("Description");
            m_appraisalDerivator = dataHolder.GetValue<ReactiveAppraisalDerivator>("AppraisalRules");
            m_occAffectDerivator = new OCCAffectDerivationComponent();
            m_emotionDispositions = new Dictionary<string, EmotionDisposition>();
            m_defaultEmotionalDisposition = new EmotionDisposition("*", 1, 0);
        }

        private void UpdateEmotions(IAppraisalFrame frame, Dictionary<string, Goal> goals, IEmotionalState emotionalState, AM am)
        {
            var emotions = m_occAffectDerivator.AffectDerivation(this, goals, frame);
            foreach (var emotion in emotions)
            {
                var activeEmotion = emotionalState.AddEmotion(emotion, am, GetEmotionDisposition(emotion.EmotionType), am.Tick);
                if (activeEmotion == null)
                    continue;
            }

        }

        /// @endcond
    }
}
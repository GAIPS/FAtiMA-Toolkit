using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAIPS.Rage;
using WellFormedNames;
using Utilities;
using SerializationUtilities;
using KnowledgeBase;
using Conditions;
using Conditions.DTOs;
using WellFormedNames.Collections;
using IQueryable = WellFormedNames.IQueryable;
using WorldModel.DTOs;
using AutobiographicMemory;

namespace WorldModel
{
    [Serializable]
    public sealed partial class WorldModelAsset : LoadableAsset<WorldModelAsset>
    {


        private StateModifierDerivator m_stateDerivator;


        protected override string OnAssetLoaded()
        {
            return null;
        }

        public void AddOrUpdateStateModifier(StateModifierDTO emotionalAppraisalRule)
        {
            m_stateDerivator.AddOrUpdateStateModifier(emotionalAppraisalRule);
        }

        /// <summary>
        /// Returns all the appraisal rules
        /// </summary>
        /// <returns>The set of dtos containing the information for all the appraisal rules</returns>
        public IEnumerable<StateModifierDTO> GetAllStateModifier()
        {
            return this.m_stateDerivator.GetStateModifiers().Select(r => new StateModifierDTO
            {
                Id = r.Id,
                EventMatchingTemplate = r.EventName,
                Desirability = r.Desirability,
                Praiseworthiness = r.Praiseworthiness,
                Conditions = r.Conditions.ToDTO()
            });
        }

        /// <summary>
        /// Returns the condition set used for evaluating a particular state modifier.
      
        public ConditionSetDTO GetAllStateModifierConditions(Guid ruleId)
        {
            var rule = m_stateDerivator.GetStateModifier(ruleId);
            if (rule == null)
                throw new ArgumentException($"Could not found requested appraisal rule for the id \"{ruleId}\"", nameof(ruleId));

            return rule.Conditions.ToDTO();
        }

        /// <summary>
        /// Returns the effects set used to make changes to the kb of a particular state modifier.

        public ConditionSetDTO GetAllStateModifierEffects(Guid ruleId)
        {
            var rule = m_stateDerivator.GetStateModifier(ruleId);
            if (rule == null)
                throw new ArgumentException($"Could not found requested appraisal rule for the id \"{ruleId}\"", nameof(ruleId));

            return rule.Effects.ToDTO();
        }

        /// <summary>
        /// Removes state modifiers from the asset.
        /// </summary>
        /// <param name="appraisalRules">A dto set of the appraisal rules to remove</param>
        public void RemoveAppraisalRules(IEnumerable<StateModifierDTO> appraisalRules)
        {
            foreach (var StateModifierDTO in appraisalRules)
            {
                m_stateDerivator.RemoveStateModifier(new StateModifier(StateModifierDTO));
            }
        }

        public WorldModelAsset()
        {

            m_stateDerivator = new StateModifierDerivator();
        }

        #region EventProcessing

        /// <summary>
        /// Appraises a set of event strings.
        ///
        /// During appraisal, the events will be recorded in the asset's autobiographical memory,
        /// and Property Change Events will update the asset's knowledge about the world, allowing
        /// the asset to use the new information derived from the events to appraise the correspondent
        /// emotions.
        /// </summary>
        /// <param name="eventNames">A set of string representation of the events to appraise</param>
        public void AppraiseEvents(IEnumerable<Name> eventNames, Name perspective, AM am, KB kb)
        {
            foreach (var n in eventNames)
            {
                var evt = am.RecordEvent(n, am.Tick);
                m_stateDerivator.Evaluate(evt, kb, perspective);
            }
        }

        public void AppraiseEvents(IEnumerable<Name> eventNames, AM am, KB kb)
        {
            AppraiseEvents(eventNames, Name.SELF_SYMBOL, am, kb);
        }

        #endregion
    }
}

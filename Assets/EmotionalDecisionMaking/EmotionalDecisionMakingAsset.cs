using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using Conditions;
using Conditions.DTOs;
using GAIPS.Rage;
using SerializationUtilities;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;
using ActionLibrary.DTOs;
namespace EmotionalDecisionMaking
{
	/// <summary>
	/// Main class of the Emotional Decision Making Asset
	/// </summary>
	[Serializable]
    public sealed class EmotionalDecisionMakingAsset : Asset<EmotionalDecisionMakingAsset>, ICustomSerialization
    {
        public static readonly Name DEFAULT_PRIORITY = Name.BuildName(1);

        private IQueryable m_kb = null;

        private List<ActionRule> m_actionRules;

        /// <summary>
        /// Asset constructor.
        /// Creates a new empty Emotional Decision Making asset.
        /// </summary>
        public EmotionalDecisionMakingAsset()
        {
            m_actionRules = new List<ActionRule>();
        }
        
		/// <summary>
		/// Registers an Emotional Appraisal Asset to be used by
		/// this Emotional Decision Making asset.
		/// </summary>
		/// <remarks>
		/// To understand Emotional Appaisal Asset functionalities, please refer to its documentation.
		/// </remarks>
		/// <param name="eaa">The instance of an Emotional Appaisal Asset to regist in this asset.</param>
		public void RegisterKnowledgeBase(IQueryable KB)
        {
            m_kb = KB;
        }

		/// <summary>
		/// Performs the decision making process,
		/// returning the actions that the assets deems to be executed.
		/// Actual action execution is left in the responsibility of the application running this asset.
		/// </summary>
		/// <returns>The set of actions that the assets wants to execute</returns>
		/// <exception cref="Exception">Thrown if there is no Emotional Appraisal Asset registed in this asset.</exception>
        public IEnumerable<IAction> Decide(Name actionType)
        {
            if (m_kb == null)
                throw new Exception(
                    $"Unlinked to a KB. Use {nameof(RegisterKnowledgeBase)} before calling any method.");

			if (m_actionRules == null)
				return Enumerable.Empty<IAction>();

            
            var result = new List<IAction>();

            foreach (var at in m_actionRules)
            {
                if (actionType != Name.UNIVERSAL_SYMBOL && actionType != at.Layer)
                    continue;

                if (at.ActivationConditions == null) // empty condition set
                {
                    result.Add(at.GenerateAction(new SubstitutionSet()));
                }
                else
                {
                    foreach (var set in at.ActivationConditions.Unify(m_kb, Name.SELF_SYMBOL, null))
                    {
                        var a = at.GenerateAction(set);
                        if(a!=null) result.Add(a);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Adds a new reactive action to the asset.
        /// </summary>
        /// <param name="newRuleDto">The DTO containing the parameters needed to generate an action rule.</param>
        /// <returns>The unique identifier of the newly created action rule.</returns>
        public Guid AddActionRule(ActionRuleDTO newRuleDto)
        {
            var newActionRule = new ActionRule(newRuleDto);
            m_actionRules.Add(newActionRule);
            return newActionRule.Id;
        }

        /// <summary>
        /// Updates an action rule definition.
        /// </summary>
        /// <param name="ruleToEdit">The DTO of the action rule we want to update</param>
        /// <param name="newRule">The DTO containing the new action rule data</param>
        public void UpdateActionRule(ActionRuleDTO ruleToEdit, ActionRuleDTO newRule)
        {
	        newRule.Conditions = ruleToEdit.Conditions;
            var newAt = new ActionRule(newRule);
            var oldAt = m_actionRules.Where(a => a.Id == ruleToEdit.Id).FirstOrDefault();
            var i = m_actionRules.IndexOf(oldAt);
            m_actionRules[i] = new ActionRule(newRule);
        }

		public void UpdateRuleConditions(Guid ruleId, ConditionSetDTO conditionSet)
		{
			var rule = m_actionRules.Where(a => a.Id == ruleId).FirstOrDefault();
            rule.ActivationConditions = new ConditionSet(conditionSet);
		}

        /// <summary>
        /// Retrives the definitions of all the stored action rules.
        /// </summary>
        /// <returns>A set of DTOs containing the data of all action rules.</returns>
        public IEnumerable<ActionRuleDTO> GetAllActionRules()
        {
	        return m_actionRules.Select(at => at.ToDTO());
        }

        /// <summary>
        /// Retrieves the definitions of a single action rule.
        /// </summary>
        /// <param name="id">The unique identifier of the action rule to retrieve.</param>
        /// <returns>The DTO containing the data of the requested action, or null if
        /// no action rule with the given id was found.</returns>
        public ActionRuleDTO GetActionRule(Guid id)
        {
            return m_actionRules.Where(a => a.Id == id).FirstOrDefault()?.ToDTO();
        }

        /// <summary>
        /// Removes a set of action rules from the asset.
        /// </summary>
        /// <param name="rulesToRemove">A set of unique identifiers of the action rules we want to remove.</param>
        public void RemoveActionRules(IList<Guid> rulesToRemove)
        {
            foreach (var id in rulesToRemove)
            {
                var r = m_actionRules.Where(a => a.Id == id).FirstOrDefault();
                this.m_actionRules.Remove(r);
            }
        }

        /// <summary>
        /// Adds a new activation condition to a action rule.
        /// </summary>
        /// <param name="selectedRuleId">The unique identifier of the action rule we want to modify.</param>
        /// <param name="newCondition">The condition we want to add to the requested action rule.</param>
        public void AddRuleCondition(Guid selectedRuleId, string newCondition)
        {
            var r = m_actionRules.Where(a => a.Id == selectedRuleId).FirstOrDefault();
            var conditions = r.ActivationConditions;
            var c = Condition.Parse(newCondition);
            r.ActivationConditions = conditions.Add(c);
        }

        /// <summary>
        /// Removes a set of activation conditions from a action rule.
        /// </summary>
        /// <param name="ruleId">The unique identifier of the action rule we want to modify.</param>
        /// <param name="conditionsToRemove">The condition we want to remove from the requested action rule.</param>
        public void RemoveRuleConditions(Guid ruleId, IEnumerable<string> conditionsToRemove)
        {
            var at = m_actionRules.Where(a => a.Id == ruleId).FirstOrDefault();
            var conds = conditionsToRemove.Select(Condition.Parse).Aggregate(at.ActivationConditions, (current, c) => current.Remove(c));
			at.ActivationConditions = conds;
        }

        /// <summary>
        /// Swaps a condition from a action rule for another.
        /// </summary>
        /// <param name="ruleId">The unique identifier of the action rule we want to modify.</param>
        /// <param name="conditionToEdit">The condition of the action rule we want to be substituted.</param>
        /// <param name="newCondition">The new condition we want the action rule to have.</param>
        public void UpdateRuleCondition(Guid ruleId, string conditionToEdit, string newCondition)
        {
            this.RemoveRuleConditions(ruleId, new[] {conditionToEdit});
            this.AddRuleCondition(ruleId, newCondition);
        }
        
#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
            dataHolder.SetValue("ActionTendencies", m_actionRules.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			if(m_actionRules == null)
                m_actionRules = new List<ActionRule>();
			var ats = dataHolder.GetValue<ActionRule[]>("ActionTendencies");
			foreach (var at in ats)
				m_actionRules.Add(at);
		}

#endregion
	}

}
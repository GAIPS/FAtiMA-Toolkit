using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Rage;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	/// <summary>
	/// Main class of the Emotional Decision Making Asset
	/// </summary>
	[Serializable]
    public sealed class EmotionalDecisionMakingAsset : LoadableAsset<EmotionalDecisionMakingAsset>, ICustomSerialization
    {
        private EmotionalAppraisalAsset m_emotionalAppraisal = null;

		/// <summary>
		/// Asset constructor.
		/// Creates a new empty Emotional Decision Making asset.
		/// </summary>
		public EmotionalDecisionMakingAsset()
        {
            ReactiveActions = new ReactiveActions();
        }

		protected override string OnAssetLoaded()
		{
			return null;
		}

		private ReactiveActions ReactiveActions { get;}

		/// <summary>
		/// Registers an Emotional Appraisal Asset to be used by
		/// this Emotional Decision Making asset.
		/// </summary>
		/// <remarks>
		/// To understand Emotional Appaisal Asset functionalities, please refer to its documentation.
		/// </remarks>
		/// <param name="eaa">The instance of an Emotional Appaisal Asset to regist in this asset.</param>
		public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
        {
            m_emotionalAppraisal = eaa;
        }

		/// <summary>
		/// Performs the decision making process,
		/// returning the actions that the assets deems to be executed.
		/// Actual action execution is left in the responsibility of the application running this asset.
		/// </summary>
		/// <returns>The set of actions that the assets wants to execute</returns>
		/// <exception cref="Exception">Thrown if there is no Emotional Appraisal Asset registed in this asset.</exception>
        public IEnumerable<IAction> Decide()
        {
            if (m_emotionalAppraisal == null)
                throw new Exception(
                    $"Unlinked to a {nameof(EmotionalAppraisalAsset)}. Use {nameof(RegisterEmotionalAppraisalAsset)} before calling any method.");

			if (ReactiveActions == null)
				return Enumerable.Empty<IAction>();

            return ReactiveActions.SelectAction(m_emotionalAppraisal.Kb, Name.SELF_SYMBOL);
        }

		/// <summary>
		/// Adds a new reactive action to the asset.
		/// </summary>
		/// <param name="newReaction">The DTO containing the parameters needed to generate a reaction.</param>
		/// <returns>The unique identifier of the newly created reaction.</returns>
        public Guid AddReaction(ReactionDTO newReaction)
        {
            var newActionTendency = new ActionTendency(newReaction);
            this.ReactiveActions.AddActionTendency(newActionTendency);
            return newActionTendency.Id;
        }

		/// <summary>
		/// Updates a reaction definition.
		/// </summary>
		/// <param name="reactionToEdit">The DTO of the reaction we want to update</param>
		/// <param name="newReaction">The DTO containing the new reaction data</param>
        public void UpdateReaction(ReactionDTO reactionToEdit, ReactionDTO newReaction)
        {
	        newReaction.Conditions = reactionToEdit.Conditions;
            var newId = this.AddReaction(newReaction);
			ReactiveActions.RemoveAction(reactionToEdit.Id);
        }

		/// <summary>
		/// Retrives the definitions of all the stored reactions.
		/// </summary>
		/// <returns>A set of DTOs containing the data of all reactions.</returns>
        public IEnumerable<ReactionDTO> GetAllReactions()
        {
	        return ReactiveActions.GetAllActionTendencies().Select(at => at.ToDTO());
        }

		/// <summary>
		/// Retrieves the definitions of a single reaction.
		/// </summary>
		/// <param name="id">The unique identifier of the reaction to retrieve.</param>
		/// <returns>The DTO containing the data of the requested action, or null if
		/// no reaction with the given id was found.</returns>
        public ReactionDTO GetReaction(Guid id)
        {
            return this.ReactiveActions.GetActionTendency(id)?.ToDTO();
        }

		/// <summary>
		/// Removes a set of reactions from the asset.
		/// </summary>
		/// <param name="reactionsToRemove">A set of unique identifiers of the reactions we want to remove.</param>
        public void RemoveReactions(IList<Guid> reactionsToRemove)
        {
            foreach (var id in reactionsToRemove)
            {
                this.ReactiveActions.RemoveAction(id);
            }
        }

		/// <summary>
		/// Adds a new activation condition to a reaction.
		/// </summary>
		/// <param name="selectedReactionId">The unique identifier of the reaction we want to modify.</param>
		/// <param name="newCondition">The condition we want to add to the requested reaction.</param>
        public void AddReactionCondition(Guid selectedReactionId, string newCondition)
        {
            var conditions = this.ReactiveActions.GetActionTendency(selectedReactionId).ActivationConditions;
            var c = Condition.Parse(newCondition);
            this.ReactiveActions.GetActionTendency(selectedReactionId).ActivationConditions = conditions.Add(c);
        }

		/// <summary>
		/// Removes a set of activation conditions from a reaction.
		/// </summary>
		/// <param name="selectedReactionId">The unique identifier of the reaction we want to modify.</param>
		/// <param name="conditionsToRemove">The condition we want to remove from the requested reaction.</param>
		public void RemoveReactionConditions(Guid selectedReactionId, IEnumerable<string> conditionsToRemove)
        {
	        var at = this.ReactiveActions.GetActionTendency(selectedReactionId);
	        var conds = at.ActivationConditions;
			foreach (var condition in conditionsToRemove)
			{
				var c = Condition.Parse(condition);
				conds = conds.Remove(c);
            }
	        at.ActivationConditions = conds;
        }

		/// <summary>
		/// Swaps a condition from a reaction for another.
		/// </summary>
		/// <param name="selectedReactionID">The unique identifier of the reaction we want to modify.</param>
		/// <param name="conditionToEdit">The condition of the reaction we want to be substituted.</param>
		/// <param name="newCondition">The new condition we want the reaction to have.</param>
		public void UpdateRectionCondition(Guid selectedReactionID, string conditionToEdit, string newCondition)
        {
            this.RemoveReactionConditions(selectedReactionID, new[] {conditionToEdit});
            this.AddReactionCondition(selectedReactionID, newCondition);
        }

#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			var defaultActionCooldown = ReactiveActions.DefaultActionCooldown;
			dataHolder.SetValue("DefaultActionPriority", defaultActionCooldown);
			context.PushContext();
			context.Context = defaultActionCooldown;
			dataHolder.SetValue("ActionTendencies", ReactiveActions.GetAllActionTendencies().ToArray());
			context.PopContext();
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			ReactiveActions.DefaultActionCooldown = dataHolder.GetValue<float>("DefaultActionPriority");
			context.PushContext();
			context.Context = ReactiveActions.DefaultActionCooldown;
			var ats = dataHolder.GetValue<ActionTendency[]>("ActionTendencies");
			foreach (var at in ats)
				ReactiveActions.AddActionTendency(at);
			context.PopContext();
		}

#endregion
	}

}
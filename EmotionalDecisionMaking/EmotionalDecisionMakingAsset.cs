using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ActionLibrary;
using AssetPackage;
using EmotionalAppraisal;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.DTOs.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
    public sealed class EmotionalDecisionMakingAsset : BaseAsset
    {
        private EmotionalAppraisalAsset m_emotionalDecisionMaking = null;

        public static EmotionalDecisionMakingAsset LoadFromFile(string filename)
        {
            EmotionalDecisionMakingAsset ea = new EmotionalDecisionMakingAsset();
            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                ea.ReactiveActions = serializer.Deserialize<ReactiveActions>(f);
            }
            return ea;
        }

        private ReactiveActions ReactiveActions { get; set; }

        public EmotionalDecisionMakingAsset()
        {
            ReactiveActions = new ReactiveActions();
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this.ReactiveActions);
        }

        public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
        {
            m_emotionalDecisionMaking = eaa;
        }

        public IEnumerable<IAction> Decide()
        {
            if (m_emotionalDecisionMaking == null)
                throw new Exception(
                    $"Unlinked to a {nameof(EmotionalAppraisalAsset)}. Use {nameof(RegisterEmotionalAppraisalAsset)} before calling any method.");

            if (ReactiveActions == null)
                return null;

            return ReactiveActions.SelectAction(m_emotionalDecisionMaking.Kb, Name.SELF_SYMBOL);
        }

        public Guid AddReaction(ReactionDTO newReaction)
        {
            var newActionTendency = new ActionTendency(newReaction);
            this.ReactiveActions.AddActionTendency(newActionTendency);
            return newActionTendency.Id;
        }

        public void UpdateReaction(ReactionDTO reactionToEdit, ReactionDTO newReaction)
        {
	        newReaction.Conditions = reactionToEdit.Conditions;
            var newId = this.AddReaction(newReaction);
            this.RemoveReactions(new []{reactionToEdit});
        }

        public IEnumerable<ReactionDTO> GetAllReactions()
        {
	        return ReactiveActions.GetAllActionTendencies().Select(at => at.ToDTO());
        }

        public ReactionDTO GetReaction(Guid id)
        {
            return this.ReactiveActions.GetActionTendency(id).ToDTO();
        }

        public IEnumerable<ConditionDTO> GetReactionsConditions(Guid id)
        {
            return GetReaction(id).Conditions.Set;
        }

        public void RemoveReactions(IList<ReactionDTO> reactionsToRemove)
        {
            foreach (var reaction in reactionsToRemove)
            {
                this.ReactiveActions.RemoveAction(reaction.Id);
            }
        }

        public void AddReactionCondition(Guid selectedReactionId, ConditionDTO newCondition)
        {
            var conditions = this.ReactiveActions.GetActionTendency(selectedReactionId).ActivationConditions;
            var c = Condition.Parse(newCondition.Condition);
            this.ReactiveActions.GetActionTendency(selectedReactionId).ActivationConditions = conditions.Add(c);
        }

        public void RemoveReactionConditions(Guid selectedReactionId, IEnumerable<ConditionDTO> conditionsToRemove)
        {
	        var at = this.ReactiveActions.GetActionTendency(selectedReactionId);
	        var conds = at.ActivationConditions;
			foreach (var condition in conditionsToRemove)
			{
				var c = Condition.Parse(condition.Condition);
				conds = conds.Remove(c);
            }
	        at.ActivationConditions = conds;
        }

        public void UpdateRectionCondition(Guid selectedReactionID, ConditionDTO conditionToEditDto,
            ConditionDTO newCondition)
        {
            this.RemoveReactionConditions(selectedReactionID, new[] {conditionToEditDto});
            this.AddReactionCondition(selectedReactionID, newCondition);
        }

       
    }

}
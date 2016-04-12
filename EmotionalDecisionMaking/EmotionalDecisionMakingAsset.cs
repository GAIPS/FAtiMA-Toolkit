using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetPackage;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.DTOs.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public sealed partial class EmotionalDecisionMakingAsset : BaseAsset
	{
		private EmotionalAppraisalAsset m_emotionalDecisionMaking;

        public string[] QuantifierTypes => Enum.GetNames(typeof(LogicalQuantifier));

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

        public ReactiveActions ReactiveActions { get; set; }

        public EmotionalDecisionMakingAsset()
        {
            ReactiveActions = new ReactiveActions();
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this.ReactiveActions);
        }

        public EmotionalDecisionMakingAsset(EmotionalAppraisalAsset eaa)
		{
			m_emotionalDecisionMaking = eaa;
		}

	    public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
	    {
	        m_emotionalDecisionMaking = eaa;
	    }
        
		public IEnumerable<IAction> Decide()
		{
			if (ReactiveActions == null)
				return null;

			return ReactiveActions.MakeAction(m_emotionalDecisionMaking.Kb,Name.SELF_SYMBOL);
		}


	    public IEnumerable<ConditionDTO> GetReactionsConditions(Guid reactionId)
	    {
	        var reaction = this.ReactiveActions.GetAllActionTendencies().FirstOrDefault(at => at.Id == reactionId);
	        if (reaction != null)
	        {
	            return reaction.ActivationConditions.Select(c => new ConditionDTO {Id = c.Id, Condition = c.ToString()});
            }
            return new List<ConditionDTO>();
	    }

        public IEnumerable<ReactiveActionDTO> GetAllReactiveActions()
        {
            var reactiveActions = this.ReactiveActions.GetAllActionTendencies().Select(at => new ReactiveActionDTO
            {
                Id = at.Id,
                Action = at.m_parameters == null ? at.ActionName.ToString() : Name.BuildName(new [] {at.ActionName}.Concat(at.m_parameters)).ToString(),
                Target = at.Target.ToString(),
                Cooldown = at.ActivationCooldown
            });
            return reactiveActions;
        }
    }
}
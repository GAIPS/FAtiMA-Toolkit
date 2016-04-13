using System;
using System.Collections.Generic;
using System.IO;
using ActionLibrary;
using AssetPackage;
using EmotionalAppraisal;
using EmotionalDecisionMaking.DTOs;
using GAIPS.Serialization;
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

		public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
	    {
	        m_emotionalDecisionMaking = eaa;
	    }
        
		public IEnumerable<IAction> Decide()
		{
			if(m_emotionalDecisionMaking==null)
				throw new Exception($"Unlinked to a {nameof(EmotionalAppraisalAsset)}. Use {nameof(RegisterEmotionalAppraisalAsset)} before calling any method.");

			if (ReactiveActions == null)
				return null;

			return ReactiveActions.SelectAction(m_emotionalDecisionMaking.Kb,Name.SELF_SYMBOL);
		}

        public IEnumerable<ActionTendenciesDTO> GetAllActionTendencies()
        {
	        return ReactiveActions.GetAllActionTendencies();
        }

		public IEnumerable<ConditionDTO> GetReactionsConditions(Guid id)
		{
			return ReactiveActions.GetDTOFromGUID(id).Conditions.Set;
		}
	}
}
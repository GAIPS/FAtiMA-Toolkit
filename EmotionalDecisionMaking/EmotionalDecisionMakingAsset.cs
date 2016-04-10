using System.Collections.Generic;
using System.IO;
using AssetPackage;
using EmotionalAppraisal;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace EmotionalDecisionMaking
{
	public sealed partial class EmotionalDecisionMakingAsset : BaseAsset
	{
		private EmotionalAppraisalAsset m_emotionalDecisionMaking;

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
	}
}
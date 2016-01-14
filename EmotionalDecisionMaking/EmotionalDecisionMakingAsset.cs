using System.Collections.Generic;
using AssetPackage;
using EmotionalAppraisal;

namespace EmotionalDecisionMaking
{
	public sealed partial class EmotionalDecisionMakingAsset : BaseAsset
	{
		private EmotionalAppraisalAsset m_emotionalDecisionMaking;

		public ReactiveActions ReactiveActions { get; set; }

		public EmotionalDecisionMakingAsset(EmotionalAppraisalAsset eaa)
		{
			m_emotionalDecisionMaking = eaa;
		}

		public IEnumerable<IAction> Decide()
		{
			if (ReactiveActions == null)
				return null;

			return ReactiveActions.MakeAction(m_emotionalDecisionMaking.Kb);
		}
	}
}
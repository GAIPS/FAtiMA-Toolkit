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

		public IAction Decide()
		{
			if (ReactiveActions == null)
				return null;

			return ReactiveActions.MakeAction(m_emotionalDecisionMaking.Perspective, m_emotionalDecisionMaking.Kb);
		}
	}
}
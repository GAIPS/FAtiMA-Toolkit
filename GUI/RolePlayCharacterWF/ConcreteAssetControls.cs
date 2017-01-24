using EmotionalAppraisal;
using EmotionalDecisionMaking;
using SocialImportance;

namespace RolePlayCharacterWF
{
	public class SIAssetControl : BaseAssetControl<SocialImportanceAsset,SocialImportanceWF.MainForm>{}

	public class EAAssetControl : BaseAssetControl<EmotionalAppraisalAsset,EmotionalAppraisalWF.MainForm> {}

	public class EDMAssetControl : BaseAssetControl<EmotionalDecisionMakingAsset,EmotionalDecisionMakingWF.MainForm> { }
}
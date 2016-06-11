using EmotionalAppraisal;
using EmotionalDecisionMaking;
using SocialImportance;

namespace RolePlayCharacterWF
{
	public class SIAssetControl : BaseAssetControl<SocialImportanceAsset>{}

	public class EAAssetControl : BaseAssetControl<EmotionalAppraisalAsset> {}

	public class EDMAssetControl : BaseAssetControl<EmotionalDecisionMakingAsset> { }
}
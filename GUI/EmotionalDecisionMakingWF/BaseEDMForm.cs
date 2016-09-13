using EmotionalDecisionMaking;
using GAIPS.AssetEditorTools;

namespace EmotionalDecisionMakingWF
{
	public class BaseEDMForm : BaseAssetForm<EmotionalDecisionMakingAsset>
	{
		protected sealed override EmotionalDecisionMakingAsset CreateEmptyAsset()
		{
			return new EmotionalDecisionMakingAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
			return "Emotional Decision Making Definition File (*.edm)|*.edm";
		}
	}
}
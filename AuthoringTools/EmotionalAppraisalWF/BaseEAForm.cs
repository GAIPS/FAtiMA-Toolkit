using EmotionalAppraisal;
using GAIPS.AssetEditorTools;

namespace EmotionalAppraisalWF
{
	public class BaseEAForm : BaseAssetForm<EmotionalAppraisalAsset>
	{
		protected override EmotionalAppraisalAsset CreateEmptyAsset()
		{
			return new EmotionalAppraisalAsset();
		}
		
		protected override string GetAssetFileFilters()
		{
			return "Emotional Appraisal Definition File (*.ea)|*.ea";
		}
	}
}
using EmotionalAppraisal;
using GAIPS.AssetEditorTools;

namespace EmotionalAppraisalWF
{
	public class BaseEAForm : BaseAssetForm<EmotionalAppraisalAsset>
	{
		private const string DEFAULT_PERSPECTIVE = "Nameless";

		protected override EmotionalAppraisalAsset CreateEmptyAsset()
		{
			return new EmotionalAppraisalAsset(DEFAULT_PERSPECTIVE);
		}
		
		protected override string GetAssetFileFilters()
		{
			return "Emotional Appraisal Definition File (*.ea)|*.ea";
		}
	}
}
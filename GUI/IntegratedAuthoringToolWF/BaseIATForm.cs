using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;

namespace IntegratedAuthoringToolWF
{
	public class BaseIATForm : BaseAssetForm<IntegratedAuthoringToolAsset>
	{
		protected override IntegratedAuthoringToolAsset CreateEmptyAsset()
		{
			return new IntegratedAuthoringToolAsset();
		}

		protected override string GetAssetFileFilters()
		{
			return "Integrated Authoring Definition File (*.iat)|*.iat";
		}
	}
}
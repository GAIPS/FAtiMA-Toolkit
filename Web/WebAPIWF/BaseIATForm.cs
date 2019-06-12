using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;

namespace WebAPIWF
{
	public class BaseIATForm : BaseAssetForm<IntegratedAuthoringToolAsset>
	{
		protected sealed override IntegratedAuthoringToolAsset CreateEmptyAsset()
		{
			return new IntegratedAuthoringToolAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
            return "Integrated Authoring Definition File (*.iat)|*.iat";
        }
	}
}
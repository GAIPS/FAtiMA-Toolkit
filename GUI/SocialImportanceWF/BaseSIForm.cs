using GAIPS.Rage;
using SocialImportance;
using GAIPS.AssetEditorTools;

namespace SocialImportanceWF
{
	public class BaseSIForm : BaseAssetForm<SocialImportanceAsset>
	{
		protected sealed override SocialImportanceAsset CreateEmptyAsset()
		{
			return new SocialImportanceAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
			return "Social Importance Definition File (*.si)|*.si";
		}
	}
}
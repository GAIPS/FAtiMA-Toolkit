using GAIPS.Rage;
using SocialImportance;
using GAIPS.AssetEditorTools;

namespace SocialImportanceWF
{
	public class BaseSIForm : BaseAssetForm<SocialImportanceAsset>
	{
		protected override SocialImportanceAsset CreateEmptyAsset()
		{
			return new SocialImportanceAsset();
		}

		protected override SocialImportanceAsset LoadAssetFromFile(string path)
		{
			return SocialImportanceAsset.LoadFromFile(path);
		}

		protected override void SaveAssetToFile(SocialImportanceAsset asset, string path)
		{
			asset.SaveToFile(path);
		}

		protected override string GetAssetCurrentPath(SocialImportanceAsset asset)
		{
			return asset.AssetFilePath;
		}

		protected override string GetAssetFileFilters()
		{
			return "Social Importance Definition File (*.si)|*.si";
		}
	}
}
using GAIPS.Rage;
using WorldModel;
using GAIPS.AssetEditorTools;

namespace WorldModelWF
{
	public class BaseWorldModelForm : BaseAssetForm<WorldModelAsset>
	{
		protected sealed override WorldModelAsset CreateEmptyAsset()
		{
			return new WorldModelAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
			return "World Model Definition File (*.wm)|*.wm";
		}
	}
}
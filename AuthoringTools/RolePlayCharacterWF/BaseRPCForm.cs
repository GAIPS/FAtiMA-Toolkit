using GAIPS.AssetEditorTools;
using RolePlayCharacter;

namespace RolePlayCharacterWF
{
	public class BaseRPCForm : BaseAssetForm<RolePlayCharacterAsset>
	{
		protected sealed override RolePlayCharacterAsset CreateEmptyAsset()
		{
			return new RolePlayCharacterAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
			return "Role Play Character Profile File (*.rpc)|*.rpc";
		}
	}
}
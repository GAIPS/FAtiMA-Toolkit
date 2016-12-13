using GAIPS.AssetEditorTools;
using RolePlayCharacter;

namespace RolePlayCharacterWF
{
	public class BaseRPCForm : BaseAssetForm<RolePlayCharacterProfileAsset>
	{
		protected sealed override RolePlayCharacterProfileAsset CreateEmptyAsset()
		{
			return new RolePlayCharacterProfileAsset();
		}

		protected sealed override string GetAssetFileFilters()
		{
			return "Role Play Character Profile File (*.rpcp)|*.rpcp";
		}
	}
}
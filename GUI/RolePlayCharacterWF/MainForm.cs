using System;
using GAIPS.AssetEditorTools;
using RolePlayCharacter;
using RolePlayCharacterWF.Properties;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : BaseRPCForm
    {
	    public MainForm()
        {
            InitializeComponent();
        }

		protected override void OnAssetDataLoaded(RolePlayCharacterAsset asset)
		{
			textBoxCharacterName.Text = asset.CharacterName;
			textBoxCharacterBody.Text = asset.BodyName;

			eaAssetControl1.SetAsset(asset.EmotionalAppraisalAssetSource, () =>
			{
				RequestAssetReload();
				return EditorTools.GetFieldValue<EmotionalAppraisal.EmotionalAppraisalAsset>(CurrentAsset, "_emotionalAppraisalAsset");
			});
			edmAssetControl1.SetAsset(asset.EmotionalDecisionMakingSource,() =>
			{
				RequestAssetReload();
				return EditorTools.GetFieldValue<EmotionalDecisionMaking.EmotionalDecisionMakingAsset>(CurrentAsset, "_emotionalDecisionMakingAsset");
			});
			siAssetControl1.SetAsset(asset.SocialImportanceAssetSource, () =>
			{
				RequestAssetReload();
				return EditorTools.GetFieldValue<SocialImportance.SocialImportanceAsset>(CurrentAsset, "_socialImportanceAsset");
			});
		}
		
		private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
			if(IsLoading)
				return;

			CurrentAsset.CharacterName = textBoxCharacterName.Text;
			SetModified();
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
			if (IsLoading)
				return;

			CurrentAsset.BodyName = textBoxCharacterBody.Text;
			SetModified();
		}

		private void eaAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.EmotionalAppraisalAssetSource = eaAssetControl1.Path;
			CurrentAsset.ReloadDefitions();
			ReloadEditor();
			SetModified();
		}

		private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
			CurrentAsset.ReloadDefitions();
			ReloadEditor();
			SetModified();
		}

		private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.SocialImportanceAssetSource = siAssetControl1.Path;
			CurrentAsset.ReloadDefitions();
			ReloadEditor();
			SetModified();
		}
	}
}

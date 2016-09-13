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
			var ea = EditorTools.GetFieldValue<EmotionalAppraisal.EmotionalAppraisalAsset>(asset, "_emotionalAppraisalAsset");
			eaAssetControl1.SetAsset(ea);

			var edm = EditorTools.GetFieldValue<EmotionalDecisionMaking.EmotionalDecisionMakingAsset>(asset, "_emotionalDecisionMakingAsset");
			edmAssetControl1.SetAsset(edm);

			var si = EditorTools.GetFieldValue<SocialImportance.SocialImportanceAsset>(asset, "_socialImportanceAsset");
			siAssetControl1.SetAsset(si);
		}
		
		private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
			CurrentAsset.CharacterName = textBoxCharacterName.Text;
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
            CurrentAsset.BodyName = textBoxCharacterBody.Text;
        }

		private void eaAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			CurrentAsset.EmotionalAppraisalAssetSource = eaAssetControl1.Path;
			ReloadAsset(sender,e);
		}

		private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			CurrentAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
			ReloadAsset(sender, e);
		}

		private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			CurrentAsset.SocialImportanceAssetSource = siAssetControl1.Path;
			ReloadAsset(sender, e);
		}

		private void ReloadAsset(object sender, EventArgs e)
		{
			CurrentAsset.ReloadDefitions();
			OnAssetDataLoaded(CurrentAsset);
		}
	}
}

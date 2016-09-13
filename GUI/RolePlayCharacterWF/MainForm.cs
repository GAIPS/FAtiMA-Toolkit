using System;
using GAIPS.AssetEditorTools;
using RolePlayCharacter;
using RolePlayCharacterWF.Properties;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : BaseRPCForm
    {
	    private bool _ignoreEvents = false;

		public MainForm()
        {
            InitializeComponent();
        }

		protected override void OnAssetDataLoaded(RolePlayCharacterAsset asset)
		{
			_ignoreEvents = true;

			textBoxCharacterName.Text = asset.CharacterName;
			textBoxCharacterBody.Text = asset.BodyName;
			var ea = EditorTools.GetFieldValue<EmotionalAppraisal.EmotionalAppraisalAsset>(asset, "_emotionalAppraisalAsset");
			eaAssetControl1.SetAsset(ea);

			var edm = EditorTools.GetFieldValue<EmotionalDecisionMaking.EmotionalDecisionMakingAsset>(asset, "_emotionalDecisionMakingAsset");
			edmAssetControl1.SetAsset(edm);

			var si = EditorTools.GetFieldValue<SocialImportance.SocialImportanceAsset>(asset, "_socialImportanceAsset");
			siAssetControl1.SetAsset(si);

			_ignoreEvents = false;
		}
		
		private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
			if(_ignoreEvents)
				return;

			CurrentAsset.CharacterName = textBoxCharacterName.Text;
			SetModified();
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
			if (_ignoreEvents)
				return;

			CurrentAsset.BodyName = textBoxCharacterBody.Text;
			SetModified();
		}

		private void eaAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (_ignoreEvents)
				return;

			CurrentAsset.EmotionalAppraisalAssetSource = eaAssetControl1.Path;
			ReloadAsset(sender,e);
			SetModified();
		}

		private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (_ignoreEvents)
				return;

			CurrentAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
			ReloadAsset(sender, e);
			SetModified();
		}

		private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (_ignoreEvents)
				return;

			CurrentAsset.SocialImportanceAssetSource = siAssetControl1.Path;
			ReloadAsset(sender, e);
			SetModified();
		}

		private void ReloadAsset(object sender, EventArgs e)
		{
			CurrentAsset.ReloadDefitions();
			OnAssetDataLoaded(CurrentAsset);
		}
	}
}

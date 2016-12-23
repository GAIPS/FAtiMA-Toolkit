using System;
using RolePlayCharacter;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : BaseRPCForm
    {
	    public MainForm()
        {
            InitializeComponent();
        }

		protected override void OnAssetDataLoaded(RolePlayCharacterProfileAsset asset)
		{
			textBoxCharacterName.Text = asset.CharacterName;
			textBoxCharacterBody.Text = asset.BodyName;

			eaAssetControl1.SetAsset(asset.EmotionalAppraisalAssetSource, () =>
			{
				RequestAssetReload();

				//erro de proposito
				//isto deve de falhar, uma vez que não está a passar a lista de dynamic properties
				// se calhar a lista deve de ser algo externo ao asset
				return EmotionalAppraisal.EmotionalAppraisalAsset.LoadFromFile(CurrentAsset.EmotionalAppraisalAssetSource);
			});
			edmAssetControl1.SetAsset(asset.EmotionalDecisionMakingSource, () =>
			 {
				 RequestAssetReload();
				 return EmotionalDecisionMaking.EmotionalDecisionMakingAsset.LoadFromFile(CurrentAsset.EmotionalDecisionMakingSource);
			 });
			siAssetControl1.SetAsset(asset.SocialImportanceAssetSource, () =>
			{
				RequestAssetReload();
				return SocialImportance.SocialImportanceAsset.LoadFromFile(CurrentAsset.SocialImportanceAssetSource);
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
			SetModified();
		}

		private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
			SetModified();
		}

		private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.SocialImportanceAssetSource = siAssetControl1.Path;
			SetModified();
		}
	}
}

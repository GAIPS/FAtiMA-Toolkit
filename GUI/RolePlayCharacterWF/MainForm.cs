using System;
using RolePlayCharacter;
using WellFormedNames;
using WellFormedNames.Exceptions;
using System.Windows.Forms;

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
            textBoxCharacterName.Text = asset.CharacterName == null ? string.Empty : asset.CharacterName.ToString();
			textBoxCharacterBody.Text = asset.BodyName;
            textBoxCharacterVoice.Text = asset.VoiceName;

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

            if (!string.IsNullOrWhiteSpace(textBoxCharacterName.Text))
            {
                try
                {
                    var newName = (Name)textBoxCharacterName.Text;
                    CurrentAsset.CharacterName = newName;
                }
                catch (ParsingException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }   
			SetModified();
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
			if (IsLoading)
				return;

			CurrentAsset.BodyName = textBoxCharacterBody.Text;
			SetModified();
		}

        private void textBoxCharacterVoice_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            CurrentAsset.VoiceName = textBoxCharacterVoice.Text;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void eaAssetControl1_Load(object sender, EventArgs e)
        {

        }

        private void edmAssetControl1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addEmotionButton_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

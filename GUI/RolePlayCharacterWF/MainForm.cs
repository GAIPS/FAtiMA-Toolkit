using System;
using System.Windows.Forms;
using GAIPS.Rage;
using RolePlayCharacter;
using RolePlayCharacterWF.Properties;


namespace RolePlayCharacterWF
{
    public partial class MainForm : Form
    {
        private RolePlayCharacterAsset _rpcAsset;
        private string _saveFileName;

        public MainForm()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                Reset(true);
            }
            else
            {
                _saveFileName = args[1];
                try
                {
	                string error;
                    _rpcAsset = RolePlayCharacterAsset.LoadFromFile(LocalStorageProvider.Instance, args[1],out error);
					if(error!=null)
						MessageBox.Show(error, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset(true);
                }
            }
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormTitle;
                this._rpcAsset = new RolePlayCharacterAsset();
            }
            else
            {
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }

            textBoxCharacterName.Text = _rpcAsset.CharacterName;
            textBoxCharacterBody.Text = _rpcAsset.BodyName;
			eaAssetControl1.Path = _rpcAsset.EmotionalAppraisalAssetSource;
			edmAssetControl1.Path = _rpcAsset.EmotionalDecisionMakingSource;
	        siAssetControl1.Path = _rpcAsset.SocialImportanceAssetSource;
        }

		private void saveHelper(bool newSaveFile)
        {
            if (newSaveFile)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "RPC File|*.rpc";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        _saveFileName = sfd.FileName;
                    }
                }
                else
                {
                    return;
                }
            }
            try
            {
				_rpcAsset.SaveToFile(LocalStorageProvider.Instance,_saveFileName);
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.UnableToSaveFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }

     
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                saveHelper(true);
            }
            else
            {
                saveHelper(false);
            }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            saveHelper(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
	                string error;
                    _rpcAsset = RolePlayCharacterAsset.LoadFromFile(LocalStorageProvider.Instance, ofd.FileName,out error);
                    _saveFileName = ofd.FileName;
					if(error!=null)
						MessageBox.Show(error, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

					Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
            _rpcAsset.CharacterName = textBoxCharacterName.Text;
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
            _rpcAsset.BodyName = textBoxCharacterBody.Text;
        }

		private void eaAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			_rpcAsset.EmotionalAppraisalAssetSource = eaAssetControl1.Path;
		}

		private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			_rpcAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
		}

		private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
		{
			_rpcAsset.SocialImportanceAssetSource = siAssetControl1.Path;
		}


		//      private void buttonSetEmotionalAppraisalSource_Click(object sender, EventArgs e)
		//      {
		//          var ofd = new OpenFileDialog();
		//          if (ofd.ShowDialog() == DialogResult.OK)
		//          {
		//              try
		//              {
		//                  var ea = EmotionalAppraisalAsset.LoadFromFile(LocalStorageProvider.Instance,ofd.FileName);
		//                  _rpcAsset.EmotionalAppraisalAssetSource = ofd.FileName;
		//                  textBoxEmotionalAppraisalSource.Text = ofd.FileName;
		//              }
		//              catch (Exception ex)
		//              {
		//                  MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		//              }
		//          }
		//      }

		//      private void buttonSetEmotionalDecisionMakingSource_Click(object sender, EventArgs e)
		//      {
		//       var ofd = new OpenFileDialog();
		//          if (ofd.ShowDialog() == DialogResult.OK)
		//          {
		//              try
		//              {
		//                  var edm = EmotionalDecisionMakingAsset.LoadFromFile(LocalStorageProvider.Instance, ofd.FileName);
		//                  _rpcAsset.EmotionalDecisionMakingSource = ofd.FileName;
		//                  textBoxEmotionalDecisionMakingSource.Text = ofd.FileName;
		//              }
		//              catch (Exception ex)
		//              {
		//                  MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		//              }
		//          }
		//      }

		//      private void buttonEditEmotionalAppraisal_Click(object sender, EventArgs e)
		//      {
		//          if (string.IsNullOrEmpty(textBoxEmotionalAppraisalSource.Text))
		//          {
		//              return;
		//          }

		//          Process.Start(EMOTIONAL_APPRAISAL_AUTHORING_TOOL, "\"" + textBoxEmotionalAppraisalSource.Text + "\"");
		//      }

		//   private void buttonEditEmotionalDecisionMaking_Click(object sender, EventArgs e)
		//   {
		//    if (string.IsNullOrEmpty(textBoxEmotionalDecisionMakingSource.Text))
		//    {
		//	    return;
		//    }

		//    Process.Start(EMOTIONAL_DECISION_MAKING_AUTHORING_TOOL, "\"" + textBoxEmotionalDecisionMakingSource.Text + "\"");

		//   }

		//private void textBoxEmotionalAppraisalSource_TextChanged(object sender, EventArgs e)
		//{
		//	buttonEditEmotionalAppraisal.Enabled = !string.IsNullOrEmpty(textBoxEmotionalAppraisalSource.Text);
		//}

		//private void textBoxEmotionalDecisionMakingSource_TextChanged(object sender, EventArgs e)
		//{
		//	buttonEditEmotionalDecisionMaking.Enabled = !string.IsNullOrEmpty(textBoxEmotionalDecisionMakingSource.Text);
		//}
	}
}

using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using RolePlayCharacter;
using RolePlayCharacterWF.Properties;


namespace RolePlayCharacterWF
{
    public partial class MainForm : Form
    {
        private const string EMOTIONAL_APPRAISAL_AUTHORING_TOOL = "EmotionalAppraisalWF.exe";
        private const string EMOTIONAL_DECISION_MAKING_AUTHORING_TOOL = "EmotionalDecisionMakingWF.exe";

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
                    _rpcAsset = RolePlayCharacterAsset.LoadFromFile(args[1],out error);
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
            textBoxEmotionalAppraisalSource.Text = _rpcAsset.EmotionalAppraisalAssetSource;
            textBoxEmotionalDecisionMakingSource.Text = _rpcAsset.EmotionalDecisionMakingSource;
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
				_rpcAsset.SaveToFile(_saveFileName);
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
                    _rpcAsset = RolePlayCharacterAsset.LoadFromFile(ofd.FileName,out error);
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

        private void buttonSetEmotionalAppraisalSource_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var ea = EmotionalAppraisalAsset.LoadFromFile(ofd.FileName);
                    _rpcAsset.EmotionalAppraisalAssetSource = ofd.FileName;
                    textBoxEmotionalAppraisalSource.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSetEmotionalDecisionMakingSource_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var edm = EmotionalDecisionMakingAsset.LoadFromFile(ofd.FileName);
                    _rpcAsset.EmotionalDecisionMakingSource = ofd.FileName;
                    textBoxEmotionalDecisionMakingSource.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEditEmotionalAppraisal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmotionalAppraisalSource.Text))
            {
                return;
            }

            Process.Start(EMOTIONAL_APPRAISAL_AUTHORING_TOOL, "\"" + textBoxEmotionalAppraisalSource.Text + "\"");
        }

        private void buttonEditEmotionalDecisionMaking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmotionalDecisionMakingSource.Text))
            {
                return;
            }

            Process.Start(EMOTIONAL_DECISION_MAKING_AUTHORING_TOOL, "\"" +textBoxEmotionalDecisionMakingSource.Text + "\"");
            
        }
    }
}

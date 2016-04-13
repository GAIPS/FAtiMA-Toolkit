using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using IntegratedAuthoringToolWF.Properties;
using RolePlayCharacter;


namespace IntegratedAuthoringToolWF
{
    public partial class MainForm : Form
    {
        private readonly string RPC_AUTHORING_TOOL = "RoleplayCharacterWF.exe";

        private IntegratedAuthoringToolAsset _iatAsset;
        private string _saveFileName;

        private BindingListView<CharacterSourceDTO> _characterSources;
         
        public MainForm()
        {
            InitializeComponent();
            Reset(true);
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormTitle;
                this._iatAsset = new IntegratedAuthoringToolAsset();
            }
            else
            {
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }

            textBoxScenarioName.Text = _iatAsset.ScenarioName;
            _characterSources = new BindingListView<CharacterSourceDTO>(this._iatAsset.GetAllCharacterSources().ToList());
            dataGridViewCharacters.DataSource = _characterSources;
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _iatAsset = IntegratedAuthoringToolAsset.LoadFromFile(ofd.FileName);
                    _saveFileName = ofd.FileName;
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
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

        private void saveHelper(bool newSaveFile)
        {
            if (newSaveFile)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "IAT File|*.iat";
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
                using (var file = File.Create(_saveFileName))
                {
                    _iatAsset.SaveToFile(file);
                }
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.UnableToSaveFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
           this.saveHelper(true);
        }

        private void buttonAddCharacter_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _iatAsset.AddCharacter(ofd.FileName);
                    _characterSources.DataSource = _iatAsset.GetAllCharacterSources().ToList();
                    _characterSources.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxScenarioName_TextChanged(object sender, EventArgs e)
        {
            this._iatAsset.ScenarioName = textBoxScenarioName.Text;
        }

        private void buttonRemoveCharacter_Click(object sender, EventArgs e)
        {
            IList<string> charactersToRemove = new List<string>();
            for (int i = 0; i < dataGridViewCharacters.SelectedRows.Count; i++)
            {
                var character = ((ObjectView<CharacterSourceDTO>)dataGridViewCharacters.SelectedRows[i].DataBoundItem).Object;
                charactersToRemove.Add(character.Name);
            }
            _iatAsset.RemoveCharacters(charactersToRemove);
            _characterSources.DataSource = _iatAsset.GetAllCharacterSources().ToList();
            _characterSources.Refresh();
        }

        private void buttonEditCharacter_Click(object sender, EventArgs e)
        {
            if (dataGridViewCharacters.SelectedRows.Count == 1)
            {
                var character = ((ObjectView<CharacterSourceDTO>)dataGridViewCharacters.SelectedRows[0].DataBoundItem).Object;
                Process.Start(RPC_AUTHORING_TOOL, "\"" + character.Source + "\"");
            }
        }
    }
}

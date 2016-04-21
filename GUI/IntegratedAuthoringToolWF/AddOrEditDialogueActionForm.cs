using System;
using System.Windows.Forms;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
    public partial class AddOrEditDialogueActionForm : Form
    {
        private IntegratedAuthoringToolAsset _iatAsset;
        private DialogueStateActionDTO _dialogueStateActionToEdit;
        private bool _isPlayerDialogue;

        public AddOrEditDialogueActionForm(IntegratedAuthoringToolAsset iatAsset, bool isPlayerDialogue, DialogueStateActionDTO dialogueStateActionToEdit = null)
        {
            InitializeComponent();
            _iatAsset = iatAsset;
            _isPlayerDialogue = isPlayerDialogue;
  
            if (dialogueStateActionToEdit != null)
            {
                buttonAddOrUpdate.Text = "Update";
                _dialogueStateActionToEdit = dialogueStateActionToEdit;
                textBoxCurrentState.Text = _dialogueStateActionToEdit.CurrentState;
                textBoxMeaning.Text = _dialogueStateActionToEdit.Meaning;
                textBoxNextState.Text = _dialogueStateActionToEdit.NextState;
                textBoxStyle.Text = _dialogueStateActionToEdit.Style;
                textBoxUtterance.Text = _dialogueStateActionToEdit.Utterance;
            }

            UpdateFormTitle();
        }

        private void UpdateFormTitle()
        {
            if (_isPlayerDialogue && _dialogueStateActionToEdit == null)
                this.Text = "Add Player Dialogue Action";
            else if (_isPlayerDialogue && _dialogueStateActionToEdit != null)
                this.Text = "Update Player Dialogue Action";
            else if (!_isPlayerDialogue && _dialogueStateActionToEdit == null)
                this.Text = "Add Agent Dialogue Action";
            else if (!_isPlayerDialogue && _dialogueStateActionToEdit != null)
                this.Text = "Update Agent Dialogue Action";
        }

        private void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = textBoxCurrentState.Text,
                    Meaning = textBoxMeaning.Text,
                    NextState = textBoxNextState.Text,
                    Style = textBoxStyle.Text,
                    Utterance = textBoxUtterance.Text
                };

                if (_dialogueStateActionToEdit == null)
                {
                    if (_isPlayerDialogue)
                    {
                        _iatAsset.AddPlayerDialogAction(newDialogueAction);
                    }
                    else
                    {
                        _iatAsset.AddAgentDialogAction(newDialogueAction);
                    }
                }
                else
                {
                    if (_isPlayerDialogue)
                    {
                        _iatAsset.EditPlayerDialogAction(_dialogueStateActionToEdit, newDialogueAction);
                    }
                    else
                    {
                        _iatAsset.EditAgentDialogAction(_dialogueStateActionToEdit, newDialogueAction);
                    }

                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

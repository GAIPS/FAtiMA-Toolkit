using System;
using System.Linq;
using System.Windows.Forms;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
    public partial class AddOrEditDialogueActionForm : Form
    {
	    private MainForm _parentForm;
        private IntegratedAuthoringToolAsset _iatAsset => _parentForm.LoadedAsset;
        private readonly DialogueStateActionDTO _dialogueStateActionToEdit;
        private readonly bool _isPlayerDialogue;

        public AddOrEditDialogueActionForm(MainForm form, bool isPlayerDialogue)
        {
            InitializeComponent();
	        _parentForm = form;
            _isPlayerDialogue = isPlayerDialogue;
        }

		public AddOrEditDialogueActionForm(MainForm form, bool isPlayerDialogue, Guid dialogId) : this(form,isPlayerDialogue)
		{
			buttonAddOrUpdate.Text = "Update";
			_dialogueStateActionToEdit =
				form.LoadedAsset.GetDialogActionById(
					isPlayerDialogue ? IATConsts.PLAYER : IATConsts.AGENT, dialogId);

			textBoxCurrentState.Text = _dialogueStateActionToEdit.CurrentState;
			textBoxNextState.Text = _dialogueStateActionToEdit.NextState;
			textBoxMeaning.Text = _dialogueStateActionToEdit.Meaning.Length == 0 ? string.Empty : _dialogueStateActionToEdit.Meaning.Aggregate((s, s1) => s + ", " + s1);
			textBoxStyle.Text = _dialogueStateActionToEdit.Style.Length == 0 ? string.Empty : _dialogueStateActionToEdit.Style.Aggregate((s, s1) => s + ", " + s1);
			textBoxUtterance.Text = _dialogueStateActionToEdit.Utterance;
		}

        private void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = textBoxCurrentState.Text,
					NextState = textBoxNextState.Text,
					Meaning = textBoxMeaning.Text.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Style = textBoxStyle.Text.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
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
				_parentForm.SetModified();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void OnLoad(object sender, EventArgs e)
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
	}
}

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

        public AddOrEditDialogueActionForm(IntegratedAuthoringToolAsset iatAsset, DialogueStateActionDTO dialogueStateActionToEdit = null)
        {
            InitializeComponent();
            _iatAsset = iatAsset;
            comboBoxSpeakerType.DataSource = new[]{DialogStateAction.SPEAKER_TYPE_PLAYER, DialogStateAction.SPEAKER_TYPE_AGENT };


            if (dialogueStateActionToEdit != null)
            {
                this.Text = "Update Dialogue Action";
                buttonAddOrUpdate.Text = "Update";

                _dialogueStateActionToEdit = dialogueStateActionToEdit;
            }
        }
        

        private void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            var newDialogueAction = new DialogueStateActionDTO
            {
                SpeakerType = comboBoxSpeakerType.Text,
                CurrentState = textBoxCurrentState.Text,
                Meaning = textBoxMeaning.Text,
                NextState = textBoxNextState.Text,
                Style = textBoxStyle.Text,
                Utterance = textBoxUtterance.Text
            };

            if (_dialogueStateActionToEdit == null)
            {
                _iatAsset.AddDialogAction(newDialogueAction);
            }
            this.Close();
        }
    }
}

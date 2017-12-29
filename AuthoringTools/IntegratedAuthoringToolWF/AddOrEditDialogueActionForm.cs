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

        public AddOrEditDialogueActionForm(MainForm form)
        {
            InitializeComponent();

            textBoxCurrentState.AllowComposedName = false;
            textBoxNextState.AllowComposedName = false;

            textBoxCurrentState.AllowVariable = false;
            textBoxNextState.AllowVariable = false;
            textBoxMeaning.AllowVariable = false;
            textBoxStyle.AllowVariable = false;

            textBoxCurrentState.AllowUniversal = false;
            textBoxNextState.AllowUniversal = false;
            textBoxMeaning.AllowUniversal = false;
            textBoxStyle.AllowUniversal = false;

            _parentForm = form;
        }

		public AddOrEditDialogueActionForm(MainForm form, bool isPlayerDialogue, Guid dialogId) : this(form)
		{
			buttonAddOrUpdate.Text = "Update";
			_dialogueStateActionToEdit = form.LoadedAsset.GetDialogActionById(dialogId);

			textBoxCurrentState.Value = (WellFormedNames.Name)_dialogueStateActionToEdit.CurrentState;
			textBoxNextState.Value = (WellFormedNames.Name)_dialogueStateActionToEdit.NextState;
            textBoxMeaning.Value = (WellFormedNames.Name)_dialogueStateActionToEdit.Meaning;
            textBoxStyle.Value = (WellFormedNames.Name)_dialogueStateActionToEdit.Style;
			textBoxUtterance.Text = _dialogueStateActionToEdit.Utterance;
		}

        private void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = textBoxCurrentState.Value.ToString(),
					NextState = textBoxNextState.Value.ToString(),
					Meaning = textBoxMeaning.Value.ToString(),
                    Style = textBoxStyle.Value.ToString(),
                    Utterance = textBoxUtterance.Text
                };

                if (_dialogueStateActionToEdit == null)
                {
                    _iatAsset.AddDialogAction(newDialogueAction);
                }
                else
                {
                    _iatAsset.EditDialogAction(_dialogueStateActionToEdit, newDialogueAction);
                }
				_parentForm.SetModified();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddOrEditDialogueActionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void AddOrEditDialogueActionForm_Load(object sender, EventArgs e)
        {

        }
    }
}

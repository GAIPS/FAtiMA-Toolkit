using System;
using System.Windows.Forms;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using GAIPS.AssetEditorTools;
using System.Collections.Generic;
using System.Linq;

namespace IntegratedAuthoringToolWF
{
    public partial class AddOrEditDialogueActionForm : Form
    {
        private IntegratedAuthoringToolAsset asset;
        private DialogueStateActionDTO dto;

        public Guid UpdatedGuid { get; private set; }
        

        public AddOrEditDialogueActionForm(IntegratedAuthoringToolAsset asset, DialogueStateActionDTO dto)
        {
            InitializeComponent();

            this.dto = dto;
            this.asset = asset;

            // Getting the suggested list
            List<DialogueStateActionDTO> dialogs = asset.GetAllDialogueActions().ToList();

            var currentStates = dialogs.Select(x => x.CurrentState.ToString()).ToList();
            var currentStateSource = new AutoCompleteStringCollection();
            var nextStates = dialogs.Select(x => x.NextState.ToString()).ToList();
            var nextStatesSource = new AutoCompleteStringCollection();

            currentStateSource.AddRange(currentStates.ToArray());
            currentStateSource.AddRange(nextStates.ToArray());

            nextStatesSource.AddRange(nextStates.ToArray());
            nextStatesSource.AddRange(currentStates.ToArray());


            var meanings = dialogs.Select(x => x.Meaning.ToString()).ToList();
            var meaningSource = new AutoCompleteStringCollection();
            var styles = dialogs.Select(x => x.Style.ToString()).ToList();
            var styleSource = new AutoCompleteStringCollection();

            meaningSource.AddRange(meanings.ToArray());
            meaningSource.AddRange(styles.ToArray());

            styleSource.AddRange(meanings.ToArray());
            styleSource.AddRange(styles.ToArray());


            textBoxCurrentState.Value = (WellFormedNames.Name)dto.CurrentState;
            textBoxCurrentState.AutoCompleteCustomSource = currentStateSource;
            textBoxNextState.Value = (WellFormedNames.Name)dto.NextState;
            textBoxNextState.AutoCompleteCustomSource = nextStatesSource;
            textBoxMeaning.Value = (WellFormedNames.Name)dto.Meaning;
            textBoxMeaning.AutoCompleteCustomSource = meaningSource;
            textBoxStyle.Value = (WellFormedNames.Name)dto.Style;
            textBoxStyle.AutoCompleteCustomSource = styleSource;
            textBoxUtterance.Text = dto.Utterance;

            //validators
            EditorTools.AllowOnlyGroundedLiteralOrNil(textBoxCurrentState);
            EditorTools.AllowOnlyGroundedLiteralOrNil(textBoxNextState);

            textBoxMeaning.AllowVariable = false;
            textBoxStyle.AllowVariable = false;
        }

        private void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var newDA = new DialogueStateActionDTO
                {
                    CurrentState = textBoxCurrentState.Value.ToString(),
                    NextState = textBoxNextState.Value.ToString(),
                    Meaning = textBoxMeaning.Value.ToString(),
                    Style = textBoxStyle.Value.ToString(),
                    Utterance = textBoxUtterance.Text
                };
                
                if ( dto.Id == Guid.Empty)
                {
                    UpdatedGuid = asset.AddDialogAction(newDA);
                }
                else
                {
                    UpdatedGuid = asset.EditDialogAction(dto, newDA);
                }
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

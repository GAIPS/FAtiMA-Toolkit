using System;
using System.Windows.Forms;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using GAIPS.AssetEditorTools;

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

            textBoxCurrentState.Value = (WellFormedNames.Name)dto.CurrentState;
            textBoxNextState.Value = (WellFormedNames.Name)dto.NextState;
            textBoxMeaning.Value = (WellFormedNames.Name)dto.Meaning;
            textBoxStyle.Value = (WellFormedNames.Name)dto.Style;
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

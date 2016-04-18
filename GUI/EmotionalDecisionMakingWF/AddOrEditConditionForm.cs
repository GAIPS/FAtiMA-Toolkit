using System;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using EmotionalDecisionMakingWF.Properties;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalDecisionMakingWF
{
    public partial class AddOrEditConditionForm : Form
    {
        private EmotionalDecisionMakingAsset _edmAsset;
        private Guid _selectedReactionId;
        private ConditionDTO _conditionToEditDTO;
        
        public AddOrEditConditionForm(EmotionalDecisionMakingAsset edmAsset, Guid selectedReactionId, ConditionDTO conditionToEdit = null)
        {
            InitializeComponent();

            _edmAsset = edmAsset;
            _conditionToEditDTO = conditionToEdit;
            _selectedReactionId = selectedReactionId;
        
            if (conditionToEdit != null)
            {
                this.Text = Resources.EditConditionFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                this.textBoxCondition.Text = conditionToEdit.Condition;
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                var newCondition = new ConditionDTO
                {
                    Condition = textBoxCondition.Text
                };
                if (_conditionToEditDTO != null)
                {
                    _edmAsset.UpdateRectionCondition(_selectedReactionId, _conditionToEditDTO, newCondition);
                }
                else
                {
                    _edmAsset.AddReactionCondition(_selectedReactionId, newCondition);
                }
                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

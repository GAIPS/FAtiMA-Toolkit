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
        private string _conditionToEdit;
        
        public AddOrEditConditionForm(EmotionalDecisionMakingAsset edmAsset, Guid selectedReactionId, string conditionToEdit = null)
        {
            InitializeComponent();

            _edmAsset = edmAsset;
            _conditionToEdit = conditionToEdit;
            _selectedReactionId = selectedReactionId;
        
            if (conditionToEdit != null)
            {
                this.Text = Resources.EditConditionFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                this.textBoxCondition.Text = conditionToEdit;
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
	            var newCondition = textBoxCondition.Text;

				if (_conditionToEdit != null)
                {
                    _edmAsset.UpdateRectionCondition(_selectedReactionId, _conditionToEdit, newCondition);
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

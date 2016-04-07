using System;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditConditionForm : Form
    {
        private AppraisalRulesVM _appraisalRulesVM;
        private ConditionDTO _conditionToEditDTO;
        
        public AddOrEditConditionForm(AppraisalRulesVM appraisalRulesVm, ConditionDTO conditionToEdit = null)
        {
            InitializeComponent();

            _appraisalRulesVM = appraisalRulesVm;
            _conditionToEditDTO = conditionToEdit;
        
            if (conditionToEdit != null)
            {
                this.Text = Resources.EditConditionEventFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                this.textBoxEvent.Text = conditionToEdit.Condition;
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                var newCondition = new ConditionDTO
                {
                    Condition = textBoxEvent.Text
                };
                if (_conditionToEditDTO != null)
                {
                    _appraisalRulesVM.UpdateCondition(_conditionToEditDTO, newCondition);
                }
                else
                {
                    _appraisalRulesVM.AddCondition(newCondition);
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

using System;
using System.Windows.Forms;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditConditionForm : Form
    {
        private AppraisalRulesVM _appraisalRulesVM;
        private string _conditionToEdit;
        
        public AddOrEditConditionForm(AppraisalRulesVM appraisalRulesVm, string conditionToEdit = null)
        {
            InitializeComponent();

            _appraisalRulesVM = appraisalRulesVm;
            _conditionToEdit = conditionToEdit;
        
            if (conditionToEdit != null)
            {
                this.Text = Resources.EditConditionEventFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                this.textBoxEvent.Text = conditionToEdit;
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
	            var newCondition = textBoxEvent.Text;

				if (_conditionToEdit != null)
                {
                    _appraisalRulesVM.UpdateCondition(_conditionToEdit, newCondition);
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

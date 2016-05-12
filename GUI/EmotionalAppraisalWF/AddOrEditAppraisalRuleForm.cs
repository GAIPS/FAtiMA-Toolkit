using System;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditAppraisalRuleForm : Form
    {
        private AppraisalRulesVM _appraisalRulesVM;
        private AppraisalRuleDTO _appraisalRuleToEdit;
        
        public AddOrEditAppraisalRuleForm(AppraisalRulesVM ruleVM, AppraisalRuleDTO ruleToEdit = null)
        {
            InitializeComponent();

            _appraisalRulesVM = ruleVM;
            _appraisalRuleToEdit = ruleToEdit;
            
            //defaultValues
            comboBoxDesirability.Text = "0";
            comboBoxPraiseworthiness.Text = "0";

            if (ruleToEdit != null)
            {
                this.Text = Resources.EditAppraisalRuleFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;

                eventTextBox.Text = ruleToEdit.EventMatchingTemplate;
                comboBoxDesirability.Text = ruleToEdit.Desirability.ToString();
                comboBoxPraiseworthiness.Text = ruleToEdit.Praiseworthiness.ToString();
            }
        }
  
        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            var newRule = new AppraisalRuleDTO()
            {
                EventMatchingTemplate = eventTextBox.Text,
                Desirability = int.Parse(comboBoxDesirability.Text),
                Praiseworthiness = int.Parse(comboBoxPraiseworthiness.Text),
				Conditions = new ConditionSetDTO()
            };
            
            try
            {
                if (_appraisalRuleToEdit != null)
                {
                    newRule.Id = _appraisalRuleToEdit.Id;
                    newRule.Conditions = _appraisalRuleToEdit.Conditions;
                }

                _appraisalRulesVM.AddOrUpdateAppraisalRule(newRule);
            
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}

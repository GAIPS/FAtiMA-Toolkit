using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase;
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
                /*
                beliefNameTextBox.Text = beliefToEdit.Name;
                beliefValueTextBox.Text = beliefToEdit.Value;
                beliefVisibilityComboBox.SelectedIndex = beliefVisibilityComboBox.FindString(beliefToEdit.Visibility.ToString());*/
            }
        }

       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPraiseworthiness_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

  
        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            var newRule = new AppraisalRuleDTO()
            {
                EventMatchingTemplate = eventTextBox.Text,
                Desirability = int.Parse(comboBoxDesirability.Text),
                Praiseworthiness = int.Parse(comboBoxPraiseworthiness.Text),
                Conditions = new List<ConditionDTO>(),
                Description = richTextBoxDescription.Text
            };

            try
            {
                _appraisalRulesVM.AddAppraisalRule(newRule);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}

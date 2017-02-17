using System;
using System.Windows.Forms;
using Conditions.DTOs;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using AutobiographicMemory;

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
            comboBoxEventType.DataSource = AppraisalRulesVM.EventTypes;

            if (ruleToEdit != null)
            {
                this.Text = Resources.EditAppraisalRuleFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                comboBoxEventType.Text = ruleToEdit.EventType;
                textBoxSubject.Text = ruleToEdit.Subject;
                if (comboBoxEventType.Text == AMConsts.ACTION_START || comboBoxEventType.Text == AMConsts.ACTION_END)
                {
                    textBoxObject.Text = ruleToEdit.Action;
                    textBoxTarget.Text = ruleToEdit.Target;
                }
                else
                {
                    labelObject.Text = "Property:";
                    labelTarget.Text = "New Value:";
                    textBoxObject.Text = ruleToEdit.Property;
                    textBoxTarget.Text = ruleToEdit.NewValue;
                }
                comboBoxDesirability.Text = ruleToEdit.Desirability.ToString();
                comboBoxPraiseworthiness.Text = ruleToEdit.Praiseworthiness.ToString();
            }
        }
  
        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            AppraisalRuleDTO newRule = new AppraisalRuleDTO();

            if(comboBoxEventType.Text.Equals(AMConsts.ACTION_START) || comboBoxEventType.Text.Equals(AMConsts.ACTION_END))
            {
                newRule = new AppraisalRuleDTO()
                {
                    EventType = comboBoxEventType.Text,
                    Subject = textBoxSubject.Text,
                    Action = textBoxObject.Text,
                    Target = textBoxTarget.Text,
                    Desirability = int.Parse(comboBoxDesirability.Text),
                    Praiseworthiness = int.Parse(comboBoxPraiseworthiness.Text),
                    Conditions = new ConditionSetDTO()
                };
            }
            else if (comboBoxEventType.Text.Equals(AMConsts.PROPERTY_CHANGE))
            {
                newRule = new AppraisalRuleDTO()
                {
                    EventType = comboBoxEventType.Text,
                    Subject = textBoxSubject.Text,
                    Action = textBoxObject.Text,
                    Target = textBoxTarget.Text,
                    Desirability = int.Parse(comboBoxDesirability.Text),
                    Praiseworthiness = int.Parse(comboBoxPraiseworthiness.Text),
                    Conditions = new ConditionSetDTO()
                };
            }
            
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

        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSubject_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

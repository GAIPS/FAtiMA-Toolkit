using AutobiographicMemory;
using Conditions.DTOs;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using System;
using System.Windows.Forms;
using WellFormedNames;

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

            //validationRules
            textBoxSubject.AllowNil = false;
            textBoxSubject.AllowComposedName = false;
           

            //defaultValues
            textBoxSubject.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
            textBoxObject.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
            textBoxTarget.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
        
            comboBoxEventType.DataSource = AppraisalRulesVM.EventTypes;

            if (ruleToEdit != null)
            {
                this.Text = Resources.EditAppraisalRuleFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                comboBoxEventType.Text = ruleToEdit.EventMatchingTemplate.GetNTerm(1).ToString();
                textBoxSubject.Value = ruleToEdit.EventMatchingTemplate.GetNTerm(2);
                textBoxObject.Value = ruleToEdit.EventMatchingTemplate.GetNTerm(3);
                textBoxTarget.Value = ruleToEdit.EventMatchingTemplate.GetNTerm(4);
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            AppraisalRuleDTO newRule = new AppraisalRuleDTO();
            try
            {
                newRule = new AppraisalRuleDTO()
                {
                    EventMatchingTemplate = WellFormedNames.Name.BuildName(
                    (Name)AMConsts.EVENT, 
                    (Name)comboBoxEventType.Text,
                    textBoxSubject.Value,
                    textBoxObject.Value,
                    textBoxTarget.Value),
                    Conditions = new ConditionSetDTO()
                };
           
                if (_appraisalRuleToEdit != null)
                {
                    newRule.Id = _appraisalRuleToEdit.Id;
                    newRule.Conditions = _appraisalRuleToEdit.Conditions;
                    newRule.AppraisalVariables = _appraisalRuleToEdit.AppraisalVariables;
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

        private void labelObject_Click(object sender, EventArgs e)
        {

        }

        private void AddOrEditAppraisalRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void AddOrEditAppraisalRuleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
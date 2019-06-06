using AutobiographicMemory;
using Conditions.DTOs;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using System;
using System.Globalization;
using System.Windows.Forms;
using WellFormedNames;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditAppraisalVariableForm : Form
    {
        private AppraisalRulesVM _appraisalRulesVM;
        private AppraisalVariableDTO _toEdit;
        private AppraisalRuleDTO _appraisalRule;

        public AddOrEditAppraisalVariableForm(AppraisalRulesVM ruleVM, AppraisalRuleDTO appraisalRule , AppraisalVariableDTO variable = null)
        {
            InitializeComponent();

            _appraisalRulesVM = ruleVM;
            _toEdit = variable;
            _appraisalRule = appraisalRule;

            //validationRules
            appraisalVariableValueTextBox.AllowNil = false;
            appraisalVariableValueTextBox.AllowComposedName = false;
             appraisalVariableValueTextBox.AllowUniversal = false;

            //defaultValues
            appraisalVariableName.Items.Add(OCCAppraisalVariables.DESIRABILITY);
            appraisalVariableName.Items.Add(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER);
            appraisalVariableName.Items.Add(OCCAppraisalVariables.PRAISEWORTHINESS);
            appraisalVariableName.Items.Add(OCCAppraisalVariables.GOALSUCCESSPROBABILITY);
            appraisalVariableName.Items.Add(OCCAppraisalVariables.LIKE);
            appraisalVariableName.SelectedItem = OCCAppraisalVariables.DESIRABILITY;
            appraisalVariableTarget.Enabled = false;
            appraisalVariableValueTextBox.Value = (Name)"0";
           
         

            

            if (_toEdit.Name != null)
            {
                
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                 appraisalVariableValueTextBox.Value = _toEdit.Value;
                appraisalVariableTarget.Value = _toEdit.Target;
                appraisalVariableName.SelectedIndex = appraisalVariableName.Items.IndexOf(_toEdit.Name);

            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            bool noDesirability = false;

            if (appraisalVariableName.SelectedItem.ToString() == OCCAppraisalVariables.GOALSUCCESSPROBABILITY)
            {
                float value = 0.0f;
                if (float.TryParse(appraisalVariableValueTextBox.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                {
                    if (value < 0 || value > 1)
                    {
                        MessageBox.Show("Goal Value must be bewteen 0 and 1", Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var name = WellFormedNames.Name.BuildName(appraisalVariableValueTextBox.Value.ToString());
                    if (!name.IsVariable)
                    {
                        MessageBox.Show("Goal Value must either be a variable or a float number", Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                        
                }

            }


            AppraisalVariableDTO newVar = new AppraisalVariableDTO();
            try
            {
                newVar = new AppraisalVariableDTO()
                {
                 Name = appraisalVariableName.SelectedItem.ToString(),
                 Target = appraisalVariableTarget.Value,
                 Value = appraisalVariableValueTextBox.Value
                };


                if(newVar.Name == OCCAppraisalVariables.DESIRABILITY_FOR_OTHER && 
                    _appraisalRule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.DESIRABILITY) == null){

                    noDesirability = true;
                    var desirability = new AppraisalVariableDTO()
                    {
                        Name = OCCAppraisalVariables.DESIRABILITY,
                        Target = (Name)"-",
                        Value = (Name)"2"
                    };

                   
                    _appraisalRule.AppraisalVariables.appraisalVariables.Add(desirability);


                }

               
              if(_appraisalRule.AppraisalVariables.appraisalVariables.Find(x=>x.Name == newVar.Name) != null)  {
                  
                    
                   _appraisalRule.AppraisalVariables.appraisalVariables.Find(x=>x.Name == newVar.Name).Value = newVar.Value;
                   _appraisalRule.AppraisalVariables.appraisalVariables.Find(x=>x.Name == newVar.Name).Target = newVar.Target;

                    
                    }
                    else {
                 
                    _appraisalRule.AppraisalVariables.appraisalVariables.Add(newVar); 

                    }
              

              
              _appraisalRulesVM.AddOrUpdateAppraisalRule(_appraisalRule);

                if (noDesirability)
                MessageBox.Show("According to the OCC Model, the \"Desirability For Others \" appraisal variable requires another Desirability concerning the consequences of the event for agent to also be present in the same rule. \n We have automatically added it, change its values at your will");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {

              if (appraisalVariableName.SelectedItem.ToString() == OCCAppraisalVariables.DESIRABILITY_FOR_OTHER || appraisalVariableName.SelectedItem.ToString() == OCCAppraisalVariables.PRAISEWORTHINESS)
            {
                 appraisalVariableTarget.Enabled = true;
                 labelTarget.Text = "Target";
            }
               else  if (appraisalVariableName.SelectedItem.ToString() == OCCAppraisalVariables.GOALSUCCESSPROBABILITY)
            {
                 appraisalVariableTarget.Enabled = true;
                labelTarget.Text = "Goal Name";
            }

            else    appraisalVariableTarget.Enabled = false;



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

        private void appraisalVariableValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Windows.Forms;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditBeliefForm : Form
    {
        private KnowledgeBaseVM _knowledgeBaseVm;
        private KnowledgeBaseVM.BeliefDTO _beliefToEdit;

        public AddOrEditBeliefForm(KnowledgeBaseVM kbVM, KnowledgeBaseVM.BeliefDTO beliefToEdit = null)
        {
            InitializeComponent();
            
            _knowledgeBaseVm = kbVM;
            _beliefToEdit = beliefToEdit;

            //Default Values 
            beliefVisibilityComboBox.DataSource = _knowledgeBaseVm.GetKnowledgeVisibilities();
            beliefVisibilityComboBox.SelectedIndex = 0;

            if (beliefToEdit != null)
            {
                this.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Edit_Belief;
                this.addOrEditBeliefButton.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Update;

                beliefNameTextBox.Text = beliefToEdit.Name;
                beliefValueTextBox.Text = beliefToEdit.Value;
                beliefVisibilityComboBox.SelectedIndex = beliefVisibilityComboBox.FindString(beliefToEdit.Visibility);
            }
        }

        private void addOrEditBeliefButton_Click(object sender, EventArgs e)
        {
            //clear errors
            addBeliefErrorProvider.Clear();
            var newBelief = new KnowledgeBaseVM.BeliefDTO
            {
                Name = this.beliefNameTextBox.Text.Trim(),
                Value = this.beliefValueTextBox.Text.Trim(),
                Visibility = this.beliefVisibilityComboBox.Text
            };

            try
            {
                if (_beliefToEdit != null)
                {
                    _knowledgeBaseVm.RemoveBeliefs(new[] {_beliefToEdit});
                    _knowledgeBaseVm.AddBelief(newBelief);
                    this.Close();
                }
                else
                {
                    _knowledgeBaseVm.AddBelief(newBelief);
                }
            }
            catch (Exception ex)
            {
                addBeliefErrorProvider.SetError(beliefNameTextBox, ex.Message);
                if (_beliefToEdit != null)
                {
                    _knowledgeBaseVm.AddBelief(_beliefToEdit);
                }
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void beliefVisibilityComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void beliefNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void beliefValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void AddOrEditBeliefForm_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditBeliefForm : Form
    {
        private KnowledgeBaseVM _knowledgeBaseVm;
        private ListView _beliefsListView;
        private bool _editMode;

        public AddOrEditBeliefForm(KnowledgeBaseVM kbVM, bool editMode = false)
        {
            InitializeComponent();

            _knowledgeBaseVm = kbVM;
            _editMode = editMode;

            //Default Values 
            beliefVisibilityComboBox.DataSource = _knowledgeBaseVm.GetKnowledgeVisibilities();
            beliefVisibilityComboBox.SelectedIndex = 0;

            if (_editMode)
            {
                this.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Edit_Belief;
                this.addOrEditBeliefButton.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Update;

                beliefNameTextBox.Text = _beliefsListView.SelectedItems[0].Text;
                beliefValueTextBox.Text = _beliefsListView.SelectedItems[0].SubItems[1].Text;
                beliefVisibilityComboBox.SelectedIndex = beliefVisibilityComboBox.FindString(_beliefsListView.SelectedItems[0].SubItems[2].Text);
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
                if(_editMode)
                    _knowledgeBaseVm.EditBelief(newBelief);
                else
                    _knowledgeBaseVm.AddBelief(newBelief); 
            }
            catch (Exception ex)
            {
                addBeliefErrorProvider.SetError(beliefNameTextBox, ex.Message);
                return;
            }
            
            /*if (_editMode)
            {
                if (beliefNameTextBox.Text != _beliefsListView.SelectedItems[0].Text)
                {
                    // the name of the belief was changed so the old belief must be removed from the KB
                    _emotionalAppraisalAsset.RemoveBelief(_beliefsListView.SelectedItems[0].Text);
                }
                _beliefsListView.SelectedItems[0].Remove();
                this.Close();
            }*/
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

using System;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisalWF.Properties;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditBeliefForm : Form
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private ListView _beliefsListView;
        private bool _editMode;

        public AddOrEditBeliefForm(ListView beliefsListView, EmotionalAppraisalAsset emotionalAppraisalAsset, bool editMode = false)
        {
            InitializeComponent();

            _beliefsListView = beliefsListView;
            _emotionalAppraisalAsset = emotionalAppraisalAsset;
            _editMode = editMode;

            //Default Values 
            beliefVisibilityComboBox.DataSource = EmotionalAppraisalAsset.GetKnowledgeVisibilities();
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

            try
            {
                _emotionalAppraisalAsset.AddBelief(this.beliefNameTextBox.Text, this.beliefValueTextBox.Text, this.beliefVisibilityComboBox.Text);
            }
            catch (Exception ex)
            {
                addBeliefErrorProvider.SetError(beliefNameTextBox, ex.Message);
                return;
            }
            
            var beliefItem = new ListViewItem(new string[]
                {
                    this.beliefNameTextBox.Text,
                    this.beliefValueTextBox.Text,
                    this.beliefVisibilityComboBox.Text
                });

            _beliefsListView.Items.Add(beliefItem);
            
            if (_editMode)
            {
                _beliefsListView.SelectedItems[0].Remove();
                this.Close();
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

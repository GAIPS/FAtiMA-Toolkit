using System;
using System.Windows.Forms;
using EmotionalAppraisalWF.Properties;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditBeliefForm : Form
    {
        private ListView _beliefsListView;
        private bool _editMode;

        public AddOrEditBeliefForm(ListView beliefsListView, bool editMode = false)
        {
            InitializeComponent();

            _beliefsListView = beliefsListView;
            _editMode = editMode;

            //Default Values 
            beliefVisibilityComboBox.DataSource = EmotionalAppraisal.EmotionalAppraisalAsset.GetKnowledgeVisibilities();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void addBeliefButton_Click(object sender, EventArgs e)
        {
           

          
        }

        private void beliefVisibilityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void addOrEditBeliefButton_Click(object sender, EventArgs e)
        {
            

            //clear errors
            addBeliefErrorProvider.Clear();

            //Check for errors:
            if (string.IsNullOrWhiteSpace(this.beliefNameTextBox.Text))
            {
                beliefNameTextBox.Focus();
                addBeliefErrorProvider.SetError(beliefNameTextBox, Resources.RequiredFieldError);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.beliefValueTextBox.Text))
            {
                beliefNameTextBox.Focus();
                addBeliefErrorProvider.SetError(beliefValueTextBox, Resources.RequiredFieldError);
                return;
            }

            if (!_editMode)
            {
                var lvi = new ListViewItem(this.beliefNameTextBox.Text);
                lvi.SubItems.Add(this.beliefValueTextBox.Text);
                lvi.SubItems.Add(this.beliefVisibilityComboBox.Text);
                _beliefsListView.Items.Add(lvi);



            }
            else
            {
                _beliefsListView.SelectedItems[0].Text = beliefNameTextBox.Text;
                _beliefsListView.SelectedItems[0].SubItems[1].Text = beliefValueTextBox.Text;
                _beliefsListView.SelectedItems[0].SubItems[2].Text = beliefVisibilityComboBox.Text;
            }
            

            if (_editMode)
            {
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

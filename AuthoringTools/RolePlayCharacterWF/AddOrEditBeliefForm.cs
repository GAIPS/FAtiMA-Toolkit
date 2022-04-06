using System;
using System.Windows.Forms;
using RolePlayCharacterWF.ViewModels;
using RolePlayCharacter;

namespace RolePlayCharacterWF
{
    public partial class AddOrEditBeliefForm : Form
    {
        private KnowledgeBaseVM _knowledgeBaseVm;
        private BeliefDTO _beliefToEdit;

        public AddOrEditBeliefForm(KnowledgeBaseVM kbVM, BeliefDTO beliefToEdit = null)
        {
            InitializeComponent();

            //DefaultValues
            beliefNameTextBox.Value = WellFormedNames.Name.BuildName("Bel(A)");
            perspectiveTextBox.Value = WellFormedNames.Name.SELF_SYMBOL;
            beliefValueTextBox.Value = WellFormedNames.Name.BuildName("True");
            certaintyTextBox.Value = 1;

            //Restrictions
            certaintyTextBox.HasBounds = true;
            certaintyTextBox.MaxValue = 1;
            certaintyTextBox.MinValue = 0;

            beliefNameTextBox.AllowLiteral = false;
            beliefNameTextBox.AllowNil = false;
            beliefNameTextBox.AllowUniversal = false;
            beliefNameTextBox.AllowVariable = false;

            beliefValueTextBox.AllowComposedName = false;
            beliefValueTextBox.AllowNil = false;
            beliefValueTextBox.AllowUniversal = false;
            beliefValueTextBox.AllowVariable = false;

            perspectiveTextBox.AllowNil = true;

            _knowledgeBaseVm = kbVM;
            _beliefToEdit = beliefToEdit;

            if (beliefToEdit != null)
            {
                this.Text = "Edit Belief";
                this.addOrEditBeliefButton.Text = "Update";

                beliefNameTextBox.Value = (WellFormedNames.Name)beliefToEdit.Name;
                beliefValueTextBox.Value = (WellFormedNames.Name)beliefToEdit.Value;
                perspectiveTextBox.Value = (WellFormedNames.Name)beliefToEdit.Perspective;
                certaintyTextBox.Value = beliefToEdit.Certainty;
            }
        }

        private void addOrEditBeliefButton_Click(object sender, EventArgs e)
        {
            //clear errors
            addBeliefErrorProvider.Clear();
            try
            {
                var newBelief = new BeliefDTO
                {
                    Name = this.beliefNameTextBox.Value.ToString(),
                    Perspective = this.perspectiveTextBox.Value.ToString(),
                    Value = this.beliefValueTextBox.Value.ToString(),
                    Certainty = this.certaintyTextBox.Value
                };
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
                Close();
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

        private void certaintyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void floatFieldBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void floatFieldBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void perspectiveTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddOrEditBeliefForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void beliefNameTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

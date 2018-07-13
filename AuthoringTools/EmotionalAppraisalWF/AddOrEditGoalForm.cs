using System.Windows.Forms;
using EmotionalAppraisalWF.ViewModels;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisal;
using WellFormedNames;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditGoalForm : Form
    {
        private EmotionalAppraisalAsset ea;
        private GoalDTO goal;

        public AddOrEditGoalForm(EmotionalAppraisalAsset asset, GoalDTO goalDto)
        {
            InitializeComponent();
            ea = asset;
            
            //Validators
            textBoxGoalName.AllowNil = false;
            textBoxGoalName.AllowUniversal = false;
            if(goalDto != null)
            {
                this.Text = "Edit Goal";
                buttonAddOrEditGoal.Text = "Update";
                goal = goalDto;
                textBoxGoalName.Value = (Name)goalDto.Name;
                floatFieldBoxSignificance.Value = goalDto.Significance;
                floatFieldBoxLikelihood.Value = goalDto.Likelihood;
            }
            else
            {
                goal = new GoalDTO();
                textBoxGoalName.Value = (Name)"g1";
            }
        }

        private void buttonAddOrEditGoal_Click(object sender, System.EventArgs e)
        {
            try
            {
                goal.Name = textBoxGoalName.Value.ToString();
                goal.Significance = floatFieldBoxSignificance.Value;
                goal.Likelihood = floatFieldBoxLikelihood.Value;
                ea.AddOrUpdateGoal(goal);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Close();
        }
  
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

       }

        private void AddOrEditGoalForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }

}

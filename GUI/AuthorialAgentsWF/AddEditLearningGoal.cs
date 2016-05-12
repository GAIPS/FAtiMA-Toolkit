using System;
using System.Windows.Forms;

namespace AuthorialAgentsWF
{
    public partial class AddEditLearningGoal : Form
    {
        private string _nameLearningGoal;
        private string _valueLearningGoal;
        private ListView _learningGoalsList;

        public string NameLearningGoal
        {
            get { return _nameLearningGoal; }

            set { _nameLearningGoal = value; }
        }

        public string ValueLearningGoal
        {
            get { return _valueLearningGoal; }

            set { _valueLearningGoal = value; }
        }

        public ListView LearningGoalsList
        {
            get { return _learningGoalsList; }

            set { _learningGoalsList = value; }
        }

        public AddEditLearningGoal(ListView learningGoalsList)
        {
            InitializeComponent();
            _learningGoalsList = learningGoalsList;
        }

        private void learningGoalSaveButton_Click(object sender, EventArgs e)
        {
            _nameLearningGoal = nameTextBox.Text;
            _valueLearningGoal = valueTextBox.Text;
            var learningGoal = new ListViewItem(new string[]
                {
                    _nameLearningGoal.Trim(),
                    _valueLearningGoal.Trim(),
                });
            _learningGoalsList.Items.Add(learningGoal);
            this.Close();
            
        }
    }
}

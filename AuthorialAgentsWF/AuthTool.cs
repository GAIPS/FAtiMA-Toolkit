using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisalWF;


namespace AuthorialAgentsWF
{

    public partial class AuthTool : Form
    {
        private ListView _learningGoalsList;
        private ListView _scenarioList;

        private ListView.SelectedListViewItemCollection _scenarioSelected;
        private ListView.SelectedListViewItemCollection _goalSelected;

        public ListView LearningGoalsList
        {
            get { return _learningGoalsList; }

            set { _learningGoalsList = value; }
        }

        public ListView ScenarioList
        {
            get { return _scenarioList; }

            set { _scenarioList = value; }
        }

        public ListView.SelectedListViewItemCollection ScenarioSelected
        {
            get { return _scenarioSelected; }

            set { _scenarioSelected = value; }
        }

        public ListView.SelectedListViewItemCollection GoalSelected
        {
            get { return _goalSelected; }

            set { _goalSelected = value; }
        }

        public AuthTool()
        {
            InitializeComponent();
            _learningGoalsList = learningGoalsView;
            _scenarioList = scenariosListView;
        }


        private void addScenario_Click(object sender, EventArgs e)
        {
            var addScenarioForm = new AddEditScenario(this);
            addScenarioForm.ShowDialog();
        }

        private void editScenarios_Click(object sender, EventArgs e)
        {
            var addScenarioForm = new AddEditScenario(this);
            addScenarioForm.ShowDialog();
        }

        private void removeScenarios_Click(object sender, EventArgs e)
        {
            if(_scenarioSelected != null)
            {
                _scenarioList.Items.Remove(_scenarioList.SelectedItems[0]);
            }

            _scenarioSelected = null;
        }

        private void editActions_Click(object sender, EventArgs e)
        {
            var addEditActions = new AddEditActions();
            addEditActions.ShowDialog();
        }

        private void addLearningGoal_Click(object sender, EventArgs e)
        {
            var addEdditLearningGoal = new AddEditLearningGoal(_learningGoalsList);
            addEdditLearningGoal.ShowDialog();
        }

        private void editLearningGoals_Click(object sender, EventArgs e)
        {
            var addEdditLearningGoal = new AddEditLearningGoal(_learningGoalsList);
            addEdditLearningGoal.ShowDialog();
        }

        private void removeLearningGoals_Click(object sender, EventArgs e)
        {
            if (_goalSelected != null)
            {
                _learningGoalsList.Items.Remove(_learningGoalsList.SelectedItems[0]);
            }

            _goalSelected = null;
        }

        private void scenariosListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _scenarioSelected = _scenarioList.SelectedItems;
        }

        private void learningGoalsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _goalSelected = _learningGoalsList.SelectedItems;
        }
    }
}

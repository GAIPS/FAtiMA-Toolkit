using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthorialAgentsWF
{

    public partial class AuthTool : Form
    {
       
        public AuthTool()
        {
            InitializeComponent();
        }

        private void addScenario_Click(object sender, EventArgs e)
        {
            var addScenarioForm = new AddEditScenario();
            addScenarioForm.ShowDialog();
        }

        private void editScenarios_Click(object sender, EventArgs e)
        {
            var addScenarioForm = new AddEditScenario();
            addScenarioForm.ShowDialog();
        }

        private void removeScenarios_Click(object sender, EventArgs e)
        {

        }

        private void editActions_Click(object sender, EventArgs e)
        {
            var addEditActions = new AddEditActions();
            addEditActions.ShowDialog();
        }

        private void addLearningGoal_Click(object sender, EventArgs e)
        {
            var addEdditLearningGoal = new AddEditLearningGoal();
            addEdditLearningGoal.ShowDialog();
        }

        private void editLearningGoals_Click(object sender, EventArgs e)
        {
            var addEdditLearningGoal = new AddEditLearningGoal();
            addEdditLearningGoal.ShowDialog();
        }

        private void removeLearningGoals_Click(object sender, EventArgs e)
        {

        }
    }
}

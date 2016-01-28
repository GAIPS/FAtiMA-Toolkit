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
    public partial class AddEditLearningGoal : Form
    {

        private string _nameLearningGoal;

        private string _valueLearningGoal;

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

        public AddEditLearningGoal()
        {
            InitializeComponent();
        }

        private void learningGoalSaveButton_Click(object sender, EventArgs e)
        {
            _nameLearningGoal = nameTextBox.Text;
            _valueLearningGoal = valueTextBox.Text;
        }
    }
}

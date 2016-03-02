using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditEmotionForm : Form
    {
        private EmotionalStateVM _emotionalStateVm;
        private EmotionDTO _emotionToEdit;

        public AddOrEditEmotionForm(EmotionalStateVM emotionalStateVM, EmotionDTO emotionToEdit = null)
        {
            InitializeComponent();

            _emotionalStateVm = emotionalStateVM;
            _emotionToEdit = emotionToEdit;


            //Default Values 
            comboBoxIntensity.Text = "1";
            comboBoxEmotionType.DataSource = _emotionalStateVm.EmotionTypes;
            // beliefVisibilityComboBox.DataSource = _knowledgeBaseVm.GetKnowledgeVisibilities();
            //beliefVisibilityComboBox.SelectedIndex = 0;

            /*if (beliefToEdit != null)
            {
                this.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Edit_Belief;
                this.addOrEditButton.Text = Resources.AddOrEditBeliefForm_AddOrEditBeliefForm_Update;

                beliefNameTextBox.Text = beliefToEdit.Name;
                beliefValueTextBox.Text = beliefToEdit.Value;
                beliefVisibilityComboBox.SelectedIndex = beliefVisibilityComboBox.FindString(beliefToEdit.Visibility.ToString());
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

        private void comboBoxIntensity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addOrEditButton_Click(object sender, EventArgs e)
        {

        }
    }
}

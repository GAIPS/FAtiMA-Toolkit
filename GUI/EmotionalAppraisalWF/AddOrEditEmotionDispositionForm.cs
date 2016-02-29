using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisalWF.ViewModels;


namespace EmotionalAppraisalWF
{
    public partial class AddOrEditEmotionDispositionForm : Form
    {
        private EmotionDispositionsVM _emotionDispositionsVM;
        private EmotionDispositionDTO _emotionDispositionToEdit;
        
        public AddOrEditEmotionDispositionForm(EmotionDispositionsVM emotionDispositionsVM, EmotionDispositionDTO emotionDispositionToEdit = null)
        {
            IEnumerable<int> seq = Enumerable.Range(1, 10);
            InitializeComponent();

            _emotionDispositionsVM = emotionDispositionsVM;
            _emotionDispositionToEdit = emotionDispositionToEdit;

            //Default Values 
            comboBoxThreshold.Items.AddRange(seq.Cast<object>().ToArray());
            comboBoxThreshold.SelectedIndex = 0;

            comboBoxDecay.Items.AddRange(seq.Cast<object>().ToArray());
            comboBoxDecay.SelectedIndex = 0;

            comboBoxEmotionType.DataSource = _emotionDispositionsVM.EmotionTypes;

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

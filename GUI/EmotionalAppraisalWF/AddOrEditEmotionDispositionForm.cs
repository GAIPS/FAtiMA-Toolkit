using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
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

	        comboBoxEmotionType.DataSource = EmotionalAppraisalAsset.EmotionTypes;

            if (emotionDispositionToEdit != null)
            {
                this.Text = Resources.EditEmotionDispositionFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;

                comboBoxEmotionType.SelectedIndex = comboBoxEmotionType.FindString(emotionDispositionToEdit.Emotion);
                comboBoxDecay.SelectedIndex = comboBoxDecay.FindString(emotionDispositionToEdit.Decay.ToString());
                comboBoxThreshold.SelectedIndex = comboBoxThreshold.FindString(emotionDispositionToEdit.Threshold.ToString());
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

        private void comboBoxIntensity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            var newEmotionDisposition = new EmotionDispositionDTO()
            {
                Emotion = comboBoxEmotionType.Text,
                Decay = int.Parse(comboBoxDecay.Text),
                Threshold = int.Parse(comboBoxThreshold.Text)
            };

            if (_emotionDispositionToEdit == null)
            {
                try
                {   
                    this._emotionDispositionsVM.AddEmotionDisposition(newEmotionDisposition);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(Resources.EmotionDispositionAlreadyExistsExceptionMessage, Resources.ErrorDialogTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this._emotionDispositionsVM.UpdateEmotionDisposition(_emotionDispositionToEdit, newEmotionDisposition);
            }
            this.Close();
        }
    }
}

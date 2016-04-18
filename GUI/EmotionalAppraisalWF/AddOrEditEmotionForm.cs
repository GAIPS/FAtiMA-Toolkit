using System;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;

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
            
            if (emotionToEdit != null)
            {
                this.Text = Resources.EditEmotionFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;

                comboBoxIntensity.Text = Math.Round(emotionToEdit.Intensity).ToString();
                comboBoxEmotionType.Text = emotionToEdit.Type;
                textBoxCauseId.Text = emotionToEdit.CauseEventId.ToString();
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
            try
            {
                var newEmotion = new EmotionDTO
                {
                    Type = comboBoxEmotionType.Text,
                    Intensity = int.Parse(comboBoxIntensity.Text),
                    CauseEventId = uint.Parse(textBoxCauseId.Text)
                };

                if (_emotionToEdit == null)
                {
                    _emotionalStateVm.AddEmotion(newEmotion);
                }
                else
                {
                    this._emotionalStateVm.UpdateEmotion(_emotionToEdit, newEmotion);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
      }
    }
}

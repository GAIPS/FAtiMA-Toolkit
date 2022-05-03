using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using RolePlayCharacterWF.ViewModels;

namespace RolePlayCharacterWF
{
    public partial class AddOrEditEmotionForm : Form
    {
        private EmotionalStateVM _emotionalStateVm;
        private EmotionDTO _emotionToEdit;
        private List<uint> _eventIds;

        public AddOrEditEmotionForm(Form parent, EmotionalStateVM emotionalStateVM, List<uint> eventIds, EmotionDTO emotionToEdit = null)
        {
            InitializeComponent();

            this.toolTip1.SetToolTip(parent, "FAtiMA-Toolkit");

            _emotionalStateVm = emotionalStateVM;
            _emotionToEdit = emotionToEdit;
            _eventIds = eventIds;
            //Default Values 
            comboBoxIntensity.Text = "1";
            comboBoxEmotionType.DataSource = EmotionalAppraisalAsset.EmotionTypes;
            if(eventIds.Count == 0)
                addOrEditButton.Enabled = false;
            this.eventComboBox.DataSource = eventIds;

            if (emotionToEdit != null)
            {
                this.Text = "Update Emotion";
                this.addOrEditButton.Text = "Update";

                comboBoxIntensity.Text = Math.Round(emotionToEdit.Intensity).ToString();
                comboBoxEmotionType.Text = emotionToEdit.Type;
                if(emotionToEdit.Target != null)
                targetBox.Text = emotionToEdit.Target.ToString();
            }

            
        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void beliefVisibilityComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    CauseEventId = _eventIds[eventComboBox.SelectedIndex],
                    Target = targetBox.Text
                };

                try
                {
                    if (_emotionToEdit == null)
                    {
                        _emotionalStateVm.AddEmotion(newEmotion);
                    }
                    else
                    {
                        this._emotionalStateVm.UpdateEmotion(_emotionToEdit, newEmotion);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
      }

      private void AddOrEditEmotionForm_KeyDown(object sender, KeyEventArgs e)
      {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
      }
    }
}

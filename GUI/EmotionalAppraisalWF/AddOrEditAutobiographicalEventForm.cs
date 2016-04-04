using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using KnowledgeBase;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditAutobiographicalEventForm : Form
    {
        private AutobiographicalMemoryVM _autobiographicalMemoryVm;
        private EventDTO _eventToEdit;
        
        public AddOrEditAutobiographicalEventForm(AutobiographicalMemoryVM amVM, EventDTO eventToEdit = null)
        {
            InitializeComponent();

            _autobiographicalMemoryVm = amVM;
            _eventToEdit = eventToEdit;

            comboBoxEventType.DataSource = _autobiographicalMemoryVm.EventTypes;
            
            if (eventToEdit != null)
            {
                this.Text = Resources.EditAutobiographicalEventFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                EventDTO newEvent = null;
                if (comboBoxEventType.Text == "PropertyChange")
                {
                    newEvent = new PropertyChangeEventDTO
                    {
                        Subject = textBoxSubject.Text,
                        Property = textBoxObject.Text,
                        NewValue = textBoxTarget.Text,
                        Time = ulong.Parse(textBoxTime.Text)
                    };
                
                }else if (comboBoxEventType.Text == "Action")
                {
                    newEvent = new ActionEventDTO()
                    {
                        Subject = textBoxSubject.Text,
                        Action = textBoxObject.Text,
                        Target = textBoxTarget.Text,
                        Time = ulong.Parse(textBoxTime.Text)
                    };
                }

                _autobiographicalMemoryVm.AddEventRecord(newEvent);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddOrEditAutobiographicalEventForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxEventType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((ComboBox) sender).Text == "PropertyChange")
            {
                labelObject.Text = "Property:";
                labelTarget.Text = "NewValue:";
            }
            else if (((ComboBox)sender).Text == "Action")
            {
                labelObject.Text = "Action:";
                labelTarget.Text = "Target";
            }
        }
    }
}

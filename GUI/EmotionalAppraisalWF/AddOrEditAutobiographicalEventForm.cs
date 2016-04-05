using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using AutobiographicMemory;
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

                _eventToEdit = _autobiographicalMemoryVm.RetrieveEventRecord(_eventToEdit.Id);
                var propertyEvent = _eventToEdit as PropertyChangeEventDTO;
                if (propertyEvent != null)
                { 
                    comboBoxEventType.Text = Constants.PROPERTY_CHANGE_EVENT;
                    textBoxSubject.Text = propertyEvent.Subject;
                    textBoxObject.Text = propertyEvent.Property;
                    textBoxTarget.Text = propertyEvent.NewValue;
                    textBoxTime.Text = propertyEvent.Time.ToString();
                }
                var actionEvent = _eventToEdit as ActionEventDTO;
                if (actionEvent != null)
                {
                    textBoxSubject.Text = actionEvent.Subject;
                    textBoxObject.Text = actionEvent.Action;
                    textBoxTarget.Text = actionEvent.Target;
                    textBoxTime.Text = actionEvent.Time.ToString();
                }

            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                EventDTO newEvent = null;
                if (comboBoxEventType.Text == Constants.PROPERTY_CHANGE_EVENT)
                {
                    newEvent = new PropertyChangeEventDTO
                    {
                        Subject = textBoxSubject.Text,
                        Property = textBoxObject.Text,
                        NewValue = textBoxTarget.Text,
                        Time = ulong.Parse(textBoxTime.Text)
                    };
                
                }else if (comboBoxEventType.Text == Constants.ACTION_EVENT)
                {
                    newEvent = new ActionEventDTO()
                    {
                        Subject = textBoxSubject.Text,
                        Action = textBoxObject.Text,
                        Target = textBoxTarget.Text,
                        Time = ulong.Parse(textBoxTime.Text)
                    };
                }

                if (_eventToEdit != null)
                {
                    newEvent.Id = _eventToEdit.Id;
                    _autobiographicalMemoryVm.UpdateEventRecord(newEvent);
                }
                else
                {
                    _autobiographicalMemoryVm.AddEventRecord(newEvent);
                }
                
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
            if (((ComboBox) sender).Text == Constants.PROPERTY_CHANGE_EVENT)
            {
                labelObject.Text = "Property:";
                labelTarget.Text = "New Value:";
            }
            else if (((ComboBox)sender).Text == Constants.ACTION_EVENT)
            {
                labelObject.Text = "Action:";
                labelTarget.Text = "Target";
            }
        }
    }
}

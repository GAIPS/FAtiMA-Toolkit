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
    public partial class AddOrEditAutobiographicalEventForm : Form
    {
        private AutobiographicalMemoryVM _autobiographicalMemoryVm;
        private EventDTO _eventToEdit;
        
        public AddOrEditAutobiographicalEventForm(AutobiographicalMemoryVM amVM, EventDTO eventToEdit = null)
        {
            InitializeComponent();

            _autobiographicalMemoryVm = amVM;
            _eventToEdit = eventToEdit;
        
            
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
                var newEvent = new EventDTO
                {
                    Event = textBoxEvent.Text,
                    Time = ulong.Parse(textBoxTime.Text)
                };
                _autobiographicalMemoryVm.AddEventRecord(newEvent);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

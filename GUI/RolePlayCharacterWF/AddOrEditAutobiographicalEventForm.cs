using System;
using System.Windows.Forms;
using AutobiographicMemory;
using AutobiographicMemory.DTOs;
using RolePlayCharacterWF.ViewModels;

namespace RolePlayCharacterWF
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

            comboBoxEventType.DataSource = AutobiographicalMemoryVM.EventTypes;
            
            if (eventToEdit != null)
            {
                this.Text = "Edit Event Record";
                this.addOrEditButton.Text = "Update";

                _eventToEdit = _autobiographicalMemoryVm.RetrieveEventRecord(_eventToEdit.Id);
                var propertyEvent = _eventToEdit as PropertyChangeEventDTO;
                if (propertyEvent != null)
                { 
                    comboBoxEventType.Text = Constants.PROPERTY_CHANGE_EVENT.ToString();
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
                if (comboBoxEventType.Text == Constants.PROPERTY_CHANGE_EVENT.ToString())
                {
                    newEvent = new PropertyChangeEventDTO
                    {
                        Subject = textBoxSubject.Text,
                        Property = textBoxObject.Text,
                        NewValue = textBoxTarget.Text,
                        Time = ulong.Parse(textBoxTime.Text)
                    };
                
                }else if (comboBoxEventType.Text == Constants.ACTION_START_EVENT.ToString() || comboBoxEventType.Text == Constants.ACTION_FINISHED_EVENT.ToString())
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	        if (((ComboBox) sender).Text == Constants.PROPERTY_CHANGE_EVENT.ToString())
	        {
		        labelObject.Text = "Property:";
		        labelTarget.Text = "New Value:";
	        }
	        else
	        {
		        var text = ((ComboBox) sender).Text;
				if (text == Constants.ACTION_START_EVENT.ToString() || text == Constants.ACTION_FINISHED_EVENT.ToString())
				{
					labelObject.Text = "Action:";
					labelTarget.Text = "Target";
				}
			}
        }
    }
}

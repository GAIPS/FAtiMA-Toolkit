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

            textBoxSubject.Value = amVM.CharName;

            //Restrictions
            textBoxSubject.AllowNil = false;
            textBoxSubject.AllowUniversal = false;
            textBoxSubject.AllowVariable = false;

            textBoxTarget.AllowUniversal = false;
            textBoxTarget.AllowVariable = false;

            textBoxObject.AllowVariable = false;
            textBoxObject.AllowUniversal = false;
            textBoxObject.AllowNil = false;

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
                    comboBoxEventType.Text = AMConsts.PROPERTY_CHANGE.ToString();
                    textBoxSubject.Value = (WellFormedNames.Name)propertyEvent.Subject;
                    textBoxObject.Value = (WellFormedNames.Name)propertyEvent.Property;
                    textBoxTarget.Value = (WellFormedNames.Name)propertyEvent.NewValue;
                    textBoxTime.Value = (int)propertyEvent.Time;
                }
                var actionEvent = _eventToEdit as ActionEventDTO;
                if (actionEvent != null)
                {
                    textBoxSubject.Value = (WellFormedNames.Name)actionEvent.Subject;
                    textBoxObject.Value = (WellFormedNames.Name)actionEvent.Action;
                    textBoxTarget.Value = (WellFormedNames.Name)actionEvent.Target;
                    textBoxTime.Value = (int)actionEvent.Time;
                }

            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                EventDTO newEvent = null;
                if (comboBoxEventType.Text == AMConsts.PROPERTY_CHANGE.ToString())
                {
                    newEvent = new PropertyChangeEventDTO
                    {
                        Subject = textBoxSubject.Value.ToString(),
                        Property = textBoxObject.Value.ToString(),
                        NewValue = textBoxTarget.Value.ToString(),
                        Time = (ulong)textBoxTime.Value
                    };
                
                }else if (comboBoxEventType.Text == AMConsts.ACTION_START || comboBoxEventType.Text == AMConsts.ACTION_END)
                {
                    var act = new ActionEventDTO();
                    if(comboBoxEventType.Text == AMConsts.ACTION_START) act.ActionState = ActionState.Start;
                    else act.ActionState = ActionState.Finished;

                    newEvent = new ActionEventDTO()
                    {
                        ActionState = act.ActionState,
                        Subject = textBoxSubject.Value.ToString(),
                        Action = textBoxObject.Value.ToString(),
                        Target = textBoxTarget.Value.ToString(),
                        Time = (ulong)textBoxTime.Value
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
	        if (((ComboBox) sender).Text == AMConsts.PROPERTY_CHANGE.ToString())
	        {
		        labelObject.Text = "Property:";
		        labelTarget.Text = "New Value:";
	        }
	        else
	        {
		        var text = ((ComboBox) sender).Text;
				if (text == AMConsts.ACTION_START || text == AMConsts.ACTION_END)
				{
					labelObject.Text = "Action:";
					labelTarget.Text = "Target";
				}
			}
        }

        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTarget_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelTarget_Click(object sender, EventArgs e)
        {

        }

        private void textBoxObject_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelObject_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSubject_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddOrEditAutobiographicalEventForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

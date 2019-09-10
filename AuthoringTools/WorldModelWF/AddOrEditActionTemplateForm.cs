using AutobiographicMemory;
using System;
using System.Windows.Forms;
using WellFormedNames;
using WorldModel;

namespace WorldModelWF
{
    public partial class AddOrEditActionTemplateForm : Form
    {
        private WorldModelAsset _asset;
        private Name _eventTemplate;

        public AddOrEditActionTemplateForm(WorldModelAsset asset, Name eventTemplate = null, int priority = 0)
        {
            InitializeComponent();

            _eventTemplate = eventTemplate;
            _asset = asset;

            //validationRules
            textBoxSubject.AllowNil = false;
            textBoxSubject.AllowComposedName = false;

            //defaultValues
            textBoxSubject.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
            textBoxObject.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
            textBoxTarget.Value = WellFormedNames.Name.UNIVERSAL_SYMBOL;
            priorityFieldBox.Value = priority;
        
            if (eventTemplate != null)
            {
                this.Text = "Update";
                this.addOrEditButton.Text = "Update";
                textBoxSubject.Value = eventTemplate.GetNTerm(2);
                textBoxObject.Value = eventTemplate.GetNTerm(3);
                textBoxTarget.Value = eventTemplate.GetNTerm(4);
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {


            if (this.Text != "Update")
            {

                _eventTemplate = WellFormedNames.Name.BuildName(
                    (Name) AMConsts.EVENT,
                    (Name) "Action-End",
                    textBoxSubject.Value,
                    textBoxObject.Value,
                    textBoxTarget.Value);

                _asset.addActionTemplate(_eventTemplate, priorityFieldBox.Value);
             
            }

            else
            {

                var _pastTemplate = _eventTemplate;

               

                _eventTemplate = WellFormedNames.Name.BuildName(
                    (Name) AMConsts.EVENT,
                    (Name) "Action-End",
                    textBoxSubject.Value,
                    textBoxObject.Value,
                    textBoxTarget.Value);


                _asset.UpdateActionTemplate(_pastTemplate, _eventTemplate, priorityFieldBox.Value);
               
            }

            Close();
            
        }

        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBoxSubject_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void labelObject_Click(object sender, EventArgs e)
        {

        }

        private void AddOrEditAppraisalRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void AddOrEditAppraisalRuleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
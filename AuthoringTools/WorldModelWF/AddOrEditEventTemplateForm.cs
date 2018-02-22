using AutobiographicMemory;
using Conditions.DTOs;
using EmotionalAppraisal.DTOs;
using System;
using System.Linq;
using System.Windows.Forms;
using WellFormedNames;
using WorldModelWF.Properties;
using EmotionalAppraisal;
using RolePlayCharacter;
using WorldModel;

namespace WorldModelWF
{
    public partial class AddOrEditEventTemplateForm : Form
    {
        private WorldModelAsset _asset;
        private Name _eventTemplate;

        public AddOrEditEventTemplateForm(WorldModelAsset asset, Name eventTemplate = null)
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
            comboBoxEventType.DataSource = EmotionalAppraisalWF.ViewModels.AppraisalRulesVM.EventTypes;


            if (eventTemplate != null)
            {
                var resultingString = eventTemplate.ToString().Split(',');

                this.Text = "Update";
                this.addOrEditButton.Text = "Update";
                comboBoxEventType.Text = resultingString.ElementAt(1);
                textBoxSubject.Value = (Name)resultingString.ElementAt(2);
                textBoxObject.Value =(Name)resultingString.ElementAt(3);
                textBoxTarget.Value =(Name)resultingString.ElementAt(4);
            }
        }

        private void addOrEditButton_Click_1(object sender, EventArgs e)
        {

            _eventTemplate = WellFormedNames.Name.BuildName(
                (Name) AMConsts.EVENT,
                (Name) comboBoxEventType.Text,
                textBoxSubject.Value,
                textBoxObject.Value,
                textBoxTarget.Value);

            _asset.addEventTemplate(_eventTemplate);
             
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
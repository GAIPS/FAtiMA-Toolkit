using System;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;
using WellFormedNames;

namespace SocialImportanceWF
{
	public partial class AddOrEditAttributionRuleForm : Form
	{
		private AttributionRuleDTO _dto;
		private AttributionRuleVM _vm;

		public ObjectView<AttributionRuleDTO> AddedObject { get; private set; } = null;

		public AddOrEditAttributionRuleForm(AttributionRuleVM vm, AttributionRuleDTO dto)
		{
			InitializeComponent();

            //Validators
            _targetVariableBox.AllowUniversal = false;
            _targetVariableBox.AllowNil = false;
            _targetVariableBox.AllowComposedName = false;
            _targetVariableBox.AllowLiteral = false;
            _valueFieldBox.OnlyIntOrVariable = true;

            _dto = dto;
			_vm = vm;
			_ruleDescriptionTextBox.Text = dto.Description;
			_valueFieldBox.Value = dto.Value;
			_targetVariableBox.Value = (Name) dto.Target;

            if(_dto.Id != Guid.Empty)
            {
                this.Text = "Edit SI Attribution Rule";
                button1.Text = "Update";
            }
			
		}

		private void Update_Rule(object sender, EventArgs evt)
		{
			try
			{
				_dto.Description = _ruleDescriptionTextBox.Text;
				_dto.Value = _valueFieldBox.Value;
				_dto.Target = _targetVariableBox.Value;
				AddedObject = _vm.AddOrUpdateRule(_dto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Close();
		}
        
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }

        private void AddOrEditAttributionRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void AddOrEditAttributionRuleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}



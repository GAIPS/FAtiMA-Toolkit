using System;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using CommeillFaut.DTOs;
using CommeillFautWF.ViewModels;
using WellFormedNames;

namespace CommeillFautWF
{
	public partial class AddInfluenceRule : Form
	{
		private InfluenceRuleDTO _dto;
		private InfluenceRuleVM _vm;

		public ObjectView<InfluenceRuleDTO> AddedObject { get; private set; } = null;

		public AddInfluenceRule(InfluenceRuleVM vm, InfluenceRuleDTO dto)
		{
			InitializeComponent();

			_dto = dto;
			_vm = vm;

			_ruleDescriptionTextBox.Text = dto.RuleName;
			_valueFieldBox.Value = dto.Value;
			_targetVariableBox.Value = (Name) dto.Target;

			button1.Text = (_dto.Id == Guid.Empty) ? "Add" : "Update";
		}

		private void Update_Rule(object sender, EventArgs evt)
		{
			try
			{
				_dto.RuleName = _ruleDescriptionTextBox.Text;
				_dto.Value = _valueFieldBox.Value;
				_dto.Target = _targetVariableBox.Value.ToString();
				_vm.AddOrUpdateInfluenceRule(_dto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Close();
		}

        private void _ruleDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using KnowledgeBase.WellFormedNames;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;

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
				AddedObject = _vm.AddOrUpdateRule(_dto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Close();
		}
	}
}

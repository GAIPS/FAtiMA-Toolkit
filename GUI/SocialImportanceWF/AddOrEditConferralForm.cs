using System;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;
using WellFormedNames;

namespace SocialImportanceWF
{
	public partial class AddOrEditConferralForm : Form
	{
		private ConferralDTO _dto;
		private ConferralsVM _vm;

		public ObjectView<ConferralDTO> AddedObject { get; private set; } = null;

		public AddOrEditConferralForm(ConferralsVM vm, ConferralDTO dto)
		{
			InitializeComponent();

			_dto = dto == null?new ConferralDTO() : dto;
			_vm = vm;

			if (dto != null)
			{
				_actionNameField.Value = (Name)dto.Action;
				_valueFieldBox.Value = dto.ConferralSI;
				_targetVariableBox.Value = (Name)dto.Target;
			}

			button1.Text = (_dto.Id == Guid.Empty) ? "Add" : "Update";
		}

		private void Update_Rule(object sender, EventArgs evt)
		{
			try
			{
				_dto.Action = _actionNameField.Value.ToString();
				_dto.ConferralSI = _valueFieldBox.Value;
				_dto.Target = _targetVariableBox.Value.ToString();
				AddedObject = _vm.AddOrUpdateConferral(_dto);
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

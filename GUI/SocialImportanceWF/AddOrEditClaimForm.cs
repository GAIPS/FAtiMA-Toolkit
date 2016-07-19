using System;
using System.Drawing;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;
using WellFormedNames;

namespace SocialImportanceWF
{
	public partial class AddOrEditClaimForm : Form
	{
		private ClaimsVM _vm;
		private ClaimDTO _previousDto;
		private bool _add;

		public ObjectView<ClaimDTO> AddedObject { get; private set; }

		public AddOrEditClaimForm(ClaimsVM vm, ClaimDTO dto)
		{
			InitializeComponent();

			_vm = vm;
			_previousDto = dto;

			_add = dto == null;
			_button.Text = _add ? "Add" : "Update";
			if (dto != null)
			{
				_actionTemplateField.Value = (Name)dto.ActionTemplate;
				_clamSI.Value = dto.ClaimSI;
			}
		}

		private void OnButton_Click(object sender, System.EventArgs evt)
		{
			var dto = new ClaimDTO() {ActionTemplate = _actionTemplateField.Value.ToString(),ClaimSI = _clamSI.Value};

			try
			{
				AddedObject = _add ? _vm.AddClaim(dto) : _vm.ReplaceClaim(_previousDto,dto);
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

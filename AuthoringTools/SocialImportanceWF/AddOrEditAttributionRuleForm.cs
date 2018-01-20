using System;
using System.Windows.Forms;
using SocialImportance.DTOs;
using WellFormedNames;
using SocialImportance;
using System.Linq;
using GAIPS.AssetEditorTools;
using Equin.ApplicationFramework;

namespace SocialImportanceWF
{
	public partial class AddOrEditAttributionRuleForm : Form
	{
		private AttributionRuleDTO dto;
        private SocialImportanceAsset asset;
        private DataGridView table;
        private BindingListView<AttributionRuleDTO> list;

        public AddOrEditAttributionRuleForm(SocialImportanceAsset asset, DataGridView table, AttributionRuleDTO dto, BindingListView<AttributionRuleDTO> list)
		{
			InitializeComponent();

            this.dto = dto;
            this.asset = asset;
            this.table = table;
            this.list = list;

            //Validators
            _targetVariableBox.AllowUniversal = false;
            _targetVariableBox.AllowNil = false;
            _targetVariableBox.AllowComposedName = false;
            _targetVariableBox.AllowLiteral = false;
            _valueFieldBox.OnlyIntOrVariable = true;

			_ruleDescriptionTextBox.Text = dto.Description;
			_valueFieldBox.Value = dto.Value;
			_targetVariableBox.Value = (Name) dto.Target;

            if(this.dto.Id != Guid.Empty)
            {
                this.Text = "Edit SI Attribution Rule";
                button1.Text = "Update";
            }
			
		}

		private void Update_Rule(object sender, EventArgs evt)
		{
			try
			{
				dto.Description = _ruleDescriptionTextBox.Text;
				dto.Value = _valueFieldBox.Value;
				dto.Target = _targetVariableBox.Value;

                if (dto.Id == Guid.Empty)
                {
                    dto.Id = asset.AddAttributionRule(dto).Id;
                }
                else
                {
                    asset.UpdateAttributionRule(dto);
                }
                EditorTools.RefreshTable(table, list, asset.GetAttributionRules(), dto.Id);
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

        private void AddOrEditAttributionRuleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void AddOrEditAttributionRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}



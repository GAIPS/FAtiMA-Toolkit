using System;
using System.Windows.Forms;
using CommeillFaut.DTOs;
using CommeillFautWF.ViewModels;
using Equin.ApplicationFramework;

using WellFormedNames;

namespace CommeillFautWF
{
    public partial class AddOrEditInfluenceRuleForm : Form
    {
        private InfluenceRuleDTO _dto;
        private InfluenceRuleVM _vm;

        public ObjectView<InfluenceRuleDTO> AddedObject { get; private set; } = null;

        public AddOrEditInfluenceRuleForm(InfluenceRuleVM vm, InfluenceRuleDTO dto)
        {
            InitializeComponent();

            _dto = dto == null ? new InfluenceRuleDTO() : dto;
            _vm = vm;

            if (dto != null)
            {
                _ruleDescriptionTextBox.Text = dto.RuleName;
                _valueFieldBox.Value = dto.Value;
                _targetVariableBox.Value = (Name) dto.Target;
            }

            button1.Text = (_dto.Id == Guid.Empty) ? "Add" : "Update";
        }

        private void Update_Rule(object sender, EventArgs evt)
        {
            try
            {
                _dto.RuleName = _ruleDescriptionTextBox.Text;
                _dto.Value = _valueFieldBox.Value;
                _dto.Target = _targetVariableBox.Value.ToString();
              
                AddedObject = _vm.AddOrUpdateInfluenceRule(_dto);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Close();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _ruleDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddOrEditInfluenceRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void _targetVariableBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

}

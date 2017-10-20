using System;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using CommeillFaut.DTOs;
using CommeillFautWF.ViewModels;
using Conditions.DTOs;
using GAIPS.AssetEditorTools;
using WellFormedNames;

namespace CommeillFautWF
{
	public partial class AddInfluenceRule : Form
	{
		private InfluenceRuleDTO _dto;
		private InfluenceRuleVM _vm;
	    private string _socialExchangeName;

		public ObjectView<InfluenceRuleDTO> AddedObject { get; private set; } = null;

		public AddInfluenceRule(InfluenceRuleVM vm, InfluenceRuleDTO dto, string _exchangeName)
		{
			InitializeComponent();
		    _socialExchangeName = _exchangeName;
		    ExchangeLabel.Text = _socialExchangeName;

            _dto = dto;
		    _vm = vm;

    
          conditionSetEditorControl1.View = _vm.ConditionSetView;

            button1.Text = (_dto.Id == Guid.Empty) ? "Add" : "Update";


		}

	/*	private void Update_Rule(object sender, EventArgs evt)
		{
			try
			{
			//	_dto.RuleName = _ruleDescriptionTextBox.Text;
			

			    _dto.RuleConditions = new ConditionSetDTO();

				_vm.AddOrUpdateInfluenceRule(_dto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Close();
		}*/

        private void _ruleDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _attRulesDataView_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OnRuleSelectionChanged()
        {
            var obj = genericPropertyDataGridControler1.CurrentlySelected;
            if (obj == null)
            {
                _vm.Selection = Guid.Empty;
                return;
            }

            var dto = ((ObjectView<InfluenceRuleDTO>)obj).Object;
            _vm.Selection = dto.Id;
        }

        private void genericPropertyDataGridControler1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Close();
        }

        private void conditionSetEditorControl1_Load(object sender, EventArgs e)
        {

        }
    }
}

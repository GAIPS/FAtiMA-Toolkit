using System;
using GAIPS.AssetEditorTools;
using SocialImportance;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;

namespace SocialImportanceWF
{
	public partial class MainForm : BaseSIForm
	{
		private AttributionRuleVM _attributionRulesVM;

		public MainForm()
		{
			InitializeComponent();
			_attributionRulesVM = new AttributionRuleVM(this);
			_dataview_attributionRules.DataSource = _attributionRulesVM.RuleList;
			_dataview_attributionRules.Columns[PropertyUtil.GetName<AttributionRuleDTO>(dto => dto.Id)].Visible = false;
			_dataview_attributionRules.Columns[PropertyUtil.GetName<AttributionRuleDTO>(dto => dto.Conditions)].Visible = false;
			_dataview_attributionRules.Columns[PropertyUtil.GetName<AttributionRuleDTO>(dto => dto.Target)].Visible = false;

			conditionSetEditor.View = _attributionRulesVM.ConditionSetView;
		}

		#region Overrides of BaseAssetForm<SocialImportanceAsset>

		protected override void LoadAssetData(SocialImportanceAsset asset)
		{
			_attributionRulesVM.Reload();
			_dataview_attributionRules.ClearSelection();
		}

		#endregion

		private void AddAttributionRule(object sender, EventArgs e)
		{
			_attributionRulesVM.CreateNewAttributionRule();
		}

		private void attributionRules_SelectionChanged(object sender, EventArgs e)
		{
			var cells = _dataview_attributionRules.SelectedRows;
			var enable = cells.Count > 0;
			
			if (enable)
				_attributionRulesVM.Selection = cells[0].Index;
			else
				_attributionRulesVM.Selection = -1;
		}
	}
}

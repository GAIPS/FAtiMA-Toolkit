using System;
using Equin.ApplicationFramework;
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

			//Attribution Rules
			_attributionRulesVM = new AttributionRuleVM(this);
			_attRulesDataView.DataController = _attributionRulesVM;
			_attRulesDataView.OnSelectionChanged += OnRuleSelectionChanged;
			
			_attRuleConditionSetEditor.View = _attributionRulesVM.ConditionSetView;

		}

		private void OnRuleSelectionChanged()
		{
			var obj = _attRulesDataView.CurrentlySelected;
			if (obj == null)
			{
				_attributionRulesVM.Selection = Guid.Empty;
				return;
			}

			var dto = ((ObjectView<AttributionRuleDTO>) obj).Object;
			_attributionRulesVM.Selection = dto.Id;
		}

		#region Overrides of BaseAssetForm<SocialImportanceAsset>

		protected override void OnAssetDataLoaded(SocialImportanceAsset asset)
		{
			_attributionRulesVM.Reload();
			_attRulesDataView.ClearSelection();
			_attRulesDataView.GetColumnByName(PropertyUtil.GetPropertyName<AttributionRuleDTO>(dto => dto.Id)).Visible = false;
			_attRulesDataView.GetColumnByName(PropertyUtil.GetPropertyName<AttributionRuleDTO>(dto => dto.Conditions)).Visible = false;
		}

        #endregion

        private void _attRulesDataView_Load(object sender, EventArgs e)
        {

        }
    }
}

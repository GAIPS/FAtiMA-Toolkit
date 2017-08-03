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
		private ClaimsVM _claimsVM;
		private ConferralsVM _conferralsVM;

		public MainForm()
		{
			InitializeComponent();

			//Attribution Rules
			_attributionRulesVM = new AttributionRuleVM(this);
			_attRulesDataView.DataController = _attributionRulesVM;
			_attRulesDataView.OnSelectionChanged += OnRuleSelectionChanged;
			
			_attRuleConditionSetEditor.View = _attributionRulesVM.ConditionSetView;

			//Claims
			_claimsVM = new ClaimsVM(this);
			_claimDataView.DataController = _claimsVM;

			//Conferrals
			_conferralsVM = new ConferralsVM(this);
			_conferralsDataView.DataController = _conferralsVM;
			_conferralsDataView.OnSelectionChanged += () =>
			{
				var c = ((ObjectView<ConferralDTO>) _conferralsDataView.CurrentlySelected)?.Object;
				_conferralsVM.SetSelectedCondition(c == null?Guid.Empty : c.Id);
			};

			_conferralsConditionSetEditor.View = _conferralsVM.ConditionsView;
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

			_claimsVM.Reload();
			_claimDataView.ClearSelection();

			_conferralsVM.Reload();
			_conferralsDataView.ClearSelection();
			_conferralsDataView.GetColumnByName(PropertyUtil.GetPropertyName<ConferralDTO>(dto => dto.Id)).Visible = false;
			_conferralsDataView.GetColumnByName(PropertyUtil.GetPropertyName<ConferralDTO>(dto => dto.Conditions)).Visible = false;
		}

		#endregion
	}
}

using System;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance;
using SocialImportance.DTOs;
using SocialImportanceWF.ViewModels;
using System.Collections;
using System.Linq;

namespace SocialImportanceWF
{
	public partial class MainForm : BaseSIForm
	{
		private AttributionRuleVM _attributionRulesVM;
        private ConditionSetView _conditions;
        private BindingListView<AttributionRuleDTO> _attributionRules;

        public MainForm()

		{
			InitializeComponent();
			//Attribution Rules
			_attributionRulesVM = new AttributionRuleVM(this);
            _attributionRules = new BindingListView<AttributionRuleDTO>((IList)null);
            dataGridViewAttributionRules.DataSource = this._attributionRules;
			
		}

		#region Overrides of BaseAssetForm<SocialImportanceAsset>

		protected override void OnAssetDataLoaded(SocialImportanceAsset asset)
		{
            _conditions = new ConditionSetView();
            _attRuleConditionSetEditor.View = _conditions;
            _conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            _attributionRules.DataSource = LoadedAsset.GetAttributionRules().ToList();
            EditorTools.HideColumns(dataGridViewAttributionRules, new[] { "Id", "Conditions" });
		}

        #endregion

        public AttributionRuleDTO CurrentlySelectedRule
        {
            get
            {
                /*if (_currentlySelected == Guid.Empty)
                    return null;

                var rule = RuleList.FirstOrDefault(r => r.Id == _currentlySelected);
                if (rule == null)
                    throw new Exception("Attribution rule not found");

                return rule;*/
                return null;
            }
        }


        private void ConditionSetView_OnDataChanged()
        {
            var selectedRule = (AttributionRuleDTO)EditorTools.GetSelectedDtoFromTable(dataGridViewAttributionRules);
            if (selectedRule == null)
                return;
            selectedRule.Conditions = _conditions.GetData();
            LoadedAsset.UpdateAttributionRule(selectedRule);
            SetModified();
        }

        private void _attRulesDataView_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {

        }
    }
}

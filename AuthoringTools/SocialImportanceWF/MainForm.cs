using System;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance;
using SocialImportance.DTOs;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using GAIPS.Rage;

namespace SocialImportanceWF
{
	public partial class MainForm : BaseSIForm
	{
		
        private ConditionSetView _conditions;
        private BindingListView<AttributionRuleDTO> _attributionRules;

        public MainForm()

		{
			InitializeComponent();
		}

		#region Overrides of BaseAssetForm<SocialImportanceAsset>

		protected override void OnAssetDataLoaded(SocialImportanceAsset asset)
		{
            //Attribution Rules
            _attributionRules = new BindingListView<AttributionRuleDTO>((IList)null);
            dataGridViewAttributionRules.DataSource = this._attributionRules;

            _attRuleConditionSetEditor.View = _conditions;

            _conditions = new ConditionSetView();
            _attRuleConditionSetEditor.View = _conditions;
            _conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            _attributionRules.DataSource = LoadedAsset.GetAttributionRules().ToList();
            EditorTools.HideColumns(dataGridViewAttributionRules, new[] {
                PropertyUtil.GetPropertyName<AttributionRuleDTO>(o => o.Id),
                PropertyUtil.GetPropertyName<AttributionRuleDTO>(o => o.Conditions) });

            _wasModified = false;
		}

        #endregion
        private void ConditionSetView_OnDataChanged()
        {
            var selectedRule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(dataGridViewAttributionRules);
            if (selectedRule == null)
                return;
            selectedRule.Conditions = _conditions.GetData();
            LoadedAsset.UpdateAttributionRule(selectedRule);
            
            SetModified();
        }

        private void dataGridViewAttributionRules_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (rule != null)
            {
                _conditions.SetData(rule.Conditions);
            }
        }

        private void buttonAddAttRule_Click(object sender, EventArgs e)
        {
            var newRule = new AttributionRuleDTO()
            {
                Description = "-",
                Value = WellFormedNames.Name.BuildName("[v]"),
                Target = WellFormedNames.Name.BuildName("[t]")
            };
            new AddOrEditAttributionRuleForm(LoadedAsset, dataGridViewAttributionRules, newRule).ShowDialog(this);
            SetModified();
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (rule != null)
            {
                new AddOrEditAttributionRuleForm(LoadedAsset, dataGridViewAttributionRules, rule).ShowDialog(this);
                SetModified();
            }
        }

        private void buttonDuplicateAttRule_Click(object sender, EventArgs e)
        {
            var r = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (r != null)
            {
                var newRule = LoadedAsset.AddAttributionRule(r);
                EditorTools.RefreshTable(dataGridViewAttributionRules, LoadedAsset.GetAttributionRules().ToList(), newRule.Id);
                SetModified();
            }
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
            var selRows = dataGridViewAttributionRules.SelectedRows;
            if (selRows.Count == 0) return;
            foreach (var r in selRows.Cast<DataGridViewRow>())
            {
                var dto = (AttributionRuleDTO)r.DataBoundItem;
                LoadedAsset.RemoveAttributionRuleById(dto.Id);
            }
            EditorTools.RefreshTable(dataGridViewAttributionRules, LoadedAsset.GetAttributionRules().ToList(), Guid.Empty);
            SetModified();
        }
    }
}

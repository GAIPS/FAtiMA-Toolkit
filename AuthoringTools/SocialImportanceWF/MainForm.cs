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
        private ConditionSetView conditions;
        private BindingListView<AttributionRuleDTO> attributionRules;

        public MainForm()
		{
			InitializeComponent();
		}

		#region Overrides of BaseAssetForm<SocialImportanceAsset>

		protected override void OnAssetDataLoaded(SocialImportanceAsset asset)
		{
            attributionRules = new BindingListView<AttributionRuleDTO>((IList)null);
            dataGridViewAttributionRules.DataSource = this.attributionRules;

            _attRuleConditionSetEditor.View = conditions;

            conditions = new ConditionSetView();
            _attRuleConditionSetEditor.View = conditions;
            conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            attributionRules.DataSource = LoadedAsset.GetAttributionRules().ToList();
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
            selectedRule.Conditions = conditions.GetData();
            LoadedAsset.UpdateAttributionRule(selectedRule);
            
            SetModified();
        }

        private void buttonAddAttRule_Click(object sender, EventArgs e)
        {
            var newRule = new AttributionRuleDTO()
            {
                Description = "-",
                Value = WellFormedNames.Name.BuildName("[v]"),
                Target = WellFormedNames.Name.BuildName("[t]")
            };
            new AddOrEditAttributionRuleForm(LoadedAsset, dataGridViewAttributionRules, newRule,attributionRules).ShowDialog(this);
            SetModified();
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (rule != null)
            {
                new AddOrEditAttributionRuleForm(LoadedAsset, dataGridViewAttributionRules, rule, attributionRules).ShowDialog(this);
                SetModified();
            }
        }

        private void buttonDuplicateAttRule_Click(object sender, EventArgs e)
        {
            var r = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (r != null)
            {
                var newRule = LoadedAsset.AddAttributionRule(r);
          EditorTools.RefreshTable(dataGridViewAttributionRules,attributionRules, LoadedAsset.GetAttributionRules().ToList(), newRule.Id);
                SetModified();
            }
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
            var selRows = dataGridViewAttributionRules.SelectedRows;
            if (selRows.Count == 0) return;
            foreach (var r in selRows.Cast<DataGridViewRow>())
            {
                var dto = ((ObjectView<AttributionRuleDTO>)r.DataBoundItem).Object;
                LoadedAsset.RemoveAttributionRuleById(dto.Id);
            }
            EditorTools.RefreshTable(dataGridViewAttributionRules, attributionRules, LoadedAsset.GetAttributionRules().ToList(), Guid.Empty);
            SetModified();
        }

        private void dataGridViewAttributionRules_SelectionChanged(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (rule != null) conditions.SetData(rule.Conditions);
            else conditions.SetData(null);
        }

        private void dataGridViewAttributionRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonEditAttRule_Click(sender, e);
        }

        private void dataGridViewAttributionRules_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonAddAttRule_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.D:
                    if (e.Control) this.buttonDuplicateAttRule_Click(sender, e);
                    break;
                case Keys.Delete:
                    this.buttonRemoveAttRule_Click(sender, e);
                    break;
            }
        }

        private void dataGridViewAttributionRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

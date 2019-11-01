using System;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance;
using SocialImportance.DTOs;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using GAIPS.Rage;
using System.IO;

namespace SocialImportanceWF
{
	public partial class MainForm : Form
	{
        private ConditionSetView conditions;
        private BindingListView<AttributionRuleDTO> attributionRules;
        private SocialImportanceAsset _loadedAsset;
        private AssetStorage _storage;
        private string _currentPath;


        public SocialImportanceAsset Asset
        {
            get { return _loadedAsset; }
            set { _loadedAsset = value; OnAssetDataLoaded(); }
        }

        public MainForm()
		{
			InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = SocialImportanceAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

		protected void OnAssetDataLoaded()
		{
            attributionRules = new BindingListView<AttributionRuleDTO>((IList)null);
            dataGridViewAttributionRules.DataSource = this.attributionRules;

            _attRuleConditionSetEditor.View = conditions;

            conditions = new ConditionSetView();
            _attRuleConditionSetEditor.View = conditions;
            conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            attributionRules.DataSource = _loadedAsset.GetAttributionRules().ToList();
            EditorTools.HideColumns(dataGridViewAttributionRules, new[] {
            PropertyUtil.GetPropertyName<AttributionRuleDTO>(o => o.Id) });
            EditorTools.UpdateFormTitle("Social Importance", _currentPath, this);
        }

        private void ConditionSetView_OnDataChanged()
        {
            var selectedRule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(dataGridViewAttributionRules);
            if (selectedRule == null)
                return;
            selectedRule.Conditions = conditions.GetData();
            _loadedAsset.UpdateAttributionRule(selectedRule);
            attributionRules.DataSource = _loadedAsset.GetAttributionRules().ToList();
            attributionRules.Refresh();
        }

        private void buttonAddAttRule_Click(object sender, EventArgs e)
        {
            var newRule = new AttributionRuleDTO()
            {
                Description = "-",
                Value = WellFormedNames.Name.BuildName("[v]"),
                Target = WellFormedNames.Name.BuildName("[t]")
            };
            this.auxAddOrUpdateItem(newRule);
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (rule != null)
            {
                this.auxAddOrUpdateItem(rule);
            }
        }

        private void auxAddOrUpdateItem(AttributionRuleDTO item)
        {
            var diag = new AddOrEditAttributionRuleForm(_loadedAsset, item);
            diag.ShowDialog(this);
            if (diag.UpdatedGuid != Guid.Empty)
            {
                attributionRules.DataSource = _loadedAsset.GetAttributionRules().ToList();
                EditorTools.HighlightItemInGrid<AttributionRuleDTO>(dataGridViewAttributionRules, diag.UpdatedGuid);
            }
        }

        private void buttonDuplicateAttRule_Click(object sender, EventArgs e)
        {
            var r = EditorTools.GetSelectedDtoFromTable<AttributionRuleDTO>(this.dataGridViewAttributionRules);
            if (r != null)
            {
                var newRule = _loadedAsset.AddAttributionRule(r);
                attributionRules.DataSource = _loadedAsset.GetAttributionRules().ToList();
                EditorTools.HighlightItemInGrid<AttributionRuleDTO>(dataGridViewAttributionRules, newRule.Id);
            }
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
            var selRows = dataGridViewAttributionRules.SelectedRows;
            if (selRows.Count == 0) return;
            foreach (var r in selRows.Cast<DataGridViewRow>())
            {
                var dto = ((ObjectView<AttributionRuleDTO>)r.DataBoundItem).Object;
                _loadedAsset.RemoveAttributionRuleById(dto.Id);
            }
            attributionRules.DataSource = _loadedAsset.GetAttributionRules().ToList();
            EditorTools.HighlightItemInGrid<AttributionRuleDTO>(dataGridViewAttributionRules, Guid.Empty);
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

        private void _attRuleConditionSetEditor_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentPath = null;
            _storage = new AssetStorage();
            _loadedAsset = SocialImportanceAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Asset Storage File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                try
                {
                    _currentPath = aux;
                    _storage = AssetStorage.FromJson(File.ReadAllText(_currentPath));
                    _loadedAsset = SocialImportanceAsset.CreateInstance(_storage);
                    OnAssetDataLoaded();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentPath = EditorTools.SaveFileDialog(_currentPath, _storage, _loadedAsset);
            EditorTools.UpdateFormTitle("Social Importance", _currentPath, this);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var old = _currentPath;
            _currentPath = null;
            _currentPath = EditorTools.SaveFileDialog(_currentPath, _storage, _loadedAsset);
            if (_currentPath == null) _currentPath = old;
            EditorTools.UpdateFormTitle("Social Importance", _currentPath, this);
        }
    }
}

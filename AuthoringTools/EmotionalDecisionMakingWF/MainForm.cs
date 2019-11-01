using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using ActionLibrary.DTOs;
using GAIPS.Rage;
using System.IO;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : Form
    {
		private BindingListView<ActionRuleDTO> actionRules;
	    private ConditionSetView conditionSetView;
        private Guid selectedActionId;
        private EmotionalDecisionMakingAsset _loadedAsset;
        private AssetStorage _storage;
        private string _currentFilePath;
        
		public MainForm()
        {
            InitializeComponent();
			this.actionRules = new BindingListView<ActionRuleDTO>((IList)null);
			dataGridViewReactiveActions.DataSource = this.actionRules;
            _storage = new AssetStorage();
            _loadedAsset = EmotionalDecisionMakingAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
		}

        public EmotionalDecisionMakingAsset Asset
        {
            get { return _loadedAsset; } 
            set { _loadedAsset = value; OnAssetDataLoaded(); } 
        }

        public void OnAssetDataLoaded()
	    {
            conditionSetView = new ConditionSetView();
            conditionSetEditor.View = conditionSetView;
            conditionSetView.OnDataChanged += conditionSetView_OnDataChanged;
            actionRules.DataSource = _loadedAsset.GetAllActionRules().ToList();

            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionRuleDTO>(dto => dto.Priority)].DisplayIndex = 3;

            EditorTools.HideColumns(dataGridViewReactiveActions, new[]
            {
                PropertyUtil.GetPropertyName<ActionRuleDTO>( d => d.Id)
            });
           
            if (actionRules.Any())
			{
				var ra = _loadedAsset.GetActionRule(actionRules.First().Id);
				UpdateConditions(ra);
			}
            EditorTools.UpdateFormTitle("Emotional Decision Making", _currentFilePath, this);
        }
		
		private void conditionSetView_OnDataChanged()
		{
            _loadedAsset.UpdateRuleConditions(selectedActionId, conditionSetView.GetData());
            actionRules.DataSource = _loadedAsset.GetAllActionRules().ToList();
            actionRules.Refresh();
		}

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            selectedActionId = reaction.Id;

	        var ra = _loadedAsset.GetActionRule(selectedActionId);
			UpdateConditions(ra);
        }

        private void buttonRemoveReaction_Click(object sender, EventArgs e)
        {
	        var ids = dataGridViewReactiveActions.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<ActionRuleDTO>) r.DataBoundItem).Object.Id).ToList();

            _loadedAsset.RemoveActionRules(ids);

            var rules = _loadedAsset.GetAllActionRules().ToList();
            actionRules.DataSource = rules;
            actionRules.Refresh();
            if(rules == null || rules.Count == 0)
            {
                UpdateConditions(null);
            }
        }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {
            new AddOrEditReactionForm(_loadedAsset).ShowDialog();
            actionRules.DataSource = _loadedAsset.GetAllActionRules().ToList();
            actionRules.Refresh();
		}

        private void buttonEditReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var selectedReaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.
                   SelectedRows[0].DataBoundItem).Object;
                
                new AddOrEditReactionForm(_loadedAsset, selectedReaction).ShowDialog();
                actionRules.DataSource = _loadedAsset.GetAllActionRules().ToList();
                 actionRules.Refresh();
            }
        }

		private void UpdateConditions(ActionRuleDTO reaction)
		{
			conditionSetView.SetData(reaction?.Conditions);
        }

		private void dataGridViewReactiveActions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditReaction_Click(sender, e);
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void conditionSetEditor_Load(object sender, EventArgs e)
        {

        }

        private void buttonDuplicateReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var a = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.SelectedRows[0].DataBoundItem).Object;
                var duplicateAction = CloneHelper.Clone(a);
                _loadedAsset.AddActionRule(duplicateAction);
                actionRules.DataSource = _loadedAsset.GetAllActionRules().ToList();
                actionRules.Refresh();
            }
        }

        private void dataGridViewReactiveActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewReactiveActions_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEditReaction_Click(sender, e);
                    //This next line is necessary to prevent the default behaviour from ocurring
                    e.Handled = true;
                    break;
                case Keys.D:
                    if (e.Control) this.buttonDuplicateReaction_Click(sender, e);
                    break;
                case Keys.Delete:
                    this.buttonRemoveReaction_Click(sender, e);
                    break;
            }

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath, _storage, _loadedAsset);
            EditorTools.UpdateFormTitle("Emotional Decision Making", _currentFilePath, this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Asset Storage File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                try
                {
                    _currentFilePath = aux;
                    _storage = AssetStorage.FromJson(File.ReadAllText(_currentFilePath));
                    _loadedAsset = EmotionalDecisionMakingAsset.CreateInstance(_storage);
                    OnAssetDataLoaded();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = null;
            _storage = new AssetStorage();
            _loadedAsset = EmotionalDecisionMakingAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var old = _currentFilePath;
            _currentFilePath = null;
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath,_storage,_loadedAsset);
            if (_currentFilePath == null) _currentFilePath = old;
            EditorTools.UpdateFormTitle("Emotional Decision Making", _currentFilePath, this);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

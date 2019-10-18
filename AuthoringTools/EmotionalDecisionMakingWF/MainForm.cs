using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using ActionLibrary.DTOs;
using GAIPS.Rage;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : BaseEDMForm
    {
		private BindingListView<ActionRuleDTO> actionRules;
	    private ConditionSetView conditionSetView;
        private Guid selectedActionId;
        
		public MainForm()
        {
            InitializeComponent();
			this.actionRules = new BindingListView<ActionRuleDTO>((IList)null);
			dataGridViewReactiveActions.DataSource = this.actionRules;
		}

	    protected override void OnAssetDataLoaded(EmotionalDecisionMakingAsset asset)
	    {
            conditionSetView = new ConditionSetView();
            conditionSetEditor.View = conditionSetView;
            conditionSetView.OnDataChanged += conditionSetView_OnDataChanged;

            actionRules.DataSource = LoadedAsset.GetAllActionRules().ToList();

            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionRuleDTO>(dto => dto.Priority)].DisplayIndex = 3;

            EditorTools.HideColumns(dataGridViewReactiveActions, new[]
            {
                PropertyUtil.GetPropertyName<ActionRuleDTO>( d => d.Id)
            });
                
           
            if (actionRules.Any())
			{
				var ra = LoadedAsset.GetActionRule(actionRules.First().Id);
				UpdateConditions(ra);
			}
		}
		
		private void conditionSetView_OnDataChanged()
		{
			LoadedAsset.UpdateRuleConditions(selectedActionId, conditionSetView.GetData());
            actionRules.DataSource = LoadedAsset.GetAllActionRules().ToList();
            actionRules.Refresh();
            SetModified();
		}

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            selectedActionId = reaction.Id;

	        var ra = LoadedAsset.GetActionRule(selectedActionId);
			UpdateConditions(ra);
        }

        private void buttonRemoveReaction_Click(object sender, EventArgs e)
        {
	        var ids = dataGridViewReactiveActions.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<ActionRuleDTO>) r.DataBoundItem).Object.Id).ToList();

			LoadedAsset.RemoveActionRules(ids);

            var rules = LoadedAsset.GetAllActionRules().ToList();
            actionRules.DataSource = rules;
            actionRules.Refresh();
			SetModified();
            if(rules == null || rules.Count == 0)
            {
                UpdateConditions(null);
            }
        }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {
            new AddOrEditReactionForm(LoadedAsset).ShowDialog();
            actionRules.DataSource = LoadedAsset.GetAllActionRules().ToList();
            actionRules.Refresh();
			SetModified();
		}

        private void buttonEditReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var selectedReaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.
                   SelectedRows[0].DataBoundItem).Object;
                
                new AddOrEditReactionForm(LoadedAsset,selectedReaction).ShowDialog();
                actionRules.DataSource = LoadedAsset.GetAllActionRules().ToList();
                 actionRules.Refresh();
                
                SetModified();
            }
        }

		private void UpdateConditions(ActionRuleDTO reaction)
		{
			conditionSetView.SetData(reaction?.Conditions);
            conditionSetEditor.

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
                LoadedAsset.AddActionRule(duplicateAction);
                actionRules.DataSource = LoadedAsset.GetAllActionRules().ToList();
                actionRules.Refresh();
                SetModified();
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
    }
}

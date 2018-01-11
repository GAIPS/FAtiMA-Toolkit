using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using ActionLibrary.DTOs;
using Conditions.DTOs;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : BaseEDMForm
    {
		private BindingListView<ActionRuleDTO> _reactiveActions;
	    private ConditionSetView _conditionSetView;
        private Guid _selectedActionId;

		public MainForm()
        {
            InitializeComponent();
			this._reactiveActions = new BindingListView<ActionRuleDTO>((IList)null);
			dataGridViewReactiveActions.DataSource = this._reactiveActions;
		}

	    protected override void OnAssetDataLoaded(EmotionalDecisionMakingAsset asset)
	    {
            _conditionSetView = new ConditionSetView();
            conditionSetEditor.View = _conditionSetView;
            _conditionSetView.OnDataChanged += conditionSetView_OnDataChanged;

            _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionRuleDTO>(dto => dto.Priority)].DisplayIndex = 3;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionRuleDTO>(dto => dto.Id)].Visible = false;
			dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionRuleDTO>(dto => dto.Conditions)].Visible = false;

            if (_reactiveActions.Any())
			{
				var ra = LoadedAsset.GetActionRule(_reactiveActions.First().Id);
				UpdateConditions(ra);
			}
		}
		
		private void conditionSetView_OnDataChanged()
		{
			LoadedAsset.UpdateRuleConditions(_selectedActionId, _conditionSetView.GetData());
            _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
            _reactiveActions.Refresh();
            SetModified();
		}

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            _selectedActionId = reaction.Id;

	        var ra = LoadedAsset.GetActionRule(_selectedActionId);
			UpdateConditions(ra);
        }

        private void buttonRemoveReaction_Click(object sender, EventArgs e)
        {
	        var ids = dataGridViewReactiveActions.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<ActionRuleDTO>) r.DataBoundItem).Object.Id).ToList();

			LoadedAsset.RemoveActionRules(ids);
            _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
            _reactiveActions.Refresh();
			SetModified();
        }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {
            new AddOrEditReactionForm(LoadedAsset).ShowDialog();
            _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
            _reactiveActions.Refresh();
			SetModified();
		}

        private void buttonEditReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var selectedReaction = ((ObjectView<ActionRuleDTO>)dataGridViewReactiveActions.
                   SelectedRows[0].DataBoundItem).Object;
                
                new AddOrEditReactionForm(LoadedAsset,selectedReaction).ShowDialog();
                _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
                 _reactiveActions.Refresh();
                
                SetModified();
            }
        }

		private void UpdateConditions(ActionRuleDTO reaction)
		{
			_conditionSetView.SetData(reaction?.Conditions);
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
                var duplicateAction = new ActionRuleDTO
                {
                    Action = a.Action,
                    Priority = a.Priority,
                    Target = a.Target,
                    Layer = a.Layer,
                    Conditions = new ConditionSetDTO
                    {
                        Quantifier = a.Conditions.Quantifier,
                        ConditionSet = (string[])a.Conditions.ConditionSet?.Clone()
                    }
                };
                LoadedAsset.AddActionRule(duplicateAction);
                _reactiveActions.DataSource = LoadedAsset.GetAllActionRules().ToList();
                _reactiveActions.Refresh();
                SetModified();
            }
        }

        private void dataGridViewReactiveActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

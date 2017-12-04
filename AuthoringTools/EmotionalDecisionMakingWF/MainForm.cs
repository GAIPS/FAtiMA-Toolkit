using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using ActionLibrary.DTOs;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : BaseEDMForm
    {
		private BindingListView<ActionDefinitionDTO> _reactiveActions;
	    private ConditionSetView _conditionSetView;
        private Guid _selectedActionId;

		public MainForm()
        {
            InitializeComponent();

			_conditionSetView = new ConditionSetView();
			conditionSetEditor.View = _conditionSetView;
			_conditionSetView.OnDataChanged += conditionSetView_OnDataChanged;

			this._reactiveActions = new BindingListView<ActionDefinitionDTO>((IList)null);
			dataGridViewReactiveActions.DataSource = this._reactiveActions;
		}

	    protected override void OnAssetDataLoaded(EmotionalDecisionMakingAsset asset)
	    {
		    _reactiveActions.DataSource = LoadedAsset.GetAllReactions().ToList();
            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionDefinitionDTO>(dto => dto.Priority)].DisplayIndex = 3;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionDefinitionDTO>(dto => dto.Id)].Visible = false;
			dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionDefinitionDTO>(dto => dto.Conditions)].Visible = false;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetPropertyName<ActionDefinitionDTO>(dto => dto.Type)].Visible = false;

            if (_reactiveActions.Any())
			{
				var ra = LoadedAsset.GetReaction(_reactiveActions.First().Id);
				UpdateConditions(ra);
			}
		}
		
		private void conditionSetView_OnDataChanged()
		{
			LoadedAsset.UpdateReactionConditions(_selectedActionId, _conditionSetView.GetData());
			SetModified();
		}

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ActionDefinitionDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            _selectedActionId = reaction.Id;

	        var ra = LoadedAsset.GetReaction(_selectedActionId);
			UpdateConditions(ra);
        }

        private void buttonRemoveReaction_Click(object sender, EventArgs e)
        {
	        var ids = dataGridViewReactiveActions.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<ActionDefinitionDTO>) r.DataBoundItem).Object.Id).ToList();

			LoadedAsset.RemoveReactions(ids);
            _reactiveActions.DataSource = LoadedAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
			SetModified();
        }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {
            new AddOrEditReactionForm(LoadedAsset).ShowDialog();
            _reactiveActions.DataSource = LoadedAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
			SetModified();
		}

        private void buttonEditReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var selectedReaction = ((ObjectView<ActionDefinitionDTO>)dataGridViewReactiveActions.
                   SelectedRows[0].DataBoundItem).Object;
                new AddOrEditReactionForm(LoadedAsset,selectedReaction).ShowDialog();
            }
            _reactiveActions.DataSource = LoadedAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
			SetModified();
        }

		private void UpdateConditions(ActionDefinitionDTO reaction)
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
    }
}

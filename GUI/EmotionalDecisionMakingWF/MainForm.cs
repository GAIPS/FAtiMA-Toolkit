using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using EmotionalDecisionMakingWF.Properties;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using KnowledgeBase.Conditions;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : Form
    {
		private EmotionalDecisionMakingAsset _edmAsset;
        private string _saveFileName;

        private BindingListView<ReactionDTO> _reactiveActions;
	    private ConditionSetView _conditionSetView;
        private Guid _selectedActionId;

        public MainForm()
        {
            InitializeComponent();

			_conditionSetView = new ConditionSetView();
			conditionSetEditor.View = _conditionSetView;
			_conditionSetView.OnDataChanged += conditionSetView_OnDataChanged;

			string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                Reset(true);
            }
            else
            {
                _saveFileName = args[1];
                try
                {
                    _edmAsset = EmotionalDecisionMakingAsset.LoadFromFile(LocalStorageProvider.Instance,_saveFileName);
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset(true);
                }
            }
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormTitle;
                this._edmAsset = new EmotionalDecisionMakingAsset();
            }
            else
            {
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }

            this._reactiveActions = new BindingListView<ReactionDTO>(_edmAsset.GetAllReactions().ToList());
            dataGridViewReactiveActions.DataSource = this._reactiveActions;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetName<ReactionDTO>(dto => dto.Id)].Visible = false;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetName<ReactionDTO>(dto => dto.Conditions)].Visible = false;

			if (_reactiveActions.Any())
	        {
		        var ra = _edmAsset.GetReaction(_reactiveActions.First().Id);
		        UpdateConditions(ra);
	        }
        }
		
		private void conditionSetView_OnDataChanged()
		{
			_edmAsset.UpdateReactionConditions(_selectedActionId, _conditionSetView.GetData());
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }


        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _edmAsset = EmotionalDecisionMakingAsset.LoadFromFile(LocalStorageProvider.Instance, ofd.FileName);
                    _saveFileName = ofd.FileName;
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                saveHelper(true);
            }
            else
            {
                saveHelper(false);
            }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveHelper(true);
        }

        private void saveHelper(bool newSaveFile)
        {
            if (newSaveFile)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "EDM File|*.edm";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        _saveFileName = sfd.FileName;
                    }
                }
                else
                {
                    return;
                }
            }
            try
            {
				_edmAsset.SaveToFile(LocalStorageProvider.Instance, _saveFileName);
				this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.UnableToSaveFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ReactionDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            _selectedActionId = reaction.Id;

	        var ra = _edmAsset.GetReaction(_selectedActionId);
			UpdateConditions(ra);
        }

        private void buttonRemoveReaction_Click(object sender, EventArgs e)
        {
	        var ids = dataGridViewReactiveActions.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<ReactionDTO>) r.DataBoundItem).Object.Id).ToList();

			_edmAsset.RemoveReactions(ids);
            _reactiveActions.DataSource = _edmAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
        }

  //      private void buttonAddReactionCondition_Click(object sender, EventArgs e)
  //      {
  //          if (_selectedActionId != Guid.Empty)
  //          {
  //              new AddOrEditConditionForm(_edmAsset,_selectedActionId).ShowDialog();
		//		UpdateConditions(_edmAsset.GetReaction(_selectedActionId));
		//	}
  //      }

  //      private void buttonRemoveReactionCondition_Click(object sender, EventArgs e)
  //      {
  //          IList<string> conditionsToRemove = new List<string>();
  //          for (int i = 0; i < dataGridViewReactionConditions.SelectedRows.Count; i++)
  //          {
  //              var reaction = ((ObjectView<string>)dataGridViewReactionConditions.SelectedRows[i].DataBoundItem).Object;
  //              conditionsToRemove.Add(reaction);
  //          }
  //          _edmAsset.RemoveReactionConditions(_selectedActionId, conditionsToRemove);
	 //       UpdateConditions(_edmAsset.GetReaction(_selectedActionId));
		//}

  //      private void buttonEditReactionCondition_Click(object sender, EventArgs e)
  //      {
  //          if (dataGridViewReactionConditions.SelectedRows.Count == 1)
  //          {
  //              var selectedCondition = ((ObjectView<string>)dataGridViewReactionConditions.
  //                  SelectedRows[0].DataBoundItem).Object;
  //              new AddOrEditConditionForm(_edmAsset, _selectedActionId, selectedCondition).ShowDialog();
  //          }
  //          _reactiveActions.DataSource = _edmAsset.GetAllReactions().ToList();
  //          _reactiveActions.Refresh();
  //      }

        private void buttonAddReaction_Click(object sender, EventArgs e)
        {
            new AddOrEditReactionForm(_edmAsset).ShowDialog();
            _reactiveActions.DataSource = _edmAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
        }

        private void buttonEditReaction_Click(object sender, EventArgs e)
        {
            if (dataGridViewReactiveActions.SelectedRows.Count == 1)
            {
                var selectedReaction = ((ObjectView<ReactionDTO>)dataGridViewReactiveActions.
                   SelectedRows[0].DataBoundItem).Object;
                new AddOrEditReactionForm(_edmAsset,selectedReaction).ShowDialog();
            }
            _reactiveActions.DataSource = _edmAsset.GetAllReactions().ToList();
            _reactiveActions.Refresh();
        }

		private void UpdateConditions(ReactionDTO reaction)
		{
			_conditionSetView.SetData(reaction?.Conditions);
		}

	    //   private void dataGridViewReactionConditions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		//      {
		//          if (e.RowIndex != -1) //exclude header cells
		//          {
		//              this.buttonEditReactionCondition_Click(sender, e);
		//          }
		//      }

		private void dataGridViewReactiveActions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditReaction_Click(sender, e);
            }
        }
    }
}

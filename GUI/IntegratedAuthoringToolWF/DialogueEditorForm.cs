using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool;

namespace IntegratedAuthoringToolWF
{
    public partial class DialogueEditorForm : Form
    {
	    private MainForm _parentForm;
	    private IntegratedAuthoringToolAsset _iatAsset => _parentForm.CurrentAsset;
        private BindingListView<GUIDialogStateAction> _playerDialogs;
        private BindingListView<GUIDialogStateAction> _agentDialogs;

        public DialogueEditorForm(MainForm parentForm)
        {
            InitializeComponent();

	        _parentForm = parentForm;

            _playerDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
	        dataGridViewPlayerDialogueActions.DataSource = _playerDialogs;
			RefreshPlayerDialogs();

            _agentDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
	        dataGridViewAgentDialogueActions.DataSource = _agentDialogs;
            RefreshAgentDialogs();
        }

	    private void RefreshPlayerDialogs()
	    {
			_playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL).Select(d => new GUIDialogStateAction(d)).ToList();
			_playerDialogs.Refresh();
			dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;
		}

	    private void RefreshAgentDialogs()
	    {
			_agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).Select(d => new GUIDialogStateAction(d)).ToList();
			_agentDialogs.Refresh();
			dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
		}

		private void buttonAddPlayerDialogueAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, true).ShowDialog();
            RefreshPlayerDialogs();
        }

        private void buttonAgentAddDialogAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, false).ShowDialog();
            RefreshAgentDialogs();
        }

        private void buttonPlayerRemoveDialogueAction_Click(object sender, System.EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewPlayerDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.PLAYER, itemsToRemove);
			RefreshPlayerDialogs();
			_parentForm.SetModified();
        }

        private void buttonAgentRemoveDialogAction_Click(object sender, System.EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewAgentDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.AGENT, itemsToRemove);
			RefreshAgentDialogs();
			_parentForm.SetModified();
		}

        private void buttonPlayerEditDialogueAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, true, item.Id).ShowDialog();
				RefreshPlayerDialogs();
            }
        }

        private void buttonAgentEditDialogAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, false, item.Id).ShowDialog();
                RefreshAgentDialogs();
            }
        }

		private void textToSpeachToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			var dialogs = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToArray();
			var t = new TextToSpeechForm(dialogs);
			t.Show(this);
		}
	}
}

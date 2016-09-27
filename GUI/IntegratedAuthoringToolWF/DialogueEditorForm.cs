using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
    public partial class DialogueEditorForm : Form
    {
	    private MainForm _parentForm;
	    private IntegratedAuthoringToolAsset _iatAsset => _parentForm.CurrentAsset;
        private BindingListView<DialogueStateActionDTO> _playerDialogs;
        private BindingListView<DialogueStateActionDTO> _agentDialogs;

        public DialogueEditorForm(MainForm parentForm)
        {
            InitializeComponent();

	        _parentForm = parentForm;

            _playerDialogs = new BindingListView<DialogueStateActionDTO>(_iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER,WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList());
            this.dataGridViewPlayerDialogueActions.DataSource = _playerDialogs;
            dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;

            _agentDialogs = new BindingListView<DialogueStateActionDTO>(_iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList());
            this.dataGridViewAgentDialogueActions.DataSource = _agentDialogs;
            dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
        }

		private void buttonAddPlayerDialogueAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, true).ShowDialog();
            _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
            _playerDialogs.Refresh();
        }

        private void buttonAgentAddDialogAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, false).ShowDialog();
            _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
            _agentDialogs.Refresh();
        }

        private void buttonPlayerRemoveDialogueAction_Click(object sender, System.EventArgs e)
        {
            IList<DialogueStateActionDTO> itemsToRemove = new List<DialogueStateActionDTO>();
            for (int i = 0; i < dataGridViewPlayerDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewPlayerDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item);
            }
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.PLAYER, itemsToRemove);
            _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
            _playerDialogs.Refresh();
			_parentForm.SetModified();
        }

        private void buttonAgentRemoveDialogAction_Click(object sender, System.EventArgs e)
        {
            IList<DialogueStateActionDTO> itemsToRemove = new List<DialogueStateActionDTO>();
            for (int i = 0; i < dataGridViewAgentDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewAgentDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item);
            }
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.AGENT, itemsToRemove);
            _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
            _agentDialogs.Refresh();
			_parentForm.SetModified();
		}

        private void buttonPlayerEditDialogueAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, true, item).ShowDialog();
                _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
                _playerDialogs.Refresh();
            }
        }

        private void buttonAgentEditDialogAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, false, item).ShowDialog();
                _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToList();
                _agentDialogs.Refresh();
            }
        }

		private void textToSpeachToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			var dialogs = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToArray();
			new TextToSpeechForm(dialogs).Show(this);
		}
	}
}

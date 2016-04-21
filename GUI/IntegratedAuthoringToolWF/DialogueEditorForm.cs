using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
    public partial class DialogueEditorForm : Form
    {
        private IntegratedAuthoringToolAsset _iatAsset;
        private BindingListView<DialogueStateActionDTO> _playerDialogs;
        private BindingListView<DialogueStateActionDTO> _agentDialogs;

        public DialogueEditorForm(IntegratedAuthoringToolAsset iatAsset)
        {
            InitializeComponent();

            this._iatAsset = iatAsset;

            _playerDialogs = new BindingListView<DialogueStateActionDTO>(iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER,IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList());
            this.dataGridViewPlayerDialogueActions.DataSource = _playerDialogs;
            dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;

            _agentDialogs = new BindingListView<DialogueStateActionDTO>(iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList());
            this.dataGridViewAgentDialogueActions.DataSource = _agentDialogs;
            dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DialogueEditorForm_Load(object sender, System.EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void buttonAddPlayerDialogueAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_iatAsset, true).ShowDialog();
            _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER,IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
            _playerDialogs.Refresh();
        }

        private void buttonAgentAddDialogAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_iatAsset, false).ShowDialog();
            _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
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
            _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
            _playerDialogs.Refresh();
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
            _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
            _agentDialogs.Refresh();
        }

        private void buttonPlayerEditDialogueAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_iatAsset, true, item).ShowDialog();
                _playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
                _playerDialogs.Refresh();
            }
        }

        private void buttonAgentEditDialogAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_iatAsset, true, item).ShowDialog();
                _agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, IntegratedAuthoringToolAsset.ANY_DIALOGUE_STATE).ToList();
                _agentDialogs.Refresh();
            }
        }
    }
}

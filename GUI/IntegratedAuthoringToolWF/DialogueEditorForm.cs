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
        private IntegratedAuthoringToolAsset _iatAsset;
        private BindingListView<DialogueStateActionDTO> _dialogActions;

        public DialogueEditorForm(IntegratedAuthoringToolAsset iatAsset)
        {
            InitializeComponent();

            this._iatAsset = iatAsset;
            _dialogActions = new BindingListView<DialogueStateActionDTO>(iatAsset.GetAllDialogActions().ToList());
            this.dataGridViewDialogActions.DataSource = _dialogActions;
        }

        private void buttonAddDialogueAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_iatAsset).ShowDialog();
            _dialogActions.DataSource = _iatAsset.GetAllDialogActions().ToList();
            _dialogActions.Refresh();
        }
    }
}

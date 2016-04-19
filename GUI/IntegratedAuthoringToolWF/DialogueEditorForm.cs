using System.Collections.Generic;
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
            _dialogActions = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
            this.dataGridViewDialogActions.DataSource = _dialogActions;
          
        }
    }
}

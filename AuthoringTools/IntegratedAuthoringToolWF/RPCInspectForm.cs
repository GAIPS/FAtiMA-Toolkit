using System.Windows.Forms;
using RolePlayCharacter;
using Equin.ApplicationFramework;
using ActionLibrary;
using System.Linq;
using GAIPS.AssetEditorTools;

namespace IntegratedAuthoringToolWF
{
    public partial class RPCInspectForm : Form
    {
        private RolePlayCharacterAsset asset;
        private BindingListView<IAction> actions;

        public RPCInspectForm(RolePlayCharacterAsset asset)
        {
            InitializeComponent();
            this.asset = asset;
            var decisions = asset.Decide().ToList();
            actions = new BindingListView<IAction>(decisions);
            dataGridViewDecisions.DataSource = actions;
            EditorTools.HideColumns(dataGridViewDecisions, new[]
            {
                PropertyUtil.GetPropertyName<IAction>(a => a.Parameters)
            });
        }
    }
}

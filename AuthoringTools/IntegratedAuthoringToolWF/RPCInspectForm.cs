using System.Windows.Forms;
using RolePlayCharacter;
using Equin.ApplicationFramework;
using ActionLibrary;
using System.Linq;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using System.Collections;

namespace IntegratedAuthoringToolWF
{
    public partial class RPCInspectForm : Form
    {
        private string rpcSource;
        private IntegratedAuthoringToolAsset iat;

        private BindingListView<IAction> actions;

        public RPCInspectForm(IntegratedAuthoringToolAsset iatAsset, string rpcSource)
        {
            InitializeComponent();
            this.iat = iatAsset;
            this.rpcSource = rpcSource;
          
            actions = new BindingListView<IAction>((IList)null);
            dataGridViewDecisions.DataSource = actions;
            EditorTools.HideColumns(dataGridViewDecisions, new[]
            {
                PropertyUtil.GetPropertyName<IAction>(a => a.Parameters)
            });
        }

        private void buttonTest_Click(object sender, System.EventArgs e)
        {
            var rpcAsset = RolePlayCharacterAsset.LoadFromFile(rpcSource);
            rpcAsset.LoadAssociatedAssets();
            iat.BindToRegistry(rpcAsset.DynamicPropertiesRegistry);

            var decisions = rpcAsset.Decide().ToList();
            actions.DataSource = decisions;
        }
    }
}

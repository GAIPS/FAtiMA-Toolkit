using System;
using IntegratedAuthoringTool;
using System.Windows.Forms;
using GAIPS.AssetEditorTools;
using System.Linq;
using WellFormedNames;
using RolePlayCharacter;

namespace IntegratedAuthoringToolWF
{
    public partial class AddCharacterForm : Form
    {
        private IntegratedAuthoringToolAsset _asset;
        public AddCharacterForm(IntegratedAuthoringToolAsset asset)
        {
            InitializeComponent();
            _asset = asset;
            EditorTools.AllowOnlyGroundedLiteral(wfNameFieldBoxCharacterName);
        }

        
        private void AddCharacterForm_Load(object sender, EventArgs e)
        {
            wfNameFieldBoxCharacterName.Value = RPCConsts.DEFAULT_CHARACTER_NAME;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                _asset.AddNewCharacter(wfNameFieldBoxCharacterName.Value);
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

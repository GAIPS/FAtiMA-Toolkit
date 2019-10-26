using System;
using IntegratedAuthoringTool;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF
{
    public partial class AddCharacterForm : Form
    {
        private IntegratedAuthoringToolAsset _asset;
        public AddCharacterForm(IntegratedAuthoringToolAsset asset)
        {
            InitializeComponent();
            _asset = asset;
        }

        
        private void AddCharacterForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = wfNameFieldBoxCharacterName.Value;
            this.Close();
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace AuthorialAgentsWF
{
    public partial class AddEditScenario : Form
    {
        /*Strings are just placeholders*/
        private BindingList<string> _npcListSelected;
        private BindingList<string> _npcListAvailable;
        private AuthTool _context;

        public AddEditScenario(AuthTool context)
        {
            InitializeComponent();
            npcNumber.Text = npcList.Items.Count.ToString();
            _context = context;
        }

        private void createNewNPC_Click(object sender, EventArgs e)
        {
            var addEditNPC = new RolePlayCharacterWF.MainForm();
            addEditNPC.ShowDialog();
        }

        private void editNPC_Click(object sender, EventArgs e)
        {
            var addEditNPC = new RolePlayCharacterWF.MainForm();
            addEditNPC.ShowDialog();
        }

        private void deleteNPC_Click(object sender, EventArgs e)
        {

        }

        private void saveScenarioButton_Click(object sender, EventArgs e)
        {

            var scenario = new ListViewItem(new string[]
                {
                    nameScenarioTextBox.Text.Trim(),
                    descriptionScenario.Text.Trim(),
                });

            _context.ScenarioList.Items.Add(scenario);

            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthorialAgentsWF
{
    public partial class AddEditScenario : Form
    {
        public AddEditScenario()
        {
            InitializeComponent();
        }

        private void createNewNPC_Click(object sender, EventArgs e)
        {
            var addEditNPC = new RolePlayCharacterWF.RolePlayCharacter();
            addEditNPC.ShowDialog();
        }

        private void editNPC_Click(object sender, EventArgs e)
        {
            var addEditNPC = new RolePlayCharacterWF.RolePlayCharacter();
            addEditNPC.ShowDialog();
        }

        private void deleteNPC_Click(object sender, EventArgs e)
        {

        }
    }
}

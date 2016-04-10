using System;
using System.Windows.Forms;
using RolePlayCharacterWF.Properties;

namespace RolePlayCharacterWF
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            Reset(true);
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormTitle;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }

        private void changeDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

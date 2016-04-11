using System;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using EmotionalDecisionMakingWF.Properties;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : Form
    {
        private EmotionalDecisionMakingAsset _edmAsset;
        private string _saveFileName;


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
                this._edmAsset = new EmotionalDecisionMakingAsset();
            }
            else
            {
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }


        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

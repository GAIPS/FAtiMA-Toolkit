using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF
{
    public partial class AddEmotionalReactionForm : Form
    {
        public bool neverShowAgain;   
        public AddEmotionalReactionForm(string actionName)
        {
            InitializeComponent();
            actionText.Text = actionName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            neverShowAgain = checkBox1.Checked;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.DialogueHelpers
{
    public partial class GenerateDialogueActions : Form
    {

        IntegratedAuthoringToolWF.MainForm parentForm;
        public GenerateDialogueActions(IntegratedAuthoringToolWF.MainForm parent)
        {
            InitializeComponent();
            stateNumberBox.Value = 5;
            optionsPerStateBox.Value = 1;
            parentForm = parent;
            this.toolTip1.SetToolTip(parent, "FAtiMA-Toolkit");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string initState = initialStateBox.Text;
            string endState = endStateBox.Text;
            int numberOfStates = stateNumberBox.Value;
            int dialoguesPerState = optionsPerStateBox.Value;

            parentForm.AddDialogueActions(initState, endState, numberOfStates, dialoguesPerState);

            this.Close();
        }
    }
}

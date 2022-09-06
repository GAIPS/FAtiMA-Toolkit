using IntegratedAuthoringToolWF.IEP;
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
    public partial class GenerateDialogue_Wizard : Form
    {
        private OutputManager _outputManager;
        public ConnectToServerForm _server;
        GPTOutputForm outputForm;
        string input;

        public GenerateDialogue_Wizard(ConnectToServerForm server, OutputManager outputManager)
        {
            InitializeComponent();
            _outputManager = outputManager;
            _server = server;

            if (!_server.connected)
            {
                MessageBox.Show("Please connect to the Wizard Server");
                _server.ShowDialog(this);
            }

            if (_server.connected)
            {
                this.button1.Enabled = true;
            }

            input = GetDialogues("");
            inputExample.Text = input.Split('\n')[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input = GetDialogues("");
            _server.ProcessDialogues(input, this.HandleOutput);
        }


        public string GetDialogues(string usedInput)
        {
            var dialogs = this._outputManager._mainIAT.GetAllDialogueActions();

            if (dialogs.Count() < 2)
            {
                return "";
            }

            else
            {
                // Preparing input
                var input = "";


                foreach (var d in dialogs)
                {

                    input += "|";
                    var currentState = d.CurrentState + "|";
                    var nextState = "";
                    var meaning = "";
                    var style = "";
                    var utterance = d.Utterance + "|";


                    if (nextStateBox.Checked)
                        nextState = d.NextState + "|";

                    if (meaningBox.Checked)
                        meaning = d.Meaning + "|";

                    if (styleBox.Checked)
                        style = d.Style + "|";

                    input += currentState + nextState + meaning + style + utterance + "\n";

                }

                if(usedInput.Length > 3)
                {
                    input += usedInput;
                }

               

                return input;
            }

        }

        public void HandleOutput()
        {
            if(_server.dialogResult != "")
            {
                outputForm = new GPTOutputForm(this, _outputManager, _server.dialogResult);
                outputForm.ShowDialog();
            }

            else
            {
                    MessageBox.Show("No output was generated. \nCould you try again?");
            }
        }

        private void nextStateBox_CheckedChanged(object sender, EventArgs e)
        {
            input = GetDialogues("");
            inputExample.Text = input.Split('\n')[0];
        }

        private void meaningBox_CheckedChanged(object sender, EventArgs e)
        {
            input = GetDialogues("");
            inputExample.Text = input.Split('\n')[0];
        }

        private void styleBox_CheckedChanged(object sender, EventArgs e)
        {
            input = GetDialogues("");
            inputExample.Text = input.Split('\n')[0];
        }
    }
}

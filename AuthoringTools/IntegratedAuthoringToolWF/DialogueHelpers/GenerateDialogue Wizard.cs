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
        ConnectToServerForm _server;
        GPTOutputForm outputForm;

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var dialogs = this._outputManager._mainIAT.GetAllDialogueActions();

            if (dialogs.Count() < 2)
            {
                MessageBox.Show("This feature uses already created Dialog Action to create Utterances. Thus it is hihgly reccomended to add more Dialog Actions.");
                Close();
                
            }

            else
            {
                // Preparing input
                var input = "";
                foreach(var d in dialogs)
                {
                    input += "|" + d.CurrentState + "|" + d.NextState + "|" + d.Style + "|" + d.Utterance + "| \n";
                }

                _server.ProcessDialogues(input, this.HandleOutput);

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class QuickAddBeliefs : Form
    {
        ConnectToServerForm _server;
        OutputManager _manager;
        string result;

        public QuickAddBeliefs(ConnectToServerForm server, OutputManager manager)
        {
            InitializeComponent();
            _server = server;
            _manager = manager;
            processInput.Enabled = false;
            processInput.Text = "  Process Input";
            if (!_server.connected)
            {
                MessageBox.Show("Please connect to the Wizard Server");
              //  _server.currentForm = this;
                _server.ShowDialog(this);
            }

            if (_server.connected)
            {
                processInput.Enabled = true;
               // if(this.Control;
            }

        }

        private void processInput_Click(object sender, EventArgs e)
        {
            processInput.Text = "Computing...";
           _server.ProcessDescription(this.descriptionText.Text, ReceivedInput);

        }

        public void ReceivedInput()
        {

            result = _server.result;
            
           _manager.ComputeStory(result);

            _manager.AcceptOutput();
            processInput.Text = "Completed...";
            Close();
        }

        private void QuickAddBeliefs_Load(object sender, EventArgs e)
        {
            if (_server.connected)
            {
                processInput.Enabled = true;
            }
        }
    }
}

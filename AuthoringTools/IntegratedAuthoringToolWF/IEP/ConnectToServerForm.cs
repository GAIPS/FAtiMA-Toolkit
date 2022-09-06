using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class ConnectToServerForm : Form
    {
        private static readonly HttpClient client = new HttpClient();

      //  public Form currentForm;

        // Default Server IP + PORT
        private static string IP = "146.193.226.21";
        private static string PORT = "8080";

        public string descriptionResult = "";
        public string dialogResult = "";
        public bool connected = false;

        public ConnectToServerForm()
        {
            InitializeComponent();
            this.serverIP.Text = IP;
            this.portText.Text = PORT;
            debugLabel.Text = "Waiting for connection";
        }

        internal void ProcessDescription(string description, MethodInvoker run)
        {
            ProcessDescriptionAsync(description, run).GetAwaiter().GetResult();
        }

        internal void ProcessDialogues(string dialogs, MethodInvoker run)
        {
            ProcessDialoguesAsync(dialogs, run).GetAwaiter().GetResult();
        }

        async Task Handshake()
        {
            // try ping server

            if (debugLabel.Text == "Server not reached")
                return;

            debugLabel.Text = "Searching for Instance...";
            panel4.BackgroundImage = null;
            client.DefaultRequestHeaders.Add("WATTHEHECK", "Anything");

            try
            {
                var r = await SendDescriptionAsync("Hello");

                if (r.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    debugLabel.Text = "Ready to Compute";
                    panel4.BackgroundImage = Properties.Resources.green;
                    connected = true;
                    this.Close();
                }
                else
                {
                    debugLabel.Text = "Code: " + r.StatusCode;
                    panel4.BackgroundImage = Properties.Resources.red;
                }
            }

            catch (Exception f)
            {
                // Discard PingExceptions and return false;

                MessageBox.Show(f.Message);
                debugLabel.Text = "Error: Instance not found";
                panel4.BackgroundImage = Properties.Resources.offline;
            }
        }

        async Task<HttpResponseMessage> SendDescriptionAsync(string _description)
        {
            // Testing purposes

            string send = "Description: " + _description;
            var content = new StringContent(send);
            
            //var response = await client.PostAsync("http://" + IP + ":" + PORT, content).ConfigureAwait(false);

            return Task.Run(() => client.PostAsync("http://" + IP + ":" + PORT, content)).Result;
        }


        async Task ProcessDescriptionAsync(string _description, MethodInvoker run)
        {
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(Environment.MachineName);

            try
            {
                //Send the Story
                await SendDescriptionAsync(_description);

                //Collect the results
                await GetScenarioAsync(run).ConfigureAwait(false);

            }
            catch (Exception f)
            {
                // Discard PingExceptions and return false;
                MessageBox.Show(f.Message);
            }
        }

        async Task GetScenarioAsync(MethodInvoker run)
        {
            IP = IP.Replace(" ", "");
            PORT = PORT.Replace(" ", "");
            var responseBytes = client.GetByteArrayAsync("http://" + IP + ":" + PORT).Result;
            descriptionResult = Encoding.Default.GetString(responseBytes);
            run.Invoke();
        }

        async Task<HttpResponseMessage> SendDialoguesAsync(string _dialogues)
        {
            // Testing purposes
            string send = "Dialogues: " + _dialogues;
            var content = new StringContent(send);

           //var response = await client.PostAsync("http://" + IP + ":" + PORT, content).ConfigureAwait(false);
            return Task.Run(() => client.PostAsync("http://" + IP + ":" + PORT, content)).Result;
        }

        async Task ProcessDialoguesAsync(string _description, MethodInvoker run)
        {
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(Environment.MachineName);
            try
            {
                //Send the Story
                await SendDialoguesAsync(_description);

                //Collect the results
                await GetDialoguesAsync(run).ConfigureAwait(false);

            }
            catch (Exception f)
            {
                // Discard PingExceptions and return false;
                MessageBox.Show(f.Message);
            }
        }

        async Task GetDialoguesAsync(MethodInvoker run)
        {
            IP = IP.Replace(" ", "");
            PORT = PORT.Replace(" ", "");
            var responseBytes = client.GetByteArrayAsync("http://" + IP + ":" + PORT).Result;
            dialogResult = Encoding.Default.GetString(responseBytes);
            run.Invoke();
        }


        private void connectToServer_Click(object sender, EventArgs e)
        {
            Handshake().GetAwaiter().GetResult();
        }

        private void serverIP_TextChanged(object sender, EventArgs e)
        {
            IP = this.serverIP.Text;
        }

        private void portText_TextChanged(object sender, EventArgs e)
        {
            PORT = this.portText.Text;
        }
    }


}

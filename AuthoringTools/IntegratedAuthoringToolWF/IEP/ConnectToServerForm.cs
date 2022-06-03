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

        public Form currentForm;

        // Default Server IP + PORT
        private static string IP = "146.193.224.192";
        private static string PORT = "8080";

        private string result = "";
        public bool connected = false;

        public ConnectToServerForm()
        {
            InitializeComponent();
            this.serverIP.Text = IP;
            this.portText.Text = PORT;
            debugLabel.Text = "Waiting for connection";
        }

        internal string ProcessDescription(string description)
        {
            ProcessDescriptionAsync(description, GetScenarioAsync()).GetAwaiter().GetResult();

            if (this.result != "")
            {

                // Do stuff
                var ret = result;
                this.result = "";
                return ret;
            }

            else return "";
        }

        async Task Handshake()
        {
            // try ping server

            if (debugLabel.Text == "Server not reached")
                return;

            debugLabel.Text = "Searching for Instance...";
            panel4.BackgroundImage = null;
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");

            try
            {
                var r = await SendDescriptionAsync("Hello");

                if (r.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    debugLabel.Text = "Ready to Compute";
                    panel4.BackgroundImage = Properties.Resources.green;
                    connected = true;
                    this.Close();
                    currentForm.ShowDialog();

                }
                else
                {
                    debugLabel.Text = "Code: " + r.StatusCode;
                    panel4.BackgroundImage = Properties.Resources.red;
                }
                //Collect the results

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


            var content = new StringContent(_description);

            //var response = await client.PostAsync("http://" + IP + ":" + PORT, content).ConfigureAwait(false);

            return Task.Run(() => client.PostAsync("http://" + IP + ":" + PORT, content)).Result;
        }


        async Task ProcessDescriptionAsync(string _description, Task run)
        {
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(Environment.MachineName);

            try
            {

                //Send the Story
                await SendDescriptionAsync(_description);


                //Collect the results
                await run.ConfigureAwait(false);

            }
            catch (Exception f)
            {
                // Discard PingExceptions and return false;

                MessageBox.Show(f.Message);


            }

        }

        async Task GetScenarioAsync()
        {
            IP = IP.Replace(" ", "");
            PORT = PORT.Replace(" ", "");
            var responseBytes = client.GetByteArrayAsync("http://" + IP + ":" + PORT).Result;
            result = Encoding.Default.GetString(responseBytes);
        }

        public string GetScenario(string description)
        {
            
            ProcessDescriptionAsync(description, GetScenarioAsync()).GetAwaiter().GetResult();

            if (this.result != "")
            {

                // Do stuff
                var ret = result;
                this.result = "";
                return ret;
            }

            else return "";
        }

        private void connectToServer_Click(object sender, EventArgs e)
        {
            Handshake();
        }
    }


}

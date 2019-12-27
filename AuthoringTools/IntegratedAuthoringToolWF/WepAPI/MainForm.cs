using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WebServer;


namespace WebAPIWF
{
    public partial class MainForm : Form
    {
    
        private delegate void UpdateUIDelegate(string message, string uiElement, string uiColor);
        private UpdateUIDelegate updateUIDelegate = null;

        private string serverStatus = "Server is not running.";
        private const string UIELEMENT_SERVERSTATUS = "ServerStatus";
        private const string UIELEMENT_OUTPUTCONSOLE = "Console";
        private Thread serverThread;
        HTTPFAtiMAServer fatimaServer;
        public IntegratedAuthoringToolWF.MainForm iat  { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.textBoxServer.Text = this.serverStatus;
            this.dataGridApiMethods.DataSource = APIResource.Set.Select(r => new APIResourceDescriptionDTO {Resource = r.Type.ToString().ToLower(), URL = r.URLFormat, Methods = "[ " + string.Join(",", r.ValidOperations) + " ]" }).ToList();
            this.dataGridApiMethods.Refresh();
            this.updateUIDelegate = new UpdateUIDelegate(this.UpdateUI);
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            fatimaServer?.Close();

            if (serverThread != null)
            {
                serverThread.Abort();
            }
            try
            {
                serverThread = new Thread(new ThreadStart(this.ServerThread));
                serverThread.Start();
            }
            catch (Exception ex)
            {
                this.textBoxServer.Text = ex.Message;
                serverThread = null;
            }

        }


        private void UpdateUI(string message, string uiElement, string color)
        {
            if (uiElement == UIELEMENT_SERVERSTATUS)
            {
                this.textBoxServer.Text = message;
            }
            else if (uiElement == UIELEMENT_OUTPUTCONSOLE)
            {
                this.richTextBoxOutuputConsole.AppendText("\n" + message + "\n", Color.FromName(color));
            }
        }

        private void ServerNotificationHandler(object sender, ServerEventArgs e)
        {
            switch (e.Type)
            {
                case MessageTypes.Error: 
                    this.Invoke(this.updateUIDelegate, new string[] { e.Message, UIELEMENT_SERVERSTATUS, "Red" });
                    break;
                case MessageTypes.Output:
                    this.Invoke(this.updateUIDelegate, new string[] { e.Message, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    break;
                case MessageTypes.Request:
                    this.Invoke(this.updateUIDelegate, new string[] { e.Message, UIELEMENT_OUTPUTCONSOLE, "Green" });
                    break;
                case MessageTypes.Status:
                    this.Invoke(this.updateUIDelegate, new string[] { e.Message, UIELEMENT_SERVERSTATUS, "Black" });
                    break;
            }
         
        }

        private void ServerThread()
        {

            try
            {
                fatimaServer?.Close();
                fatimaServer = new HTTPFAtiMAServer() { IatFilePath = iat._currentScenarioFilePath, AssetFilePath = iat.textBoxPathAssetStorage.Text, Port = int.Parse(this.textBoxPort.Text) };
                fatimaServer.OnServerEvent += ServerNotificationHandler;
                fatimaServer.Run();
            }
            catch (Exception ex)
            {
                this.Invoke(this.updateUIDelegate, new string[] { "Error while loading the assets!", UIELEMENT_SERVERSTATUS, "Red" });
                fatimaServer?.Close();
                return;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridApiMethods_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }
    }

}

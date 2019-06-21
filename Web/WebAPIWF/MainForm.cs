using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using WellFormedNames;
using WorldModel;

namespace WebAPIWF
{
    public partial class MainForm : BaseIATForm
    {
        IntegratedAuthoringToolAsset iat;
       

        private delegate void UpdateUIDelegate(string message, string uiElement);
        private UpdateUIDelegate updateUIDelegate = null;

        private string serverStatus = "Server is not running.";
        private const string UIELEMENT_SERVERSTATUS = "ServerStatus";
        private const string UIELEMENT_OUTPUTCONSOLE = "Console";
        private HttpListener server;
        private Thread serverThread;


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
            this.dataGridApiMethods.DataSource = APIMethod.Methods;
            this.dataGridApiMethods.Refresh();
            this.updateUIDelegate = new UpdateUIDelegate(this.UpdateUI);
        }

        private void Test()
        {
            this.textBoxServer.Text = "BLABAL";

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            if (serverThread != null)
            {
                if (server!=null && server.IsListening)
                {
                    server.Close();
                }
                serverThread.Abort();
            }
            try
            {
                serverThread = new Thread(new ThreadStart(this.WebServer));
                serverThread.Start();
            }catch(Exception ex)
            {
                this.textBoxServer.Text = ex.Message;
                serverThread = null;
            }

        }


        private void UpdateUI(string message, string uiElement)
        {
            if(uiElement == UIELEMENT_SERVERSTATUS)
            {
                this.textBoxServer.Text = message;
            }else if(uiElement == UIELEMENT_OUTPUTCONSOLE)
            {
                this.richTextBoxOutuputConsole.AppendText("\n" + message + "\n");
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                this.richTextBoxOutuputConsole.Focus();
            }
        }


        private void LoadCharacters(IntegratedAuthoringToolAsset iat, List<RolePlayCharacterAsset> rpcs)
        {
                foreach (var source in iat.GetAllCharacterSources())
                {
                    var rpc = RolePlayCharacterAsset.LoadFromFile(source.Source);
                    rpc.LoadAssociatedAssets();
                    iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
                    rpcs.Add(rpc);
                }           
        }

        private void WebServer()
        {
            IntegratedAuthoringToolAsset iat = null;
            WorldModelAsset wm = null;
            List<RolePlayCharacterAsset> rpcs = new List<RolePlayCharacterAsset>();
            
            try
            {
                iat = IntegratedAuthoringToolAsset.LoadFromFile(LoadedAsset.AssetFilePath);
                if(iat.m_worldModelSource!=null)
                {
                    wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
                }
                LoadCharacters(iat, rpcs);
            }
            catch (Exception ex)
            {
                this.Invoke(this.updateUIDelegate, new string[] { "Error while loading the assets!", "ServerStatus" });
                return;
            }

            try
            {
                server = new HttpListener();
                int port = int.Parse(this.textBoxPort.Text); 
                server.Prefixes.Add("http://localhost:" + port + "/");
                server.Start();
                this.Invoke(this.updateUIDelegate,"Server started on port: " + port + "...", UIELEMENT_SERVERSTATUS);
            }catch(Exception ex)
            {
                this.Invoke(this.updateUIDelegate, new string[] { "Server error: " + ex.Message, UIELEMENT_SERVERSTATUS});
                server.Close();
                return;
            }


            while (true)
            {
                HttpListenerContext context = server.GetContext();
                string responseJson = "";
                var request = context.Request;

                if (request.RawUrl.StartsWith("/favicon.ico"))
                    continue; //ignore this request

                if (request.HttpMethod == "GET")
                {
                    if (request.RawUrl.StartsWith("/" + APIMethod.DECIDE+"?c="))
                    {
                        var characterName = new String(request.RawUrl.SkipWhile(c => c != '=').Skip(1).ToArray()).ToLowerInvariant();
                        this.Invoke(this.updateUIDelegate, new string[] { "New GET /" + APIMethod.DECIDE + " request for character: "+characterName+" !", UIELEMENT_OUTPUTCONSOLE });
                        var rpc = rpcs.Where(r => r.CharacterName.ToString().ToLowerInvariant() == characterName).FirstOrDefault();
                        var decisions = rpc?.Decide();
                        if (decisions != null)
                        {
                            List<DecisionDTO> resultDTO = new List<DecisionDTO>();
                            foreach (var d in decisions)
                            {
                                string utterance = null;
                                if (string.Equals(d.Key.ToString(),IATConsts.DIALOG_ACTION_KEY, StringComparison.OrdinalIgnoreCase))
                                {
                                    utterance = iat.GetDialogAction(d, out utterance).Utterance;
                                    utterance = rpc.ProcessWithBeliefs(utterance);
                                }
                                resultDTO.Add(new DecisionDTO { Action = d.Name.ToString(), Target = d.Target.ToString(), Utterance = utterance, Utility = d.Utility }) ;
                            }
                            responseJson = JsonConvert.SerializeObject(resultDTO);
                            this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE });
                        }
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.CHARACTERS))
                    {
                        this.Invoke(this.updateUIDelegate,new string[] { "New GET /" + APIMethod.CHARACTERS + " request!", UIELEMENT_OUTPUTCONSOLE });
                        var result = new List<CharacterDTO>();
                        foreach (var rpc in rpcs)
                        {
                            result.Add(new CharacterDTO { Name = rpc.CharacterName.ToString(), Emotions = rpc.GetAllActiveEmotions(), Mood = rpc.Mood , Tick = rpc.Tick});
                        }
                        responseJson = JsonConvert.SerializeObject(result);
                        this.Invoke(this.updateUIDelegate, new string[] {"Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE });
                    }
                    else
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_UNKNOWN_GET_REQUEST + ": " + request.RawUrl, UIELEMENT_OUTPUTCONSOLE });
                        responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_UNKNOWN_GET_REQUEST);
                    }
                }
                else if (request.HttpMethod == "POST")
                {
                    string requestBody = string.Empty;
                    if (request.HasEntityBody)
                    {
                        using (Stream body = context.Request.InputStream) // here we have data
                        {
                            using (StreamReader reader = new StreamReader(body, context.Request.ContentEncoding))
                            {
                                requestBody = reader.ReadToEnd();
                            }
                        }
                    }
                    if (request.RawUrl.StartsWith("/" + APIMethod.PERCEIVE))
                    {
                        Console.WriteLine("New POST /" + APIMethod.PERCEIVE.Name + " request!");
                        if (!string.IsNullOrEmpty(requestBody))
                        {
                            var events = JsonConvert.DeserializeObject<string[]>(requestBody);
                            foreach (var ev in events)
                            {
                                Name evName = null;
                                try
                                {
                                    evName = WellFormedNames.Name.BuildName(ev);
                                    foreach (var rpc in rpcs)
                                    {
                                        rpc.Perceive(evName);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.Invoke(this.updateUIDelegate, new string[] { String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message), UIELEMENT_OUTPUTCONSOLE });
                                    responseJson = JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                                }
                            }
                        }
                        else
                        {
                            this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_EMPTY_EVENT_LIST, UIELEMENT_OUTPUTCONSOLE });
                            responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_EVENT_LIST);
                        }
                    }
                    if (request.RawUrl.StartsWith("/" + APIMethod.EXECUTE))
                    {
                        Console.WriteLine("New POST /" + APIMethod.EXECUTE.Name + " request!");
                        if (!string.IsNullOrEmpty(requestBody))
                        {
                            Name ev = null;
                            try
                            {
                                var requests = JsonConvert.DeserializeObject<ExecuteRequestDTO[]>(requestBody);
                                foreach (var a in requests)
                                {
                                    ev = EventHelper.ActionEnd(a.Subject, a.Action, a.Target);
                                    foreach (var rpc in rpcs)
                                    {
                                        rpc.Perceive(ev);
                                    }
                                    if(wm != null)
                                    {
                                        var eventEffects = wm.Simulate(new[] { ev });
                                        foreach(var eff in eventEffects)
                                        {
                                            if(eff.ObserverAgent == WellFormedNames.Name.UNIVERSAL_SYMBOL)
                                            {
                                                foreach (var rpc in rpcs)
                                                {
                                                    rpc.Perceive(EventHelper.PropertyChange(eff.PropertyName, eff.NewValue, (Name)a.Subject));
                                                }
                                            }
                                            else
                                            {
                                                var obs = rpcs.Where(r => r.CharacterName == eff.ObserverAgent).FirstOrDefault();
                                                {
                                                    obs?.Perceive(EventHelper.PropertyChange(eff.PropertyName, eff.NewValue, (Name)a.Subject));
                                                }
                                            }
                                        }
                                    }
                                 }
                            }
                             catch (Exception ex)
                            {
                                this.Invoke(this.updateUIDelegate, new string[] { String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message), UIELEMENT_OUTPUTCONSOLE });
                                responseJson = JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                            }
                        }
                        else
                        {
                            this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_EMPTY_ACTION_REQUEST_LIST, UIELEMENT_OUTPUTCONSOLE });
                            responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_ACTION_REQUEST_LIST);
                        }
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.UPDATE))
                    {
                        Console.WriteLine("New POST /" + APIMethod.UPDATE + " request!");
                        if (string.IsNullOrEmpty(requestBody))
                        {
                            foreach (var rpc in rpcs)
                            {
                                rpc.Update();
                            }
                        }
                        else
                        {
                            var ticks = JsonConvert.DeserializeObject<int>(requestBody);
                            foreach (var rpc in rpcs)
                            {
                                for (int i = 0; i < ticks; i++)
                                {
                                    rpc.Update();
                                }
                            }
                        }
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.RESET))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "New POST /" + APIMethod.RESET + " request!", UIELEMENT_OUTPUTCONSOLE });
                        iat = IntegratedAuthoringToolAsset.LoadFromFile(LoadedAsset.AssetFilePath);
                        if (iat.m_worldModelSource != null)
                        {
                            wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
                        }
                        LoadCharacters(iat, rpcs);
                        responseJson = JsonConvert.SerializeObject("RPC Reset");
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE });
                    }
                    else
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_UNKNOWN_POST_REQUEST+": " + request.RawUrl, UIELEMENT_OUTPUTCONSOLE });
                        responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_UNKNOWN_POST_REQUEST);
                    }
                }

                HttpListenerResponse response = context.Response;
                response.ContentType = "application/json";

                byte[] buffer = Encoding.UTF8.GetBytes(responseJson);
                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);
                context.Response.Close();
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

using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace WebAPIWF
{
    public partial class MainForm : BaseIATForm
    {
    
        private delegate void UpdateUIDelegate(string message, string uiElement, string uiColor);
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
                if (server != null && server.IsListening)
                {
                    server.Close();
                }
                serverThread.Abort();
            }
            try
            {
                serverThread = new Thread(new ThreadStart(this.WebServer));
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
                if (iat.m_worldModelSource != null)
                {
                    wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
                }
                LoadCharacters(iat, rpcs);
            }
            catch (Exception ex)
            {
                this.Invoke(this.updateUIDelegate, new string[] { "Error while loading the assets!", UIELEMENT_SERVERSTATUS, "Red"});
                return;
            }

            try
            {
                server = new HttpListener();
                int port = int.Parse(this.textBoxPort.Text);
                server.Prefixes.Add("http://localhost:" + port + "/");
                server.Start();
                this.Invoke(this.updateUIDelegate, "Server started on port: " + port + "...", UIELEMENT_SERVERSTATUS, "Black");
            }
            catch (Exception ex)
            {
                this.Invoke(this.updateUIDelegate, new string[] { "Server error: " + ex.Message, UIELEMENT_SERVERSTATUS, "Red"});
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
                    if (request.RawUrl.StartsWith("/" + APIMethod.DECIDE + "?c="))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "GET /" + APIMethod.DECIDE + " request!", UIELEMENT_OUTPUTCONSOLE, "Green"});
                        responseJson = this.HandleDecideRequest(request.RawUrl, iat, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.ASK))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "GET /" + APIMethod.ASK + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        responseJson = this.HandleAskRequest(request.RawUrl, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.CHARACTERS))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "GET /" + APIMethod.CHARACTERS + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        responseJson = this.HandleCharactersRequest(request.RawUrl, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_UNKNOWN_GET_REQUEST + ": " + request.RawUrl, UIELEMENT_OUTPUTCONSOLE, "Red" });
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
                        this.Invoke(this.updateUIDelegate, new string[] { "POST /" + APIMethod.PERCEIVE + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        this.Invoke(this.updateUIDelegate, new string[] { "Input:\n" + requestBody, UIELEMENT_OUTPUTCONSOLE, "Orange" });
                        responseJson = this.HandlePerceiveRequest(request.RawUrl, requestBody, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.EXECUTE))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "POST /" + APIMethod.EXECUTE + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        this.Invoke(this.updateUIDelegate, new string[] { "Input:\n" + requestBody, UIELEMENT_OUTPUTCONSOLE, "Orange" });
                        responseJson = this.HandleExecuteRequest(requestBody, wm, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.UPDATE))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "POST /" + APIMethod.UPDATE + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        this.Invoke(this.updateUIDelegate, new string[] { "Input:\n" + requestBody, UIELEMENT_OUTPUTCONSOLE, "Orange" });
                        responseJson = this.HandleUpdateRequest(requestBody, rpcs);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.RESET))
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { "POST /" + APIMethod.RESET + " request!", UIELEMENT_OUTPUTCONSOLE, "Green" });
                        this.Invoke(this.updateUIDelegate, new string[] { "Input:\n" + requestBody, UIELEMENT_OUTPUTCONSOLE, "Orange" });
                        responseJson = this.HandleResetRequest(request.RawUrl, rpcs, out iat, out wm);
                        this.Invoke(this.updateUIDelegate, new string[] { "Result:\n" + responseJson, UIELEMENT_OUTPUTCONSOLE, "Blue" });
                    }
                    else
                    {
                        this.Invoke(this.updateUIDelegate, new string[] { APIErrors.ERROR_UNKNOWN_POST_REQUEST + ": " + request.RawUrl, UIELEMENT_OUTPUTCONSOLE, "Red" });
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


        private string HandleDecideRequest(string request, IntegratedAuthoringToolAsset iat, List<RolePlayCharacterAsset> rpcs)
        {
            List<DecisionDTO> resultDTO = new List<DecisionDTO>();

            var characterName = new String(request.SkipWhile(c => c != '=').Skip(1).ToArray()).ToLowerInvariant();
             var rpc = rpcs.Where(r => r.CharacterName.ToString().ToLowerInvariant() == characterName).FirstOrDefault();
            var decisions = rpc?.Decide();

            if (decisions != null)
            {
                foreach (var d in decisions)
                {
                    string utterance = null;
                    if (string.Equals(d.Key.ToString(), IATConsts.DIALOG_ACTION_KEY, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            utterance = iat.GetDialogAction(d, out utterance).Utterance;
                        }catch(Exception ex)
                        {
                            return JsonConvert.SerializeObject(string.Format(APIErrors.ERROR_UNKOWN_SPEAK_ACTION, d));
                        }
                        utterance = rpc.ProcessWithBeliefs(utterance);
                    }
                    resultDTO.Add(new DecisionDTO { Action = d.Name.ToString(), Target = d.Target.ToString(), Utterance = utterance, Utility = d.Utility });
                }
            }
            return JsonConvert.SerializeObject(resultDTO);
        }

        private string HandleAskRequest(string request, List<RolePlayCharacterAsset> rpcs)
        {
            try
            {
                var reqParamsURL = new String(request.SkipWhile(c => c != '=').Skip(1).ToArray()).ToLowerInvariant();
                string[] reqParams = reqParamsURL.Split('&');
                var characterName = reqParams[0];
                var beliefHead = new String(reqParams[1].SkipWhile(c => c != '=').Skip(1).ToArray());
                var beliefBody = new String(reqParams[2].SkipWhile(c => c != '=').Skip(1).ToArray());
                var belief = WellFormedNames.Name.BuildName(beliefHead + "(" + beliefBody + ")");
                var rpc = rpcs.Where(r => r.CharacterName.ToString().ToLowerInvariant() == characterName).FirstOrDefault();
                var beliefResult = rpc.GetBeliefValue(belief.ToString());
                return JsonConvert.SerializeObject(beliefResult);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_ASK);
            }
        }

        private string HandleCharactersRequest(string request, List<RolePlayCharacterAsset> rpcs)
        {
            var result = new List<CharacterDTO>();
            foreach (var rpc in rpcs)
            {
                result.Add(new CharacterDTO { Name = rpc.CharacterName.ToString(), Emotions = rpc.GetAllActiveEmotions(), Mood = rpc.Mood, Tick = rpc.Tick });
            }
            return JsonConvert.SerializeObject(result);
        }


        private string HandlePerceiveRequest(string request, string requestBody, List<RolePlayCharacterAsset> rpcs)
        {
            string[] events = {};
            string character = WellFormedNames.Name.UNIVERSAL_STRING;

            if (request.StartsWith("/" + APIMethod.PERCEIVE + "?c="))
            {
               character = new String(request.SkipWhile(c => c != '=').Skip(1).ToArray()).ToLowerInvariant();
            }

            if (!string.IsNullOrEmpty(requestBody))
            {
                events = JsonConvert.DeserializeObject<string[]>(requestBody);
                foreach (var ev in events)
                {
                    Name evName = null;
                    try
                    {
                        evName = WellFormedNames.Name.BuildName(ev);
                        if (character == WellFormedNames.Name.UNIVERSAL_STRING)
                        {
                            foreach (var rpc in rpcs)
                            {
                                rpc.Perceive(evName);
                            }
                        }
                        else
                        {
                            var rpc = rpcs.Where(r => r.CharacterName.ToString().ToLowerInvariant() == character).FirstOrDefault();
                            rpc.Perceive(evName);
                        }
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                    }
                }
            }
            else
            {
               return  JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_EVENT_LIST);
            }
            return JsonConvert.SerializeObject(string.Format("{0} event(s) perceived by {1}", events.Count(), character));
        }

        private string HandleExecuteRequest(string requestBody, WorldModelAsset wm, List<RolePlayCharacterAsset> rpcs)
        {
            IEnumerable<EffectDTO> eventEffects = null;

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
                        if (wm != null)
                        {
                            eventEffects = wm.Simulate(new[] { ev });
                            foreach (var eff in eventEffects)
                            {
                                if (eff.ObserverAgent == WellFormedNames.Name.UNIVERSAL_SYMBOL)
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
                    return JsonConvert.SerializeObject((string.Format("{0} actions(s) executed and {1} effects triggered", requests.Count(), eventEffects.Count())));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                }
            }
            else
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_ACTION_REQUEST_LIST);
            }
        }

        private string HandleUpdateRequest(string requestBody, List<RolePlayCharacterAsset> rpcs)
        {
            int ticks = 0;
            if (string.IsNullOrEmpty(requestBody))
            {
                foreach (var rpc in rpcs)
                {
                    rpc.Update();
                }
            }
            else
            {
                try
                {
                    ticks = JsonConvert.DeserializeObject<int>(requestBody);
                    foreach (var rpc in rpcs)
                    {
                        for (int i = 0; i < ticks; i++)
                        {
                            rpc.Update();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_UPDATE);
                }
            }
            return JsonConvert.SerializeObject(string.Format("Updated {0} ticks!", ticks));
        }

        private string HandleResetRequest(string request, List<RolePlayCharacterAsset> rpcs,out IntegratedAuthoringToolAsset iat,  out WorldModelAsset wm)
        {
            wm = null;
            iat = IntegratedAuthoringToolAsset.LoadFromFile(LoadedAsset.AssetFilePath);
            if (iat.m_worldModelSource != null)
            {
                wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
            }
            rpcs = new List<RolePlayCharacterAsset>();
            LoadCharacters(iat, rpcs);
            return JsonConvert.SerializeObject("Scenario Loaded.");
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridApiMethods_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }
    }

}

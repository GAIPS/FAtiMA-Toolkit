using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using WellFormedNames;

namespace WebAPIWF
{
    public partial class MainForm : BaseIATForm
    {
        IntegratedAuthoringToolAsset iat;
        RolePlayCharacterAsset rpc;

        private delegate void UpdateStatusDelegate();
        private UpdateStatusDelegate updateStatusDelegate = null;

        //http://www.daveoncsharp.com/2009/09/create-a-worker-thread-for-your-windows-form-in-csharp/

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
            this.dataGridApiMethods.DataSource = APIMethod.Methods;
            this.dataGridApiMethods.Refresh();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var serverThread = new Thread(new ThreadStart(this.WebServer));
            serverThread.Start();
        }



        private void WebServer()
        {
            int port = 8080;

            //Loading the FAtiMA Assets (The scenario name must also be configurable)
            var iat = IntegratedAuthoringToolAsset.LoadFromFile(LoadedAsset.AssetFilePath);
            var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
            
            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://localhost:" + port + "/"); //TODO: Make the port configurable
            server.Start();
            Console.WriteLine("Listening on port " + port + "...");

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                string responseJson = "";
                var request = context.Request;

                if (request.HttpMethod == "GET")
                {
                    if (request.RawUrl.StartsWith("/" + APIMethod.DECIDE))
                    {
                        Console.WriteLine("New GET /" + APIMethod.DECIDE + " request!");
                        var decisions = rpc.Decide();
                        responseJson = JsonConvert.SerializeObject(decisions);
                    }

                    else if (request.RawUrl.StartsWith("/" + APIMethod.EMOTIONALSTATE))
                    {
                        Console.WriteLine("New GET /" + APIMethod.EMOTIONALSTATE + " request!");
                        //var es = new EmotionalStateDTO { Mood = rpc.Mood, Emotions = rpc.GetAllActiveEmotions().ToList() };
                        //responseJson = JsonConvert.SerializeObject(es);
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.RESET))
                    {
                        Console.WriteLine("New GET /" + APIMethod.RESET + " request!");
                        rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
                        rpc.LoadAssociatedAssets();
                        iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
                        responseJson = JsonConvert.SerializeObject("RPC Reset");
                    }
                    else
                    {
                        Console.WriteLine(APIErrors.ERROR_UNKNOWN_GET_REQUEST);
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
                                    rpc.Perceive(evName);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                                    responseJson = JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(APIErrors.ERROR_EMPTY_EVENT_LIST);
                            responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_EVENT_LIST);
                        }
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.UPDATE))
                    {
                        Console.WriteLine("New POST /" + APIMethod.UPDATE + " request!");
                        rpc.Update();
                        responseJson = JsonConvert.SerializeObject(rpc.Tick);
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.TRANSLATE))
                    {
                        var speakAction = JsonConvert.DeserializeObject<string>(requestBody);
                        Console.WriteLine("New POST /" + APIMethod.TRANSLATE + " " + speakAction + " request!");
                        try
                        {
                            var speakName = WellFormedNames.Name.BuildName(speakAction);
                            var actions = iat.GetDialogueActions(speakName.GetNTerm(1), speakName.GetNTerm(2), speakName.GetNTerm(3), speakName.GetNTerm(4));
                            if (actions.Any())
                            {
                                //TODO: return an array of utterances when more than one action is found
                                var processedUtt = rpc.ProcessWithBeliefs(actions.FirstOrDefault()?.Utterance);
                                responseJson = JsonConvert.SerializeObject(processedUtt);
                            }
                            else
                            {
                                Console.WriteLine(String.Format(APIErrors.ERROR_UNKOWN_SPEAK_ACTION, speakAction));
                                responseJson = JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_UNKOWN_SPEAK_ACTION, speakAction));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(String.Format(APIErrors.ERROR_EXCEPTION_TRANSLATE, speakAction, ex.Message));
                            responseJson = JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, speakAction, ex.Message));
                        }
                    }
                    else
                    {
                        Console.WriteLine(APIErrors.ERROR_UNKNOWN_POST_REQUEST);
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
    }

}

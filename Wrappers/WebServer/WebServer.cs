using IntegratedAuthoringTool;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;



namespace WebServer
{
    public class HTTPFAtiMAServer
    {
        public string IatFilePath { get; set; }
        public string AssetFilePath { get; set; }
        public int Port { get; set; } = 80;
        public int HTTPSPort { get; set; } = 443;

        public bool startWithCurrentScenario;

        public const int MAX_INSTANCES = 100;
        private HttpListener server;

        public event EventHandler<ServerEventArgs> OnServerEvent;

        public void Close()
        {
            server?.Close();
        }

        public void Run()
        {
            ServerState state = new ServerState();

            if (AssetFilePath != null && IatFilePath != null && startWithCurrentScenario)
            {
                state = this.LoadCurrentScenario(state);
            }
            try
            {
                server = new HttpListener();
                server.Prefixes.Add("http://*:" + this.Port + "/");
                server.Prefixes.Add("https://*:" + this.HTTPSPort + "/");

                server.Start();
                OnServerEvent(this, new ServerEventArgs
                {
                    Message = "FAtiMA server started on port '" + this.Port + "'",
                    Type = MessageTypes.Status
                });
            }
            catch (Exception ex)
            {
                server.Close();
                OnServerEvent(this, new ServerEventArgs
                {
                    Message = ex.Message,
                    Type = MessageTypes.Error
                });
                return;
            }

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                string responseJson = "";
                try
                {
                    var rq = new APIRequest(context.Request);

                    if (!string.IsNullOrEmpty(rq.ErrorMessage))
                    {
                        responseJson = JsonConvert.SerializeObject(rq.ErrorMessage);
                        OnServerEvent(this, new ServerEventArgs
                        {
                            Message = "Error: " + responseJson,
                            Type = MessageTypes.Error
                        });
                    }
                    else
                    {
                        OnServerEvent(this, new ServerEventArgs
                        {
                            Message = rq.Method + " request: " + context.Request.RawUrl + " [" + DateTime.Now + "]",
                            Type = MessageTypes.Request
                        });

                        if (!rq.Resource.IsHttpMethodValid(rq))
                            responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_INVALID_HTTP_METHOD);
                        else if (!rq.Resource.HasAuthorization(rq.Key, rq.ScenarioName, state))
                            responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_ACCESS_DENIED);
                        else
                        {
                            if (rq.Method == HTTPMethod.RESET) {
                                
                                state = this.LoadCurrentScenario(state);
                                responseJson = JsonConvert.SerializeObject("Scenario reset.");
                            }
                            else
                                responseJson = rq.Resource.Execute(rq, state);
                        }
                        OnServerEvent(this, new ServerEventArgs
                        {
                            Message = "Result: " + responseJson,
                            Type = MessageTypes.Output
                        });

                    }
                }
                catch (Exception ex)
                {
                    responseJson = JsonConvert.SerializeObject(ex.Message);
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

        ServerState LoadCurrentScenario(ServerState state)
        {
            var storage = GAIPS.Rage.AssetStorage.FromJson(File.ReadAllText(AssetFilePath));
            var iat = IntegratedAuthoringTool.IntegratedAuthoringToolAsset.FromJson(File.ReadAllText(IatFilePath), storage);

            state.Scenarios[iat.ScenarioName.ToLower()] = new IntegratedAuthoringToolAsset[HTTPFAtiMAServer.MAX_INSTANCES + 1];

            state.Scenarios[iat.ScenarioName.ToLower()][0] = iat;

            state.Scenarios[iat.ScenarioName.ToLower()] = new IntegratedAuthoringToolAsset[HTTPFAtiMAServer.MAX_INSTANCES + 1];

            //instance of id 1 is automatically created
            state.Scenarios[iat.ScenarioName.ToLower()][1] = iat;

            return state;
        }
    }

    public class ServerEventArgs : EventArgs
    {
        public string Message { get; set; }
        public MessageTypes Type { get; set; }
    }

}

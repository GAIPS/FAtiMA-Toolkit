using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace WebServer
{

    public class HTTPFAtiMAServer
    {
        public string IatFilePath { get; set; }
        public int Port { get; set; }

        private const string DEFAULT_INSTANCE_ID = "Default";
        private HttpListener server;

        public event EventHandler<ServerEventArgs> OnServerEvent;

        private void LoadCharacters(IntegratedAuthoringToolAsset iat, string sessionName, ConcurrentDictionary<string,List<RolePlayCharacterAsset>> rpcs)
        {
            if (rpcs.ContainsKey(sessionName))
            {
                rpcs[sessionName] = new List<RolePlayCharacterAsset>(); 
            }
            else
            {
                rpcs.GetOrAdd(sessionName, new List<RolePlayCharacterAsset>());
            }
                

            foreach (var source in iat.GetAllCharacterSources())
            {
                var j = iat.AssetFilePath;
                var rpc = RolePlayCharacterAsset.LoadFromFile(source.Source);
                rpc.LoadAssociatedAssets();
                iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
                
                rpcs[sessionName].Add(rpc);
            }
        }

        public void Close()
        {
            server?.Close();
        }

        public void Run()
        {

            IntegratedAuthoringToolAsset iat;
            ConcurrentDictionary<string, List<RolePlayCharacterAsset>> rpcs = new ConcurrentDictionary<string, List<RolePlayCharacterAsset>>();
            WorldModelAsset wm = null;

            iat = IntegratedAuthoringToolAsset.LoadFromFile(IatFilePath);
            if (iat.m_worldModelSource != null && !string.IsNullOrWhiteSpace(iat.m_worldModelSource.RelativePath))
            {
                wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
            }
            LoadCharacters(iat, DEFAULT_INSTANCE_ID, rpcs);

            try
            {
                server = new HttpListener();
                server.Prefixes.Add("http://*:" + this.Port + "/");
                server.Start();
                OnServerEvent(this, new ServerEventArgs
                {
                    Message = "Server started on port '" + this.Port + "' on scenario '" + iat.ScenarioName + "'",
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
                var request = context.Request;

                if (request.RawUrl.StartsWith("/favicon.ico"))
                    continue; //ignore this request

                OnServerEvent(this, new ServerEventArgs
                {
                    Message = request.HttpMethod + " request: " + request.RawUrl + " [" + DateTime.Now + "]",
                    Type = MessageTypes.Request
                });

                string apiMethod = request.Url.Segments[request.Url.Segments.Length - 1].ToLower();
                string instance = request.Url.Segments.Length == 3 ?  request.Url.Segments[1].ToLower() : DEFAULT_INSTANCE_ID;
                
                if(instance != DEFAULT_INSTANCE_ID) instance = instance.Remove(instance.Length - 1);

                if(instance != DEFAULT_INSTANCE_ID && !rpcs.ContainsKey(instance) && apiMethod != APIMethods.CREATE.ToString())
                    responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_UNKNOWN_INSTANCE);
                else if (request.HttpMethod == "GET")
                {

                    if (apiMethod == APIMethods.DECIDE.ToString())
                    {
                        var charName = request.QueryString["c"].ToLower();
                        responseJson = this.HandleDecideRequest(charName, iat, rpcs[instance]);
                    }
                    else if (apiMethod == APIMethods.ASK.ToString())
                    {
                        var charName = request.QueryString["c"].ToLower();
                        var beliefHead = request.QueryString["bh"].ToLower();
                        var beliefBody = request.QueryString["bb"].ToLower();
                        var belief = Name.BuildName(beliefHead + "(" + beliefBody + ")");
                        responseJson = this.HandleAskRequest(charName, belief, rpcs[instance]);
                    }
                    else if (apiMethod == APIMethods.CHARACTERS.ToString())
                    {
                        responseJson = this.HandleCharactersRequest(rpcs[instance]);
                    }
                    else
                    {
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
                    if (apiMethod == APIMethods.PERCEIVE.ToString())
                    {
                        var charName = request.QueryString["c"].ToLower();
                        responseJson = this.HandlePerceiveRequest(charName, requestBody, rpcs[instance]);
                    }
                    else if (apiMethod == APIMethods.EXECUTE.ToString())
                    {
                        responseJson = this.HandleExecuteRequest(requestBody, wm, rpcs[instance]);
                    }
                    else if (apiMethod == APIMethods.UPDATE.ToString())
                    {
                        responseJson = this.HandleUpdateRequest(requestBody, rpcs[instance]);
                    }
                   
                    else if (apiMethod == APIMethods.CREATE.ToString())
                    {
                        if(instance != DEFAULT_INSTANCE_ID)
                        {
                            LoadCharacters(iat, instance, rpcs);
                        }
                        responseJson = JsonConvert.SerializeObject("Instance '"+ instance + "'created."); ;
                    }
                    else
                    {
                        responseJson = JsonConvert.SerializeObject(APIErrors.ERROR_UNKNOWN_POST_REQUEST);
                    }
                }

                OnServerEvent(this, new ServerEventArgs
                {
                    Message = "Result: " + responseJson,
                    Type = MessageTypes.Output
                });


                HttpListenerResponse response = context.Response;
                response.ContentType = "application/json";

                byte[] buffer = Encoding.UTF8.GetBytes(responseJson);
                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);
                context.Response.Close();
            }
        }

        private string HandleDecideRequest(string characterName, IntegratedAuthoringToolAsset iat, List<RolePlayCharacterAsset> rpcs)
        {
            List<DecisionDTO> resultDTO = new List<DecisionDTO>();

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
                        }
                        catch (Exception)
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

        private string HandleAskRequest(string characterName, Name belief, List<RolePlayCharacterAsset> rpcs)
        {
            try
            {
                var rpc = rpcs.Where(r => r.CharacterName.ToString().ToLowerInvariant() == characterName).FirstOrDefault();
                var beliefResult = rpc.GetBeliefValue(belief.ToString());
                return JsonConvert.SerializeObject(beliefResult);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_ASK);
            }
        }

        private string HandleCharactersRequest(List<RolePlayCharacterAsset> rpcs)
        {
            var result = new List<CharacterDTO>();
            foreach (var rpc in rpcs)
            {
                result.Add(new CharacterDTO { Name = rpc.CharacterName.ToString(), Emotions = rpc.GetAllActiveEmotions(), Mood = rpc.Mood, Tick = rpc.Tick });
            }
            return JsonConvert.SerializeObject(result);
        }


        private string HandlePerceiveRequest(string character, string requestBody, List<RolePlayCharacterAsset> rpcs)
        {
            string[] events = Array.Empty<string>();
           
            if (!string.IsNullOrEmpty(requestBody))
            {
                events = JsonConvert.DeserializeObject<string[]>(requestBody);
                foreach (var ev in events)
                {
                    Name evName = null;
                    try
                    {
                        evName = Name.BuildName(ev);
                        if (character == Name.UNIVERSAL_STRING)
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
                        return JsonConvert.SerializeObject(string.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                    }
                }
            }
            else
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_EVENT_LIST);
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

        private string HandleResetRequest(ConcurrentDictionary<string, List<RolePlayCharacterAsset>> rpcs, out IntegratedAuthoringToolAsset iat, out WorldModelAsset wm)
        {
            wm = null;
            iat = IntegratedAuthoringToolAsset.LoadFromFile(this.IatFilePath);
            if (iat.m_worldModelSource != null)
            {
                wm = WorldModelAsset.LoadFromFile(iat.GetWorldModelSource().Source);
            }
            rpcs = new ConcurrentDictionary<string, List<RolePlayCharacterAsset>>();
            LoadCharacters(iat, DEFAULT_INSTANCE_ID, rpcs);
            return JsonConvert.SerializeObject("Scenario Loaded.");
        }

    }

    public class ServerEventArgs : EventArgs
    {
        public string Message { get; set; }
        public MessageTypes Type { get; set; }
    }

}

using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using EmotionalAppraisal.DTOs;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WellFormedNames;

namespace FAtiMAHTTPServer
{
    class Program
    {

        static string ERROR_UNKNOWN_GET_REQUEST = "ERROR: Unknown GET request was sent!";
        static string ERROR_UNKNOWN_POST_REQUEST = "ERROR: Unknown PUT request was sent!";
        static string ERROR_EMPTY_EVENT_LIST = "ERROR: The 'perceive' method requires a list of events as input!";
        static string ERROR_EXCEPTION_PERCEIVE = "ERROR: When perceiving event '{0}' the following exception occured: {1}!";
        static string ERROR_EXCEPTION_TRANSLATE = "ERROR: When translating action '{0}' the following exception occured: {1}!";
        static string ERROR_UNKOWN_SPEAK_ACTION = "ERROR: Could not find a dialogue for the following speak action: '{0}'!";

        static string SCENARIO_FILE =  "../../../Scenarios/WebServerScenario.iat";


        private static RolePlayCharacterAsset InitializeRPC(IntegratedAuthoringToolAsset iat)
        {
            var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
            return rpc;
        }

        static void Main(string[] args)
        {
            int port = 8080;

            //Loading the FAtiMA Assets (The scenario name must also be configurable)
            var iat = IntegratedAuthoringToolAsset.LoadFromFile(SCENARIO_FILE);
            var rpc = InitializeRPC(iat);

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
                    if(request.RawUrl.StartsWith("/" + APIMethod.DECIDE))
                    {
                        Console.WriteLine("New GET /"+ APIMethod.DECIDE + " request!");
                        var decisions = rpc.Decide();
                        responseJson = JsonConvert.SerializeObject(decisions);
                    }

                    else if(request.RawUrl.StartsWith("/" + APIMethod.EMOTIONALSTATE))
                    {
                        Console.WriteLine("New GET /"+ APIMethod.EMOTIONALSTATE + " request!");
                        var es = new EmotionalStateDTO { Mood = rpc.Mood, Emotions = rpc.GetAllActiveEmotions().ToList() };
                        responseJson = JsonConvert.SerializeObject(es);
                    }
                    else if (request.RawUrl.StartsWith("/" + APIMethod.RESET))
                    {
                        Console.WriteLine("New GET /" + APIMethod.RESET + " request!");
                        rpc = InitializeRPC(iat);
                        responseJson = JsonConvert.SerializeObject("RPC Reset");
                    }
                    else
                    {
                        Console.WriteLine(ERROR_UNKNOWN_GET_REQUEST);
                        responseJson = JsonConvert.SerializeObject(ERROR_UNKNOWN_GET_REQUEST);
                    }
                }else if (request.HttpMethod == "POST")
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
                                    evName = Name.BuildName(ev);
                                    rpc.Perceive(evName);
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(String.Format(ERROR_EXCEPTION_PERCEIVE,ev,ex.Message));
                                    responseJson = JsonConvert.SerializeObject(String.Format(ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(ERROR_EMPTY_EVENT_LIST);
                            responseJson = JsonConvert.SerializeObject(ERROR_EMPTY_EVENT_LIST);
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
                            var speakName = Name.BuildName(speakAction);
                            var actions = iat.GetDialogueActions(speakName.GetNTerm(1), speakName.GetNTerm(2), speakName.GetNTerm(3), speakName.GetNTerm(4));
                            if (actions.Any())
                            {
                                //TODO: return an array of utterances when more than one action is found
                                var processedUtt = rpc.ProcessWithBeliefs(actions.FirstOrDefault()?.Utterance);
                                responseJson = JsonConvert.SerializeObject(processedUtt);
                            }
                            else
                            {
                                Console.WriteLine(String.Format(ERROR_UNKOWN_SPEAK_ACTION, speakAction));
                                responseJson = JsonConvert.SerializeObject(String.Format(ERROR_UNKOWN_SPEAK_ACTION, speakAction));
                            }
                        }catch(Exception ex)
                        {
                            Console.WriteLine(String.Format(ERROR_EXCEPTION_TRANSLATE, speakAction, ex.Message));
                            responseJson = JsonConvert.SerializeObject(String.Format(ERROR_EXCEPTION_PERCEIVE, speakAction, ex.Message));
                        }
                    }
                    else
                    {
                        Console.WriteLine(ERROR_UNKNOWN_POST_REQUEST);
                        responseJson = JsonConvert.SerializeObject(ERROR_UNKNOWN_POST_REQUEST);
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

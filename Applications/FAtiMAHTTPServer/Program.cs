using IntegratedAuthoringTool;
using RolePlayCharacter;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FAtiMAHTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8080;

            //Loading the FAtiMA Assets (The scenario name must also be configurable)
            var iat = IntegratedAuthoringToolAsset.LoadFromFile("../../../Scenarios/WebServerScenario.iat");
            var currentState = IATConsts.INITIAL_DIALOGUE_STATE;
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

                if (context.Request.HttpMethod == "GET")
                {
                    if(context.Request.RawUrl == "/decide")
                    {
                        Console.WriteLine("New GET '/decide' request!");
                        
                    }
                    else if(context.Request.RawUrl == "/mood")
                    {
                        Console.WriteLine("New GET '/mood' request!");
                        responseJson = "{ \"mood\" : "  + rpc.Mood + "}";
                    }
                    else if (context.Request.RawUrl == "/strongest-emotion")
                    {
                        Console.WriteLine("New GET '/strongest-emotion' request!");
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

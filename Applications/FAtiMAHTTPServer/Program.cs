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
using WebServer;

namespace FAtiMAHTTPServer
{
    class Program
    {

        private static RolePlayCharacterAsset InitializeRPC(IntegratedAuthoringToolAsset iat)
        {
            var rpc = RolePlayCharacterAsset.LoadFromFile(iat.GetAllCharacterSources().FirstOrDefault().Source);
            rpc.LoadAssociatedAssets();
            iat.BindToRegistry(rpc.DynamicPropertiesRegistry);
            return rpc;
        }

        static void Main(string[] args)
        {
            string port = args[0];
            string file = args[1];

            HTTPFAtiMAServer server = new HTTPFAtiMAServer() { IatFilePath = file, Port = int.Parse(port) };
            server.Run();
        }
    }
}

using System;
using WebServer;

namespace FAtiMAHTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HTTPFAtiMAServer server = null;
            try
            {
                string port = args[0];
                string scenarioFile = args[1];
                string storageFile = args[2];

                server = new HTTPFAtiMAServer() { IatFilePath = scenarioFile, AssetFilePath = storageFile, Port = int.Parse(port) };

                server.OnServerEvent += ServerNotificationHandler;
                server.Run();
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                server?.Close(); 
            }
        }

        static void ServerNotificationHandler(object sender, ServerEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

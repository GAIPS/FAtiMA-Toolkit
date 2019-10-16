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
                string file = args[1];
                server = new HTTPFAtiMAServer() { IatFilePath = file, Port = int.Parse(port) };
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

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
                if (args.Length == 1) 
                { 
                    string httpPort = args[0];
                    server = new HTTPFAtiMAServer() { Port = int.Parse(httpPort)};
                }
                else if (args.Length == 2)
                {
                    string httpPort = args[0];
                    string httpsPort = args[1];
                    server = new HTTPFAtiMAServer() { Port = int.Parse(httpPort), HTTPSPort = int.Parse(httpsPort) };
                }
                else server = new HTTPFAtiMAServer();

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


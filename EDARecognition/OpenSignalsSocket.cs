using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

namespace EDARecognition {

    public class OpenSignalsSocket
    {
        public String host = "127.0.0.1";
        public Int32 port = 30000;

        public bool SocketReady { get; private set; }
        internal String input_buffer = "";
        TcpClient tcp_socket;
        NetworkStream net_stream;

        StreamWriter socket_writer;
        StreamReader socket_reader;

        private EDARecognitionAsset EDARecognitionAsset { get; set; }

        public Thread SocketThread { get; private set; }

        public OpenSignalsSocket(EDARecognitionAsset asset)
        {
            this.SocketReady = false;
            this.EDARecognitionAsset = asset;
            this.SetupSocket();
        }

        private void Update()
        {
            string received_data = this.ReadSocket();

            //writeSocket("Unity Echo");
            if (received_data != "")
            {
                // Do something with the received data,
                var splitData = received_data.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var messageType = splitData[0];

                if (messageType.Equals("EDA"))
                {
                    var eda = float.Parse(splitData[1], CultureInfo.InvariantCulture.NumberFormat);
                    this.EDARecognitionAsset.SetSkinConductanceLevel(eda);
                }
                else if (messageType.Equals("EDAPeakRate"))
                {
                    var edaPeekRate = float.Parse(splitData[1],CultureInfo.InvariantCulture.NumberFormat);
                    this.EDARecognitionAsset.SetSkinConductanceRate(edaPeekRate);
                }

            }
        }

        public void SetupSocket()
        {
            try
            {
                tcp_socket = new TcpClient(host, port);

                net_stream = tcp_socket.GetStream();
                socket_writer = new StreamWriter(net_stream);
                socket_reader = new StreamReader(net_stream);

                this.SocketReady = true;

                this.SocketThread = new Thread(() =>
                {
                    while (this.SocketReady)
                    {
                        this.Update();
                    }
                    this.CloseSocket();
                });

                this.SocketThread.Start();
            }
            catch (Exception e)
            {
                // Something went wrong
                Console.WriteLine("Socket error: " + e);
            }
        }

        public void WriteSocket(string line)
        {
            if (!this.SocketReady)
                return;

            line = line + "\r\n";
            socket_writer.Write(line);
            socket_writer.Flush();
        }

        public String ReadSocket()
        {
            if (!this.SocketReady)
                return "";

            if (net_stream.DataAvailable)
                return socket_reader.ReadLine();

            return "";
        }

        public void CloseSocket()
        {
            if (!this.SocketReady)
                return;

            socket_writer.Close();
            socket_reader.Close();
            tcp_socket.Close();
            this.SocketReady = false;
        }
    }
}
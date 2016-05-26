using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace LocalPrintScreen
{
    abstract class UdpDataClient
    {
        protected static IPAddress _serverIP;
        protected static int _serverPort;
        protected static IPEndPoint serverIPEndPoint;
        protected static Socket socket;


        public UdpDataClient(int Port,IPAddress serverIP)
        {
            _serverPort = Port;
            _serverIP = serverIP;
           
        }

        public void Stop()
        {
            if (socket != null)
            {
                socket.Close();
            } 
        }

    }
}

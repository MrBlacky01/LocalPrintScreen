using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LocalPrintScreen
{
    class TcpComandClient
    {
        Socket handler;
        int _serverPort;
        IPAddress _serverIP;
        
        public TcpComandClient(int serverPort, IPAddress serverIP)
        {
            _serverPort = serverPort;
            _serverIP = serverIP;
            Connect();
        }

        public void Stop()
        {
            handler.Disconnect(false);
            handler.Dispose();
            handler.Close();
        }

        private void Connect()
        {
            handler = new Socket(_serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // Соединяем сокет с удаленной точкой
            handler.Connect(_serverIP, _serverPort);
        }

        public void Send(string message)
        {
            try
            {
                byte[] data = new byte[message.Length];
                data = Encoding.UTF8.GetBytes(message);
                handler.Send(data);
            }
            catch(Exception exept)
            {
                MessageBox.Show(exept.Message);
            }
        }

        public  Task<byte[]> Receive()
        {
            byte[] data = new byte[2000];
            try
            {
                return Task.Run(()=>
                {
                    int countOfBytes = handler.Receive(data);
                    byte[] message = new byte[countOfBytes];
                    Array.Copy(data, message, message.Length);
                    return message;
                });           
            }
            catch(Exception exept)
            {
                MessageBox.Show(exept.ToString());
                return null;
            }
        }
    }
}

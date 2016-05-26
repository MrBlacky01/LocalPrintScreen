using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LocalPrintScreen
{
    class UdpVideoReciver : UdpDataClient
    {
        protected static int receiveDataLength;

        public UdpVideoReciver(int Port, IPAddress serverIP) : base(Port, serverIP)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverIPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            socket.Bind(serverIPEndPoint);
        }

        public static Task<byte[]> Receive()
        {
            try
            {

                byte[] data = new byte[65001];
                return Task.Run(() =>
                {
                    EndPoint Remote = (EndPoint)serverIPEndPoint;
                    receiveDataLength = socket.ReceiveFrom(data, ref Remote);
                    byte[] realData = new byte[receiveDataLength];
                    Array.Copy(data, realData, receiveDataLength);
                    return realData;
                });
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

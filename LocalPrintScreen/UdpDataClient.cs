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
    class UdpDataClient
    {
        private static IPAddress _serverIP;
        private static int _serverPort;
        private static int _clientPort ;
        private static IPEndPoint serverIPEndPoint;
        private static Socket server;
        private static IPEndPoint clientIPEndPoint;
        private static Socket client;
        private static int receiveDataLength;

        public UdpDataClient(int serverPort,int clientPort,IPAddress serverIP)
        {
            _serverPort = serverPort;
            _clientPort = clientPort;
            _serverIP = serverIP;
           
        }

        public void InitializeSendSocket()
        {
            serverIPEndPoint = new IPEndPoint(_serverIP, _serverPort);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void Stop()
        {
            if (server != null)
            {
                server.Close();
            } 
        }

        public void InitializeReceiveSocket()
        {
            clientIPEndPoint = new IPEndPoint(IPAddress.Any, _clientPort );
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(clientIPEndPoint);
        }

        public void Send(byte[] message,byte x, byte y)
        {
            try
            {
                int countOfBytes = 0;
                byte i = 0;

                while (countOfBytes < message.Length)
                {
                    byte[] temp;
                    if ((message.Length - countOfBytes) > 65001)
                    {
                        temp = new byte[65000];
                        temp[0] = (byte)(i + 1);
                    }
                    else
                    {
                        if (countOfBytes == 0)
                        {
                            temp = new byte[message.Length + 3];
                        }
                        else
                        {
                            temp = new byte[message.Length - countOfBytes + 1];
                        }
                        temp[0] = 255;
                    }    
                    if (i == 0)
                    {
                        temp[1] = x;
                        temp[2] = y;
                        Array.Copy(message, countOfBytes, temp, 3, temp.Length - 3);
                        countOfBytes += 64997;
                    }
                    else
                    {
                        Array.Copy(message, countOfBytes, temp, 1, temp.Length - 1);
                        countOfBytes += 64999;
                    }
                    i++;
                    server.SendTo(temp, temp.Length, SocketFlags.None, serverIPEndPoint);

                }
            }
            catch (Exception e)
            {
                server.Close();
                throw new Exception("End wor of UdpData client");
            }
        }

        
        public static Task<byte[]> Receive()
        {
            try
            {

                byte[] data = new byte[65001];
                return Task.Run(() =>
                {
                    EndPoint Remote = (EndPoint)clientIPEndPoint;
                    receiveDataLength = client.ReceiveFrom(data, ref Remote);
                    byte[] realData = new byte[receiveDataLength];
                    Array.Copy(data, realData, receiveDataLength);
                    return realData;
                });
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}

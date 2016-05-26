using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LocalPrintScreen
{
    class UdpVideoSender : UdpDataClient
    {
        public UdpVideoSender(int Port, IPAddress serverIP) : base(Port, serverIP)
        {
            serverIPEndPoint = new IPEndPoint(_serverIP, _serverPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void Send(byte[] message, byte x, byte y)
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
                    socket.SendTo(temp, temp.Length, SocketFlags.None, serverIPEndPoint);

                }
            }
            catch (Exception e)
            {
                socket.Dispose();
                socket.Close();
            }
        }
    }
}

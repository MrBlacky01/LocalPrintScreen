using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;


namespace LocalPrintScreen
{
    public partial class LocalPrintScreen : Form
    {
        public static Stopwatch timer = new Stopwatch();
        Thread tRec;
        Thread tForSendData;
        static Task tForPaintingCursor;
        Task tForReceiving;
        private static bool  endOfTranslation = false;
        static int i;
        public static int clientPort = 9050;
        public static int serverPort = 11000;
        public static int serverPortForVideo = 12000;
        static Graphics imageForPictureBox;
        static int PictureBoxHeignt, PictureBoxWidth;
        public static IPAddress serverIP;
        private static UdpDataClient server;
        private static TcpComandClient mainServer;
        private static TextBox tempTextBox;
        private static List<byte[]> ListOfDgrams = new List<byte[]>();
        private static Image Cursor;
        private static NumericUpDown CountOfCadrs, QualityOfCadr;
        private static TextBox textBoxChating;
        private static string Login;


        public LocalPrintScreen()
        {
            InitializeComponent();

            pictureBoxForReceiving.Image = new Bitmap(pictureBoxForReceiving.Width, pictureBoxForReceiving.Height);
            pictureBoxForReceiving.BackColor = Color.White;
            CountOfCadrs = numericUpDownForCadrsPerSecond;
            QualityOfCadr = numericUpDownForQuality;
            imageForPictureBox = pictureBoxForReceiving.CreateGraphics();
            PictureBoxWidth = pictureBoxForReceiving.Width;   //x
            PictureBoxHeignt = pictureBoxForReceiving.Height; //y   
            Cursor = Image.FromFile("cursor-16 (1).png");

            
            tempTextBox = textBox2;
            textBoxServerIP.Text = "127.0.0.1";
            textBoxForName.Text = "Black Dragon";
            textBoxForMessage.Text = "qwerty";
        
        }
        
        private void buttonStartTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = false;
            mainServer.Send("START:");
            server = new UdpDataClient(serverPortForVideo, clientPort, serverIP);
            server.InitializeSendSocket();
            tRec = new Thread(new ThreadStart(MakingScreens));
            tRec.Start();
        }

        private static void MakingScreens()
        {
            timer.Start();
            while (endOfTranslation == false)
            {
                if (timer.ElapsedMilliseconds > 100)
                {
                    Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    Graphics graphics = Graphics.FromImage(printscreen as Image);
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                   // printscreen.Save("printscreen" + i.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
 
                    byte[] b = WorkWithGrafic.VaryQualityLevel(printscreen,(int)QualityOfCadr.Value);

                    byte x = Convert.ToByte(MousePosition.X * 100 / Screen.PrimaryScreen.Bounds.Width) ;
                    byte y = Convert.ToByte(MousePosition.Y * 100 / Screen.PrimaryScreen.Bounds.Height); ;
                    server.Send(b,x,y);
                    i++;
                    timer.Restart();
                }
            }
        }

       

        public static void ReturnedData(Bitmap image)
        {
            //imageForPictureBox.DrawImage(image,0,0,x,y);

        } 

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
            
            bool check = IPAddress.TryParse(textBoxServerIP.Text,out serverIP);
            if ((check == false)&&(textBoxForName.Text == ""))
            {
                return;
            }
            Login = textBoxForName.Text;
            mainServer = new TcpComandClient(serverPort, serverIP);
            mainServer.Send("CONNECT:" + textBoxForName.Text);
            ReceiveResponseAsync();

        }

        private static async void ReceiveResponseAsync()
        {
            byte[] data ;
            try
            {
                while(true)
                {
                    data = await mainServer.Receive();
                    if (data != null)
                    {
                        string message = Encoding.UTF8.GetString(data);
                        AnalysisOfResponse(message);
                    }
                }
            }
            catch(Exception exept)
            {
                MessageBox.Show(exept.ToString());
            }
        }

        private static async void AnalysisOfResponse(string message)
        {
            int posOfDoubleDot = message.IndexOf(":");           
            string command = message.Substring(0, posOfDoubleDot);
            switch (command)
            { 
                case "MESSAGE":
                    {                       
                        tempTextBox.AppendText(message.Substring(posOfDoubleDot + 1, message.Length - posOfDoubleDot - 1) + "\r\n");
                    }
                    break;
                case "START":
                    {
                        server = new UdpDataClient(serverPortForVideo, clientPort, serverIP);
                        server.InitializeReceiveSocket();
                        ReceiveDataAsync();
                    }
                    break;
                case "DISCONNECT":
                    {

                    }
                    break;
                case "STOPSOUND":
                    {

                    }
                    break;
                case "RESUMESOUND":
                    {

                    }
                    break;
                case "EXIT":
                    {

                    }
                    break;
            }
            await Task.Delay(1);
        }

        private static async void ReceiveDataAsync()
        {
            byte[] data = new byte[65010];
            while(true)
            {
                data = await UdpDataClient.Receive();
                if (data != null)
                {
                    if (data.Length > 20)
                    {
                        MakeScreen(data);
                    }
                    
                   // tempTextBox.AppendText(data.Length.ToString()+" " +data[0].ToString()+"\r\n");
                }
            }
        }

        private static void MakeScreen(byte[] data)
        {
            try
            {
                ListOfDgrams.Add(data);
                if (data[0] == 255)
                {
                    
                    byte[] realData = new byte[LengthOfArraysInList(ListOfDgrams)];
                    int countOfData = 0;
                    Array.Copy(ListOfDgrams[0], 3, realData, 0, ListOfDgrams[0].Length - 3);
                    countOfData += ListOfDgrams[0].Length - 3;
                    for (int i = 1; i < ListOfDgrams.Count();i++)
                    {
                        Array.Copy(ListOfDgrams[i], 1, realData, countOfData, ListOfDgrams[i].Length - 1);
                        countOfData += ListOfDgrams[i].Length - 1; 
                    }
                
                    Image screen = WorkWithGrafic.ByteArrayToImage(realData);
                    imageForPictureBox.DrawImage(screen, 0, 0, PictureBoxWidth, PictureBoxHeignt);
                    Point a = new Point(PictureBoxWidth * ListOfDgrams[0][1] / 100, PictureBoxHeignt * ListOfDgrams[0][2] / 100);
                    tForPaintingCursor =  new Task(() => PaintCursor(a));
                    tForPaintingCursor.Start();
                    ListOfDgrams.Clear();
                }
               
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void PaintCursor(Point e)
        {
            imageForPictureBox.DrawImage(Cursor, e.X, e.Y, 16, 16);
        }


        private static int LengthOfArraysInList(List<byte[]> list)
        {
            int result = 0;
            foreach(byte[] array in list)
            {
                result += array.Length - 1;
            }
            return result - 2;
        }

        private void LocalPrintScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainServer != null)
            {
                mainServer.Send("EXIT:");
                mainServer.Stop();
            }
            if (server != null)
            {
                server.Stop();
            }
            
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                mainServer.Send("MESSAGE:"+ Login+": "+ textBoxForMessage.Text);
                textBoxForMessage.Clear();
            }
            finally
            {

            }
        }

        private void buttonFinishTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = true;
        }
    }
}

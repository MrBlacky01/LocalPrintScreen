using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;


namespace LocalPrintScreen
{
    public partial class LocalPrintScreen : Form
    {
        public static Stopwatch timer = new Stopwatch();

        Thread tRec;
        static Task tForPaintingCursor;


        private static bool  endOfTranslation = false;
        private static string Login;

        static int i;
        public static int clientPort = 9050;
        public static int serverPort = 11000;
        public static int serverPortForVideo = 12000;
        public static int countOfCadrs = 10;
        public static int qualityOfCadr = 20;

        static Graphics imageForPictureBox;
        static int PictureBoxHeignt, PictureBoxWidth;
        public static IPAddress serverIP;
        private static UdpDataClient server;
        private static TcpComandClient mainServer;
        
        private static List<byte[]> ListOfDgrams = new List<byte[]>();
        private static Image Cursorer;

        private static NumericUpDown CountOfCadrs, QualityOfCadr;
        private static Button buttonConnect, buttonSend, buttonTranslate, buttonFinish;
        private static TextBox tempTextBox,textBoxServer, textBoxLogin;

        


        public LocalPrintScreen()
        {
            InitializeComponent();

            pictureBoxForReceiving.Image = new Bitmap(pictureBoxForReceiving.Width, pictureBoxForReceiving.Height);
            pictureBoxForReceiving.BackColor = Color.White;

            CountOfCadrs = numericUpDownForCadrsPerSecond;
            QualityOfCadr = numericUpDownForQuality;
            buttonSend = buttonSendMessage;
            buttonConnect = buttonConnectToServer;
            buttonFinish = buttonFinishTranslation;
            buttonTranslate = buttonStartTranslation;

            imageForPictureBox = pictureBoxForReceiving.CreateGraphics();
            PictureBoxWidth = pictureBoxForReceiving.Width;   //x
            PictureBoxHeignt = pictureBoxForReceiving.Height; //y   
            Cursorer = Image.FromFile("cursor-16 (1).png");

            numericUpDownForCadrsPerSecond.Enabled = false;
            numericUpDownForQuality.Enabled = false;
            buttonFinishTranslation.Enabled = false;
            buttonSendMessage.Enabled = false;
            buttonStartTranslation.Enabled = false;
            textBoxServer = textBoxServerIP;
            textBoxLogin = textBoxForName;
            
            tempTextBox = textBox2;
            textBoxServerIP.Text = "127.0.0.1";
            textBoxForName.Text = "Black Dragon";
            textBoxForMessage.Text = "qwerty";
        
        }
        
        private static void MakeConnectVisible()
        {
            buttonConnect.Enabled = false;
            buttonSend.Enabled = true;
            buttonTranslate.Enabled = true;
            textBoxLogin.Enabled = false;
            textBoxServer.Enabled = false;
          
        }

        private void buttonStartTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = false;
            mainServer.Send("START:");
            buttonFinish.Enabled = true;
            numericUpDownForCadrsPerSecond.Enabled = true;
            numericUpDownForQuality.Enabled = true;
            server = new UdpVideoSender(serverPortForVideo, serverIP);//UdpDataClient(serverPortForVideo, clientPort, serverIP);
            tRec = new Thread(new ThreadStart(MakingScreens));
            tRec.Start();
        }

        private static void MakingScreens()
        {
            timer.Start();
            while (endOfTranslation == false)
            {
                if (timer.ElapsedMilliseconds > (1000 / countOfCadrs))
                {
                    Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    Graphics graphics = Graphics.FromImage(printscreen as Image);
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                   // printscreen.Save("printscreen" + i.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
 
                    byte[] b = WorkWithGrafic.VaryQualityLevel(printscreen,qualityOfCadr/2);

                    byte x = Convert.ToByte(MousePosition.X * 100 / Screen.PrimaryScreen.Bounds.Width) ;
                    byte y = Convert.ToByte(MousePosition.Y * 100 / Screen.PrimaryScreen.Bounds.Height); ;
                    (server as UdpVideoSender).Send(b,x,y);
                    i++;
                    timer.Restart();
                }
            }
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
                case "CONNECT":
                    {
                        MakeConnectVisible();
                    }
                    break;
                case "MESSAGE":
                    {                       
                        tempTextBox.AppendText(message.Substring(posOfDoubleDot + 1, message.Length - posOfDoubleDot - 1) + "\r\n");
                    }
                    break;
                case "START":
                    {
                        buttonTranslate.Enabled = false;
                        server = new UdpVideoReciver(clientPort, serverIP);//UdpDataClient(serverPortForVideo, clientPort, serverIP);
                        ReceiveDataAsync();
                    }
                    break;
                case "DISCONNECT":
                    {
                        buttonTranslate.Enabled = true;
                        server.Stop();
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
                data = await UdpVideoReciver.Receive();
                if (data != null)
                {
                    if (data.Length > 20)
                    {
                        MakeScreen(data);
                    }
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

        private void numericUpDownForCadrsPerSecond_ValueChanged(object sender, EventArgs e)
        {
            countOfCadrs = (int)numericUpDownForCadrsPerSecond.Value;
        }

        private void numericUpDownForQuality_ValueChanged(object sender, EventArgs e)
        {
            qualityOfCadr = (int)numericUpDownForQuality.Value;
        }

        private static void PaintCursor(Point e)
        {
            imageForPictureBox.DrawImage(Cursorer, e.X, e.Y, 16, 16);
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
            mainServer.Send("DISCONNECT:");
            server.Stop();
        }
    }
}

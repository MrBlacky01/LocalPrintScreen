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
        Task tForReceiving;
        private static bool  endOfTranslation = false;
        static int i;
        public static int clientPort = 9050;
        public static int serverPort = 11000;
        static Graphics imageForPictureBox;
        static int x, y;
        public static IPAddress serverIP;
        private static UdpDataClient server;
        private static TextBox tempTextBox;

        public LocalPrintScreen()
        {
            InitializeComponent();

            pictureBoxForReceiving.Image = new Bitmap(pictureBoxForReceiving.Width, pictureBoxForReceiving.Height);
            pictureBoxForReceiving.BackColor = Color.White;
            imageForPictureBox = pictureBoxForReceiving.CreateGraphics();
            //imageForPictureBox = Graphics.FromImage(pictureBoxForReceiving.Image);
            x = pictureBoxForReceiving.Width;
            y = pictureBoxForReceiving.Height;
            tempTextBox = textBox2;
            //buttonFinishTranslation.Click += buttonFinishTranslation_Click();
            //tRec = new Thread(new ThreadStart(buttonFinishTranslation_Click));
            //tRec.Start();
        }
        
        private void buttonStartTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = false;
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
                    
                    
                    byte[] b = VaryQualityLevel(printscreen);

                   // ImageConverter converter = new ImageConverter();
                    //Image a = BitmapToImage(printscreen);
                    //byte[] b = ((byte[])converter.ConvertTo(printscreen, typeof(byte[])));
                    //MessageBox.Show(b.Count().ToString());
                    //imageForPictureBox.DrawImage(printscreen, 0, 0, x, y);
                    //Send(b);
                    server.Send(b);
                    //ReturnedData(printscreen);
                    i++;
                    timer.Restart();
                }
            }
        }

        private static Image BitmapToImage(Bitmap map)
        {
            Stream imageStream = new MemoryStream();
            map.Save(imageStream, ImageFormat.Png);
            return Image.FromStream(imageStream);
        }

        private static byte[] ImageToByteArray(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        } 

        private static Image ByteArrayToImage(byte[] array)
        {
            return (Image)((new ImageConverter()).ConvertFrom(array));
        }

        private static byte[] VaryQualityLevel(Bitmap image)
        {
            // Get a bitmap.
            Bitmap bmp1 = image;
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
  
            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            long a = 15;
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, a);
            myEncoderParameters.Param[0] = myEncoderParameter;
            Stream b = new MemoryStream();

            //bmp1.Save("TestPhotoQualityFifty" + i.ToString() + ".jpg", jpgEncoder, myEncoderParameters);

            bmp1.Save(b, jpgEncoder, myEncoderParameters);

            byte[] value = new byte[b.Length];
            b.Write(value, 0, (int)b.Length);
            return value;

        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void ReturnedData(Bitmap image)
        {
            //imageForPictureBox.DrawImage(image,0,0,x,y);

        } 

        public static void Send(byte[] message)
        {

            IPEndPoint ipEndPoint = new IPEndPoint(serverIP, serverPort);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                int countOfBytes = 0;
                byte i = 0;

                while (countOfBytes < message.Length)
                {
                    byte[] temp;
                    if ((message.Length - countOfBytes) > 60000)
                    {
                        temp = new byte[60001];
                    }
                    else
                    {
                        temp = new byte[message.Length - countOfBytes + 1];
                    }

                    temp[0] = i;
                    Array.Copy(message, countOfBytes, temp, 1, temp.Length - 1);

                    countOfBytes += 60000;
                    i++;
                    server.SendTo(temp,temp.Length,SocketFlags.None,ipEndPoint);
                    
                    
                }
            }
            catch(Exception e)
            {
                server.Close();
                MessageBox.Show(e.Message);
            }
        }

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
             IPAddress.TryParse(textBoxserverIP.Text,out serverIP);
            byte[] messageForConnect = new byte[1];
            messageForConnect[0] = 1;
            if (serverIP != null)
            {
                server = new UdpDataClient(serverPort, clientPort, serverIP);
                server.Send(messageForConnect);
                ReceiveDataAsync();
       
            }
            //Send(messageForConnect);  
        }

        private async void ReceiveDataAsync()
        {
            byte[] data = new byte[65010];
            while(true)
            {
                data = await UdpDataClient.Receive();
                if (data != null)
                {
                    tempTextBox.AppendText(data.Length.ToString()+" " +data[0].ToString()+"\r\n");
                }
            }
        }

        private void buttonFinishTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = true;
        }
    }
}

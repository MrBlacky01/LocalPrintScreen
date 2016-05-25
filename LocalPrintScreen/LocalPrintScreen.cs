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
        static Graphics imageForPictureBox;
        static int PictureBoxHeignt, PictureBoxWidth;
        public static IPAddress serverIP;
        private static UdpDataClient server;
        private static TcpComandClient mainServer;
        private static TextBox tempTextBox;
        private static List<byte[]> ListOfDgrams = new List<byte[]>();
        private static Image Cursor;

        public LocalPrintScreen()
        {
            InitializeComponent();

            pictureBoxForReceiving.Image = new Bitmap(pictureBoxForReceiving.Width, pictureBoxForReceiving.Height);
            pictureBoxForReceiving.BackColor = Color.White;
            imageForPictureBox = pictureBoxForReceiving.CreateGraphics();
            PictureBoxWidth = pictureBoxForReceiving.Width;   //x
            PictureBoxHeignt = pictureBoxForReceiving.Height; //y   
            Cursor = Image.FromFile("cursor-16 (1).png");
            
            tempTextBox = textBox2;
            textBoxServerIP.Text = "127.0.0.1";
        
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
                    byte x = Convert.ToByte(MousePosition.X * 100 / Screen.PrimaryScreen.Bounds.Width) ;
                    byte y = Convert.ToByte(MousePosition.Y * 100 / Screen.PrimaryScreen.Bounds.Height); ;
                    server.Send(b,x,y);
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
            long a = 50;
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, a);
            myEncoderParameters.Param[0] = myEncoderParameter;
            MemoryStream b = new MemoryStream();

            //bmp1.Save("TestPhotoQualityFifty" + i.ToString() + ".jpg", jpgEncoder, myEncoderParameters);

            bmp1.Save(b, jpgEncoder, myEncoderParameters);

            byte[] value = new byte[b.Length];
            value = b.ToArray();
    
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

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
            
            bool check = IPAddress.TryParse(textBoxServerIP.Text,out serverIP);
            if ((check == false)&&(textBoxForName.Text == ""))
            {
                return;
            }
            mainServer = new TcpComandClient(serverPort, serverIP);
            mainServer.Send("CONNECT: " + textBoxForName.Text);
            string response = Encoding.UTF8.GetString(mainServer.Receive());
            if (response != "")
            {
                MessageBox.Show(response);
            }
            /*byte[] messageForConnect = new byte[1];
            messageForConnect[0] = 1;
            if (serverIP != null)
            {
                server = new UdpDataClient(serverPort, clientPort, serverIP);
                server.Send(messageForConnect,0,0);
                ReceiveDataAsync();
                
       
            } */
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
                    
                    tempTextBox.AppendText(data.Length.ToString()+" " +data[0].ToString()+"\r\n");
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
                
                    Image screen = ByteArrayToImage(realData);
                    imageForPictureBox.DrawImage(screen, 0, 0, PictureBoxWidth, PictureBoxHeignt);
                    // imageForPictureBox.DrawEllipse(new Pen(Color.Yellow, 3), PictureBoxWidth * ListOfDgrams[0][1] / 100, PictureBoxHeignt * ListOfDgrams[0][2] / 100, 20, 20);
                    Point a = new Point(PictureBoxWidth * ListOfDgrams[0][1] / 100, PictureBoxHeignt * ListOfDgrams[0][2] / 100);
                    tForPaintingCursor =  new Task(() => PaintCursor(a));
                    tForPaintingCursor.Start();
                    //imageForPictureBox.DrawImage(Cursor, PictureBoxWidth * ListOfDgrams[0][1] / 100, PictureBoxHeignt * ListOfDgrams[0][2] / 100, 16, 16);
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
        private void buttonFinishTranslation_Click(object sender, EventArgs e)
        {
            endOfTranslation = true;
        }
    }
}

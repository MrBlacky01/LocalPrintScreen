using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace LocalPrintScreen
{
    static class WorkWithGrafic
    {
        public static Image BitmapToImage(Bitmap map)
        {
            Stream imageStream = new MemoryStream();
            map.Save(imageStream, ImageFormat.Png);
            return Image.FromStream(imageStream);
        }

        public static byte[] ImageToByteArray(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image ByteArrayToImage(byte[] array)
        {
            return (Image)((new ImageConverter()).ConvertFrom(array));
        }

        public static byte[] VaryQualityLevel(Bitmap image, int parametr)
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
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, parametr);
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
    }
}

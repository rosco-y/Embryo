using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Embyo
{
    internal class cDMC
    {
        private Bitmap img;
        Dictionary<String, int> _colorsDictionary;

        public cDMC(Bitmap img)
        {
            _colorsDictionary = new Dictionary<String, int>();

            this.img = img;
        }


        void loadColorsDictionary()
        {
            _colorsDictionary = new Dictionary<string, int>();

            using (var flossStream = new FileStream("C:\\PROJECTS\\Embryo\\Embyo\\Embyo\\floss2hex.dat", FileMode.Open))
            using (var flossReader = new StreamReader(flossStream))
            {
                while (!flossReader.EndOfStream)
                {
                    var line = flossReader.ReadLine();
                    var splitLine = line.Split(' ');
                    _colorsDictionary.Add(splitLine[1], int.Parse(splitLine[0]));
                }
            }
        }




        public void AddPixel(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;


            _pixelColor = Color.FromArgb(r, g, b);
            _hexValue = $"{_pixelColor.R.ToString("X2")}{_pixelColor.G.ToString("X2")}{_pixelColor.B.ToString("X2")}";
            //if (_colorsDictionary.ContainsKey(_hexValue))
            //{
            //    _colorsDictionary[_hexValue]++;
            //}
            //else
            //{
            //    _colorsDictionary.Add(_hexValue, 0);
            //    _hashValue = (int)r + (int)g + (int)b;

            //    _dmc = Convert(r, g, b);
            //    _colors.Add(_hexValue);
            //    _cRGBList.Add((cRGB)this.MemberwiseClone());
            //}

        }

        private void CountColors(Bitmap picBoxPhoto)
        {
            var cnt = new HashSet<System.Drawing.Color>();

            Bitmap bmp = picBoxPhoto;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            // Copy the RGB values into the array.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            IntPtr ptr = bmpData.Scan0;
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int count = 0;
            int stride = bmpData.Stride;
            
            for (int column = 0; column < bmpData.Height; column++)
            {
                for (int row = 0; row < bmpData.Width; row++)
                {
                    b[count] = rgbValues[(column * stride) + (row * 3)];
                    g[count] = rgbValues[(column * stride) + (row * 3) + 1];
                    r[count] = rgbValues[(column * stride) + (row * 3) + 2];
                    var color = new
                    _rgb.AddPixel(r[count], g[count], b[count]);                    
                    count++;
                }
            }

        }
    }
}
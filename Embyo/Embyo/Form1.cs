using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mime;

using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Embyo
{
    public partial class Embryo : Form
    {

        cRGB _rgb = new cRGB();
        public Embryo()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog(this) == DialogResult.OK)
            {
               
                Cursor = Cursors.WaitCursor;
                Bitmap Displayimage = new Bitmap(dlgOpenFile.FileName);

                picBoxPhoto.Dock = DockStyle.Fill;
                picBoxPhoto.Image = Displayimage;
                Bitmap img = new Bitmap(dlgOpenFile.FileName);
                cDMC = new cDMC(img);
                CountColors(img);
                _rgb.Statistics();
                Cursor = Cursors.Default;
            }
        }

        private void CountColors(Bitmap picBoxPhoto)
        {
            var cnt = new HashSet<System.Drawing.Color>();

            //Bitmap bmp = new Bitmap(picBoxPhoto.Image);
            Bitmap bmp = picBoxPhoto;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            byte[] r = new byte[bytes / 3];
            byte[] g = new byte[bytes / 3];
            byte[] b = new byte[bytes / 3];
            List<cRGB> colors = new List<cRGB>();
            // Copy the RGB values into the array.
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
                    _rgb.AddPixel(r[count], g[count], b[count]);
                    count++;
                }
            }

        }

        private void Embryo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = true;
                Properties.Settings.Default.Minimised = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Location = Location;
                Properties.Settings.Default.Size = Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = false;
            }
            else
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = true;
            }
            Properties.Settings.Default.Save();
        }

        //private void Embryo_Load(object sender, EventArgs e)
        //{
        //    if (Properties.Settings.Default.Maximised)
        //    {
        //        Location = Properties.Settings.Default.Location;
        //        WindowState = FormWindowState.Maximized;
        //        Size = Properties.Settings.Default.Size;
        //    }
        //    else if (Properties.Settings.Default.Minimised)
        //    {
        //        Location = Properties.Settings.Default.Location;
        //        WindowState = FormWindowState.Minimized;
        //        Size = Properties.Settings.Default.Size;
        //    }
        //    else
        //    {
        //        Location = Properties.Settings.Default.Location;
        //        Size = Properties.Settings.Default.Size;
        //    }
        //}

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReport report = new frmReport();

        }
    }
}

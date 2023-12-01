using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embyo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog(this) == DialogResult.OK)
            {
                Bitmap image = new Bitmap(dlgOpenFile.FileName);
                picBoxPhoto.Dock = DockStyle.Fill;
                picBoxPhoto.Image = image;
            }
        }
    }
}

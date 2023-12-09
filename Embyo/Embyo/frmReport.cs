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
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        public void AddItem(string hexValue, int Count)
        {
            string item = $"{hexValue}, {Count}";
            lstOutput.Items.Add(item);
        }
      
        public void AddString(string text)
        {
            lstOutput.Items.Add(text);  
        }
    }
}

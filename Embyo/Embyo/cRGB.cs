using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.IO;
using System.Reflection;
namespace Embyo
{
    internal class cRGB : IComparable<cRGB>
    {
        byte _r;
        byte _g;
        byte _b;
        int _hashValue;
        Color _pixelColor;
        string _hexValue;
        HashSet<string> _colors;
        Dictionary<int, string> _colorsDictionary;

        List<cRGB> _cRGBList;
        string _dmc;

        public cRGB()
        {
            _colors = new HashSet<string>();
            _cRGBList = new List<cRGB>();
            loadColorsDictionary();
        }

        public void loadColorsDictionary()
        {
            _colorsDictionary = new Dictionary<int, string>();

            using (var flossStream = new FileStream("C:\\PROJECTS\\Embryo\\Embyo\\Embyo\\floss2hex.dat", FileMode.Open))
            using (var flossReader = new StreamReader(flossStream))
            {
                while (!flossReader.EndOfStream)
                {
                    var line = flossReader.ReadLine();
                    var splitLine = line.Split(' ');
                    _colorsDictionary.Add(int.Parse(splitLine[0]), splitLine[1]);
                }
            }
        }

        public void AddPixel(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;


            //_pixelColor = Color.FromArgb(r, g, b);
            //_hexValue = $"{_pixelColor.R.ToString("X2")}{_pixelColor.G.ToString("X2")}{_pixelColor.B.ToString("X2")}";
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

        private string Convert(byte r, byte g, byte b)
        {
            Color colorRGB = Color.FromArgb(r, g, b);
            int dmc = (colorRGB.R / 3) * 16 + (colorRGB.G / 3) * 4 + (colorRGB.B / 3);
            return dmc.ToString();
        }

        public override string ToString()
        {
            return DMC;
        }

        public String DMC
        {
            get { return _dmc; }
        }

        public int ColorCount
        {
            get { return _colors.Count; }
        }

        int _colorCount;
        string _mode;
        string _max;
        public async void Statistics()
        {
            _colorCount = _colorsDictionary.Keys.Count;
            string[] hashValues = _colors.ToArray();
            Array.Sort(hashValues);
            _mode = hashValues[hashValues.Length / 2];
            _max = hashValues[hashValues.Length - 1];
            await Report();
        }

        public async Task Report()
        {
            frmReport _reportForm = null;
            DateTime startReport = DateTime.Now;
            await Task.Run(() =>
            {
                _reportForm = new frmReport();

                _reportForm.AddString($"Colors = {_colorCount}, Mode = {_mode}, Max = {_max}.");

                foreach (var c in _cRGBList)
                {

                    String lstItem = $"HexCode: {c.HexValue}, DMC Code: {_dmc}";
                    _reportForm.AddString(lstItem);
                }
            });
            DateTime endReport = DateTime.Now;
            var reportTime = endReport - startReport;
            _reportForm.AddString($"Report Generation Time: {reportTime}");

            _reportForm.Show();
        }

        public int CompareTo(cRGB other)
        {
            if (this._hashValue < other._hashValue)
            {
                return -1;
            }
            else
            { }
            if (this._hashValue == other._hashValue)
                return 0;
            else
                return 1;
        }

        public string HexValue
        {
            get { return _hexValue; }
        }
    }
}

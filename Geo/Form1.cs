using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double x1 = 150;
        double y1 = 200;

        double x2 = 300;
        double y2 = 50;

        double x3 = 450;
        double y3 = 200;

        UInt32 color_1 = 0xFFcc1343; 
        UInt32 color_2 = 0xFFeae817; 
        UInt32 color_3 = 0xFF92eeaf; 

        Bitmap picture;

        private void button1_Click(object sender, EventArgs e)
        {
            picture = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Draw();
            pictureBox1.Image = picture;
        }

        public void Draw()
        {
            for (int y = 0; y < pictureBox1.Height; y++)
                for (int x = 0; x < pictureBox1.Width; x++)
                    picture.SetPixel(x, y, Color.FromArgb((int)Get_pixel_color(x, y)));
        }

        public UInt32 Get_pixel_color(int x, int y)
        {
            UInt32 pix_val;

            double k1, k2, k3;
            pix_val = 0xFFFFFFFF;

            k1 = ((y2 - y3) * ((double)(x) - x3) + (x3 - x2) * ((double)(y) - y3)) /
            ((y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3));
            k2 = ((y3 - y1) * ((double)(x) - x3) + (x1 - x3) * ((double)(y) - y3)) /
                ((y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3));

            k3 = 1 - k1 - k2;

            if (k1 >= 0 && k1 <= 1 && k2 >= 0 && k2 <= 1 && k3 >= 0 && k3 <= 1)
            {
                pix_val = (UInt32)0xFF000000 |
                    ((UInt32)(k1 * ((color_1 & 0x00FF0000) >> 16) + k2 * ((color_2 & 0x00FF0000) >> 16) + k3 * ((color_3 & 0x00FF0000) >> 16)) << 16) |
                    ((UInt32)(k1 * ((color_1 & 0x0000FF00) >> 8) + k2 * ((color_2 & 0x0000FF00) >> 8) + k3 * ((color_3 & 0x0000FF00) >> 8)) << 8) |
                    (UInt32)(k1 * (color_1 & 0x000000FF) + k2 * (color_2 & 0x000000FF) + k3 * (color_3 & 0x000000FF));
            }

            return pix_val;
        }
    }
}

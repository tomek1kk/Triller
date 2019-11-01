using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Triller
{
    public partial class Triller : Form
    {

        List<Triangle> triangles = new List<Triangle>();
        public DrawSettings settings;
        public Bitmap bitmap;
        public Bitmap normalBitmap;
        bool draw = false;
        const int N = 6;
        const int M = 8;
        

        public Triller()
        {
            InitializeComponent();
            HelperFunctions.GenerateTriangles(triangles, panel1.Width, panel1.Height, M, N);
        }
       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            settings = new DrawSettings(this);
            Graphics g = e.Graphics;

            foreach (var t in triangles)
            {
                if (draw == true)
                    HelperFunctions.FillPolygon(t.Points, g, this, t);
                //t.Render(g, Pens.Black);
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var bitmapp = new Bitmap(dlg.FileName);
                    pictureBox1.Image = bitmapp;
                    bitmap = bitmapp;
                }
            }

        }

        private void panel6_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            panel6.BackColor = colorDialog1.Color;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton4.Checked == true)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var bitmap = new Bitmap(dlg.FileName);
                    pictureBox2.Image = bitmap;
                    normalBitmap = bitmap;
                }
            }
        }
        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            pictureBox3.BackColor = colorDialog2.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            draw = true;
            panel1.Invalidate();
        }

    }
}

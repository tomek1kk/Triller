using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Triller
{
    public partial class Triller : Form
    {
        DirectBitmap directBitmap;
        List<Triangle> triangles = new List<Triangle>();
        List<(Triangle, int)> trianglesToMove;
        bool moving = false;
        public DrawSettings settings;
        public Bitmap bitmap;
        public Bitmap normalBitmap;
        bool draw = false;
        const int N = 6;
        const int M = 8;
        

        public Triller()
        {
            InitializeComponent();
            directBitmap = new DirectBitmap(panel1.Width, panel1.Height);
            panel1.BackgroundImage = directBitmap.Bitmap;
            HelperFunctions.GenerateTriangles(triangles, panel1.Width, panel1.Height, M, N);
        }
       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            Console.WriteLine("Moving: " + moving);



            foreach (var t in triangles)
                t.Render(g, Pens.Black);




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
            moving = false;
            draw = true;
            RefreshScreen();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            trianglesToMove = new List<(Triangle,int)>();

            foreach (Triangle t in triangles)
            {
                if (HelperFunctions.InArea(e.Location, t.A, 10))
                    trianglesToMove.Add((t, 0));
                if (HelperFunctions.InArea(e.Location, t.B, 10))
                    trianglesToMove.Add((t, 1));
                if (HelperFunctions.InArea(e.Location, t.C, 10))
                    trianglesToMove.Add((t, 2));
            }

            if (trianglesToMove.Count > 0)
            {
                moving = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving == true)
            {
                foreach (var t in trianglesToMove)
                {
                    if (t.Item2 == 0) // move point A
                        t.Item1.A = e.Location;
                    if (t.Item2 == 1) // move point B
                        t.Item1.B = e.Location;
                    if (t.Item2 == 2) // move point C
                        t.Item1.C = e.Location;

                }
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = true;
            moving = false;
            directBitmap = new DirectBitmap(panel1.Width, panel1.Height);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            settings = new DrawSettings(this);
            foreach (var t in triangles)
            {
                if (moving == false && draw == true)
                {
                    HelperFunctions.FillPolygon(t.Points, this, t, directBitmap);

                }
            }
            panel1.BackgroundImage = directBitmap.Bitmap;
            panel1.Invalidate();
        }
    }
}

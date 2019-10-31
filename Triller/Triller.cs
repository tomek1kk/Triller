using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Triller
{
    public partial class Triller : Form
    {

        List<Triangle> triangles = new List<Triangle>();
        const int N = 6;
        const int M = 8;

        public Triller()
        {
            InitializeComponent();
            GenerateTriangles();
        }

        private void GenerateTriangles()
        {
            var triangleA = panel1.Width / M;
            var triangleB = panel1.Height / N;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    triangles.Add(new Triangle
                    {
                        A = new Point(j * triangleA, i * triangleB),
                        B = new Point((j + 1) * triangleA, i * triangleB),
                        C = new Point(j * triangleA, (i + 1) * triangleB)
                    });
                    triangles.Add(new Triangle
                    {
                        A = new Point((j + 1) * triangleA, i * triangleB),
                        B = new Point(j * triangleA, (i + 1) * triangleB),
                        C = new Point((j + 1) * triangleA, (i + 1) * triangleB)
                    });
                }
            }
        }

        private void FillPolygon(List<Point> points, Brush color, Graphics g)
        {

            List<Point> pointsSorted = new List<Point>(points);
            pointsSorted.Sort((p1, p2) => p1.Y > p2.Y ? 1 : -1);
            List<int> ind = new List<int>();
            for (int i = 0; i < pointsSorted.Count; i++)
                ind.Add(points.FindIndex(p => p == pointsSorted[i]));

            int ymin = pointsSorted[0].Y;
            int ymax = pointsSorted[pointsSorted.Count - 1].Y;

            List<Edge> AET = new List<Edge>();

            for (int y = ymin; y <= ymax; y++)
            {
                for (int k = 0; k < points.Count; k++)
                {
                    if (points[ind[k]].Y == y - 1)
                    {
                        if (points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1].Y > points[ind[k]].Y)
                        {
                            AET.Add(new Edge
                            {
                                A = points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1],
                                B = points[ind[k]]
                            });
                        }
                        else
                        {
                            AET.Remove(AET.Find(e => e.A == points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1] && e.B == points[ind[k]]));
                        }
                        if (points[(ind[k] + 1) % points.Count].Y > points[ind[k]].Y)
                        {
                            AET.Add(new Edge
                            {
                                A = points[(ind[k] + 1) % points.Count],
                                B = points[ind[k]]
                            });
                        }
                        else
                        {
                            AET.Remove(AET.Find(e => e.A == points[(ind[k] + 1) % points.Count] && e.B == points[ind[k]]));
                        }
                    }
                }
                AET.Sort((e1, e2) => e1.GetX(y) > e2.GetX(y) ? 1 : -1);
                for (int j = 0; j < AET.Count / 2; j++)
                {
                    for (int p = AET[2 * j + 1].GetX(y); p >= AET[2 * j].GetX(y); p--)
                        g.FillRectangle(color, p, y, 1, 1);
                }
            }


        }

        private void Triller_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush b = Brushes.Red;
            foreach (var t in triangles)
            {

                //FillPolygon(t.Points, b, g);
                t.Render(g, Pens.Black);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

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
                    pictureBox2.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            pictureBox3.BackColor = colorDialog2.Color;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Triller_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var t in triangles)
            {
                Console.WriteLine(t.A + "    " + t.B + "     " + t.C);
                t.Render(g, Pens.Black);
            }
        }
    }
}

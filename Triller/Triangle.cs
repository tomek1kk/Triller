using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Triller
{
    public class Triangle
    {

        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }

        public Color ColorA { get; set; }
        public Color ColorB { get; set; }
        public Color ColorC { get; set; }

        public Color ColorAN { get; set; }
        public Color ColorBN { get; set; }
        public Color ColorCN { get; set; }

        public double kd, ks;
        public int m;

        public bool colors = false;
        public bool normalColors = false;

        public List<Color> interpolationMap;

        public void SetCoefficients()
        {
            Random r = new Random();
            
            kd = (double)(r.Next() % 101) / 100;
            ks = (double)(r.Next() % 101) / 100;
            m = r.Next() % 100 + 1;
        }

        public List<Point> Points
        {
            get
            {
                List<Point> points = new List<Point>();
                points.Add(A);
                points.Add(B);
                points.Add(C);
                return points;
            }
        }

        public void Render(Graphics g, Pen color)
        {
            g.DrawPolygon(color, Points.ToArray());
        }

        public Color GetInterpolationPixel(int x, int y, Color ColorA, Color ColorB, Color ColorC)
        {
            int dA = (int)Math.Sqrt((A.X - x) * (A.X - x) + (A.Y - y) * (A.Y - y));
            int dB = (int)Math.Sqrt((B.X - x) * (B.X - x) + (B.Y - y) * (B.Y - y));
            int dC = (int)Math.Sqrt((C.X - x) * (C.X - x) + (C.Y - y) * (C.Y - y));

            if (dA == 0)
                return ColorA;
            if (dB == 0)
                return ColorB;
            if (dC == 0)
                return ColorC;

            double wA = 1 / (double)dA;
            double wB = 1 / (double)dB;
            double wC = 1 / (double)dC;

            int R = (int)((wA * ColorA.R + wB * ColorB.R + wC * ColorC.R) / (wA + wB + wC)); 
            int G = (int)((wA * ColorA.G + wB * ColorB.G + wC * ColorC.G) / (wA + wB + wC)); 
            int Bl = (int)((wA * ColorA.B + wB * ColorB.B + wC * ColorC.B) / (wA + wB + wC));

            return Color.FromArgb(255, R, G, Bl);
        }

        public void SetColors(Color c1, Color c2, Color c3)
        {
            ColorA = c1;
            ColorB = c2;
            ColorC = c3;
            colors = true;
        }

        public void SetNormalColors(Color c1, Color c2, Color c3)
        {
            ColorAN = c1;
            ColorBN = c2;
            ColorCN = c3;
            normalColors = true;
        }

    }
}

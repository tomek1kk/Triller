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

        public double kd, ks;
        public int m;

        public void SetCoefficients()
        {
            Random r = new Random();
            int m;
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

        

    }
}

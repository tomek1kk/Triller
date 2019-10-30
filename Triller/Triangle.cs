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

        public void Render(Graphics g, Pen color)
        {
            Point[] points = new Point[3];
            points[0] = A;
            points[1] = B;
            points[2] = C;
            g.DrawPolygon(color, points);
        }

    }
}

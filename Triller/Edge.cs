using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller
{
    public class Edge
    {
        public Point A { get; set; }
        public Point B { get; set; }

        public int ReversedM
        {
            get
            {
                return Math.Abs(A.X - B.X) / Math.Abs(A.Y - B.Y);
            }
        }

        public int YMax
        {
            get
            {
                return A.Y > B.Y ? A.Y : B.Y;
            }
        }

        public int GetX(int y)
        {
            return ((y - A.Y) * (B.X - A.X) / (B.Y - A.Y)) + A.X;
        }

    }
}

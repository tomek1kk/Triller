using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller
{
    public class MyVector
    {
        public MyVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MyVector()
        {

        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static double MyCos(MyVector v1, MyVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
    }
}

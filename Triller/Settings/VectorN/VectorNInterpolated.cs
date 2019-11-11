using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.VectorN
{
    public class VectorNInterpolated : IVectorN
    {
        private Triangle t;
        private int x;
        private int y;

        public VectorNInterpolated(int x, int y, Triangle t)
        {
            this.x = x;
            this.y = y;
            this.t = t;
        }

        public MyVector N
        {
            get
            {
                var pixel = t.GetInterpolationPixel(x, y, Triller.Instance.normalBitmap.GetPixel(t.A.X, t.A.Y), Triller.Instance.normalBitmap.GetPixel(t.B.X, t.B.Y), Triller.Instance.normalBitmap.GetPixel(t.C.X, t.C.Y));
                return new MyVector((double)(pixel.R - 127) / 128, (double)(pixel.G - 127) / 128, (double)(pixel.B) / 255);
            }
        }
    }
}

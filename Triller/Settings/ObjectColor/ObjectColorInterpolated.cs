using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.ObjectColor
{
    public class ObjectColorInterpolated : IObjectColor
    {
        private Triangle t;
        private int x;
        private int y;

        public ObjectColorInterpolated(int x, int y, Triangle t)
        {
            this.x = x;
            this.y = y;
            this.t = t;
        }

        public Color ObjectColor
        {
            get
            {
                return t.GetInterpolationPixel(x, y, Triller.Instance.bitmap.GetPixel(t.A.X, t.A.Y), 
                    Triller.Instance.bitmap.GetPixel(t.B.X, t.B.Y), Triller.Instance.bitmap.GetPixel(t.C.X, t.C.Y));
            }
        }
    }
}

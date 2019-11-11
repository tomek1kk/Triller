using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.VectorN
{
    public class VectorNFromTexture : IVectorN
    {
        private int x;
        private int y;

        public VectorNFromTexture(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public MyVector N
        {
            get
            {
                var pixel = Triller.Instance.normalBitmap.GetPixel(x, y);
                return new MyVector((double)(pixel.R - 127) / 128, (double)(pixel.G - 127) / 128, (double)(pixel.B) / 255);
            }
        }
    }
}

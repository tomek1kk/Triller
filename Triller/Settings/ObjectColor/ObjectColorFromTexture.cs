using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triller.Settings.ObjectColor;

namespace Triller
{
    public class ObjectColorFromTexture : IObjectColor
    {
        private int x;
        private int y;

        public ObjectColorFromTexture(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Color ObjectColor
        {
            get
            {
                return Triller.Instance.bitmap.GetPixel(x, y);
            }
        }
    }
}

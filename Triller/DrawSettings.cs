using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triller
{
    public class DrawSettings
    {
        public DrawSettings(Triller triller)
        {
            //objectColor = triller.panel1;

               
        }

        public readonly ObjectColor objectColor;
        public readonly Light light;
        public readonly VectorN vectorN;
        public readonly FillColor fillColor;
        public readonly Coefficients coefficients;

    }

    public enum ObjectColor { Constant, FromTexture };
    public enum Light { Constant, AnimationPoint };
    public enum VectorN { Constant, FromTexture };
    public enum FillColor { Exact, Interpolated, Hybrid };
    public enum Coefficients { Constant, Random };
}

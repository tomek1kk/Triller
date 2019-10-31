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
        public readonly ObjectColor objectColor;
        public readonly Light light;
        public readonly VectorN vectorN;
        public readonly FillColor fillColor;
        public readonly Coefficients coefficients;

        public DrawSettings(Triller triller)
        {
            objectColor = triller.radioButton1.Checked ? ObjectColor.Constant : ObjectColor.FromTexture;
            light = triller.radioButton10.Checked ? Light.Constant : Light.AnimationPoint;
            vectorN = triller.radioButton3.Checked ? VectorN.Constant : VectorN.FromTexture;
            fillColor = triller.radioButton5.Checked ? FillColor.Exact : (triller.radioButton6.Checked ? FillColor.Interpolated : FillColor.Hybrid);
            coefficients = triller.radioButton8.Checked ? Coefficients.Constant : Coefficients.Random;
        }

    }

    public enum ObjectColor { Constant, FromTexture };
    public enum Light { Constant, AnimationPoint };
    public enum VectorN { Constant, FromTexture };
    public enum FillColor { Exact, Interpolated, Hybrid };
    public enum Coefficients { Constant, Random };
}

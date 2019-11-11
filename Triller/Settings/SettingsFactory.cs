using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triller.Settings.Coefficients;
using Triller.Settings.Light;
using Triller.Settings.ObjectColor;
using Triller.Settings.VectorN;

namespace Triller.Settings
{
    public class SettingsFactory
    {
        Triller t = Triller.Instance;

        public ICoefficients GetCoefficients()
        {
            if (t.radioButton8.Checked)
                return new CoefficientsConstant();
            else
                return new CoefficientsRandom();
        }

        public ILight GetLight()
        {
            if (t.radioButton10.Checked)
                return new LightConstant();
            else
                return new LightAnimationPoint();
        }

        public IObjectColor GetObjectColor(int x, int y, Triangle triangle)
        {
            if (t.radioButton1.Checked)
                return new ObjectColorConstant();
            if (GetFillColor() == FillColor.Exact)
                return new ObjectColorFromTexture(x, y);
            return new ObjectColorInterpolated(x, y, triangle);
        }

        public IVectorN GetVectorN(int x, int y, Triangle triangle)
        {
            if (t.radioButton3.Checked)
                return new VectorNConstant();
            if (GetFillColor() == FillColor.Interpolated || GetFillColor() == FillColor.Exact)
                return new VectorNFromTexture(x, y);
            else
                return new VectorNInterpolated(x, y, triangle);
        }

        public Color GetLightColor()
        {
            return t.pictureBox3.BackColor;
        }

        private FillColor GetFillColor()
        {
            if (t.radioButton5.Checked)
                return FillColor.Exact;
            else if (t.radioButton6.Checked)
                return FillColor.Interpolated;
            return FillColor.Hybrid;
        }
    }
}

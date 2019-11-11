using System;
using Triller;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.Coefficients
{
    public class CoefficientsConstant : ICoefficients
    {
        public double Kd
        {
            get
            {
                return (double)Triller.Instance.trackBar1.Value / 100;
            }

        }

        public double Ks
        {
            get
            {
                return (double)Triller.Instance.trackBar2.Value / 100;
            }
        }

        public int M
        {
            get
            {
                return Triller.Instance.trackBar3.Value;
            }
        }
    }
}

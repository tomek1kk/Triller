using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.Coefficients
{
    public class CoefficientsRandom : ICoefficients
    {
        Random r = new Random();
        public double Kd
        {
            get
            {
                return (double)(r.Next() % 101) / 100;
            }
        }

        public double Ks
        {
            get
            {
                return (double)(r.Next() % 101) / 100;
            }
        }

        public int M
        {
            get
            {
                return r.Next() % 100 + 1;
            }
        }

    }
}

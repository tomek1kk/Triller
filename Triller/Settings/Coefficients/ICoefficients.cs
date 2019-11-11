using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings
{
    public interface ICoefficients
    {
        double Kd { get; }
        double Ks { get; }
        int M { get; }
    }
}

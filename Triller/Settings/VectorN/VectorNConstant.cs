using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.VectorN
{
    public class VectorNConstant : IVectorN
    {
        public MyVector N
        {
            get
            {
                return new MyVector(0, 0, 1);
            }
        }
    }
}

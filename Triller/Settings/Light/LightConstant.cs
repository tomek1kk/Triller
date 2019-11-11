using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.Light
{
    public class LightConstant : ILight
    {
        public MyVector L
        {
            get
            {
                return new MyVector(0, 0, 1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.ObjectColor
{
    public class ObjectColorConstant : IObjectColor
    {
        public Color ObjectColor
        {
            get
            {
                return Triller.Instance.panel6.BackColor;
            }
        }
    }
}

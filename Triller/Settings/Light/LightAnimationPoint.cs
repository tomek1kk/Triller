using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller.Settings.Light
{
    public class LightAnimationPoint : ILight
    {
        private int x;
        private int y;
        private int animationX;
        private int animationY;

        public LightAnimationPoint(int x, int y, Point animation)
        {
            this.x = x;
            this.y = y;
            this.animationX = animation.X;
            this.animationY = animation.Y;
        }

        public MyVector L
        {
            get
            {
               
                double norm = Math.Sqrt((animationX - x) * (animationX - x) + (animationY - y) * (animationY - y));
                if (norm < 0.1)
                    return new MyVector(Math.Abs((animationX - x)), Math.Abs((animationY - y)), 1);
                return new MyVector(Math.Abs((animationX - x) / norm), Math.Abs((animationY - y) / norm), 1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class MovableCircle : MovableShape
    {
        private double radius;

        public MovableCircle(double radus)
        {
            this.radius = radus;
        }

        public override void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override double CalculateSurface()
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public override void Move(int deltaX, int deltaY)
        {
            x += deltaX;
            y += deltaY;
        }
    }
}

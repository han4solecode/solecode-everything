using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public abstract class MovableShape : IShape, IMovable
    {
        protected int x, y;

        // IShape interface
        public abstract void SetPosition(int x, int y);
        public abstract double CalculateSurface();

        // IMovable interface
        public abstract void Move(int deltaX, int deltaY);
    }
}

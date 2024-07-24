using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Triangle: IShape, IMovable
    {
        private int x, y, baseSurface, height;

        public Triangle(int baseSurface, int height)
        {
            this.baseSurface = baseSurface;
            this.height = height;
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public double CalculateSurface()
        {
            return this.baseSurface * this.height * 0.5;
        }

        public void Move(int deltaX, int deltaY)
        {
            this.x += deltaX;
            this.y += deltaY;
        }
    }
}

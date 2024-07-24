using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public interface IShape
    {
        void SetPosition(int x, int y);
        double CalculateSurface();
    }
}

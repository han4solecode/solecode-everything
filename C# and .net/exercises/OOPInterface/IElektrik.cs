using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPInterface
{
    public interface IElektrik
    {
        int DayaBaterai
        {
            get; set;
        }

        void Charge(int jumlah);
    }
}

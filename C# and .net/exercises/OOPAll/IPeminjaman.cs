using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public interface IPeminjaman
    {
        bool Pinjam();
        bool Kembalikan();

        bool DapatDipinjam { get; set; }
    }
}

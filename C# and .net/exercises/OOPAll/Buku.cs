using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class Buku : Item, IPeminjaman
    {
        // Buku props
        public string? Penulis { get; set; }
        public int JumlahHalaman { get; set; }

        // IPeminjaman prop
        public bool DapatDipinjam {  get; set; }

        // Item prop
        public override string? JenisItem { get; set; }

        // IPeminjaman methods
        public bool Pinjam()
        {
            return true;
        }

        public bool Kembalikan()
        {
            return true;
        }

        // Item methods
        public override decimal HitungDenda(int hariTerlambat)
        {
            throw new NotImplementedException();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
        }

    }
}

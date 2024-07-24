using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class DVD : Item, IPeminjaman
    {
        // DVD props
        public int DurariMenit {  get; set; }
        public string? Sutradara { get; set; }

        // IPeminjaman prop
        public bool DapatDipinjam { get; set; }

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

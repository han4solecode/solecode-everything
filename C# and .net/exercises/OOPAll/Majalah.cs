using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class Majalah : Item
    {
        // Majalah props
        public int NomorEdisi { get; set; }
        public string? Kategori { get; set; }

        // Item prop
        public override string? JenisItem { get; set; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public abstract class Item
    {
        /*protected string? judul;
        protected int tahunTerbit;
        protected string? penerbit;*/

        public string? Judul { get; set; }
        public int TahunTerbit { get; set; }
        public string? Penerbit { get; set; }

        public abstract string JenisItem { get; set; }

        public abstract decimal HitungDenda(int hariTerlambat);

        public virtual void DisplayInfo()
        {
            Console.WriteLine("Judul: {0}\nTahun Terbut: {1}\nPenerbit: {2}", Judul, TahunTerbit, Penerbit, JenisItem);
        }
    }
}

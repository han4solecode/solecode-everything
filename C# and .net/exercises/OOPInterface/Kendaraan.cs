using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPInterface
{
    public abstract class Kendaraan : IKendaraan
    {
        protected string? merk;
        protected int tahun;

        protected Kendaraan(string? merk, int tahun)
        {
            this.merk = merk;
            this.tahun = tahun;
        }

        public abstract void Nyalakan();
        public abstract void Matikan();
        public abstract string InfoKendaraan();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public abstract class Pengguna
    {
        public string? Nama {  get; set; }
        public string? ID { get; set; }

        public abstract decimal HitungBiayaKeanggotaan();

        public virtual string DisplayInfo()
        {
            return "Nama: " + Nama + " ID: " + ID;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class Dosen : Pengguna
    {
        // Dosen prop
        public string? Departemen {  get; set; }
        public string? Gelar { get; set; }

        // Pengguna method
        public override decimal HitungBiayaKeanggotaan()
        {
            throw new NotImplementedException();
        }

        public override string DisplayInfo()
        {
            return "Nama: " + Nama + " ID: " + ID + " Departemen: " + Departemen + " Gelar: " + Gelar;
        }
    }
}

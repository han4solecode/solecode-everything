using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class Mahasiswa : Pengguna
    {
        // Mahasiswa props
        public string? Jurusan {  get; set; }
        public int Semester { get; set; }

        // Pengguna method
        public override decimal HitungBiayaKeanggotaan()
        {
            throw new NotImplementedException();
        }

        public override string DisplayInfo()
        {
            return "Nama: " + Nama + " ID: " + ID + " Jurusan: " + Jurusan + " Semester: " + Semester.ToString();
        }
    }
}

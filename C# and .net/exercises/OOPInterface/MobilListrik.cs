using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPInterface
{
    public class MobilListrik : Kendaraan, IElektrik
    {
        public int DayaBaterai { get; set; }

        public void Charge(int jumlah)
        {
            Console.WriteLine("Tambah daya baterai sebanyak: {0}%", jumlah);
            DayaBaterai = DayaBaterai + jumlah;
            if (DayaBaterai >= 100 )
            {
                DayaBaterai = 100;
                Console.WriteLine("Daya baterai sudah 100%");
            }
        }

        public MobilListrik(string merk, int tahun, int dayaBaterai) : base(merk, tahun)
        {
            this.merk = merk;
            this.tahun = tahun;
            DayaBaterai = dayaBaterai;
        }

        public override void Nyalakan()
        {
            throw new NotImplementedException();
        }

        public override void Matikan()
        {
            throw new NotImplementedException();
        }

        public override string InfoKendaraan()
        {

            return ("Merk: " + merk + " Tahun: " + tahun + " Daya Baterai: " + DayaBaterai + "%");
        }
    }
}

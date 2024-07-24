using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPInterface
{
    public class Mobil : Kendaraan
    {
        private int jumlahPintu;

        public Mobil(int jumlahPintu, string merk, int tahun): base(merk, tahun)
        {
            this.jumlahPintu = jumlahPintu;
            this.merk = merk;
            this.tahun = tahun;
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

            return ("Merk: " + merk + " Tahun: " + tahun + " Jumlah Pintu: " + jumlahPintu.ToString());
        }
    }
}

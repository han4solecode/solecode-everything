using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPInterface
{
    public class KoleksiKendaraan
    {
        private List<IKendaraan> listKendaraan = []; 

        public void TambahKendaraan(IKendaraan kendaraan)
        {
            this.listKendaraan.Add(kendaraan);
        }

        public void TampilkanSemuaInfo()
        {
            foreach (IKendaraan k in this.listKendaraan)
            {
                Console.WriteLine(k.InfoKendaraan());
            }

        }

        public void ChargeKendaraanListrik(int jumlah)
        {
            foreach (Kendaraan k in this.listKendaraan)
            {
                if (k is IElektrik kendaraanListrik)
                {
                    kendaraanListrik.Charge(jumlah);
                }
            }
        }
    }
}

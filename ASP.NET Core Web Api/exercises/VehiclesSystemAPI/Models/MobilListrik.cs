using VehiclesSystemAPI.Interfaces;

namespace VehiclesSystemAPI.Models
{
    public class MobilListrik : Mobil, IElektrik
    {
        public int DayaBaterai { get; set; }

        public void Charge(int jumlah)
        {

        }

        public override void Nyalakan()
        {
            base.Nyalakan();
        }

        public override string InfoKendaraan()
        {
            return $"{base.InfoKendaraan()} | Daya Baterai: {DayaBaterai}";
        }
    }
}
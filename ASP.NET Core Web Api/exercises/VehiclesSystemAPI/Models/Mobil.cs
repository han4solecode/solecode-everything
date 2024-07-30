namespace VehiclesSystemAPI.Models
{
    public class Mobil : Kendaraan
    {
        public int JumlahPintu { get; set; }

        /*public Mobil(int id, string merk, int tahun, int jumlahPintu) : base(id, merk, tahun)
        {
            JumlahPintu = jumlahPintu;
        }*/

        public override void Nyalakan()
        {
            base.Nyalakan();
        }

        public override void Matikan()
        {
            base.Matikan();
        }

        public override string InfoKendaraan()
        {
            return $"{base.InfoKendaraan()} | Jumlah Pintu: {JumlahPintu}";
        }

    }
}
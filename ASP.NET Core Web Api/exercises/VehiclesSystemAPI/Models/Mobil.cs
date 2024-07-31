using System.ComponentModel.DataAnnotations;

namespace VehiclesSystemAPI.Models
{
    public class Mobil : Kendaraan
    {
        [Required]
        public int JumlahPintu { get; set; }

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
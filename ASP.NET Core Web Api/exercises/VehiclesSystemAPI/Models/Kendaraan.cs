using System.ComponentModel.DataAnnotations;

namespace VehiclesSystemAPI.Models
{
    public class Kendaraan
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Merk { get; set; }
        [Required]
        public int Tahun {  get; set; }

        public virtual void Nyalakan()
        {

        }

        public virtual void Matikan()
        {

        }

        public virtual string InfoKendaraan()
        {
            return $"Id: {Id} | Merk: {Merk} | Tahun: {Tahun}";
        }

    }
}
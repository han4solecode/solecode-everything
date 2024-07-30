namespace VehiclesSystemAPI.Models
{
    public class Kendaraan
    {
        public int Id { get; set; }
        public string? Merk { get; set; }
        public int Tahun {  get; set; }

        /*public Kendaraan(int id, string merk, int tahun)
        {
            Id = id;
            Merk = merk;
            Tahun = tahun;
        }*/

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
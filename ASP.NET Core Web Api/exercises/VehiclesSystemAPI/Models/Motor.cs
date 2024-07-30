namespace VehiclesSystemAPI.Models
{
    public class Motor : Kendaraan
    {
        public string? JenisMotor { get; set; }

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
            return $"{base.InfoKendaraan()} | Jenis Motor: {JenisMotor}";
        }
    }
}
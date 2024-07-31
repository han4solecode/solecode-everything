using VehiclesSystemAPI.Models;

namespace VehiclesSystemAPI.Interfaces
{
    public interface IKendaraanService
    {
        List<Kendaraan> GetAllKendaraan();
        Kendaraan? GetKendaraanById(int id);
        Kendaraan AddKendaraan(Kendaraan kendaraan);
        /*Mobil AddMobil(Mobil mobil);
        Motor AddMotor(Motor motor);
        MobilListrik AddMobilListrik(MobilListrik mobilListrik);
        MotorListrik AddMotorListrik(MotorListrik mobilListrik);*/
        Kendaraan? UpdateKendaraan(int id, Kendaraan inputKendaraan);
        bool DeleteKendaraan(int id);
        void ChargeKendaraanListrik(int jumlah);
        string? TampilkanSemuaKendaraan();
    }
}

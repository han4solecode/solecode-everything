using VehiclesSystemAPI.Models;

namespace VehiclesSystemAPI.Interfaces
{
    public interface IKendaraanService
    {
        List<Kendaraan> GetAllKendaraan();
        Kendaraan? GetKendaraanById(int id);
        Kendaraan AddKendaraan(Kendaraan kendaraan);
        Kendaraan? UpdateKendaraan(int id, Kendaraan inputKendaraan);
        bool DeleteKendaraan(int id);
        void ChargeKendaraanListrik(int jumlah);
        string? TampilkanSemuaKendaraan();
    }
}

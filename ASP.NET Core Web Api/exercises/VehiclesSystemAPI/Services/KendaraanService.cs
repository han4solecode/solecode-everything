using System.Text;
using VehiclesSystemAPI.Interfaces;
using VehiclesSystemAPI.Models;

namespace VehiclesSystemAPI.Services
{
    public class KendaraanService : IKendaraanService
    {
        private static List<Kendaraan> _kendaraan = [];

        public List<Kendaraan> GetAllKendaraan()
        {
            return _kendaraan;
        }

        public Kendaraan? GetKendaraanById(int id)
        {
            var kendaraan = _kendaraan.FirstOrDefault(k => k.Id == id);

            if (kendaraan == null)
            {
                return null;
            }

            return kendaraan;
        }

        public Kendaraan AddKendaraan(Kendaraan kendaraan)
        {
            _kendaraan.Add(kendaraan);
            return kendaraan;
        }

        /*public Mobil AddMobil(Mobil mobil)
        {
            _kendaraan.Add(mobil);
            return mobil;
        }

        public Motor AddMotor(Motor motor)
        {
            _kendaraan.Add(motor);
            return motor;
        }

        public MobilListrik AddMobilListrik(MobilListrik mobilListrik)
        {
            _kendaraan.Add(mobilListrik);
            return mobilListrik;
        }

        public MotorListrik AddMotorListrik(MotorListrik motorListrik)
        {
            _kendaraan.Add(motorListrik);
            return motorListrik;
        }*/

        public Kendaraan? UpdateKendaraan(int id, Kendaraan inputKendaraan)
        {
            var kendaraan = GetKendaraanById(id);

            if (kendaraan == null)
            {
                return null;
            }

            kendaraan.Id = inputKendaraan.Id;
            kendaraan.Merk = inputKendaraan.Merk;
            kendaraan.Tahun = inputKendaraan.Tahun;

            return kendaraan;
        }

        public bool DeleteKendaraan(int id)
        {
            var kendaraan = _kendaraan.FirstOrDefault(k => k.Id == id);

            if (kendaraan != null)
            {
                _kendaraan.Remove(kendaraan);
                return true;
            }

            return false;

        }

        public void ChargeKendaraanListrik(int jumlah)
        {
            foreach (Kendaraan k in _kendaraan)
            {
                if (k is IElektrik ke)
                {
                    ke.Charge(jumlah);

                    if (ke.DayaBaterai >= 100)
                    {
                        ke.DayaBaterai = 100;
                    }
                }
            }
        }

        public string TampilkanSemuaKendaraan()
        {
            StringBuilder sb = new();

            foreach (Kendaraan k in _kendaraan)
            {
                sb.AppendLine(k.InfoKendaraan());
            }

            return sb.ToString();
        }

    }
}

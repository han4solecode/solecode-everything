using System.Text;
using VehiclesSystemAPI.Data;
using VehiclesSystemAPI.Interfaces;
using VehiclesSystemAPI.Models;

namespace VehiclesSystemAPI.Services
{
    public class KendaraanService : IKendaraanService
    {
        // private static List<Kendaraan> _kendaraan = [];
        private readonly AppDbContext _context;

        public KendaraanService(AppDbContext context)
        {
            _context = context;
        }

        public List<Kendaraan> GetAllKendaraan()
        {
            // return _kendaraan;
            return _context.Vehicles.ToList();
        }

        public Kendaraan? GetKendaraanById(int id)
        {
            // var kendaraan = _kendaraan.FirstOrDefault(k => k.Id == id);
            var kendaraan = _context.Vehicles.FirstOrDefault(v => v.Id == id);

            if (kendaraan == null)
            {
                return null;
            }

            return kendaraan;
        }

        public Kendaraan AddKendaraan(Kendaraan kendaraan)
        {
            // _kendaraan.Add(kendaraan);
            
            _context.Vehicles.Add(kendaraan);
            _context.SaveChanges();

            return kendaraan;
        }

        public Kendaraan? UpdateKendaraan(int id, Kendaraan inputKendaraan)
        {
            var kendaraan = GetKendaraanById(id);

            if (kendaraan == null)
            {
                return null;
            }

            // kendaraan.Id = inputKendaraan.Id;
            // kendaraan.Merk = inputKendaraan.Merk;
            // kendaraan.Tahun = inputKendaraan.Tahun;

            var updatedKendaraan = new Kendaraan()
            {
                Id = id,
                Merk = inputKendaraan.Merk,
                Tahun = inputKendaraan.Tahun,
            };

            _context.Vehicles.Update(updatedKendaraan);
            _context.SaveChanges();

            return updatedKendaraan;
        }

        public bool DeleteKendaraan(int id)
        {
            var kendaraan = GetKendaraanById(id);

            if (kendaraan == null)
            {
                return false;
            }

            // _kendaraan.Remove(kendaraan);
            _context.Vehicles.Remove(kendaraan);
            _context.SaveChanges();
            return true;

        }

        public void ChargeKendaraanListrik(int jumlah)
        {
            foreach (Kendaraan k in _context.Vehicles)
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

            foreach (Kendaraan k in _context.Vehicles)
            {
                sb.AppendLine(k.InfoKendaraan());
            }

            return sb.ToString();
        }

    }
}

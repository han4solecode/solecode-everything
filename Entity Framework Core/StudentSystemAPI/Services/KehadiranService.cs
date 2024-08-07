using StudentSystemAPI.Data;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Services
{
    public class KehadiranService : IKehadiranService
    {
        private readonly AppDbContext _context;

        public KehadiranService(AppDbContext context)
        {
            _context = context;
        }

        public Kehadiran? AddKehadiran(Kehadiran kehadiran)
        {
            _context.Kehadirans.Add(kehadiran);
            _context.SaveChanges();
            return kehadiran;
        }

        public bool DeleteKehadiran(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Kehadiran> GetAllKehadiran()
        {
            return _context.Kehadirans.ToList();
        }

        public Kehadiran? GetKehadiranById(int id)
        {
            throw new NotImplementedException();
        }

        public Kehadiran? UpdateKehadiran(int id, Kehadiran inputKehadiran)
        {
            throw new NotImplementedException();
        }
    }
}

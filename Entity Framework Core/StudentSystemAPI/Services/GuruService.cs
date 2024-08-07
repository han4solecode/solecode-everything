using StudentSystemAPI.Data;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Services
{
    public class GuruService : IGuruService
    {
        private readonly AppDbContext _context;

        public GuruService(AppDbContext context)
        {
            _context = context;
        }

        public Guru? AddGuru(Guru guru)
        {
            /*Guru newGuru = new Guru()
            {
                Name = guru.Name,
                Email = guru.Email,
                MataKuliah = guru.MataKuliah,
            };*/

            _context.Gurus.Add(guru);
            _context.SaveChanges();

            return guru;
        }

        public bool DeleteGuru(int id)
        {
            var guru = GetGuruById(id);

            if (guru == null)
            {
                return false;
            }

            _context.Gurus.Remove(guru);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Guru> GetAllGuru()
        {
            return _context.Gurus.ToList();
        }

        public Guru? GetGuruById(int id)
        {
            var guru = _context.Gurus.SingleOrDefault(g => g.GuruId == id);

            if (guru == null)
            {
                return null;
            }

            return guru;
        }

        public Guru? UpdateGuru(int id, Guru inputGuru)
        {
            throw new NotImplementedException();
        }
    }
}

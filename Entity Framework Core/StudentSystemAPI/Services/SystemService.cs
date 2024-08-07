using Microsoft.EntityFrameworkCore;
using StudentSystemAPI.Data;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;
using System.Text;

namespace StudentSystemAPI.Services
{
    public class SystemService : ISystemService
    {
        private readonly AppDbContext _context;

        public SystemService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Student>> CariSiswa(string input)
        {
            var student = await _context.Students.Where(s => s.Name == input || s.Kelas == input).ToListAsync();

            /*if (student == null)
            {
                return null;
            }*/

            return student;
        }

        public async Task<IEnumerable<Student>> DaftarSiswa()
        {
            var allStudent = await _context.Students.ToListAsync();
            return allStudent;
        }

        public async Task<IEnumerable<Guru>> GuruAktif()
        {
            /*var guruHadir = await (from g in _context.Kehadirans
                                   group g by g.GuruId).ToListAsync();*/

            var gurus = await _context.Gurus.ToListAsync();

            var guruHadir = await _context.Kehadirans
                      .GroupBy(g => g.GuruId)
                      .Select(g => new
                      {
                          GuruId = g.Key,
                          JumlahKehadiran = g.ToList().Count(),
                      }).ToListAsync();

            var guruAktif = (from a in gurus
                             join b in guruHadir on a.GuruId equals b.GuruId
                             orderby b.JumlahKehadiran descending
                             select a);

            return guruAktif.Take(5).ToList();

           /* var aaaaaa = from a in _context.Kehadirans
                         group a by a.GuruId into aaaaab*/
                         


        }

        public async Task<IEnumerable<Student>> KehadiranRendah()
        {
            var kehadiranCount = await _context.Kehadirans.CountAsync();
            var students = await _context.Students.ToListAsync();
            var studentHadir = await _context.Kehadirans
                      .GroupBy(s => s.StudentId)
                      .Select(s => new
                      {
                          StudentId = s.Key,
                          JumlahKehadiran = s.ToList().Count(),
                      }).ToListAsync();

            // studentHadir.Where(s => (s.JumlahKehadiran/kehadiranCount*100) < 75).ToList();

            var kehadiranRendah = from a in students
                                  join b in studentHadir on a.StudentId equals b.StudentId
                                  where (b.JumlahKehadiran/kehadiranCount*100) < 75
                                  select a;

            return kehadiranRendah.ToList();
        }

        public async Task<IEnumerable<Student>> SiswaBerprestasi()
        {
            var siswaBerprestasi = await _context.Students.OrderByDescending(s => s.NilaiRataRata).Take(3).ToListAsync();
            return siswaBerprestasi;
        }

        public async Task<string> SiswaPerJurusan()
        {
            var siswaPerJurusan = await (from s in _context.Students
                                   group s by s.Major).ToListAsync();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var student in siswaPerJurusan)
            {
                // Console.WriteLine($"Major: {student.Key} | Student count: {student.Count()}");
                /*foreach (var item in student)
                {
                    Console.WriteLine($"{item.Major} | {student.Count()}");
                }*/
                stringBuilder.AppendLine($"Major: {student.Key} | Student count: {student.Count()}");
            }

            return stringBuilder.ToString();
        }

        public async Task<string> StatistikSekolah()
        {
            var studentCount = await _context.Students.CountAsync();
            var studentAvgAge = await _context.Students.AverageAsync(s => s.Age);
            var guruCount = await _context.Gurus.CountAsync();
            var kehadirans = await _context.Kehadirans.ToListAsync();

            var totalPertemuan = await _context.Kehadirans.CountAsync();
            var totalKehadiran = await _context.Kehadirans.Where(k => k.Status == "Hadir").CountAsync();
            var persentaseKehadiran = totalKehadiran / totalPertemuan * 100;

            // Console.WriteLine($"Total Siswa: {studentCount} | Total Guru: {guruCount} | Umur Rata-Rata Siswa: {studentAvgAge} | Persentase kehadiran: {persentaseKehadiran}");
            return ($"Total Siswa: {studentCount} | Total Guru: {guruCount} | Umur Rata-Rata Siswa: {studentAvgAge} | Persentase kehadiran: {persentaseKehadiran}");
        }
    }
}

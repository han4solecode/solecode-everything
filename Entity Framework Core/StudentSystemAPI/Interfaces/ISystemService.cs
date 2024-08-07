using StudentSystemAPI.Models;

namespace StudentSystemAPI.Interfaces
{
    public interface ISystemService
    {
        Task<IEnumerable<Student>> DaftarSiswa();

        Task<IEnumerable<Student>> CariSiswa(string input);

        Task<string> SiswaPerJurusan();

        Task<IEnumerable<Guru>> GuruAktif();

        Task<IEnumerable<Student>> SiswaBerprestasi();

        Task<IEnumerable<Student>> KehadiranRendah();

        Task<string> StatistikSekolah();
    }
}

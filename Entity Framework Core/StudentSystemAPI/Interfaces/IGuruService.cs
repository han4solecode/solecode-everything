using StudentSystemAPI.Models;

namespace StudentSystemAPI.Interfaces
{
    public interface IGuruService
    {
        IEnumerable<Guru> GetAllGuru();

        Guru? GetGuruById(int id);

        Guru? AddGuru(Guru guru);

        Guru? UpdateGuru(int id, Guru inputGuru);

        bool DeleteGuru(int id);
    }
}

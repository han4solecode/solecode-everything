using StudentSystemAPI.Models;

namespace StudentSystemAPI.Interfaces
{
    public interface IKehadiranService
    {
        IEnumerable<Kehadiran> GetAllKehadiran();

        Kehadiran? GetKehadiranById(int id);

        Kehadiran? AddKehadiran(Kehadiran kehadiran);

        Kehadiran? UpdateKehadiran(int id, Kehadiran inputKehadiran);

        bool DeleteKehadiran(int id);
    }
}

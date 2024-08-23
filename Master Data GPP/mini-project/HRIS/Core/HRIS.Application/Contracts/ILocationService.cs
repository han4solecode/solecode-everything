using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocations(int a, int b);

        Task<Location?> GetLocationById(int id);

        Task<bool> AddNewLocation(Location location);

        Task<bool> UpdateExistingLocation(int id, Location inputLocation);

        Task<bool> DeleteExistingLocation(int id);
    }
}
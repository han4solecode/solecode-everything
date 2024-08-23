using HRIS.Application.Contracts;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;

namespace HRIS.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> AddNewLocation(Location location)
        {
            try
            {
                await _locationRepository.Create(location);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteExistingLocation(int id)
        {
            var location = await _locationRepository.GetById(id);
            
            if (location == null)
            {
                return false;
            }

            try
            {
                await _locationRepository.Delete(location);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Location>> GetAllLocations(int a, int b)
        {
            var locations = await _locationRepository.GetAll(a, b);

            return locations;
        }

        public async Task<Location?> GetLocationById(int id)
        {
            var location = await _locationRepository.GetById(id);
            
            return location;
        }

        public async Task<bool> UpdateExistingLocation(int id, Location inputLocation)
        {
            var location = await _locationRepository.GetById(id);

            if (location == null)
            {
                return false;
            }

            location.Address = inputLocation.Address;
            location.Deptno = inputLocation.Deptno;

            await _locationRepository.Update(location);
            return true;
        }
    }
}
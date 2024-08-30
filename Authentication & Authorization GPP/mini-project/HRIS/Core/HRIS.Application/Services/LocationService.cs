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
            var loc = await _locationRepository.GetById(id);

            if (loc == null)
            {
                return false;
            }

            await _locationRepository.Delete(loc);

            return true;
        }

        public async Task<IEnumerable<Location>> GetAllLocations(int a, int b)
        {
            var locs = await _locationRepository.GetAll(a, b);

            return locs;
        }

        public async Task<Location?> GetLocationById(int id)
        {
            var loc = await _locationRepository.GetById(id);

            return loc;
        }

        public async Task<bool> UpdateExistingLocation(int id, Location inputLocation)
        {
            var loc = await _locationRepository.GetById(id);

            if (loc == null)
            {
                return false;
            }

            loc.Address = inputLocation.Address;

            await _locationRepository.Update(loc);

            return true;
        }
    }
}
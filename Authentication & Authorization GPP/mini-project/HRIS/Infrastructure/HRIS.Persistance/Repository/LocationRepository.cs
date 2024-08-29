using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Location entity)
        {
            await _context.Locations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Location entity)
        {
            _context.Locations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location>> GetAll(int recordsPerPage, int currentPage)
        {
            var locations = await _context.Locations.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return locations;
        }

        public async Task<Location?> GetById(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            return location;
        }

        public async Task Update(Location entity)
        {
            _context.Locations.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
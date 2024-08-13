using Microsoft.EntityFrameworkCore;
using VehiclesSystemAPI.Models;

namespace VehiclesSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Kendaraan> Vehicles { get; set; }
    }
}

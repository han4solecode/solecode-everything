using Microsoft.EntityFrameworkCore;
using SimpleLibrartyManagementSystemAPI_Postgres.Models;

namespace SimpleLibrartyManagementSystemAPI_Postgres.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("SLMSWebAPIDatabase"));
        }

        public DbSet<Book> Books { get; set; }
    }
}
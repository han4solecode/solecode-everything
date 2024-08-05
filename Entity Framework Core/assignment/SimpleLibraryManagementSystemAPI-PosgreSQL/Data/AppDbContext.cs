using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Models;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("SLMSWebAPIDatabase"));
        }

        public DbSet<Book> Books { get; set; }
    }
}
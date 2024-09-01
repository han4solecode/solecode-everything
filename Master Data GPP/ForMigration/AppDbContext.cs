using Microsoft.EntityFrameworkCore;

namespace ForMigration
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=LMS; username=solecode; Password=12344321");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }
    }
}
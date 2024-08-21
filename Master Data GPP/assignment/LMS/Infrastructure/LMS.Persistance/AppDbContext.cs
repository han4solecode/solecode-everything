using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<LibraryCard> LibraryCards { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using SLMS.Domain.Entities;

namespace SLMS.Persistance.Data
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Lending> Lendings { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Bookid).HasName("books_pkey");
            });

            modelBuilder.Entity<Lending>(entity =>
            {
                entity.HasKey(e => e.Lendingid).HasName("lendings_pkey");

                entity.Property(e => e.Borrowdate).HasDefaultValueSql("CURRENT_DATE");

                entity.HasOne(d => d.Book).WithMany(p => p.Lendings).HasConstraintName("fk_lendings_books_bookid");

                entity.HasOne(d => d.User).WithMany(p => p.Lendings).HasConstraintName("fk_lendings_users_userid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Userid).HasName("users_pkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
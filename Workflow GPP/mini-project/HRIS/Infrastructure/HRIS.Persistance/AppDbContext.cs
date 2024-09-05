using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        public AppDbContext() { }
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=HRIS2; username=solecode; Password=12344321");
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Employee>(entity =>
        //     {
        //         entity.HasKey(e => e.Id).HasName("employees_pkey");

        //         // entity.Property(e => e.Status).HasDefaultValueSql("'Active'::character varying");
        //         // entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //         entity.HasOne(d => d.Department).WithMany(p => p.Employees)
        //             .OnDelete(DeleteBehavior.SetNull)
        //             .HasConstraintName("fk_AspNetUsers_departments_deptno");
        //     });
        // }

        // public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Workson> Worksons { get; set; }
        public DbSet<EmpDependent> EmpDependents { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
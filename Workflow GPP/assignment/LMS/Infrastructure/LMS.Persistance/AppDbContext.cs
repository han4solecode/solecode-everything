using LMS.Domain.Entities;
using LMS.Domain.Entities.Workflow;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance
{
    public partial class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=LMS2; username=solecode; Password=12344321");
        }

        public virtual DbSet<Book> Books { get; set; }

        // public DbSet<User> Users { get; set; }

        public DbSet<LibraryCard> LibraryCards { get; set; }

        public DbSet<Lending> Lendings { get; set; }

        // workflow DbSet<>
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowSequence> WorkflowSequences { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
    }
}
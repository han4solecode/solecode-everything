using HRIS.Domain.Entity;
using HRIS.Domain.Entity.Workflow;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance
{
    public class AppDbContext : IdentityDbContext<Employee, AppRole, string>
    {
        public AppDbContext() { }
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql("Host=localhost; Database=HRIS3; username=solecode; Password=12344321");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Workson> Worksons { get; set; }
        public DbSet<EmpDependent> EmpDependents { get; set; }
        public DbSet<Location> Locations { get; set; }

        // Workflow DbSets
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowSequence> WorkflowSequences { get; set; }
        public DbSet<NextStepRule> NextStepRules { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
    }
}
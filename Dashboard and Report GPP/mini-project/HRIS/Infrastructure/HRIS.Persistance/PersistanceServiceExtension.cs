using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using HRIS.Persistance.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRIS.Persistance
{
    public static class PersistanceServiceExtension
    {
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseNpgsql(connection);
            });

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksonRepository, WorksonRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEmpDependentRepository, EmpDependentRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Employee, AppRole>(opt => {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
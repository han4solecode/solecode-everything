using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services;
using CSWebAPI.Persistance.Repositories;
// using CSWebAPI.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSWebAPI.Persistance
{
    public static class ServiceExtention
    {
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseNpgsql(connection);
            });
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksonRepository, WorksonRepository>();
            services.AddScoped<IInfoRepository, InfoRepository>();
        }
    }
}
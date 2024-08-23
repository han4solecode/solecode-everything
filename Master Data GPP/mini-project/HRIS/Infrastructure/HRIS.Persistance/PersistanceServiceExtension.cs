using HRIS.Application.Persistance;
using HRIS.Persistance.Repository;
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
            services.AddDbContext<HrisContext>(opt => {
                opt.UseNpgsql(connection);
            });

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
        }
    }
}
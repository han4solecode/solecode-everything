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
        }
    }
}
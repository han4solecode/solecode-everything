using LMS.Application.Persistance;
using LMS.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Persistance
{
    public static class ApplicationServiceExtension
    {
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseNpgsql(connection);
            });

            services.AddScoped<IBookRepostory, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
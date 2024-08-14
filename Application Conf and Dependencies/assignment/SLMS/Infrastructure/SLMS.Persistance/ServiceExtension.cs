using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SLMS.Application.Repositories;
using SLMS.Persistance.Data;
using SLMS.Persistance.Repositories;

namespace SLMS.Persistance
{
    public static class ServiceExtension
    {
        public static void ConfiguringPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LibraryContext>(opt => {
                opt.UseNpgsql(connection);
            });
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRIS.Persistance
{
    public static class PersistanceServiceExtension
    {
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {

        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Employee, IdentityRole>(opt => {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
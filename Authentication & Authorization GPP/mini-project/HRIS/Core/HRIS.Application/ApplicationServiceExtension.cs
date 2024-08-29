using HRIS.Application.Contracts;
using HRIS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRIS.Application
{
    public static class ApplicationServiceExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
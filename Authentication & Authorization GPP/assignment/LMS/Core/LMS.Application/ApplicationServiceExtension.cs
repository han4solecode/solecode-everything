using LMS.Application.Contracts;
using LMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Application
{
    public static class ApplicationServiceExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
using CSWebAPI.Application.Services.Features;
using CSWebAPI.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CSWebAPI.Application
{
    public static class AppServiceExtention
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IWorksonService, WorksonService>();
            services.AddScoped<IInfoService, InfoService>();
        }
    }
}
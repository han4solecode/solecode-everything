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
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IEmpDependentService, EmpDependentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IWorksonService, WorksonService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
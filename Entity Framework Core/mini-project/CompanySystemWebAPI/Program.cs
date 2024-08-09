
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CompanySystemWebAPI.Api;
using CompanySystemWebAPI.Data;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("PostgresDbConnection");
        builder.Services.AddDbContext<AppDbContext>(option => {
            option.UseNpgsql(connectionString);
        });

        builder.Services.AddControllers();

        builder.Services.AddApiVersioning(option => {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
            // option.ApiVersionReader = ApiVersionReader.Combine(
            //     new QueryStringApiVersionReader("api-version"),
            //     new HeaderApiVersionReader("X-Version"),
            //     new MediaTypeApiVersionReader("ver")
            // );
        }).AddApiExplorer(option => {
            option.GroupNameFormat = "'v'VVV";
            option.SubstituteApiVersionInUrl = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // var info = new OpenApiInfo()
        // {
        //     Title = "Company System Web API Documentation",

        // };

        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureOptions<ConfifureSwaggerOptions>();
        // builder.Services.ConfigureOptions

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IWorksonService, WorksonService>();

        var app = builder.Build();
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(option => {
                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    option.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"Company System Web API {desc.GroupName}");
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

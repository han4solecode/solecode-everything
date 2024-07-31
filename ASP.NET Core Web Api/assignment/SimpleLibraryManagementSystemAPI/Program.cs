using System.Reflection;
using Microsoft.OpenApi.Models;
using SimpleLibraryManagementSystemAPI.Interfaces;
using SimpleLibraryManagementSystemAPI.Services;

namespace SimpleLibraryManagementSystemAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<IBooksService, BooksService>();

        builder.Services.AddEndpointsApiExplorer();

        var info = new OpenApiInfo()
        {
            Title = "Simple Library Management System API",
            Description = "A web API to manage books at our library",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "M. Farhan Athaullah",
                Email = "farhan@solecode.id"
            }
        };

        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", info);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(u =>
            {
                u.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Simple Library Management System API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

using VehiclesSystemAPI.Interfaces;
using VehiclesSystemAPI.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace VehiclesSystemAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        var info = new OpenApiInfo()
        {
            Title = "Vehicles System API Documentation",
            Version = "v1",
            Description = "A web API for accessing vehicle system",
            Contact = new OpenApiContact()
            {
                Name = "M. Farhan Athaullah",
                Email = "farhan@solecode.id",
            }

        };

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", info);

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddSingleton<IKendaraanService, KendaraanService>();

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
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Vehicle System API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

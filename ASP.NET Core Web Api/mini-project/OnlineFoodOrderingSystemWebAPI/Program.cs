using System.Reflection;
using Microsoft.OpenApi.Models;
using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Services;

namespace OnlineFoodOrderingSystemWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        var info = new OpenApiInfo()
        {
            Title = "Online Food Ordering System API Documentation",
            Version = "v1",
            Description = "A food ordering system web API",
            Contact = new OpenApiContact()
            {
                Name = "M. Farhan Athaullah",
                Email = "farhan@solecode.id",
            }
        };

        builder.Services.AddSwaggerGen(opt => {
            opt.SwaggerDoc("v1", info);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddSingleton<IMenuService, MenuService>();
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IOrderService, OrderService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(u => {
                u.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Online Food Ordering System API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

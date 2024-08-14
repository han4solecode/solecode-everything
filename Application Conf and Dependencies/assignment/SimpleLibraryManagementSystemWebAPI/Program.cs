using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemWebAPI.Data;
using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Options;
using SimpleLibraryManagementSystemWebAPI.Repositories;

namespace SimpleLibraryManagementSystemWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration["ConnectionStrings:PostgresDbConnection"];
        builder.Services.AddDbContext<LibraryContext>(option => {
            option.UseNpgsql(connectionString);
        });

        // inject appsettings with options pattern
        var libraryConfig = builder.Configuration.GetSection(LibraryOptions.SettingName);
        builder.Services.Configure<LibraryOptions>(libraryConfig);

        builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

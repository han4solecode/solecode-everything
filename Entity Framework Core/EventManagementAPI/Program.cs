using EventManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var conntectionString = builder.Configuration.GetConnectionString("MySQLDbConnection");
        builder.Services.AddDbContext<AppDbContext>(option =>
        {
            option.UseMySQL(conntectionString);
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

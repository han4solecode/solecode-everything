
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Data;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Interfaces;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Repositories;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(option => {
            option.UseNpgsql(builder.Configuration.GetConnectionString("SLMSWebAPIDatabase"));
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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

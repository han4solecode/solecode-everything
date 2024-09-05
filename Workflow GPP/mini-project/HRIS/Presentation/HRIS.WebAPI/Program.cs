using System.Text;
using HRIS.Persistance;
using HRIS.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HRIS.Application.Options;

namespace HRIS.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.ConfigurePersistance(builder.Configuration);
        builder.Services.ConfigureIdentity();
        builder.Services.ConfigureApplication();

        var configuration = builder.Configuration;

        builder.Services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme =
            opt.DefaultChallengeScheme =
            opt.DefaultForbidScheme =
            opt.DefaultScheme =
            opt.DefaultSignInScheme =
            opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt => {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience= configuration["JWT:Audience"],
                ValidateIssuerSigningKey= true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]!)),
            };
        });

        // MailSettings options
        builder.Services.Configure<MailOptions>(builder.Configuration.GetSection(MailOptions.MailSettings));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

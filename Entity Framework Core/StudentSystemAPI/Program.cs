using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using StudentSystemAPI.Data;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Services;

namespace StudentSystemAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var conntectionString = builder.Configuration.GetConnectionString("SQLServerDbConntection");
        builder.Services.AddDbContext<AppDbContext>(option =>
        {
            option.UseSqlServer(conntectionString);
        });

        builder.Services.AddControllers();

        builder.Services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
            option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
            option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.

            //------------------------------------------------//
            option.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver")); //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
                                                       //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
        }).AddApiExplorer(options => {
            options.GroupNameFormat = "'v'VVV"; //The say our format of our version number “‘v’major[.minor][-status]”
            options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
        });

        builder.Services.AddScoped<IStudentService, StudentService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

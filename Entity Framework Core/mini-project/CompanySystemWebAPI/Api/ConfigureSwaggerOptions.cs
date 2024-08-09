using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CompanySystemWebAPI.Api
{
    public class ConfifureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    desc.GroupName,
                    CreateVersionInfo(desc)
                );
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                Title = "Company System Web API Documentation",
                Version = desc.ApiVersion.ToString(),
                Description = "A company system web API",
                Contact = new OpenApiContact()
                {
                    Name = "M. Farhan Athaullah",
                    Email = "farhan@solecode.id"
                }
            };

            return info;
        }
        
    }
}
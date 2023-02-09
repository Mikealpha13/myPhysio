using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.Swagger
{
    public class ConfigureSwaggerOption : IConfigureOptions<SwaggerGenOptions>
    {

        private readonly IApiVersionDescriptionProvider _provider;


        public ConfigureSwaggerOption(IApiVersionDescriptionProvider provider) => _provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateAPIForInfoVersions(description));

            }
        }

        private static OpenApiInfo CreateAPIForInfoVersions(ApiVersionDescription description)
        {

            switch (description.GroupName)
            {
                case "v1":
                    var info = new OpenApiInfo()
                    {
                        Title = "MyPhysio API",
                        Version = description.ApiVersion.ToString(),
                        Description = "This api is used to process the backend operation for mobile applications",
                        Contact = new OpenApiContact() { Email = "v.singh@techtherapy.co.in", Name = "Vikram Singh" }
                    };
                    if (description.IsDeprecated)
                    {
                        info.Description += "This api version is obsolete";
                    }
                    return info;
                default:
                    return new OpenApiInfo();
            }

        }
    }
}

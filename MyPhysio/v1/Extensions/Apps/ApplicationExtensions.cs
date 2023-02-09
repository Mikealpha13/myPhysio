
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Extensions.Apps
{

    /// <summary>
    /// This class is used to configure the HTTPPipeline middleware
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// This method is created to configure the routing middleware
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureRouting(this IApplicationBuilder app)
        {
            app.UseRouting();
        }

        /// <summary>
        /// This method is created to configure the swagger middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app,IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        /// <summary>
        /// This method is used to configure the endpoint
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureEndpoint(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


        public static void ConfigureHealthCheck(this IApplicationBuilder app)
        {

            app.UseHealthChecks("/healths", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI();
        }
    }
}

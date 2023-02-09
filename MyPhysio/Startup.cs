using MyPhysio.v1.Extensions.Apps;
using MyPhysio.v1.Extensions.Services;
using MyPhysio.v1.Infrastructure.DependencyContainer;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPhysio.Domain.ServiceModels;

namespace MyPhysio
{
    public class Startup
    {
        private IConfiguration Configuration { get; }


        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(x =>
            {
                x.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

            });
            services.AddSingleton<AccessToken>();
            services.RegisterLogger(Configuration);
            services.RegisterFilters();
            //services.RegisterHttpClients(Configuration);
            services.RegisterController();
            //services.RegisterHostedServices();
            services.RegisterMediator();
            services.RegisterCustomConfiguration(Configuration);
            services.RegisterFluentValidation();
            services.RegisterHealthCheck();
            services.RegisterHealthCheckUI();
            services.RegisterAuthentication();
            services.RegisterSwagger();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new APIModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new DomainModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureSwagger(apiVersionDescriptionProvider);
            app.ConfigureEndpoint();
            app.ConfigureHealthCheck();
        }
    }
}

using MyPhysio.Domain.Configuration;
using MyPhysio.Domain.Enums;
using MyPhysio.v1.Infrastructure.CircuitBreaker;
using MyPhysio.v1.Infrastructure.Filters;
using MyPhysio.v1.Infrastructure.HealthChecks;
using MyPhysio.v1.Infrastructure.HostedServices;
using MyPhysio.v1.Infrastructure.Middlewares;
using MyPhysio.v1.Infrastructure.Swagger;
using MyPhysioAPI.v1.Extensions.Datatypes;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyPhysio.v1.Extensions.Services
{

    /// <summary>
    /// This class is used as extension for services 
    /// </summary>
    public static class ServiceExtensions
    {

        /// <summary>
        /// This method is used to register the controller.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterController(this IServiceCollection services)
        {

            services.AddControllers();
        }

        /// <summary>
        /// This method regsiter the hosted services
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterHostedServices(this IServiceCollection services)
        {

            services.AddHostedService<MyPhysioHostedServices>();
        }

        /// <summary>
        /// This method is used to register mediator as services
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterMediator(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterCustomConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            services.Configure<ConnectionDataSource>(configuration);
        }

        /// <summary>
        /// This method is used to register the health check in UI Framework
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterHealthCheckUI(this IServiceCollection services)
        {

            services.AddHealthChecksUI().AddInMemoryStorage();
        }

        /// <summary>
        /// This method is used to register the Helath Check in Framework
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterHealthCheck(this IServiceCollection services)
        {

            services.AddHealthChecks().AddCheck<MyPhysioHealthCheck>("Dependencies Health Check");
        }

        /// <summary>
        /// This method is used to register the fluent validation in system.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterFluentValidation(this IServiceCollection services)
        {

            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// This method is used to register the filter in the system
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterFilters(this IServiceCollection services)
        {

            services.AddScoped<ExceptionFilter>();
            services.AddScoped<AuthorizesAttribute>();
            services.AddScoped<ProfilingMiddleware>();
        }

        /// <summary>
        /// This method is used to register the logger in the system
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterLogger(this IServiceCollection services, IConfiguration configuration)
        {

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        /// <summary>
        /// This method is used to register the swagger in the system
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterSwagger(this IServiceCollection services)
        {

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOption>();

            services.AddSwaggerGen(x =>
            {
                x.OperationFilter<SwaggerDefaultValues>();
                x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using Bearer Scheme."
                });
                x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference=new OpenApiReference()
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string []{ }
                    }
                });

            });
        }

        /// <summary>
        /// This method is used to register the authorization pipleine
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterAuthentication(this IServiceCollection services)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(z =>
            {
                z.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = "",
                    ValidAudience = "",

                };
            });
        }

        /// <summary>
        /// This method is used to create httpclientfactory at run time .
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterHttpClients(this IServiceCollection services,IConfiguration configuration)
        {
            #region TDG Client
            string baseAddress = configuration.GetSection("Storis:BaseUrl").Value;
            
            string endpoint = configuration.GetSection("Storis:AccessToken").Value;            

            var accessToken = baseAddress.GetAccessToken(configuration.GetSection("Storis:TDG:ClientId").Value, configuration.GetSection("Storis:TDG:Secret").Value, endpoint);

            services.AddHttpClient(HTTPClients.TDGStori.ToString(), x =>
             {
                 x.BaseAddress = new Uri(baseAddress);

                 x.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",accessToken);
                 x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             }).AddPolicyHandler(RetryPolicies.GetRetryPolicy()).AddHttpMessageHandler<ProfilingMiddleware>(); 
            #endregion


            #region DSG CLient
            string DSGbaseAddress = configuration.GetSection("Storis:BaseUrl").Value;            

            var DSGaccessToken = DSGbaseAddress.GetAccessToken(configuration.GetSection("Storis:DSG:ClientId").Value, configuration.GetSection("Storis:DSG:Secret").Value, endpoint);
           
            services.AddHttpClient(HTTPClients.DSGStori.ToString(), x =>
            {
                x.BaseAddress = new Uri(DSGbaseAddress);

                x.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",DSGaccessToken);
                x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddPolicyHandler(RetryPolicies.GetRetryPolicy()).AddHttpMessageHandler<ProfilingMiddleware>();

            #endregion

            services.BuildServiceProvider();

        }

      

    }
}

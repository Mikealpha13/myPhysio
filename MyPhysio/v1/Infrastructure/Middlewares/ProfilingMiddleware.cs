using MyPhysio.Domain.ServiceModels;
using MyPhysioAPI.v1.Extensions.Datatypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.Middlewares
{
    /// <summary>
    /// This class is injected in HTTP Request pipeline to act as a dispatcher
    /// </summary>
    public class ProfilingMiddleware : DelegatingHandler
    {
        private readonly ILogger<ProfilingMiddleware> _logger;
        private readonly IConfiguration _configuration;
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ProfilingMiddleware(ILogger<ProfilingMiddleware> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
          
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<string> headerValues = request.Headers.GetValues("Authorization");
                var stringarray = headerValues.FirstOrDefault().Split(" ");
                string newAccessToken = string.Empty;
                var acessToken = stringarray[1];                           
                if (!acessToken.ValidateAccessToken())
                {
                    
                        var accountName = acessToken.GetTokenClient();
                        if (accountName.ToLower().Equals(_configuration.GetSection("Storis:DSG:AccountName").Value.ToLower()))
                        {
                            string endpoint = _configuration.GetSection("Storis:AccessToken").Value;
                            string DSGbaseAddress = _configuration.GetSection("Storis:BaseUrl").Value;
                            newAccessToken = DSGbaseAddress.GetAccessToken(_configuration.GetSection("Storis:DSG:ClientId").Value, _configuration.GetSection("Storis:DSG:Secret").Value, endpoint);
                        }
                        else
                        {
                            string baseAddress = _configuration.GetSection("Storis:BaseUrl").Value;
                            string endpoint = _configuration.GetSection("Storis:AccessToken").Value;
                            newAccessToken = baseAddress.GetAccessToken(_configuration.GetSection("Storis:TDG:ClientId").Value, _configuration.GetSection("Storis:TDG:Secret").Value, endpoint);

                        }
                    
                    
                    request.Headers.Remove("Authorization");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",newAccessToken);
                }

                _logger.LogInformation($"{request.RequestUri} Request Initiated", request);                
                var result = await base.SendAsync(request, cancellationToken);
                _logger.LogInformation($"{request.RequestUri} Request Completed", result);
                return result;


            }
            catch (Exception ex)
            {

                _logger.LogError($"Error occurred in profiling for {request.RequestUri}");
                return null;
            }
        }


    }
}

using EBridge.Domain.EntityModels;
using EBridge.Domain.Enums;
using EBridge.Domain.ServiceModels;
using EBridge.Infrastructure.Contracts;
using EBridge.v1.Command;
using EBridge.v1.Contracts;
using EBridge.v1.ViewModels.Request;
using EBridge.v1.ViewModels.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EBridge.v1.Handlers
{

    /// <summary>
    /// 
    /// </summary>
    public class CustomerDetailCommandHandler : IRequestHandler<CustomerDetailCommand, CustomerDetailResponseViewModel>
    {

        private ILogger<CustomerDetailCommandHandler> _customerDetailCommandlogger;
        private IServiceClient<object, CustomerDetailServiceModelResponse> _customerService;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerDetailCommandlogger"></param>
        /// <param name="customerService"></param>
        /// <param name="configuration"></param>
        public CustomerDetailCommandHandler(ILogger<CustomerDetailCommandHandler> customerDetailCommandlogger,
                                            IServiceClient<object, CustomerDetailServiceModelResponse> customerService,
                                            IConfiguration configuration
                                    )
        {
            _customerDetailCommandlogger = customerDetailCommandlogger ?? throw new ArgumentNullException(nameof(customerService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof (configuration));


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CustomerDetailResponseViewModel> Handle(CustomerDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResponse = new CustomerDetailServiceModelResponse();

                string endpoint = string.Format(_configuration.GetSection("Storis:CustomerDetails").Value, request._customerRequest.CustomerId);

                switch (request._customerRequest.Client)
                {
                    
                    case HTTPClients.DSGStori:
                        serviceResponse = await _customerService.Get(endpoint, HTTPClients.DSGStori);
                        break;
                    case HTTPClients.TDGStori:                       
                    default:
                        serviceResponse = await _customerService.Get(endpoint, HTTPClients.TDGStori);
                        break;

                }
                
                return new CustomerDetailResponseViewModel() { Details = serviceResponse };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

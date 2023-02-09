
using EBridge.Domain.Enums;
using EBridge.Domain.ServiceModels;
using EBridge.Infrastructure.Contracts;
using EBridge.v1.Contracts;
using EBridgeAPI.v1.Command;
using EBridgeAPI.v1.ViewModels.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EBridgeAPI.v1.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerPhoneCommandHandler : IRequestHandler<CustomerPhoneCommand, CustomerPhoneResponseViewModel>
    {
        private ILogger<CustomerPhoneCommand> _logger;
        private IServiceClient<object,CustomerPhoneServiceModelResponse> _customerPhoneService;
        private readonly IConfiguration _configuration;
        private readonly IResponseProcessor<CustomerPhoneResponseViewModel,CustomerPhoneServiceModelResponse> _customerProcessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="customerPhoneService"></param>
        /// <param name="customerProcessor"></param>
        public CustomerPhoneCommandHandler(IConfiguration configuration,ILogger<CustomerPhoneCommand>logger,IServiceClient<object, CustomerPhoneServiceModelResponse> customerPhoneService, IResponseProcessor<CustomerPhoneResponseViewModel, CustomerPhoneServiceModelResponse> customerProcessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _customerPhoneService = customerPhoneService ?? throw new ArgumentNullException(nameof(customerPhoneService));
            _customerProcessor = customerProcessor ?? throw new ArgumentNullException(nameof(customerProcessor));
        }
        public async Task<CustomerPhoneResponseViewModel> Handle(CustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerResponse = new CustomerPhoneServiceModelResponse();

                var response = new CustomerPhoneResponseViewModel() { RequestId = request._customerPhoneRequest.RequestID };

                string endpoint = string.Format(_configuration.GetSection("Storis:CustomerByPhone").Value, request._customerPhoneRequest.PhoneNumber);

                switch (request._customerPhoneRequest.Client)
                {

                    case HTTPClients.DSGStori:
                        customerResponse = await _customerPhoneService.Get(endpoint, HTTPClients.DSGStori);                        
                        break;
                    case HTTPClients.TDGStori:
                    default:
                        customerResponse = await _customerPhoneService.Get(endpoint, HTTPClients.TDGStori);
                        break;

                }

                if(customerResponse==null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.ErrorMessage = "Invalid Request";
                    return response;
                }
                else
                {
                   var viewModelResponse= _customerProcessor.ToViewModel(customerResponse);
                    response.CustomerDetails.AddRange(viewModelResponse.CustomerDetails);
                    response.StatusCode = customerResponse.success?(int)HttpStatusCode.OK:(int)HttpStatusCode.BadRequest;
                    response.Success = customerResponse.success;
                    response.ErrorMessage = customerResponse.message;
                    return response;
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

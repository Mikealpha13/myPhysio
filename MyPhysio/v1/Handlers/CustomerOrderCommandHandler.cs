using EBridge.Domain.Enums;
using EBridge.Domain.ServiceModels;
using EBridge.Infrastructure.Contracts;
using EBridge.v1.Contracts;
using EBridgeAPI.v1.Command;
using EBridgeAPI.v1.ViewModels.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class CustomerOrderCommandHandler : IRequestHandler<CustomerOrderCommand, CustomerOrderResponseViewModel>
    {

        private ILogger<CustomerOrderCommandHandler> _logger;
        private IServiceClient<object, CustomerOrderServiceModelResponse> _customerOrderService;
        private readonly IConfiguration _configuration;
        private readonly IResponseProcessor<CustomerOrderResponseViewModel, CustomerOrderServiceModelResponse> _customerProcessor;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="customerPhoneService"></param>
        /// <param name="customerProcessor"></param>
        public CustomerOrderCommandHandler(IConfiguration configuration, ILogger<CustomerOrderCommandHandler> logger, IServiceClient<object, CustomerOrderServiceModelResponse> customerPhoneService, IResponseProcessor<CustomerOrderResponseViewModel, CustomerOrderServiceModelResponse> customerProcessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _customerOrderService = customerPhoneService ?? throw new ArgumentNullException(nameof(customerPhoneService));
            _customerProcessor = customerProcessor ?? throw new ArgumentNullException(nameof(customerProcessor));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CustomerOrderResponseViewModel> Handle(CustomerOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerResponse = new CustomerOrderServiceModelResponse();

                var response = new CustomerOrderResponseViewModel() { RequestId = request.customerOrderRequestViewModel.RequestID };

                string endpoint = string.Format(_configuration.GetSection("Storis:OrderByCustomerId").Value, request.customerOrderRequestViewModel.customerID);

                switch (request.customerOrderRequestViewModel.Client)
                {

                    case HTTPClients.DSGStori:
                        customerResponse = await _customerOrderService.Get(endpoint, HTTPClients.DSGStori);
                        break;
                    case HTTPClients.TDGStori:
                    default:
                        customerResponse = await _customerOrderService.Get(endpoint, HTTPClients.TDGStori);
                        break;

                }

                if (customerResponse == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.ErrorMessage = "Invalid Request";
                    return response;
                }
                else
                {
                    var viewModelResponse = _customerProcessor.ToViewModel(customerResponse);
                    response.Orders.AddRange(viewModelResponse.Orders);
                    response.StatusCode = customerResponse.success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
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

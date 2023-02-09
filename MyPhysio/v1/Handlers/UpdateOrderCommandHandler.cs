using EBridge.Domain.Enums;
using EBridge.Domain.ServiceModels;
using EBridge.Domain.ServiceModels.Request;
using EBridge.Infrastructure.Contracts;
using EBridge.v1.Contracts;
using EBridgeAPI.v1.Command;
using EBridgeAPI.v1.ViewModels.Request;
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
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderUpdateResponseViewModel>
    {
        private ILogger<UpdateOrderCommandHandler> _logger;
        private IConfiguration _configuration;
        private IServiceClient<OrderUpdateRequestServiceModel, OrderUpdateResponseServiceModel>_orderUpdateClient;
        private IRequestProcessor<OrderUpdateRequestServiceModel, OrderUpdateRequestViewModel> _requestProcessor;
        private IResponseProcessor<OrderUpdateResponseViewModel, OrderUpdateResponseServiceModel> _responseProcessor;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="orderUpdateClient"></param>
        /// <param name="requestProcessor"></param>
        /// <param name="responseProcessor"></param>
        public UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger,
                                                IConfiguration configuration,
                                                IServiceClient<OrderUpdateRequestServiceModel, OrderUpdateResponseServiceModel> orderUpdateClient,
                                                IRequestProcessor<OrderUpdateRequestServiceModel, OrderUpdateRequestViewModel> requestProcessor,
                                                IResponseProcessor<OrderUpdateResponseViewModel, OrderUpdateResponseServiceModel> responseProcessor
                                                )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderUpdateClient = orderUpdateClient ?? throw new ArgumentNullException(nameof(orderUpdateClient));
            _requestProcessor = requestProcessor ?? throw new ArgumentNullException(nameof(requestProcessor));
            _responseProcessor = responseProcessor ?? throw new ArgumentNullException(nameof(responseProcessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OrderUpdateResponseViewModel> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderResponse = new OrderUpdateResponseServiceModel();

                var response = new OrderUpdateResponseViewModel() { RequestId = request._orderUpdateRequest.RequestID };

                string endpoint = _configuration.GetSection("Storis:OrderUpdate").Value;

                var requestBody = _requestProcessor.ToExternalModel(request._orderUpdateRequest);

                switch (request._orderUpdateRequest.Client)
                {

                    case HTTPClients.DSGStori:
                        orderResponse = await _orderUpdateClient.Put(requestBody, endpoint, HTTPClients.DSGStori);
                        break;
                    case HTTPClients.TDGStori:
                    default:
                        orderResponse = await _orderUpdateClient.Put(requestBody, endpoint, HTTPClients.TDGStori);
                        break;

                }

                if (orderResponse == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.ErrorMessage = "Invalid Request";

                }
                else
                {
                    response.StatusCode = orderResponse.success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
                    response.Success = orderResponse.success;
                    response.ErrorMessage = orderResponse.message;



                }
                return response;


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

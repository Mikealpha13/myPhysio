
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
    public class PartialUpdateOrderCommandHandler : IRequestHandler<PartialUpdateOrderCommand, PartialUpdateResponseViewModel>
    {

        private ILogger<PartialUpdateOrderCommandHandler> _logger;
        private IConfiguration _configuration;
        private IServiceClient<PartialUpdateRequestServiceModel, PartialUpdateResponseServiceModel> _partialUpdateClient;
        private IRequestProcessor<PartialUpdateRequestServiceModel, PartialUpdateRequestViewModel> _requestProcessor;
        private IResponseProcessor<PartialUpdateResponseViewModel, PartialUpdateResponseServiceModel> _responseProcessor;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="partialUpdateClient"></param>
        /// <param name="requestProcessor"></param>
        /// <param name="responseProcessor"></param>
        public PartialUpdateOrderCommandHandler(ILogger<PartialUpdateOrderCommandHandler>logger, 
                                                IConfiguration configuration,
                                                IServiceClient<PartialUpdateRequestServiceModel, PartialUpdateResponseServiceModel> partialUpdateClient,
                                                IRequestProcessor<PartialUpdateRequestServiceModel, PartialUpdateRequestViewModel> requestProcessor,
                                                IResponseProcessor<PartialUpdateResponseViewModel, PartialUpdateResponseServiceModel> responseProcessor
                                                )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _partialUpdateClient = partialUpdateClient ?? throw new ArgumentNullException(nameof(partialUpdateClient));
            _requestProcessor = requestProcessor ?? throw new ArgumentNullException(nameof(requestProcessor));
            _responseProcessor = responseProcessor ?? throw new ArgumentNullException(nameof(responseProcessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<PartialUpdateResponseViewModel> Handle(PartialUpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderResponse = new PartialUpdateResponseServiceModel();

                var response = new PartialUpdateResponseViewModel() { RequestId = request._partialUpdateRequest.RequestID };

                string baseAddress = _configuration.GetSection("Storis:BaseUrl").Value;
                string endpoint = string.Format(_configuration.GetSection("Storis:PartialFullFillment").Value, request._partialUpdateRequest.FullfillmentId);

                endpoint = baseAddress + endpoint;

                var requestBody = _requestProcessor.ToExternalModel(request._partialUpdateRequest);

                switch (request._partialUpdateRequest.Client)
                {

                    case HTTPClients.DSGStori:
                        orderResponse = await _partialUpdateClient.Patch(requestBody,endpoint, HTTPClients.DSGStori);
                        break;
                    case HTTPClients.TDGStori:
                    default:
                        orderResponse = await _partialUpdateClient.Patch(requestBody, endpoint, HTTPClients.TDGStori);
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

using EBridge.Domain.Enums;
using EBridge.Domain.ServiceModels;
using EBridge.Infrastructure.Contracts;
using EBridgeAPI.v1.Command;
using EBridgeAPI.v1.ViewModels.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EBridgeAPI.v1.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDetailsCommandHandler : IRequestHandler<OrderDetailCommand, OrderResponseViewModel>
    {
        private ILogger<OrderDetailsCommandHandler> _logger;
        private IConfiguration _configuration;
        private IServiceClient<object, OrderDetailsServiceModelResponse> _orderClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="orderClient"></param>
        public OrderDetailsCommandHandler(ILogger<OrderDetailsCommandHandler>logger,IConfiguration configuration, IServiceClient<object, OrderDetailsServiceModelResponse>orderClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _orderClient = orderClient ?? throw new ArgumentNullException(nameof(orderClient));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async  Task<OrderResponseViewModel> Handle(OrderDetailCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var orderResponse = new OrderDetailsServiceModelResponse();

                var response = new OrderResponseViewModel() { RequestId = request.OrderRequest.RequestID };

                var endpointBuilder = BuildEndpoint(request);           

                string endpoint = string.Format(_configuration.GetSection("Storis:OrderDetails").Value, endpointBuilder);

                switch (request.OrderRequest.Client)
                {

                    case HTTPClients.DSGStori:
                        orderResponse = await _orderClient.Get(endpoint, HTTPClients.DSGStori);
                        break;
                    case HTTPClients.TDGStori:
                    default:
                        orderResponse = await _orderClient.Get(endpoint, HTTPClients.TDGStori);
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
                    response.response = orderResponse;
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


        private string BuildEndpoint(OrderDetailCommand request)
        {
            try
            {
               var endpointBuilder= new StringBuilder();
                request.OrderRequest.Orders.ForEach(z =>{
                    endpointBuilder.Append($"OrderIds={z}&");
                });
                if (request.OrderRequest.ScheduleDates) endpointBuilder.Append(_configuration.GetSection("Storis:OrderDetailSchedule").Value.ToString());
                else endpointBuilder.Remove(endpointBuilder.ToString().Length - 1, 1);
                return endpointBuilder.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

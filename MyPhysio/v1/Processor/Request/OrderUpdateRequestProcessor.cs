using MyPhysio.Domain.ServiceModels.Request;
using MyPhysio.v1.Contracts;
using MyPhysioAPI.v1.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Processor.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderUpdateRequestProcessor : IRequestProcessor<OrderUpdateRequestServiceModel, OrderUpdateRequestViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderUpdateRequestServiceModel ToExternalModel(OrderUpdateRequestViewModel request)
        {
            return new OrderUpdateRequestServiceModel()
            {
                deliveryDate=request.deliveryDate,
                deliveryInstructions=request.deliveryInstruction,
                orderId=request.OrderId
            };
        }
    }
}

using MyPhysioAPI.v1.ViewModels.Request;
using MyPhysioAPI.v1.ViewModels.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateOrderCommand:IRequest<OrderUpdateResponseViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly OrderUpdateRequestViewModel _orderUpdateRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderUpdateRequest"></param>
        public UpdateOrderCommand(OrderUpdateRequestViewModel orderUpdateRequest)
        {
            _orderUpdateRequest = orderUpdateRequest ?? throw new ArgumentNullException(nameof(orderUpdateRequest));
        }
    }
}

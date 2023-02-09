
using MediatR;
using MyPhysioAPI.v1.ViewModels.Request;
using MyPhysioAPI.v1.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDetailCommand:IRequest<OrderResponseViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly OrderRequestViewModel OrderRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRequestViewModel"></param>
        public OrderDetailCommand(OrderRequestViewModel orderRequestViewModel)
        {
            OrderRequest = orderRequestViewModel ?? throw new ArgumentNullException(nameof(orderRequestViewModel));
        }
    }
}

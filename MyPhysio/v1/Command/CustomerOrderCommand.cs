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
    public class CustomerOrderCommand:IRequest<CustomerOrderResponseViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly CustomerOrderRequestViewModel customerOrderRequestViewModel ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerOrderRequest"></param>
        public CustomerOrderCommand(CustomerOrderRequestViewModel customerOrderRequest)
        {
            customerOrderRequestViewModel = customerOrderRequest ?? throw new ArgumentNullException(nameof(customerOrderRequest));
        }
    }
}

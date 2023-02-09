using MyPhysio.Domain.ServiceModels;
using MyPhysio.v1.Contracts;
using MyPhysioAPI.v1.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Processor.Response.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerOrderResponseProcessor : IResponseProcessor<CustomerOrderResponseViewModel, CustomerOrderServiceModelResponse>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CustomerOrderResponseViewModel ToViewModel(CustomerOrderServiceModelResponse request)
        {
            var response = new CustomerOrderResponseViewModel();
            if (request.success && request.data.orders != null && request.data.orders.Count > 0 )
            {
                response.Orders.AddRange(request.data.orders);
            }
            return response;
        }
    }
}

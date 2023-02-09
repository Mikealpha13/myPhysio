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
    public class OrderUpdateResponseProcessor : IResponseProcessor<OrderUpdateResponseViewModel, OrderUpdateResponseServiceModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderUpdateResponseViewModel ToViewModel(OrderUpdateResponseServiceModel request)
        {
            throw new NotImplementedException();
        }
    }
}
